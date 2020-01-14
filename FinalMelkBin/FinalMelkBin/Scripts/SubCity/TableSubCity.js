
var SelectedTr = null;
var FirstTr = null;
var LastTr = null;
function tableSubCity(ListData, Content, Columns, Buttons, option) {

    if (ListData == null || ListData.length == 0) {
        return "";
    }
    var _table = '<table id="tableSubCity" class="table text-center table-striped table-bordered table-sm" cellspacing="0""><thead><tr>';

    //var Columns = Object.keys(ListData[0]);
    if (option.IsRowNumber) {
        _table += '<td>ردیف</td>';
    }
    for (var i in Columns) {

        if (Columns[i].Data.toUpperCase() == "ID")
            continue;

        _table += '<td  text-center>' + Columns[i].Title + '</td>';
    }
    //if (Buttons != null) {
    //    for (var i in Buttons) {
    //        _table += '<td>' + Buttons[i].Text + '</td>';
    //    }
    //}


    _table += '<td  text-center>امکانات</td>';

    _table += '</tr></thead><tbody id="BodyData">';

    for (var i in ListData) {

        var trid = "";
        var ListtdBtn = '';

        var listtd = '';

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
        //for (var j in Buttons) {

        //    listtd += '<td><button data-number="' + j + '" type="button" id="btnRegister" class="btn btn-' + Buttons[j].classname + '"><span class="glyphicon glyphicon-' + Buttons[j].Icon + '"></span>&nbsp;&nbsp;' + Buttons[j].Text + '</button></td>';
        //}
        listtd += '<td><button data-number="0" type="button"  data-toggle="modal" data-target="#RemoveModal" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;حذف</button>    <button data-number="1" type="button"  class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span>&nbsp;&nbsp;ویرایش</button></td>';

        listtd += ListtdBtn;

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

