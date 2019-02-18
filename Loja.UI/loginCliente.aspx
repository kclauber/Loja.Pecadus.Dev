<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeFile="loginCliente.aspx.cs" Inherits="loginCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
    <nav class="pec-breadcrumb hidden-xs">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <nav>
              <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
                <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Home</li>
                <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Login</li>
              </ol>
            </nav>
          </div>
        </div>
      </div>
    </nav>

    <!-- ====================== CONTEUDO ====================== */ -->
    <section class="cadastro cont-int">
      <div class="container">

        <div class="row">
            <div class="col-md-12">
                <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                    Login de cliente</h2>
            </div>
        </div>

        <div class="row">
          <form name="cadastro" class="cadastro-form" runat="server">
              <div class="row">
                  <div class="col-md-4 center-block">
                      <div class="modal-body">
                        <div class="form-group">
                          <label for="InputEmail1">E-mail</label>
                          <div class="input-group">
                            <input type="email" class="form-control" id="txtEmailLoginCompra" placeholder="Email">
                            <div class="input-group-addon">
                              <i class="glyphicon glyphicon-envelope" aria-hidden="true"></i>
                            </div>
                          </div>
                        </div>
                        <div class="form-group">
                          <label for="InputPassword1">Senha</label>
                          <div class="input-group">
                            <input type="password" class="form-control" id="txtSenhaLoginCompra" placeholder="Senha">
                            <div class="input-group-addon">
                              <i class="glyphicon glyphicon-lock" aria-hidden="true"></i>
                            </div>
                          </div>
                        </div>
                        <div class="links-modal">
                          <div class="col-xs-6">
                            <div class="row">
                              <div class="checkbox">
                                <label>
                                  <input type="checkbox" id="chkLembrarCompra">Lembrar-me *
                                </label>
                              </div>
                            </div>
                          </div>
                          <div class="col-xs-6">
                            <div class="row">
                              <div class="checkbox">
                                <a href="" data-toggle="modal" data-target="#lembreteSenha" style="color:#233241;">
                                  <u>Esqueci minha senha</u>
                                </a>
                              </div>
                            </div>
                          </div>
                          <div class="col-xs-12" style="font-size:x-small;">*Esta função utiliza cookies</div>
                        </div>
                        <div class="col-sm-12 form-group">
                            <a class="col-sm-12 btn btn-secundary" title="Entrar" onclick="login('true');">Entrar</a>                      
                        </div>

                        <div class="col-xs-12 col-sm-12">
                          <p style="color:#233241;">Não tem uma conta? 
                          <a style="color:#233241;" href="/Cadastro/"><u>Cadastre-se</u></a></p>
                        </div>
                      </div>   
                  </div>
              </div>

          </form>
        </div><!-- Final Row -->

      </div>
    </section>

</asp:Content>

