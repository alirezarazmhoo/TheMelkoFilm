function LoadMelk() {
    MyAjax("Get", "GetMelkInfo", { 'description': Description.value, 'StatusCode': MelkStatusCode.value}, function (listMelk) {
        tableMelk(listMelk, divMelk, [{ Data: "ID", Title: "ID" }, { Data: "Name", Title: "نام ملک" }, { Data: "Price", Title: "قیمت" }, { Data: "Mobile", Title: "موبایل" }, { Data: "CityName", Title: "استان" }, { Data: "StatusID", Title: "وضعیت" }, { Data: "ExpireDate", Title: "انقضا" }], [{ Text: "Removemelk", classname: "danger", Icon: "remove", Event: Remove }, { Text: "ثبت وضعیت", classname: "primary", Icon: "edit", Event: edit }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: informations }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: StatusRegister }], { IsRowNumber: true, IsSelected: true });
    });
}
function informations(id) {
    popupWindow = window.open("../Admin/ShowMelkInformation/" + id + ""
        , 'popUpWindow', 'height=500,width=1000,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes')
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveMelk", { "id": id }, function (Data) {
            $("#sucessModal").click();
            delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);
         
        });

    }));


}
function edit(id) {
   


}
function StatusRegister(id) {


    if ($('#OkStatusButtun').click(function () {

        var Puser = $("#userInformation").val();

        var _Done = false;
        if ($("#melkStatus:checked").val() != null) {

            var statusvalue = $("#melkStatus:checked").val();
            var adminComment = $('#adminComment').val();
           
            MyAjax("Post", "StatusRegister", { 'statusvalue': statusvalue, 'id': id, 'adminComment': adminComment }, function () {
                $('#adminComment').val('');

                location.reload();

                _Done = true;
                if (_Done) {
                    MyAjax("Post", "UserActivity", { 'id': id, 'Puser': Puser, 'statusvalue': statusvalue }, function () {

                    });
                }

            });
        }
     
              else {
            alert("موردی را انتخاب کنید");
            location.reload();
            }
        
    }));



}
function SearchMelk() {
    LoadMelk();
}
function resetStatusMelkModal() {
    $('#adminComment').val('');
    location.reload();
}
function resetMelkModal() {
    location.reload();

}
