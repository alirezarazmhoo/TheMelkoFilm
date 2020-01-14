
function EquipmentSubCategoriModel(id, namee, equipmentCategoriId) {
    this.id = id;
    this.namee = namee;
    this.equipmentCategoriId = equipmentCategoriId;
}
var _EquipmentSubCategoriModel = new EquipmentSubCategoriModel(0, "", 0);
var ListEquipmentSubCategori = [];




function LoadEquipmentSubCategori() {
    MyAjax("Get", "GetEquipmentSubCategori", { 'description': Description.value }, function (listdata) {

        ListEquipmentSubCategori = listdata;
        tableEquipmentSubCategori(listdata, divdownCat, [{ Data: "ID", Title: "ID" }, { Data: "NameEquipmentCategori", Title: "گروه" }, { Data: "Name", Title: "زیرگروه" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Removeee }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Removeee(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "EquipmentSubCategoriRemove", { "id": id }, function (Data) {
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
    //btnRegister.click();
    var _find = false;
    for (var i in ListEquipmentSubCategori) {
        if (ListEquipmentSubCategori[i].ID == id) {
            _find = true;
            EquipmentSubCategoriModel.id = ListEquipmentSubCategori[i].ID;
            EquipmentSubCategoriModel.namee = ListEquipmentSubCategori[i].Name;
            EquipmentSubCategoriModel.equipmentCategoriId = ListEquipmentSubCategori[i].equipmentCategoriId;
            break;
        }
    }
    if (_find) {
        EquipmentSubCategori.value = EquipmentSubCategoriModel.namee;
        ID.value = EquipmentSubCategoriModel.id;
        EquipmentCategoriId.value = EquipmentSubCategoriModel.equipmentCategoriId;


    }
}
function SearchEquipmentSubCategori() {
    LoadEquipmentSubCategori();
}
function equipmentsubcateremove() {
    location.reload();
}