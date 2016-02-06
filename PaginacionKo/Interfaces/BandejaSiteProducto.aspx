<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="BandejaSiteProducto.aspx.cs" Inherits="WebApplicationIntegrada.Interfaces.BandejaSiteProducto" %>

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

<asp:Content ID="ctnResultado" ContentPlaceHolderID="cphResultado" runat="server">
    <table>
        <thead>
            <tr>
                <th>
                    <input type="checkbox" id="chkPadre" data-bind="click: chkPadreCheck" /></th>
                <th>Nombre</th>
                <th>Precio</th>
                <th>Venta</th>
            </tr>
        </thead>
        <tbody data-bind="foreach: productos">
            <tr>
                <td>
                    <input type="checkbox" data-bind="click: $parent.selectRow" id="chkHijo" /></td>
                <td data-bind="text: Nombre"></td>
                <td data-bind="text: Precio"></td>
                <td data-bind="text: Venta"></td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="ctnScript" ContentPlaceHolderID="cphScript" runat="server">
    <script type="text/javascript">

        function formatoUrlBusqueda(pagina) {
            var url = 'producto/' + pagina + '/' + $('#selectPagina').val() + '/' + $('#paramFiltro').val();
            return url;
        }
    </script>
</asp:Content>