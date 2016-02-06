<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Indice.aspx.cs" Inherits="BundlingMinification.Indice" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%: Styles.Render("~/style/vendor","~/style/app") %>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lbltexto" Text="Etiqueta con texto de prueba"></asp:Label>
        <asp:Button runat="server" ID="aceptar" Text="Accion con texto de prueba"/>
    </div>
    </form>
</body>
</html>
