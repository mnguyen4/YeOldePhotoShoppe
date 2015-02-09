<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="yops.Contents.Register" %>
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Ye Olde Photo Shoppe
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/yops/CSS/Default.css" />
</asp:Content>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <h1>Register</h1>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="dialog2columns">
        <tr>
            <td><label for="Username" runat="server"><span>Username</span></label></td>
            <td><input type="text" id="Username" name="Username" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><label for="Password" runat="server"><span>Password</span></label></td>
            <td><input type="password" id="Password" name="Password" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><label for="ConfPassword" runat="server"><span>Confirm Password</span></label></td>
            <td><input type="password" id="ConfPassword" name="ConfPassword" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><label for="FirstName" runat="server"><span>First Name</span></label></td>
            <td><input type="text" id="FirstName" name="FirstName" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><label for="LastName" runat="server"><span>Last Name</span></label></td>
            <td><input type="text" id="LastName" name="LastName" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><label for="Email" runat="server"><span>Email</span></label></td>
            <td><input type="email" id="Email" name="Email" runat="server" required="required" /></td>
        </tr>
        <tr>
            <td><button data-theme="b" id="SubmitBtn" onserverclick="Submit_Click" runat="server">Submit</button></td>
            <td><input type="button" id="CancelBtn" onserverclick="Cancel_Click" value="Cancel" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
