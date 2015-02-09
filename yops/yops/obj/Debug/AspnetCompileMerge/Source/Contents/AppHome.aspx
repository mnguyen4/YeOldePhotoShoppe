<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="AppHome.aspx.cs" Inherits="yops.Contents.AppHome" %>
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Ye Olde Photo Shoppe
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/yops/CSS/Default.css" />
    <script type="text/javascript" src="/yops/JS/UserAccount.js"></script>
    <script type="text/javascript" src="/yops/JS/Ajax.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            var userAccount = new UserAccount(
                $.cookie("firstName"),
                $.cookie("lastName"),
                $.cookie("username"),
                $.cookie("email")
            );

            $("#LogoutButton").click(function (event) {
                userAccount.doLogout($.cookie("token"));
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <a data-role="button" class="ui-btn-left" id="AccountButton">Account</a>
    <h1>Photo Shoppe</h1>
    <a data-role="button" class="ui-btn-right" id="LogoutButton">Logout</a>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyContent" runat="server">
</asp:Content>
