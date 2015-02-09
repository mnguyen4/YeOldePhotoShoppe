<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="yops.Contents.Account" %>
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

            $("#BackButton").click(function (event) {
                location.replace("AppHome.aspx");
            });

            $("#ChangePassword").collapsible({
                collapse: function (event, ui) {
                    $("#<%= OldPassword.ClientID %>").val("");
                    $("#<%= NewPassword.ClientID %>").val("");
                    $("#<%= ConfPassword.ClientID %>").val("");
                }
            });

            var userAccount = new UserAccount(
                $.cookie("<%= UserConstants.FIRST_NAME %>"),
                $.cookie("<%= UserConstants.LAST_NAME %>"),
                $.cookie("<%= UserConstants.USERNAME %>")
            );

            var firstName = "<%= FirstName.ClientID %>";
            var lastName = "<%= LastName.ClientID %>";
            var email = "<%= Email.ClientID %>";

            userAccount.retrieveAccount("Username", firstName, lastName, email, token);
        });
    </script>
</asp:Content>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <a data-role="button" class="ui-btn-left" id="BackButton">Back</a>
    <h1>Account</h1>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="dialog2columns">
        <tr>
            <td><label for="Username"><span>Username</span></label></td>
            <td><input type="text" id="Username" name="Username" disabled /></td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="ChangePassword" data-role="collapsible">
                    <h2>Change Password</h2>
                    <label for="OldPassword" runat="server"><span>Old Password</span></label>
                    <input type="password" id="OldPassword" name="OldPassword" runat="server" />
                    <label for="NewPassword" runat="server"><span>New Password</span></label>
                    <input type="password" id="NewPassword" name="NewPassword" runat="server" />
                    <label for="ConfPassword" runat="server"><span>Confirm New Password</span></label>
                    <input type="password" id="ConfPassword" name="ConfPassword" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td><label for="FirstName" runat="server"><span>First Name</span></label></td>
            <td><input type="text" id="FirstName" name="FirstName" runat="server" /></td>
        </tr>
        <tr>
            <td><label for="LastName" runat="server"><span>Last Name</span></label></td>
            <td><input type="text" id="LastName" name="LastName" runat="server" /></td>
        </tr>
        <tr>
            <td><label for="Email" runat="server"><span>Email</span></label></td>
            <td><input type="email" id="Email" name="Email" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2"><button data-theme="b" id="SubmitBtn" onserverclick="Submit_Click" runat="server">Submit</button></td>
        </tr>
    </table>
</asp:Content>
