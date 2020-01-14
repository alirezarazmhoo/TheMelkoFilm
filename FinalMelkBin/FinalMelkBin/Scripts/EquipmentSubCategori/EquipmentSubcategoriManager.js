function theEquipmentSubCategori(namee, equipmentCategoriId, id) {

    this.namee = namee;

    this.equipmentCategoriId = equipmentCategoriId;
    this.id = id;
}


var _theEquipmentSubCategori = new theEquipmentSubCategori("", 0, 0);




function EquipmentSubcategoriregister() {

    if ($(EquipmentSubCategori).val() === '' || !CheckValidation(FormRegisterEquipmentSubCategori)) {
        $(EquipmentSubCategoriEmpty).fadeIn();
        $(EquipmentSubCategoriEmpty).fadeOut(4000);

        return;
    }
    theEquipmentSubCategori.namee = EquipmentSubCategori.value;
    theEquipmentSubCategori.equipmentCategoriId = EquipmentCategoriId.value;
    theEquipmentSubCategori.id = ID.value;
    if (CheckValidation(FormRegisterEquipmentSubCategori)) {
        MyAjax("Post", "EquipmentSubCategoriRegister", theEquipmentSubCategori, function (Data) {
            if (Data === "fail") {
                CloseModal.click();
                $('#errorCityRepat').click();
                var delayInMilliseconds = 2000;

                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);

            }

            else {

                location.reload();
            }


        });
    }

}
