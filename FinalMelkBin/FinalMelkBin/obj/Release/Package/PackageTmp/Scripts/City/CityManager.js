function CityModel(id, cityName) {
    this.id = id;
    this.cityName = cityName;
}

var _CityModel = CityModel(0, "");

function CityRegister() {

    if ($(CityName).val() === '' ) {
        $(Cityempty).fadeIn();
        $(Cityempty).fadeOut(4000);

        return;
    }
    CityModel.cityName = CityName.value;
    CityModel.id = ID.value;

 

        MyAjax("Post", "CityRegister", CityModel, function (Data) {
            if (Data != null) {

               $('#CloseModal').click();
                $('#errorCityRepat').click();
                  var delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);

            }

            else {
                $('#CloseModal').click();

                location.reload();
            }
            //$(CitySuuccess).fadeIn();
            //$(CitySuuccess).fadeOut(3000);

            //var delayInMilliseconds = 2000;

            //setTimeout(function () {
            //    window.location.reload();
            //}, delayInMilliseconds);
        });
    
    $('#CityModal').on('hidden', function () {
        $.clearInput();
    });
}