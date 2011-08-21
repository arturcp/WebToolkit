<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Zip
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Zip Test</h2>

    <p>
        Teste de compressão: <%= ViewBag.CompressTest %>
    </p>

    <p>
        Teste de extração: <%= ViewBag.ExtractTest%>
    </p>


</asp:Content>
