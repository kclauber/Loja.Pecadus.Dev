<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="carrinhoCompras.aspx.cs" Inherits="Loja.UI.Pecadus.carrinhoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <asp:Panel ID="pnlCarrinho" runat="server" Visible="true">
      <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
      <nav class="pec-breadcrumb hidden-xs">
        <div class="container">
          <div class="row">
            <div class="col-md-12">
              <nav>
                <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="/SexShop/">Home</a></li>
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Carrinho</li>
                </ol>
              </nav>
            </div>
          </div>
        </div>
      </nav>

        <!-- ====================== CONTEUDO PRODUTOS INTERNO ====================== */ -->
        <section class="carrinho cont-int">
            <div class="container">

                <div class="row">
                    <div class="col-md-12">
                        <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                            Carrinho</h2>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <div class="tabela-carrinho">
                        <asp:Repeater ID="repCarrinho" runat="server" OnItemDataBound="repCarr_ItemDataBound">
                            <HeaderTemplate>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th colspan="2">Produto</th>
                                        <th class="hidden-xs">Código</th>
                                        <th class="hidden-xs">Preço Unitário</th>
                                        <th class="hidden-xs">Quantidade</th>
                                        <th class="hidden-xs">Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr>
                                        <th>
                                            <asp:LinkButton ID="lnkExcluir" OnClick="RemoverItem" CssClass="btn btn-remove" runat="server">
                                                <i class="glyphicon glyphicon-remove"></i>
                                            </asp:LinkButton>
                                        </th>
                                        <td>
                                            <asp:Image ID="imgProduto" ImageUrl="/imagensProdutos/sem-foto-2.jpg" CssClass="img-responsive" runat="server" />
                                        </td>
                                        <td style="border-top:0;"><asp:Label ID="lblNomeProduto" runat="server"/></td>
                                        <td style="border-top:0;"><asp:Label ID="lblCodProduto" runat="server"/></td>
                                        <td style="border-top:0;"><asp:Label ID="lblPrecoProduto" runat="server"/></td>
                                        <td style="border-top:0;">
                                            <div class="form-group">                                                
                                                <asp:DropDownList ID="ddlQuantidade" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidade_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="border-top:0;"><asp:Label ID="lblPrecoTotalProduto" runat="server"/></td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    <tr>
                                        <th></th>
                                        <td class="" colspan="6" align="right">
                                            <div class="input-group">                                                
                                                <asp:TextBox ID="txtCepDestino" CssClass="form-control" Text="" EnableViewState="true" runat="server" />
                                                <span class="input-group-btn">
                                                    <asp:Button id="btnCalcularFrete" OnClick="btnCalcularFrete_OnClick" UseSubmitBehavior="true" Text="Calcular frete" CssClass="btn btn-primary" runat="server" />
                                                </span>
                                            </div>
                                            <asp:Panel ID="pnlFrete" Visible="false" runat="server">
                                                <div class="form-group" id="divValoresFrete" style="text-align:left; float:right; padding-right:50px;">
                                                    <asp:RadioButton ID="rdFreteSedex" AutoPostBack="true" GroupName="frete" CssClass="freteLabel" runat="server" /><br />
                                                    <asp:RadioButton ID="rdFretePac" AutoPostBack="true" GroupName="frete" CssClass="freteLabel" runat="server" />
                                                </div>
                                            </asp:Panel>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th></th>
                                        <td class="" colspan="6" align="right">
                                            Valor total: 
                                            <asp:Label ID="lblPrecoTotalCompra" runat="server"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="col-md-12 botoes-carrinho">
                                <a class="btn btn-primary" href="/SexShop/" title="Adicionar mais produtos">Adicionar mais produtos</a>
                                <a class="btn btn-secundary" href="javascript:alert('Estamos redirecionando você para o sistema do PagSeguro.\nPor favor aguarde.');" title="Concluir Compra">Concluir Compra</a>
                            </div>
                            </FooterTemplate>
                        </asp:Repeater>
                            <div class="col-md-12 botoes-carrinho">
                                * Sua compra será processada pelo PagSeguro UOL em ambiente completamente seguro.
                            </div>
                        </div>
                        <!-- /.im-int -->
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>    
    <asp:Panel ID="pnlVazio" Visible="false" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h2>Olá, seu carrinho de compras ainda está vazio.</h2>
                    <p style="color: #233241;">
                        Continue navegando e coloque coisas incríveis nele.
                    </p>
                    <p style="color: #233241;">
                        Se você já tem cadastro, clique 
                            <a style="color: #233241;" href="" data-toggle="modal" data-target="#login">
                                <u>aqui</u>
                            </a> e faça login. <br />
                        Você poderá:<br />
                        <ul style="color: #233241;">
                            <li>- Ver os produtos no seu carrinho de compras personalizado.</li>
                            <li>- Acessar sua lista de favoritos.</li>
                            <li>- Agilizar o processo de compra.</li>
                        </ul>
                    </p>                 
                    <a class="btn btn-primary" href="/SexShop/" title="Continuar navegando">Continuar navegando</a>
                </div>
            </div>
        </div>
        <br /><br />
    </asp:Panel>
    </form>
<script type="text/javascript">
    function CalcularFrete() {
        var _txtCepDestino = $('#txtCepDestino').val();
        var _divValoresFrete = $('#divValoresFrete');

        //if (_txtCepDestino == '') {
        //    alert('Informe seu cep para realizar o cálculo.');
        //    return;
        //}
        //if (_txtCepDestino.replace('-', '').length != 8) {
        //    alert('Cep inválido.');
        //    return;
        //}
        
        $.ajax({
            type: "POST",
            url: "/carrinhoCompras.aspx/CalcularFrete",
            data: "{ cepDestino: '"+ _txtCepDestino +"' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        })
        .done(function (ret) {
            _divValoresFrete.show();

            $('#ContentPlaceHolder1_repCarrinho_rdFreteSedex').val(ret.d[0].Tipo + ';' + ret.d[0].Valor.toFixed(2).replace('.', ','));
            $('#spnFreteSedex').html(ret.d[0].Tipo + ' - R$ ' + ret.d[0].Valor.toFixed(2).replace('.', ',') + ' ( ' + ret.d[0].Prazo + ' dias )');            

            $('#ContentPlaceHolder1_repCarrinho_rdFretePac').val(ret.d[1].Tipo + ';' + ret.d[1].Valor.toFixed(2).replace('.', ','));
            $('#spnFretePac').html(ret.d[1].Tipo + ' - R$ ' + ret.d[1].Valor.toFixed(2).replace('.', ',') + ' ( ' + ret.d[1].Prazo + ' dias )');
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
            console.log(textStatus);
            console.log(errorThrown);
        });
    }
</script>
</asp:Content>
