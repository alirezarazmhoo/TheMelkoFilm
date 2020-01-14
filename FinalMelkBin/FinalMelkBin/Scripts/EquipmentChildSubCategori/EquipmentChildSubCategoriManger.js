
function theChildCategoriName(childSubEquipmentCategoriName, equipmentSubCategoriId, id) {

    this.childSubEquipmentCategoriName = childSubEquipmentCategoriName;

    this.equipmentSubCategoriId = equipmentSubCategoriId;
    this.id = id;
}


var _theChildCategoriName = new theChildCategoriName("", 0, 0);




function ChildSubcategoriregister() {

    if ($(childSubCategoriName).val() === '') {
        $(SubCategoriEmpty).fadeIn();
        $(SubCategoriEmpty).fadeOut(4000);

        return;
    }
    theChildCategoriName.childSubEquipmentCategoriName = childSubCategoriName.value;
    theChildCategoriName.equipmentSubCategoriId = SubCat.value;
    theChildCategoriName.id = ID.value;

    MyAjax("Post", "EquipmentChildSubCategoriRegister", theChildCategoriName, function (Data) {
        if (Data != null) {
            CloseModal.click();
            $('#errorCityRepat').click();
            var delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);

        }

        else {
            CloseModal.click();

            location.reload();
        }


    });

}
function ResetSubCategoriModal() {
    location.reload();
}
