window.functionReturnParameter = (dynamicParameter) => {
    alert(JSON.stringify(dynamicParameter));

    return {
        MyValue: "Valore da Javascript"
    }
}

window.functionCallNET = () => {
    DotNet.invokeMethodAsync("BlazorJavascript", "CambiaValore",
        'Questa stringa è stata sostituita')
        .then(d => {
            alert(d);
        });
}


window.functionCallNET2 = (dotNetHelper) => {
    dotNetHelper.invokeMethodAsync("CambiaValoreAllaUI", 'Questa stringa è stata sostituita');
}