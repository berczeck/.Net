<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteTag.aspx.cs" 
         Inherits="AutoCompleteTockenInput.Interfaces.AutoCompleteTag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <script type="text/javascript" src="../Content/jquery-1.10.1.min.js" > </script>
        <script type="text/javascript" src="../Content/jquery.tokeninput.js" > </script>
        <link rel="stylesheet" type="text/css" href="../Content/token-input.css" />
        <link rel="stylesheet" type="text/css" href="../Content/token-input-facebook.css" />

        <script type="text/javascript">
            $(document).ready(function() {
                $("#my-text-input").tokenInput("/webapi/autocomplete?proyecto=2",
                    {
                        queryParam: "nombre",
                        theme: "facebook",
                        minChars: 2,
                        searchDelay: 300,
                        tokenValue: "id",
                        propertyToSearch: "nombre",
                        hintText: "Escriba un término de búsqueda",
                        noResultsText: "No hay resultados",
                        searchingText: "Buscando...",
                        tokenDelimiter: ";",
                        tokenLimit: 2 ,
                        preventDuplicates: true
                    }
                );
            });
        </script>

    </head>
    <body>
        <form id="form1" runat="server">
            <div>
    
                <input type="text" id="my-text-input" />

            </div>
        </form>
    </body>
</html>