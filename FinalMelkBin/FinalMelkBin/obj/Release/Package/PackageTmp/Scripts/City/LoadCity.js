/// <reference path="../model.js" />
function CityModel(id, CityName) {
    this.id = id;
    this._CityName = CityName;
}
var theCity = new CityModel(0, "");
var listCityes = [];


function LoadCity() {
    MyAjax("Get", "GetCityName", { 'description': Description.value }, function (listdata) {

        listCityes = listdata;
        tableCity(listdata, divProduct, [{ Data: "ID", Title: "ID" }, { Data: "Name", Title: "نام استان" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Remove }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveCity", { "id": id }, function (Data) {
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
    
    $("#btnRegister").click();
    var _find = false;
    for (var i in listCityes) {
        if (listCityes[i].ID == id) {
            _find = true;
            CityModel.id = listCityes[i].ID;
            CityModel._CityName = listCityes[i].Name;
            break;
        }
    }
    if (_find) {
        ID.value = CityModel.id;
        CityName.value = CityModel._CityName;


    }

}
function SearchCity() {
   
    LoadCity();
}
function ResetRemoveModalCity() {
    location.reload();
}