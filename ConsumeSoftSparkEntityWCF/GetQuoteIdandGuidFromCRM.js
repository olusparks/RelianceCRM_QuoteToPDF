function AlertTextField() {
    var QuoteNumber= Xrm.Page.data.entity.attributes.get("quotenumber").getValue();
    var GUIDvalue = Xrm.Page.data.entity.getId();
    GUIDvalue =  GUIDvalue.replace("{", "").replace("}", "");
    
    var url = "http://www.crmentity.somee.com/index.aspxx?quotenumber="+QuoteNumber+"&guidValue="+GUIDvalue;
     windows.location.href = url


    var url = "http://www.crmentity.somee.com/index.aspx";
    var http = new XMLHttpRequest();
    var params = "quotenumber="+ QuoteNumber + "&guidValue="+GUIDvalue;
    http.open("POST", url, true);

    http.onreadystatechange = function () {
        if (http.readyState == 4 && http.status == 200) {
            alert(http.responseText);
        }
    }

    http.send(params);
}

function post(path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}


//var url = "http://www.crmentity.somee.com/index.aspxx?quotenumber="QUO-00016-P2R0K6"&guidValue="5AA19FC1-DAA8-E711-80E7-1458D04377A8;
// windows.location.href = url
//alert("QuoteNumber: " + QuoteNumber + "\n GuidValue: " + GUIDvalue + "\n URL:" + url);

//var http = new XMLHttpRequest();
//var url = "http://www.crmentity.somee.com/index.aspx?quotenumber="+ QuoteNumber + "&guidValue="+GUIDvalue;
//var params = "quotenumber="+ QuoteNumber + "&guidValue="+GUIDvalue;
//http.open("POST", url, true);

//http.setRequestHeader("Accept", "application/json");
//http.setRequestHeader("Content-type", "application/json;  charset=utf-8");

//http.onreadystatechange = function () {//Call a function when the state changes.
//    if (http.readyState == 4 && http.status == 200) {
//        alert(http.responseText);
//    }
//}

//http.send(params);
//alert("QuoteNumber: " + QuoteNumber +  "\n GuidValue: " + GUIDvalue  + "\n URL:"+windows.location.href);