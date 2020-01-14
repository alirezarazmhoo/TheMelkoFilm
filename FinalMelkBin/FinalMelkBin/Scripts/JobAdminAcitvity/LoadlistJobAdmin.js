function LoadJobListAdmin() {
    MyAjax("Get", "GetJobAdminInfo", { 'StatusCode': StatusCode.value, 'Description': Description.value }, function (listdata) {

        tableJobAdmin(listdata, divAdmin, [{ Data: "ID", Title: "ID" }, { Data: "AdminName", Title: "ثبت شده توسط" }, { Data: "Edate", Title: "درتاریخ" }, { Data: "job", Title: "عنوان شغل" }, { Data: "StatusSelected", Title: "وضعیت انتخاب شده " }, { Data: "StatusJob", Title: "وضعیت فعلی این آگهی" }], [{ Text: "Remove", classname: "danger", Icon: "remove", Event: Remove }], { IsRowNumber: true, IsSelected: true });

    });


}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveJobActivity", { "id": id }, function () {
            location.reload();
        });
    }));
}
function SearchJobAdminActivity() {
    LoadJobListAdmin();
}
function JobRemoveAll() {


    if ($("#OkRemoveAllButtun").click(function () {

        listRoles = [];
        selectedRole = {};
        $("#AdminJobActivity tbody tr").each(function () {

            var id = $(this).attr("data-id");
            item = {};
            item["ID"] = id;
            listRoles.push(item);
        });
        selectedRole["listRoles"] = listRoles;

        $.ajax({
            type: "Post",
            url: "RemoveAllRecordsForJobAdminActivity",
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