<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="yops.Default" %>
<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Ye Olde Photo Shoppe
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/yops/CSS/Default.css" />
</asp:Content>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <h1>Login</h1>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="dialog2columns">
        <tr>
            <td><label for="Username" runat="server"><span>Username</span></label></td>
            <td><input type="text" id="Username" name="Username" runat="server" required /></td>
        </tr>
        <tr>
            <td><label for="Password" runat="server"><span>Password</span></label></td>
            <td><input type="password" id="Password" name="Password" runat="server" required /></td>
        </tr>
        <tr>
            <td><button data-theme="b" id="LoginBtn" onserverclick="Login_Click" runat="server" >Login</button></td>
            <td><input type="button" id="RegisterBtn" onserverclick="Register_Click" value="Register" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
