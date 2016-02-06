(function() {

    String.prototype.format = function() {
        var args = arguments;
        return this.replace(/\{\{|\}\}|\{(\d+)\}/g, function(m, n) {
            if (m == "{{") {
                return "{";
            }
            if (m == "}}") {
                return "}";
            }
            return args[n];
        });
    };

    // Private function

    function getColumnsForScaffolding(data) {
        if ((typeof data.length !== 'number') || data.length === 0) {
            return [];
        }
        var columns = [];
        for (var propertyName in data[0]) {
            columns.push({ headerText: propertyName, rowText: propertyName });
        }
        return columns;
    }

    function getTotalPages(totalItems, pageSize) {
        var totalPages = Math.floor(totalItems / pageSize) + 1;
        return totalPages;
    }

    ko.simpleGrid = {
        // Defines a view model class you can use to populate a grid
        viewModel: function(configuration) {
            var self = this;

            this.data = configuration.data;
            this.totalServerItems = configuration.totalServerItems;
            this.currentPageIndex = configuration.currentPageIndex;
            this.pageSize = configuration.pageSize;
            this.edit = configuration.edit;

            // If you don't specify columns configuration, we'll use scaffolding
            this.columns = configuration.columns || getColumnsForScaffolding(ko.utils.unwrapObservable(this.data));

            this.maxPageIndex = function() {
                var totalPages = getTotalPages(self.totalServerItems(), self.pageSize());
                return totalPages;
            };

            this.totalPagesPaginator = ko.computed(function() {
                var tt = self.maxPageIndex();
                return getTotalPages(tt, 10);
            }, this);

            this.currentPagePaginator = ko.observable(1);

            this.pageOptions = ko.observableArray([10, 25, 50, 100]);

            this.selectedItem = ko.observable();

            this.clickable = function(data) {
                var clickable = configuration.clickable || false;
                if (typeof data['clickable'] !== 'undefined')
                    return data.clickable === true;
                return clickable === true;
            };

            this.doubleClick = function(data, event) {
                if (typeof self.edit == 'function') {
                    self.edit(self.selectedItem);
                } else if (event.type == 'dblclick') {
                    if (window.getSelection) {
                        if (window.getSelection().empty) { // Chrome
                            window.getSelection().empty();
                        } else if (window.getSelection().removeAllRanges) { // Firefox
                            window.getSelection().removeAllRanges();
                        }
                    } else if (document.selection) { // IE?
                        document.selection.empty();
                    }
                }
            };
            this.selectRow = function(data, event) {
                self.selectedItem(data);
            };
        }
    };

    // Templates used to render the grid
    var templateEngine = new ko.nativeTemplateEngine();

    templateEngine.addTemplate = function(templateName, templateMarkup) {
        document.write("<script type='text/html' id='" + templateName + "'>" + templateMarkup + "<" + "/script>");
    };

    templateEngine.addTemplate("ko_simpleGrid_grid", "\
                    <table class=\"table table-condensed table-stripped table-bordered\" cellspacing=\"0\">\
                        <thead>\
                            <tr data-bind=\"foreach: columns\">\
                               <th data-bind=\"text: headerText\"></th>\
                            </tr>\
                        </thead>\
                        <tbody data-bind=\"foreach: data\">\
                           <tr data-bind=\"css: { 'selected': $root.selectedItem() === $data }, foreach: $parent.columns\">\
                                <!-- ko ifnot: $root.clickable($data) -->\
                                    <td data-bind=\"html: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></td>\
                                <!-- /ko -->\
                                <!-- ko if: $root.clickable($data) -->\
                                    <td data-bind=\"event: { click: function(data, event) { $root.selectRow($parent, event) }, dblclick: $root.doubleClick }, html: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></td>\
                                <!-- /ko -->\
                            </tr>\
                        </tbody>\
                    </table>");

    templateEngine.addTemplate("ko_simpleGrid_pageLinks", "\
                                    <select class=\"span1\" data-bind=\"options: $root.pageOptions, value: $root.pageSize \"></select>\
                                    <div class=\"pagination pagination-left\">\
                                        <ul>\
                                            <li class=\"disabled\" ><a href=\"#\">&laquo;</a></li>\
                                                <!-- ko foreach: ko.utils.range(1, maxPageIndex() > 10 ? 10 : maxPageIndex()) -->\
                                                <li data-bind=\"css: { active: $data == $root.currentPageIndex() }\"><a href=\"#\" data-bind=\"text: $data, click: function() { $root.currentPageIndex($data); } \"></a></li>\
                                                <!-- /ko -->\
                                            <li class=\"disabled\" ><a href=\"#\">&raquo;</a></li>\
                                        </ul>\
                                    </div>");

    // The "simpleGrid" binding
    ko.bindingHandlers.simpleGrid = {
        init: function() {
            return { 'controlsDescendantBindings': true };
        },
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function(element, viewModelAccessor, allBindingsAccessor) {
            var viewModel = viewModelAccessor(), allBindings = allBindingsAccessor();

            // Empty the element
            while (element.firstChild)
                ko.removeNode(element.firstChild);

            // Allow the default templates to be overridden
            var gridTemplateName = allBindings.simpleGridTemplate || "ko_simpleGrid_grid",
                pageLinksTemplateName = allBindings.simpleGridPagerTemplate || "ko_simpleGrid_pageLinks";

            // Render the main grid
            var gridContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(gridTemplateName, viewModel, { templateEngine: templateEngine }, gridContainer, "replaceNode");

            // Render the page links
            var pageLinksContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(pageLinksTemplateName, viewModel, { templateEngine: templateEngine }, pageLinksContainer, "replaceNode");
        }
    };

    ko.bindingHandlers.dateFormatter = {
        update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);

            var momentum = moment(valueUnwrapped);
            if (momentum != null)
                momentum = momentum.utc().format('DD/MM/YYYY');

            $(element).editable('destroy');

            $(element).editable({
                format: 'DD/MM/YYYY',
                viewformat: 'DD/MM/YYYY',
                url: function(args) {
                    var dfd = new $.Deferred();
                    var dateField = valueAccessor();
                    setTimeout(function() {
                        var tmp = moment(args.value);
                        if (tmp != null)
                            dateField(args.value);
                        else value(null);
                        dfd.resolve();
                    }, 100);
                    return dfd.promise();
                }
            });

            $(element).text(momentum);
            $(element).editable('setValue', momentum);
        }
    };
})();