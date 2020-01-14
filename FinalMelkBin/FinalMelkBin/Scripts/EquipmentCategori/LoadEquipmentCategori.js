
function EquipmentCategoriModel(id, namee) {
    this.id = id;
    this.namee = namee;
}
var _EquipmentCategoriModel = new EquipmentCategoriModel(0, "");
var ListEquipmentCategories = [];


function LoadTheEquipmentCategories() {
    MyAjax("Get", "GetEquipmentCategoriName", { 'description': Description.value }, function (listdata) {
        ListEquipmentCategories = listdata;

        tableEquipmentCategori(listdata, divCategori, [{ Data: "ID", Title: "ID" }, { Data: "Name", Title: "نام گروه " }], [{ Text: "RemoveJobCategori", classname: "danger", Icon: "remove", Event: Remove }, { Text: "EditJobCategori", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveEquipmentCategori", { "id": id }, function (Data) {
            if (Data === "fail") {
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
    ////btnRegister.click();
    var _find = false;
    for (var i in ListEquipmentCategories) {
        if (ListEquipmentCategories[i].ID == id) {
            _find = true;
            EquipmentCategoriModel.id = ListEquipmentCategories[i].ID;
            EquipmentCategoriModel.namee = ListEquipmentCategories[i].Name;
            break;
        }
    }
    if (_find) {
        ID.value = EquipmentCategoriModel.id;
        EquipmentCategori.value = EquipmentCategoriModel.namee;

    }

}
function SearchEuipmentCategori() {
    LoadTheEquipmentCategories();
}
function ResetEquipmentCateMOdal() {
    location.reload();
}