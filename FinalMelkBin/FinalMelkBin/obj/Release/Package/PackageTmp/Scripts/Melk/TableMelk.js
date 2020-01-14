
var SelectedTr = null;
var FirstTr = null;
var LastTr = null;
function tableMelk(ListData, Content, Columns, Buttons, option) {

    if (ListData == null || ListData.length == 0) {
        return "";
    }
    var _table = '<table id="tableMelk"   class="table table-striped table-bordered table-sm" cellspacing="0"><thead><tr>';

    //var Columns = Object.keys(ListData[0]);
    if (option.IsRowNumber) {
        _table += '<th>ردیف</th>';
    }
    for (var i in Columns) {

        if (Columns[i].Data.toUpperCase() == "ID")
            continue;

        _table += '<th>' + Columns[i].Title + '</th>';
    }
    //if (Buttons != null) {
    //    for (var i in Buttons) {
    //        _table += '<td>' + Buttons[i].Text + '</td>';
    //    }
    //}


    _table += '<th>امکانات</th>';

    _table += '</tr></thead><tbody id="BodyData">';

    for (var i in ListData) {

        var trid = "";
        var ListtdBtn = '';
        var trstatus = '';
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
            if (Columns[j].Data == "StatusID") {
                trstatus = ListData[i][Columns[j].Data];
                if (trstatus == 1) {

                    listtd += '<td class="success text-success" style="font-weight:bold">تاییدشده</td>';
                }

                if (trstatus == 2) {

                    listtd += '<td class="warning text-warning" style="font-weight:bold">در حال انتظار</td>';
                }
                if (trstatus == 3) {

                    listtd += '<td class="danger text-danger" style="font-weight:bold">تاییدنشده</td>';
                }

            
            }
            if (Columns[j].Data == "StatusID") {
                continue;
            }
            listtd += '<td>' + ListData[i][Columns[j].Data] + '</td>';
        }
        //for (var j in Buttons) {

        //    listtd += '<td><button data-number="' + j + '" type="button" id="btnRegister" class="btn btn-' + Buttons[j].classname + '"><span class="glyphicon glyphicon-' + Buttons[j].Icon + '"></span>&nbsp;&nbsp;' + Buttons[j].Text + '</button></td>';
        //}
        listtd += '<td><button data-number="0" type="button" id="btnRegister" data-toggle="modal" data-target="#RemoveModal" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;حذف</button>   <button data-number="2" type="button" id="btnRegister" class="btn btn-warning" ><span class="glyphicon glyphicon-eye"></span>&nbsp;&nbsp;جزییات</button>  <button data-number="3" type="button" id="btnstatusRegister" class="btn btn-info" data-toggle="modal" data-target="#StatusModal"><span class="glyphicon glyphicon-eye" ></span>&nbsp;&nbsp;مجوز</button></td>';

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
    //   var table = '#tableMelk';
    //$('#maxRows').on('change', function () {
    //    $('.pagination').html('')
    //    var trnum = 0
    //    var maxRows = parseInt($(this).val())
    //    var totalRows = $(table + ' tbody tr').length
    //    $(table + ' tr:gt(0)').each(function () {
    //        trnum++
    //        if (trnum > maxRows) {
    //            $(this).hide()
    //        }
    //        if (trnum <= maxRows) {
    //            $(this).show()
    //        }
    //    })
    //    if (totalRows > maxRows) {
    //        var pagenum = Math.ceil(totalRows / maxRows)
    //        for (var i = 1; i <= pagenum;) {
    //            $('.pagination').append('<li data-page="' + i + '">\<span>' + i++ + '<span class="sr-only">(current)</span></span>\</li>').show()


    //        }
    //    }
    //    $('.pagination li:first-child').addClass('active')
    //    $('.pagination li').on('click', function () {
    //        var pageNum = $(this).attr('data-page')
    //        var trIndex = 0;
    //        $('.pagination li').removeClass('active')
    //        $(this).addClass('active')
    //        $(table + ' tr:gt(0)').each(function () {
    //            trIndex++
    //            if (trIndex > (maxRows * pageNum) || trIndex <= ((maxRows * pageNum) - maxRows)) {
    //                $(this).hide()
    //            } else {
    //                $(this).show()
    //            }
    //        })
    //    })
    //})
    //$(function () {
    //    $('table tr:eq(0)').prepend('<th>ID</th>')
    //    var id = 0;
    //    $('table tr:gt(0)').each(function () {
    //        id++
    //        $(this).prepend('<td>' + id + '</td>')

    //    })
    //})

}

