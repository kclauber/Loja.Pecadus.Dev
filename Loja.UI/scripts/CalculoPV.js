function CalcularPV(_fretePCU, _imposto, _txOperadora, _custoFixPeriodo, _custoVarPeriodo, _perdaPeriodo, _MKP) {
    if ($("#ContentPlaceHolder1_txtPrecoCusto").val() !== undefined) {
        var PCU = parseFloat($("#ContentPlaceHolder1_txtPrecoCusto").val().replace(",", "."));
        var vendaPeriodo = parseFloat($("#ContentPlaceHolder1_txtVendaPeriodo").val().replace(",", "."));
        var MKP = parseFloat($("#ContentPlaceHolder1_txtMKP").val().replace(",", "."));
        var fretePCU = parseFloat(_fretePCU.replace(",", "."));
        var imposto = parseFloat(_imposto.replace(",", "."));
        var txOperadora = parseFloat(_txOperadora.replace(",", "."));
        var custoFixPeriodo = parseFloat(_custoFixPeriodo.replace(",", "."));
        var custoVarPeriodo = parseFloat(_custoVarPeriodo.replace(",", "."));
        var perdaPeriodo = parseFloat(_perdaPeriodo.replace(",", "."));

        var CAM = PCU + fretePCU;
        var pCustoFix = (custoFixPeriodo / vendaPeriodo) * 100;
        var pCustoVar = (custoVarPeriodo / vendaPeriodo) * 100;
        var pCustoPerdas = (perdaPeriodo / vendaPeriodo) * 100;

        var pTotalCusto = parseFloat(imposto) +
                      parseFloat(txOperadora) +
                      parseFloat(pCustoFix) +
                      parseFloat(pCustoVar) +
                      parseFloat(pCustoPerdas);

        var PV = CAM + (CAM * ((pTotalCusto + MKP) / 100)); // <<==== Este é o preço de venda

        $("#ContentPlaceHolder1_txtPrecoCusto").val(PCU.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtFretePCU").val(fretePCU.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtCAM").val(CAM.toFixed(2).replace(".", ","));

        $("#ContentPlaceHolder1_txtImposto").val(imposto.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtTxOperadora").val(txOperadora.toFixed(2).replace(".", ","));

        $("#ContentPlaceHolder1_txtPCustFixo").val(pCustoFix.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtPCustoVariavel").val(pCustoVar.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtPPerdas").val(pCustoPerdas.toFixed(2).replace(".", ","));

        $("#ContentPlaceHolder1_txtTotalCustos").val(pTotalCusto.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtCustoFixPeriodo").val(custoFixPeriodo.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtCustoVarPeriodo").val(custoVarPeriodo.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtPerdaPeriodo").val(perdaPeriodo.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtVendaPeriodo").val(vendaPeriodo.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtMKP").val(MKP.toFixed(2).replace(".", ","));
        $("#ContentPlaceHolder1_txtPreco").val(PV.toFixed(2).replace(".", ","));
    }
}
