<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeFile="calculoFrete.aspx.cs" Inherits="Loja.UI.Pecadus.calculoFrete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblMensagem" runat="server"></asp:Label>
    <asp:TextBox ID="txtCepDestino" runat="server"></asp:TextBox>
    <asp:Button ID="btnCalcular" runat="server" Text="Button" 
        onclick="btnCalcular_Click" />
    <asp:Button ID="btnEncomenda" runat="server" Text="Button" 
        onclick="btnEncomenda_Click" />
</asp:Content>