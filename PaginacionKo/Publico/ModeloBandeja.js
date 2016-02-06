function bandejaViewModel() {
    var self = this;
    self.tamanioPagina = ko.observableArray(['3', '6', '9']);
    self.totalFilas = ko.observable(1);
    self.paginaActual = ko.observable(0);
    self.resultado = ko.observableArray([]);
    self.numeroPaginas = ko.observable(1);

    self.visibleGrilla = ko.observable(false);
    self.habilitadoAnterior = ko.observable(false);
    self.habilitadoSiguiente = ko.observable(false);
    self.resultadoNombre = ko.observable('');

    self.crearRutaBase = function(ruta){
        return '/webApi/query/' + ruta;
    }
    self.mapeo = function(data) {
        self.limpiar();
        self.visibleGrilla(data.TotalFilas > 0);
        self.totalFilas(data.TotalFilas);
        self.numeroPaginas(data.NumeroPaginas);
        $.each(data.Resultado, function(k, l) {
            self.resultado.push(l);
        });
        self.habilitadoSiguiente(self.paginaActual() < self.numeroPaginas());
    };
    self.chkPadreCheck = function(row) {
        $('.chkHijo').prop('checked', $('chkPadre').is(":checked"));
        return true;
    };
    self.eliminar = function() {
        alert(self.resultadoNombre());
    };
    self.eliminarFila = function (row) {
        var myArray = [row.Id];
        var myJson = JSON.stringify(myArray);
        
        $.ajax({
            url: "/webapi/command/activar/" + formatoUrlEliminacion(row),
            type: "POST",
            data: myJson,
            datatype: "json",
            contentType: "application/json",
            success: function (data) { alert("Registro Eliminado"); self.buscar(); }
        });

        /*$.post("/webapi/command/activar/" + formatoUrlEliminacion(row), {param: myJson}, function (resultado) {
        //$.post("/webapi/producto/", { "Id": row.Id }, function (data) {
        //$.post(self.crearRutaBase(formatoUrlEliminacion(row)), { "Id": row.Id }, function (data) {
            alert("Registro Eliminado");
            self.buscar();
        });*/
        return true;
    };
    
    self.selectRow = function (row) {
        var valor = row.Id + ' ' + row.Nombre + ' ' + row.Precio;
        $('#hfIdentificadores').val(valor);
        self.resultadoNombre(valor);
        self.seletedRow(row);
        return true;
    };
    self.seletedRow = ko.observable();

    self.formatoUrl = function(pagina) {
        return self.crearRutaBase(formatoUrlBusqueda(pagina));// "/webapi/" + formatoUrlBusqueda(pagina);
    };
    self.buscar = function() {
        $.get(self.formatoUrl(0), self.mapeo);
        self.paginaActual(0);
        self.habilitadoAnterior(false);
    };
    self.limpiar = function() {
        self.visibleGrilla(false);
        self.resultado.removeAll();
    };
    self.ultima = function() {
        $.get(self.formatoUrl(self.numeroPaginas()), self.mapeo);
        self.paginaActual(self.numeroPaginas());
        self.habilitadoAnterior(true);
        self.habilitadoSiguiente(false);
    };
    self.siguiente = function() {
        var paginaSiguiente = self.paginaActual() + 1;
        if (paginaSiguiente <= self.numeroPaginas()) {
            $.get(self.formatoUrl(paginaSiguiente), self.mapeo);
            self.paginaActual(paginaSiguiente);
            self.habilitadoAnterior(true);
        }
        self.habilitadoSiguiente(paginaSiguiente < self.numeroPaginas());
    };
    self.anterior = function() {
        var paginaAnterior = self.paginaActual() - 1;
        if (paginaAnterior >= 0) {
            $.get(self.formatoUrl(paginaAnterior), self.mapeo);
            self.paginaActual(paginaAnterior);
            self.habilitadoSiguiente(true);
        }
        self.habilitadoAnterior(paginaAnterior > 0);
    };
    self.chkPadre = function(e, o) {
        $(e[1]).find("input").click(function() {
            var checkCab = $(".tmc").find("[type='checkbox']:eq(0)");
            var total = $(".tmc").find("[type='checkbox']:gt(0)").length;
            var chekados = $(".tmc [type='checkbox']:gt(0):checked").length;
            checkCab[0].checked = (total == chekados);

            if ($(e[1]).find("input")[0].checked) {
                $(e[1]).addClass("seleccionado");
            } else {
                $(e[1]).removeClass("seleccionado");
            }
        });
        $(e[1]).find("td:gt(0)").click(function() {
            $(e[1]).find("input").click();
        });
    };
}

;
burbujaIn = function(d, e) {
    $(".burbuja").css({ position: "absolute", top: e.clientY, left: e.clientX });
    $(".burbuja").html(
        "Informacion Importante: " +
            "<br />Fecha Creación: " + d.FechaCreacion +
            "<br />Fecha Modificacion: " + d.FechaModificacion +
            "<br />Nombre: " + d.Nombre +
            "<br />Días: " + d.Dias);
    $(e.currentTarget).mousemove(
        function(e) {
            $(".burbuja").css({ position: "absolute", top: e.pageY, left: e.pageX });
            $(".burbuja").show(200);
        });
    $(".burbuja").show();
};
burbujaOut = function(d, e) { $(".burbuja").hide(); };
$(document).ready(function() {
    ko.applyBindings(new bandejaViewModel());
});