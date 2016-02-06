<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationIntegrada.Interfaces.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server" method="post" action="http://www.google.com">
            <div>
                <input id="data" type="hidden" />
                <button id="enviar" value="Enviar" />
            </div>
        </form>
    </body>
</html>