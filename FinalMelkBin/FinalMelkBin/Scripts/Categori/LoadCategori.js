
function CategoriModel(id, CategoriName) {
    this.id = id;
    this.categoriName = CategoriName;
}
var theCategori = new CategoriModel(0, "");
var ListCategories = [];


function LoadTheCategories() {
    MyAjax("Get", "GetCategoriName", { 'description': Description.value }, function (listdata) {
        ListCategories = listdata;

        tableCategori(listdata, divCategori, [{ Data: "ID", Title: "ID" }, { Data: "Name", Title: "نام دسته" }], [{ Text: "RemoveCategori", classname: "danger", Icon: "remove", Event: Remove }, { Text: "EditCategori", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveCategori", { "id": id }, function (Data) {
            if (Data != null) {
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
    for (var i in ListCategories) {
        if (ListCategories[i].ID == id) {
            _find = true;
            CategoriModel.id = ListCategories[i].ID;
            CategoriModel.categoriName = ListCategories[i].Name;
            break;
        }
    }
    if (_find) {
        ID.value = CategoriModel.id;
        Categori.value = CategoriModel.categoriName;

    }

}
function SearchCity() {
    LoadTheCategories();
}
function ResetRemoveModalCategori() {
    location.reload();
}