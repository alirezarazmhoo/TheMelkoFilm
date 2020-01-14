
    function DownCategoriModel(id, DownCategoriName, CategoriID) {
        this.id = id;
    this.DownCategoriName = DownCategoriName;
    this.CategoriID = CategoriID;
}
var _DownCategori = new DownCategoriModel(0, "", 0);
var ListDownCategori = [];




function LoadDownCategori() {
    MyAjax("Get", "GetSubCategoriInfo", { 'description': Description.value }, function (listdata) {

            ListDownCategori = listdata;
            tableSubCategori(listdata, divdownCat, [{ Data: "ID", Title: "ID" }, { Data: "CategoriName", Title: "گروه اصلی " },{ Data: "Name", Title: "زیردسته اول" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Removeee }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

        });
    }
function Removeee(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "DownCatDel", { "id": id }, function (Data) {


        if (Data == null) {
            $("#errorRemove").click();
            if ($('#CloseErrorModal').on('click', function () {

                location.reload();
            }));
            var delayInMilliseconds = 3000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);

        }
        else {

            $("#sucessModal").click();
            delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);

        }

    });
    }));

    }
       
function edit(id) {
    $('#btnRegister').click();
        //btnRegister.click();
    var _find = false;
        for (var i in ListDownCategori) {
            if (ListDownCategori[i].ID == id) {
        _find = true;
    DownCategoriModel.id = ListDownCategori[i].ID;
    DownCategoriModel.DownCategoriName = ListDownCategori[i].Name;
    DownCategoriModel.CategoriID = ListDownCategori[i].CategoriID;
    break;
}
}
        if (_find) {
            downCategori.value = DownCategoriModel.DownCategoriName;
    ID.value = DownCategoriModel.id;
    theDownCategori.value = DownCategoriModel.CategoriID;


}
}
function SearchCity() {
    Load();
}