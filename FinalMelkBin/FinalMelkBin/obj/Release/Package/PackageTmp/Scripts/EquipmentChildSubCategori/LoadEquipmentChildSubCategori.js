/// <reference path="equipmentchildsubcategoritable.js" />


function theChildCategoriName(id, childSubCategoriName, subCategoriId) {
    this.id = id;
    this.childSubCategoriName = childSubCategoriName;
    this.subCategoriId = subCategoriId;
}
var _theChildCategoriName = new theChildCategoriName(0, "", 0);
var ListDownCategori = [];


function LoadEquipmentSubCategori() {
    MyAjax("Get", "GetEquipmentChildSubCategori", { 'description': Description.value }, function (listdata) {

        ListDownCategori = listdata;
        tableEquipmentChildSubCategori(listdata, divdownCat, [{ Data: "ID", Title: "ID" }, { Data: "CategoriName", Title: "گروه اصلی " }, { Data: "SubCategori", Title: "زیردسته اول" }, { Data: "ChildSubCategori", Title: "زیردسته دوم" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Removeee }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Removeee(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "EquipmentChildSubCatDel", { "id": id }, function (Data) {

            if (Data == "fail") {
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
            theChildCategoriName.id = ListDownCategori[i].ID;
            theChildCategoriName.childSubCategoriName = ListDownCategori[i].ChildSubCategori;
            theChildCategoriName.subCategoriId = ListDownCategori[i].subCategoriId;
            break;
        }
    }
    if (_find) {
        childSubCategoriName.value = theChildCategoriName.childSubCategoriName;
        ID.value = theChildCategoriName.id;
        SubCat.value = theChildCategoriName.subCategoriId;


    }
}
function Search() {
    LoadEquipmentSubCategori();
}
function ResetSubCategoriModal() {
    location.reload();
}