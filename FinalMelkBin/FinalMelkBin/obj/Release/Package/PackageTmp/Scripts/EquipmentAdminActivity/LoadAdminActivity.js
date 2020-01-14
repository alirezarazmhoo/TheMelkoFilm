function LoadEquipmentListAdmin() {
    MyAjax("Get", "GetEquipmentAdminInfo", { 'StatusCode': StatusCode.value, 'Description': Description.value }, function (listdata) {

        tableEquipmentAdmin(listdata, divAdmin, [{ Data: "ID", Title: "ID" }, { Data: "AdminName", Title: "ثبت شده توسط" }, { Data: "Edate", Title: "درتاریخ" }, { Data: "Equipment", Title: "عنوان وسیله" }, { Data: "StatusSelected", Title: "وضعیت انتخاب شده " }, { Data: "StatusJob", Title: "وضعیت فعلی این آگهی" }], [{ Text: "Remove", classname: "danger", Icon: "remove", Event: Remove }], { IsRowNumber: true, IsSelected: true });

    });


}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveEquipmentActivity", { "id": id }, function () {
            location.reload();
        });
    }));
}
function SearchEquipmentAdminActivity() {
    LoadEquipmentListAdmin();
}
function EquipmentRemoveAll() {


    if ($("#OkRemoveAllButtun").click(function () {

        listRoles = [];
        selectedRole = {};
        $("#EquipmentActivity tbody tr").each(function () {

            var id = $(this).attr("data-id");
            item = {};
            item["ID"] = id;
            listRoles.push(item);
        });
        selectedRole["listRoles"] = listRoles;

        $.ajax({
            type: "Post",
            url: "RemoveAllRecordsForEquipmentAdminActivity",
            dataType: 'json',
            data: selectedRole,
            success: function () {
                $("#sucessModal").click();
                delayInMilliseconds = 2000;

                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);

            }
        });


    }));

}
