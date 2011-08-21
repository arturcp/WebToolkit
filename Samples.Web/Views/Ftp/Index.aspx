<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FTP
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>FTP</h2>

    <% using(Html.BeginForm("Show", "Ftp", FormMethod.Post, null)){ %>
        Host: <input type="text" value="" name="host" /><br />
        Usuário: <input type="text" value="" name="user" /><br />
        Senha: <input type="text" value="" name="password" /><br />
        Caminho: <input type="text" value="" name="path" /><br />
        <input type="submit" value="Listar" name="action"/>
        <input type="submit" value="Upload" name="action"/>
        <input type="submit" value="Rename" name="action"/>
        <input type="submit" value="Delete" name="action"/>
    <% } %>

</asp:Content>
