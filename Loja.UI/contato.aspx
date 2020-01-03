<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="contato.aspx.cs" Inherits="Loja.UI.Pecadus.contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
  <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
  <nav class="pec-breadcrumb hidden-xs">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav>
            <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="#">Home</a></li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Contato</li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </nav>

    <!-- ====================== CONTEUDO CATEGORIAS ====================== */ -->
    <section class="cadastro cont-int">
      <div class="container">

        <div class="row">
            <div class="col-md-12">
                <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                    Contato</h2>
                <p style="color: #233241;">Nos envie suas dúvidas ou pedidos e entraremos em contato com você.</p>
                <br />
            </div>
        </div>

        <div class="row">
          <form class="cadastro-form">
            <div class="col-md-6">          
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputNome1">Nome *</label>
                    <input type="text" class="form-control" id="exampleInputNome1" placeholder="Nome Completo">
                  </div>
                </div>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputCPF1">E-mail *</label>
                    <input type="text" class="form-control" id="exampleInputCPF1" placeholder="Digite aqui seu e-mail">
                  </div>
                </div>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputTelCel1">Telefone de contato</label>
                    <input type="text" class="form-control" id="exampleInputTelCel1" placeholder="(00) 00000-0000">
                  </div>
                </div>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputCPF1">Mensagem *</label>
                    <textarea class="form-control" id="exampleInputMensagem" placeholder="Em que podemos lhe ajudar?"
                        rows="10"></textarea>
                  </div>
                </div>
                <div class="col-sm-12">
                  <a href="" title="#" class="btn btn-secundary" aria-label="Left Align" role="button">Enviar</a>
                </div>
            </div>
          </form>
        </div><!-- Final Row -->

      </div>
    </section>
</asp:Content>
