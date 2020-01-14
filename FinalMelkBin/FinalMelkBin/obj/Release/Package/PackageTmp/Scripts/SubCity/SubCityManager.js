
function TownName(subCityname, cityId,ID) {

    this.subCityname = subCityname;
   
    this.cityId = cityId;
    this.id = ID;
}


var _Town = new TownName("", 0,0);



function TownShipRegister() {

    if ($(TownShip).val() === '' ) {
        $(TownShipempty).fadeIn();
        $(TownShipempty).fadeOut(4000);

        return;
    }

    TownName.subCityname = $('#TownShip').val();
    TownName.cityId = $('#TheCityID').val();
    TownName.id = $('#ID').val();

    MyAjax("Post", "subCityRegister", TownName, function (Data) {
        if (Data == "fail") {
            //CloseModal.click();
            resetInputSubCityModal();
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


            //CloseModal.click();
            //$(TownCitySucces).fadeIn();
            //$(TownCitySucces).fadeOut(3000);

            //var delayInMilliseconds = 2000;

            //setTimeout(function () {
            //    window.location.reload();
            //}, delayInMilliseconds);


        });
    
    
}
function resetInputSubCityModal() {
    $('#TownShip').val('');

}