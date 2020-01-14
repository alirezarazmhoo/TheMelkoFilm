
function EquipmentCategorimodel(id, namee) {
    this.id = id;
    this.namee = namee;
}
var _EquipmentCategorimodel = new EquipmentCategorimodel(0, "");

function EquipmentCategoriRegister() {

    if ($(EquipmentCategori).val() === '' || !CheckValidation(FormRegisterEquipmentcategori)) {
        $(EquipmentCategoriempty).fadeIn();
        $(EquipmentCategoriempty).fadeOut(4000);

        return;
    }
    if (CheckValidation(FormRegisterEquipmentcategori)) {
        EquipmentCategorimodel.namee = EquipmentCategori.value;
        EquipmentCategorimodel.id = ID.value;

        MyAjax("Post", "AddEquipmentCategori", EquipmentCategorimodel, function (Data) {
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