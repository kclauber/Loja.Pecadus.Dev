<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height:400px;display:table-cell; vertical-align:middle;">
    <table>
        <tr>
            <td align="center" valign="middle" colspan="2">
                Digite seu usuário e senha<br />
                <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
                <br />
            </td>
        </tr>        
        <tr>
            <td align="right">Usuário:</td>
            <td><asp:TextBox ID="txtUsuario" Width="110" runat="server"/></td>
        </tr>
        <tr>
            <td align="right">Senha:</td>
            <td><asp:TextBox ID="txtSenha" TextMode="Password" Width="110" runat="server"/></td>
        </tr>
        <tr>
            <td align="center" valign="middle" colspan="2">
                <asp:Button ID="btnLogar" runat="server" Text="Logar" OnClick="btnLogar_Click" Style="height: 26px" />                
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

