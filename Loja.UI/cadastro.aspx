<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeFile="cadastro.aspx.cs" Inherits="cadastro" %>

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
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="#">Home</a></li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Cadastro</li>
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
                    Cadastro</h2>
            </div>
        </div>

        <div class="row">
          <form name="cadastro" class="cadastro-form" runat="server">
              <div class="col-md-12">
                  <asp:ValidationSummary ID="vSumm1" runat="server" DisplayMode="BulletList"
                        HeaderText="<h3>Por favor informe os seguintes dados:</h3>" CssClass="vSumm" />
              </div>

            <div class="col-md-6">          
                <h3>Dados pessoais</h3>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputNome1">
                        Nome
                        <asp:RequiredFieldValidator ID="reqNome" runat="server" ErrorMessage="Nome completo" ControlToValidate="txtNome" Text="*" ForeColor="Red" />
                    </label>                    
                    <input type="text" class="form-control" id="txtNome" placeholder="Nome Completo" runat="server" />
                  </div>
                </div>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputCPF1">
                        CPF
                        <asp:RequiredFieldValidator ID="reqCPF" runat="server" ErrorMessage="CPF" ControlToValidate="txtCPF" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtCPF" placeholder="Digite aqui seu CPF" runat="server" />
                  </div>
                </div>
                <div class="col-sm-4 col-xs-6">
                  <div class="form-group">
                    <label for="exampleInputDataNasc1">Data de Nascimento</label>
                    <input type="text" class="form-control" id="txtDataNascimento" placeholder="00/00/0000" runat="server" />
                  </div>
                </div>
                <div class="col-sm-4 col-xs-6">
                  <div class="form-group">
                    <label for="exampleInputTelRes1">Telefone Residencial</label>
                    <input type="text" class="form-control" id="txtTelefone" placeholder="(00) 0000-0000" runat="server" />
                  </div>
                </div>
                <div class="col-sm-4 col-xs-6">
                  <div class="form-group">
                    <label for="exampleInputTelCel1">Celular</label>
                    <input type="text" class="form-control" id="txtCelular" placeholder="(00) 00000-0000" runat="server" />
                  </div>
                </div>
            </div>
            <div class="col-md-6 col-xs-12">          
                <h3>Endereço para entrega</h3>
                <div class="col-sm-10">
                  <div class="form-group">
                    <label for="endereco">
                        Endereço
                        <asp:RequiredFieldValidator ID="reqEndereco" runat="server" ErrorMessage="Endereço" ControlToValidate="txtEndereco" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtEndereco" placeholder="Rua, av., praça,..." runat="server" />
                  </div>    
                </div>
                <div class="col-sm-2">
                  <div class="form-group">
                    <label for="endereco">
                        Num.
                        <asp:RequiredFieldValidator ID="reqNum" runat="server" ErrorMessage="Número" ControlToValidate="txtNumero" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtNumero" placeholder="n° 000" runat="server" />
                  </div>    
                </div>
                <div class="col-sm-6">
                  <div class="form-group">
                    <label for="complemento">Complemento</label>
                    <input type="text" class="form-control" id="txtComplemento" placeholder="Apto, Bloco, Fundos,..." runat="server" />
                  </div>    
                </div>
                <div class="col-sm-6">
                  <div class="form-group">
                    <label for="cidade">
                        Bairro
                        <asp:RequiredFieldValidator ID="reqBairro" runat="server" ErrorMessage="Bairro" ControlToValidate="txtBairro" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtBairro" placeholder="Bairro" runat="server" />
                  </div> 
                </div>
                <div class="col-sm-6 col-xs-12">
                  <div class="form-group">
                    <label for="cidade">
                        Cidade
                        <asp:RequiredFieldValidator ID="reqCidade" runat="server" ErrorMessage="Cidade" ControlToValidate="txtCidade" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtCidade" placeholder="Cidade" runat="server" />
                  </div> 
                </div>
                <div class="col-sm-2 col-xs-6">
                  <div class="form-group">
                    <label for="estado">
                        UF
                        <asp:RequiredFieldValidator ID="reqEstado" runat="server" ErrorMessage="UF" ControlToValidate="ddlEstado" Text="*" ForeColor="Red" />
                    </label>
                    <select class="form-control" name="ddlEstado" id="ddlEstado" runat="server" >
                      <option value="" disabled selected>--</option>
                      <option value="AC">AC</option>
                      <option value="AL">AL</option>
                      <option value="AP">AP</option>
                      <option value="AM">AM</option>
                      <option value="BA">BA</option>
                      <option value="CE">CE</option>
                      <option value="DF">DF</option>
                      <option value="ES">ES</option>
                      <option value="GO">GO</option>
                      <option value="MA">MA</option>
                      <option value="MT">MT</option>
                      <option value="MS">MS</option>
                      <option value="MG">MG</option>
                      <option value="PA">PA</option>
                      <option value="PB">PB</option>
                      <option value="PR">PR</option>
                      <option value="PE">PE</option>
                      <option value="PI">PI</option>
                      <option value="RJ">RJ</option>
                      <option value="RN">RN</option>
                      <option value="RS">RS</option>
                      <option value="RO">RO</option>
                      <option value="RR">RR</option>
                      <option value="SC">SC</option>
                      <option value="SP">SP</option>
                      <option value="SE">SE</option>
                      <option value="TO">TO</option>
                    </select>
                  </div>
                </div>
                <div class="col-sm-4 col-xs-6">
                  <div class="form-group">
                    <label for="cep">
                        CEP
                        <asp:RequiredFieldValidator ID="reqCEP" runat="server" ErrorMessage="CEP" ControlToValidate="txtCEP" Text="*" ForeColor="Red" />
                    </label>
                    <input type="text" class="form-control" id="txtCEP" placeholder="CEP" runat="server" />
                  </div> 
                </div>
            </div>
            <div class="col-md-12 col-xs-12">
              <h3>Dados para acesso</h3>
              <div class="col-sm-4">
                <div class="form-group">
                  <label for="exampleInputEmail1">
                      E-mail
                      <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="E-mail" ControlToValidate="txtEmail" Text="*" ForeColor="Red" />
                  </label>
                  <input type="text" class="form-control" id="txtEmail" placeholder="Email" runat="server" />
                </div>
              </div>
              <div class="col-sm-3">
                <div class="form-group">
                  <label for="exampleInputSenha1">
                      Senha
                      <asp:RequiredFieldValidator ID="reqSenha1" runat="server" ErrorMessage="Senha" ControlToValidate="txtSenha1" Text="*" ForeColor="Red" />
                  </label>
                  <input type="password" class="form-control" id="txtSenha1" placeholder="Senha" runat="server" />
                </div>
              </div>
              <div class="col-sm-3">
                <div class="form-group">
                  <label for="exampleInputConfirmSenha1">
                      Confirmar Senha
                      <asp:RequiredFieldValidator ID="reqSenha2" runat="server" ErrorMessage="Confirmação de senha" ControlToValidate="txtSenha2" Text="*" ForeColor="Red" />
                      <asp:CompareValidator ID="compVal1" runat="server" ErrorMessage="A senhas digitadas não são iguais" ControlToCompare="txtSenha1" ControlToValidate="txtSenha2" Text="*" ForeColor="Red" />
                  </label>
                  <input type="password" class="form-control" id="txtSenha2" placeholder="Senha" runat="server" />
                </div>
              </div>
              <div class="col-sm-2">
                  <asp:Button ID="btnCadastrar" runat="server" Text="Enviar" OnClick="btnCadastroNovo_Click" CssClass="btn btn-secundary" />
              </div>
            </div>
          </form>
        </div><!-- Final Row -->

      </div>
    </section>


</asp:Content>

