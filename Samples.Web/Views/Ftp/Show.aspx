<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Show
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Show</h2>

<% if (ViewBag.List != null)
   { %>
    <% foreach (string item in ViewBag.List)
       { %>
        <p>
            <%= item%>
        </p>
 <% } %>
 <% } %>


</asp:Content>
