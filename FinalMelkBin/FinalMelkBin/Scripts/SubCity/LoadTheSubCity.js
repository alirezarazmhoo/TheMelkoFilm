function TownShipModel(id, CityName,CityID) {
    this.id = id;
    this.townShipName = CityName;
    this.CityID = CityID;
}
var _theCity = new TownShipModel(0, "",0);
var listTownShip = [];




function LoadTheTownShip() {
    MyAjax("Get", "GetSubCityInfo", { 'description': Description.value }, function (listdata) {
        listTownShip = listdata;
        tableSubCity(listdata, divTownShip, [{ Data: "ID", Title: "ID" }, { Data: "CityName", Title: "استان مربوطه" }, { Data: "Name", Title: "نام شهرستان" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Remove }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveSubCity", { "id": id }, function (Data) {
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


            location.reload();
            }
        });
    }));
}
function edit(id) {
   
       
    //btnSubcityRegister.click();
    $('#btnSubcityRegister').click();
    var _find = false;
    for (var i in listTownShip) {
        if (listTownShip[i].ID == id) {
            _find = true;
            TownShipModel.id = listTownShip[i].ID;
            TownShipModel.townShipName = listTownShip[i].Name;
            TownShipModel.CityID = listTownShip[i].CityID;
            break;
        }
    }
    if (_find) {
        TownShip.value = TownShipModel.townShipName;
        ID.value = TownShipModel.id;
        TheCityID.value = TownShipModel.CityID;
        

    }
    

}
function SearchCity() {
    LoadTheTownShip();
}
function ResetRemovemodalsubcity() {
    location.reload();
}