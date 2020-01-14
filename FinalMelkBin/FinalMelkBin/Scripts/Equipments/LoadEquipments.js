function LoadEquipments() {
    MyAjax("Get", "GetEquipments", { 'description': Description.value, 'StatusCode': EquipmentStatusCode.value }, function (listEquipments) {
        tableEquipments(listEquipments, divMelk, [{ Data: "ID", Title: "ID" }, { Data: "Title", Title: " عنوان" }, { Data: "pexpired", Title: "تاریخ انقضا" }, { Data: "City", Title: "استان" }, { Data: "StatusID", Title: "وضعیت" }], [{ Text: "Removemelk", classname: "danger", Icon: "remove", Event: Remove }, { Text: "ثبت وضعیت", classname: "primary", Icon: "edit", Event: edit }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: informations }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: StatusRegister }], { IsRowNumber: true, IsSelected: true });
    });
}
function informations(id) {
    popupWindow = window.open("../Admin/ShowEquipmentInformation/" + id + ""
        , 'popUpWindow', 'height=500,width=1000,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes')
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveEquipment", { "id": id }, function (Data) {
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
        if ($("#EquipmentStatus:checked").val() != null) {

            var statusvalue = $("#EquipmentStatus:checked").val();
            var adminComment = $("#adminComment").val();
            MyAjax("Post", "EquipmentStatusRegister", { 'statusvalue': statusvalue, 'id': id, 'adminComment': adminComment }, function () {
                $('#adminComment').val('');

                location.reload();
                _Done = true;
                if (_Done) {
                    MyAjax("Post", "EquipmentUserActivity", { 'id': id, 'Puser': Puser, 'statusvalue': statusvalue }, function () {

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
function SearchEquipment() {
    LoadEquipments();
}
function resetStatusModal() {
    $('#adminComment').val('');

    location.reload();
}
function ResertRemoveEquipment() {

    location.reload();
}
