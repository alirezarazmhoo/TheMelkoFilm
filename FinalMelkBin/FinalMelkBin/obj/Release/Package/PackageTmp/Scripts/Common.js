function MyAjax(Method, Action, SendData, CallBackFunction, ResponseType) {
    var http = new XMLHttpRequest();

    http.onreadystatechange = function () {

        if (http.readyState == 4) {
            if (http.status >= 200 && http.status < 300) {
                //Success
                CallBackFunction(http.response);
            }
            else {
                // Error
                alert("خطا" + " " + http.statusText + " کد:" + http.status);
            }
        }
    }

    http.responseType = "json";
    if (ResponseType != null)
        http.responseType = ResponseType;

    var strdata = ConvertObjectToQueryString(SendData);

    if (Method == "Get" && strdata != "") {
        Action += "?" + strdata;
        strdata = "";
    }
    http.open(Method, Action, true);
    http.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    http.send(strdata);
}
function ShowErrorMessage(code) {
    var str = "خطا";
    if (code >= 400 && code < 500)
        str = "خطای سمت مشتری";
    if (code >= 500)
        str = "خطای سمت سرور";
    if (code == 401 || code == 403)
        str = " شما به این قسمت دسترسی ندارید ";
    if (code == 404)
        str = " منبع درخواستی معتبر نیست ";


    if (code == 520)
        str = "خطای فرمت";
    if (code == 521)
        str = "خطای تقسیم بر صفر";
    if (code == 522)
        str = "خطا در بازیابی رشته";
    if (code == 530)
        str = "خطای منبع داده";
    if (code == 535)
        str = "کاربر گرامی شما به این قسمت دسترسی ندارید";
    if (code == 531)
        str = "اطلاعات تکراری است";
    if (code == 533)
        str = "به پایگاه داده دسترسی ندارید";
    if (code == 532)
        str = "به دلیل اینکه این اطلاعات پیش نیاز اطلاعات دیگر هستند قابل تغییر نیستند";

    alert(str);
}


function ConvertObjectToQueryString(ob) {
    if (ob == null)
        return "";
    var str = "";
    var count = Object.keys(ob).length;
    var i = 0;
    for (var prop in ob) {
        i++;
        str += prop + "=" + ob[prop];
        if (i < count)
            str += "&";
    }

    return str;
}



var SelectedTr = null;
var FirstTr = null;
var LastTr = null;
function MyGrid(ListData, Content, Columns, Buttons, option) {

    if (ListData == null || ListData.length == 0) {
        return "";
    }
    var _table = '<table  class="table text-center table-responsive table-bordered table-hover"><thead><tr>';

    //var Columns = Object.keys(ListData[0]);
    if (option.IsRowNumber) {
        _table += '<td>ردیف</td>';
    }
    for (var i in Columns) {

        if (Columns[i].Data.toUpperCase() == "ID")
            continue;

        _table += '<td>' + Columns[i].Title + '</td>';
    }
    if (Buttons != null) {
        for (var i in Buttons) {
            _table += '<td>' + Buttons[i].Text + '</td>';
        }
    }
    _table += '</tr></thead><tbody id="BodyData">';

    for (var i in ListData) {
        var listtd = '';
        var trid = "";
        if (option.IsRowNumber) {
            var r = parseInt(i) + 1;
            listtd += '<td>' + r + '</td>';
        }
        for (var j in Columns) {

            if (Columns[j].Data.toUpperCase() == "ID") {
                trid = 'data-ID="' + ListData[i][Columns[j].Data] + '"';
                continue;
            }
            listtd += '<td>' + ListData[i][Columns[j].Data] + '</td>';
        }
        for (var j in Buttons) {

            listtd += '<td><button data-number="' + j + '" type="button"  class="btn btn-primary"><span class="glyphicon glyphicon-' + Buttons[j].Icon + '"></span>&nbsp;&nbsp;' + Buttons[j].Text + '</button></td>';
        }

        _table += '<tr ' + trid + '>' + listtd + '</tr>';

    }

    _table += '</tbody></table>';



    if (option.IsNavigated) {
        _table += '<div>';
        _table += '<button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-left"></span>&nbsp;</button>&nbsp;';
        _table += '<button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-right"></span>&nbsp;</button>';
        _table += '</div>';

    }
    Content.innerHTML = _table;

    if (option.IsSelected) {
        var listtr = document.querySelectorAll("#" + Content.id + " table tbody tr");

        var FirstTr = listtr[0];
        var LastTr = listtr[listtr.length - 1];


        for (var i = 0; i < listtr.length; i++) {

            listtr[i].addEventListener("click", function (e) {

                if (SelectedTr != null) {
                    SelectedTr.className = "";
                }
                SelectedTr = this;
                this.className = "bg-info";
                var id = this.getAttribute("data-id");
                if (option.SelectEvent != null)
                    option.SelectEvent(id);
            });
        }


        if (option.IsNavigated) {
            var Btns = document.querySelectorAll("#" + Content.id + " div button");
            if (Btns != null) {
                Btns[0].addEventListener("click", function () {

                    if (SelectedTr == null) {
                        SelectedTr = FirstTr;
                    }
                    else {

                        SelectedTr.className = "";
                        if (SelectedTr == LastTr) {
                            SelectedTr = FirstTr;
                        }
                        else {
                            SelectedTr = SelectedTr.nextSibling;
                        }
                    }

                    SelectedTr.className = "bg-info";
                    var id = SelectedTr.getAttribute("data-id");
                    if (option.SelectEvent != null)
                        option.SelectEvent(id);
                });


                Btns[1].addEventListener("click", function () {
                    if (SelectedTr == null) {
                        SelectedTr = LastTr;
                    }
                    else {
                        SelectedTr.className = "";


                        if (SelectedTr == FirstTr) {

                            SelectedTr = LastTr;
                        }
                        else {
                            SelectedTr = SelectedTr.previousSibling;
                        }
                    }

                    SelectedTr.className = "bg-info";
                    var id = SelectedTr.getAttribute("data-id");
                    if (option.SelectEvent != null)
                        option.SelectEvent(id);
                });
            }
        }
    }

    if (Buttons != null) {
        var listbuttons = document.querySelectorAll("#" + Content.id + " table tbody tr td button");
        for (var i = 0; i < listbuttons.length; i++) {
            listbuttons[i].addEventListener("click", function () {
                var id = this.parentNode.parentNode.getAttribute("data-id");
                Buttons[this.getAttribute('data-number')].Event(id);
            });
        }
    }
}

