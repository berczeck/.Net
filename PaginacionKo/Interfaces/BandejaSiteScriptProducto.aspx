<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/SiteScript.Master" AutoEventWireup="true" CodeBehind="BandejaSiteScriptProducto.aspx.cs" Inherits="WebApplicationIntegrada.Interfaces.BandejaSiteScriptProducto" %>

<asp:Content ID="ctnFiltro" ContentPlaceHolderID="cphFiltro" runat="server">
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
</asp:Content>

<asp:Content ID="ctnResultadoCabecera" ContentPlaceHolderID="cphResultadoCabecera" runat="server">
    <th></th>
    <th>Nombre</th>
    <th>Precio</th>
    <th>Venta</th>
</asp:Content>

<asp:Content ID="ctnResultadoColumnas" ContentPlaceHolderID="cphResultadoColumnas" runat="server">
    <td>
        <a href="#" data-bind="click: $parent.eliminarFila, clickBubble: false">Del</a>
    </td>
    <td><a href="#" data-bind="text: Nombre, attr: { href: $parent.crearRutaBase(urlPagina + '?id=' + Id) }"></a></td>
    <td data-bind="text: Precio"></td>
    <td data-bind="text: Venta"></td>
</asp:Content>

<asp:Content ID="ctnScript" ContentPlaceHolderID="cphScript" runat="server">
    <script type="text/javascript">

        var urlPagina = 'producto';

        function formatoUrlBusqueda(pagina) {
            return urlPagina + '?pagina=' + pagina + '&tamanio=' + $('#selectPagina').val() + '&nombre=' + $('#paramFiltro').val();
        }

        function formatoUrlEliminacion(row) {
            return urlPagina;
        }
    </script>
</asp:Content>
