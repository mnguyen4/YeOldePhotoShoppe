<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="Registered.aspx.cs" Inherits="yops.Contents.Registered" %>
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    User Account Created
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/yops/CSS/Default.css" />
    <script type="text/javascript" src="/yops/JS/Registered.js"></script>
</asp:Content>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <h1>Registration Successful</h1>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="centered-content">
        <b><span>You will be redirected in <span id="Timer"></span> seconds.</span></b>
    </div>
</asp:Content>
