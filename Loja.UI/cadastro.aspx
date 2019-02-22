<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeFile="cadastro.aspx.cs" Inherits="cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <style>
        form.cadastro-form .btn-primary {
            margin-top: 23px;
            min-width: 190px;
        }
        .txtCepDestino {
            margin-top: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form name="cadastro" class="cadastro-form" runat="server">
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

    <!-- ====================== CONTEUDO ====================== */ -->
    <section class="cadastro cont-int">
      <div class="container">

        <div class="row">
            <div class="col-md-12">
                <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                    <asp:Label ID="lblTitulo" Text="Cadastro" runat="server" />
                </h2>
            </div>
        </div>

        <div class="row">
          
              <div class="col-md-12">
                  <asp:ValidationSummary ID="vSumm1" runat="server" ShowMessageBox="true" ShowSummary="false"
                        HeaderText="Por favor informe os seguintes dados:" CssClass="vSumm" />
              </div>

            <div class="col-md-6">          
                <h3>Dados pessoais</h3>
                <div class="col-sm-12">
                  <div class="form-group">
                    <label for="exampleInputNome1">
                        Nome
                        <asp:RequiredFieldValidator ID="reqNome" runat="server" ErrorMessage="Nome completo" Display="Static"
                            ControlToValidate="txtNome" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="reqCEP" runat="server" ErrorMessage="CEP" ControlToValidate="txtCepDestino" Text="*" ForeColor="Red" />
                    </label>
                    <asp:TextBox ID="txtCepDestino" CssClass="form-control" OnTextChanged="txtCepDestino_TextChanged" AutoPostBack="true" runat="server" />
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
                  <asp:Button ID="btnAtualizar" runat="server" Text="atualizar dados" OnClick="btnAtualizarCadastro_Click" CausesValidation="false" CssClass="btn btn-primary" Visible="false"/>
                  <asp:Button ID="btnCadastrar" runat="server" Text="cadastrar dados" OnClick="btnCadastroNovo_Click" CssClass="btn btn-secundary" Visible="true"/>
              </div>
            </div>
          
        </div><!-- Final Row -->
          <asp:Panel ID="pnlFinalizarCompra" Visible="false" runat="server">
              <div class="row" style="border: blue solid 0px;">
                  <div class="col-md-6">
                  </div>
                  <div class="col-md-6">
                      <h3>Opções de entrega</h3>
                      <div class="col-md-12 col-xs-12">
                          <p style="color: #233241;">
                              <asp:CustomValidator ID="valFrete" runat="server" ErrorMessage="Selecione uma opção de frete" ForeColor="Red" ClientValidationFunction="ValidateFrete">*</asp:CustomValidator>
                              <b>Selecione o modo de entrega da sua compra.</b>
                              <br />
                              <span style="font-size:small">
                                <asp:Label ID="lblObservacao" runat="server" />
                              </span>
                          </p>
                      </div>
                      <div class="row" style="border: blue solid 0px;">
                          <div class="col-md-12 col-xs-12">
                              <div class="col-md-2"></div>
                              <div class="col-md-5 col-xs-6">
                                <asp:RadioButton ID="rdFreteSedex" AutoPostBack="true" GroupName="Frete" OnCheckedChanged="rdFrete_CheckedChanged" Text="SEDEX - R$ 29,80 (3 dias)" CssClass="freteLabel" runat="server" />
                              </div>
                              <div class="col-md-5 col-xs-6">
                                <asp:RadioButton ID="rdFretePac" AutoPostBack="true" GroupName="Frete" OnCheckedChanged="rdFrete_CheckedChanged" Text="PAC - R$ 18,55 (9 dias)" CssClass="freteLabel" runat="server" />
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
              <div class="row" style="border: blue solid 0px;">
                  <div class="col-md-12 col-xs-12">
                      <div class="col-sm-10">               
                      </div>
                      <div class="col-sm-2">
                          <br />
                          <asp:Button ID="btnFinalizarCompra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-finalizarCompra btn-secundary" 
                                      UseSubmitBehavior="false" Text="Concluir Compra" Visible="true" CausesValidation="true"  runat="server"/>
                      </div>
                </div>
              </div>
          </asp:Panel>
          
      </div>
    </section>
<script type = "text/javascript">
    function ValidateFrete(sender, args) {
        args.IsValid = $("input[name='ctl00$ContentPlaceHolder1$Frete']:checked").length > 0;
    }
</script>
</form>
</asp:Content>

