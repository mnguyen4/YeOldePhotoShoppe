<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="AppHome.aspx.cs" Inherits="yops.Contents.AppHome" %>
<%@ Import Namespace="yops_lib.Constants" %>
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Ye Olde Photo Shoppe
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/yops/CSS/Default.css" />
    <script type="text/javascript" src="/yops/JS/UserAccount.js"></script>
    <script type="text/javascript" src="/yops/JS/Ajax.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $.cookie.raw = true;
            var token = $.cookie("<%= UserConstants.TOKEN %>");
            var userAccount = new UserAccount(
                $.cookie("<%= UserConstants.FIRST_NAME %>"),
                $.cookie("<%= UserConstants.LAST_NAME %>"),
                $.cookie("<%= UserConstants.USERNAME %>")
            );

            $("#LogoutButton").click(function (event) {
                $.removeCookie("<%= UserConstants.FIRST_NAME %>", { path: "/" });
                $.removeCookie("<%= UserConstants.LAST_NAME %>", { path: "/" });
                $.removeCookie("<%= UserConstants.USERNAME %>", { path: "/" });
                $.removeCookie("<%= UserConstants.EMAIL %>", { path: "/" });
                $.removeCookie("<%= UserConstants.TOKEN %>", { path: "/" });
                userAccount.doLogout(token);
            });

            $("#AccountButton").click(function (event) {
                location.replace("Account.aspx");
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
