//-----------------------------------//
function CurrencyFormatted(amount)
{
    var i = parseFloat(amount);
    if(isNaN(i)) { i = 0.00; }
    var minus = '';
    if(i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if(s.indexOf('.') < 0) { s += '.00'; }
    if(s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}

function float2moeda(num) {
   x = 0;
   if(num<0) {
      num = Math.abs(num);
      x = 1;
   }
   if(isNaN(num)) num = "0";
      cents = Math.floor((num*100+0.5)%100);

   num = Math.floor((num*100+0.5)/100).toString();

   if(cents < 10) cents = "0" + cents;
		for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		{
			num = num.substring(0,num.length-(4*i+3))+'.'+num.substring(num.length-(4*i+3));
		}
	
	ret = num + ',' + cents;
	
	if (x == 1)
		ret = ' - ' + ret;
			
	return ret;
}

function limpaVar(palavra, chars) {
    var replaces = chars.split("");
    for (var i = 0; i < replaces.length; i++) {
        palavra = palavra.split(replaces[i]).join("");
    }
    return palavra;
}
