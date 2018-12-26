sendToCreditCard =
    function (Instituicao,
              Parcelas,
              NumeroCartao,
              ExpiracaoMes,
              ExpiracaoAno,
              CodigoSeguranca,
              NomePortador,
              DataNascimento,
              Telefone,
              Identidade) {
        var settings = {
            "Forma": "CartaoCredito",
            "Instituicao": $(Instituicao).val(),
            "Parcelas": $('select' + Parcelas).val(),
            "Recebimento": "AVista",
            "CartaoCredito": {
                "Numero": $(NumeroCartao).val(),
                "Expiracao": $('select' + ExpiracaoMes).val() + "/" + $('select' + ExpiracaoAno).val(),
                "CodigoSeguranca": $(CodigoSeguranca).val(),
                "Portador": {
                    "Nome": $(NomePortador).val(),
                    "DataNascimento": $(DataNascimento).val(),
                    "Telefone": $(Telefone).val(),
                    "Identidade": $(Identidade).val()
                }
            }
        }
        MoipWidget(settings);
    };


var sucessoValidacao =
    function (data) {
        location.href = "/pecadus/RetornoMoip/?msg=processado" +
                        "&Status=" + data.Status +
                        "&IDMoip=" + data.CodigoMoIP +
                        "&CodOperadora=" + data.CodigoRetorno;
    };

var erroValidacao = 
    function (data) {

    var str = JSON.stringify(data);    
    str = limpaVar(str, '0123456789[]{:,\"')
    str = str.replace(/Codigo/g, "");
    str = str.replace(/Mensagem/g, "");
    str = str.replace(/}/g, "\n");

    str = "Por favor, preencha os valores destacados ou corrija os seguintes problemas:" + 
          "\n\n" + str + " ";
    alert(str);
    jQuery("#btnSendToMoip").attr('disabled', false);
};
