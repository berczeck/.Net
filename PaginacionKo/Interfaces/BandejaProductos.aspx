<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BandejaProductos.aspx.cs" Inherits="WebApplicationIntegrada.Interfaces.BandejaProductos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <script src="../Publico/knockout-2.2.1.js"> </script>
        <script src="../Publico/jquery-1.10.1.min.js"> </script>
        <style type="text/css">
            #paramNroFilas { width: 45px; }

            #paginaActual { width: 43px; }

            #numeroPaginas { width: 30px; }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="scrManager" runat="server">
            </asp:ScriptManager>
            <div id="divGrilla">
                <asp:UpdatePanel ID="upnlFiltro" runat="server">
                    <ContentTemplate>
                        <!--PlaceHolder-->
                        <table>
                            <tr>
                                <td>Filtro:</td>
                                <td>
                                    <input id="paramFiltro" value="prod" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlPrincipal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrincipal_SelectedIndexChanged">
                                        <asp:ListItem Text="Hola"></asp:ListItem>
                                        <asp:ListItem Text="Hi"></asp:ListItem>
                                        <asp:ListItem Text="Hails"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlHijo" runat="server"></asp:DropDownList>                                
                                </td>
                            </tr>
                        </table>
                        <!--PlaceHolder-->
                    </ContentTemplate>
                </asp:UpdatePanel>
            
                <asp:Button ID="btnPrueba" runat="server" Text="PostBack" OnClick="btnPrueba_Click" />

                <table>
                    <tr>
                        <td>
                            <button data-bind="click: buscar">
                                Buscar producto</button>
                        </td>
                        <td>
                            <button data-bind="click: limpiar">
                                Limpiar
                            </button>
                        </td>
                        <td>
                            <button data-bind="click: eliminar">
                                Eliminar producto
                            </button>
                        </td>
                    </tr>
                </table>

                <div data-bind="visible: visibleGrilla">
                    <div>
                        <!--PlaceHolder-->
                        <table>
                            <thead>
                                <tr>
                                    <th><input type="checkbox" id="chkPadre" data-bind="click: chkPadreCheck" /></th>
                                    <th>Nombre</th>
                                    <th>Precio</th>
                                    <th>Venta</th>
                                </tr>
                            </thead>
                            <tbody data-bind="foreach: productos">
                                <tr>
                                    <td><input type="checkbox" data-bind="click: $parent.selectRow" id="chkHijo" /></td>
                                    <td data-bind="text: Nombre"></td>
                                    <td data-bind="text: Precio"></td>
                                    <td data-bind="text: Venta"></td>
                                </tr>
                            </tbody>
                        </table>
                        <!--PlaceHolder-->
                    </div>
                    <div>
                        Filas:
                        <input id="paramNroFilas" data-bind="value: totalFilas" />
                        Numero de Paginas:
                        <input id="numeroPaginas" data-bind="value: numeroPaginas" />
                        <select id="selectPagina" data-bind="options: filtroPagina, event: { change: buscar }"></select>
                        <table>
                            <tr>
                                <td>
                                    <button data-bind="click: buscar, enable: habilitadoAnterior"><<</button></td>
                                <td>
                                    <button data-bind="click: anterior, enable: habilitadoAnterior"><</button></td>
                                <td>
                                    <input data-bind="value: paginaActual" />
                                </td>
                                <td>
                                    <button data-bind="click: siguiente, enable: habilitadoSiguiente">></button></td>
                                <td>
                                    <button data-bind="click: ultima, enable: habilitadoSiguiente">>></button></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript">
            var cantidadPaginas = ['3', '6', '9'];

            function formatoUrlBusqueda(pagina) {
                var url = '/webapi/producto/' + pagina + '/' + $('#selectPagina').val() + '/' + $('#paramFiltro').val();
                return url;
            }
        </script>

        <script type="text/javascript">

        //function Producto(nombre, precio, venta) {
        //    var self = this;
        //    self.Nombre = ko.observable(nombre);
        //    self.Precio = ko.observable(precio);
        //    self.Venta = ko.observable(venta);
        //}

            function productoViewModel() {
                var self = this;
                self.filtroPagina = ko.observableArray(cantidadPaginas);
                self.totalFilas = ko.observable(1);
                self.paginaActual = ko.observable(0);
                self.productos = ko.observableArray([]);
                self.numeroPaginas = ko.observable(1);

                self.visibleGrilla = ko.observable(false);
                self.habilitadoAnterior = ko.observable(false);
                self.habilitadoSiguiente = ko.observable(false);
                self.productoNombre = ko.observable('');

                self.mapeo = function(data) {
                    self.limpiar();
                    self.visibleGrilla(data.TotalFilas > 0);
                    self.totalFilas(data.TotalFilas);
                    self.numeroPaginas(data.NumeroPaginas);
                    $.each(data.Resultado, function(k, l) {
                        self.productos.push(l);
                    });
                };
                self.chkPadreCheck = function(row) {
                    $('.chkHijo').prop('checked', $('chkPadre').is(":checked"));
                    return true;
                };
                self.eliminar = function() {
                    alert(self.productoNombre());
                };
                self.selectRow = function(row) {
                    self.productoNombre(row.Id + ' ' + row.Nombre + ' ' + row.Precio);
                    self.seletedRow(row);
                    return true;
                };
                self.seletedRow = ko.observable();

                //SobreEscribir
                self.formatoUrl = function(pagina) {
                    return formatoUrlBusqueda(pagina);
                };
                self.buscar = function() {
                    //$.ajax({
                    //    type: "GET",
                    //    url: '/webapi/producto/0/' + $('#selectPagina').val() + '/' + $('#paramFiltro').val(),
                    //    cache: false,
                    //    success: function (data) {
                    //        self.mapeo(data, 0);
                    //    },
                    //    error: function (xhr, ajaxOptions, thrownError) {
                    //        alert(thrownError);
                    //    }
                    //});
                    $.get(self.formatoUrl(0), self.mapeo);
                    self.paginaActual(0);
                    self.habilitadoAnterior(false);
                    self.habilitadoSiguiente(true);
                };
                self.limpiar = function() {
                    self.visibleGrilla(false);
                    self.productos.removeAll();
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
            }

            ;

            $(document).ready(function() {
                ko.applyBindings(new productoViewModel());
            });

        </script>
    </body>
</html>