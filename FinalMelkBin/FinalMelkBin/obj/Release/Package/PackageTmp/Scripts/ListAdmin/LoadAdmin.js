function LoadListAdmin() {
    MyAjax("Get", "GetAdminInfo", { 'StatusCode': StatusCode.value, 'Description': Description.value }, function (listdata) {

        tableAdmin(listdata, divAdmin, [{ Data: "ID", Title: "ID" }, { Data: "AdminName", Title: "ثبت شده توسط" }, { Data: "Edate", Title: "درتاریخ" }, { Data: "Melk", Title: "ملک" }, { Data: "StatusSelected", Title: "وضعیت انتخاب شده " }, { Data: "StatusMelk", Title: "وضعیت فعلی این آگهی" }], [{ Text: "Remove", classname: "danger", Icon: "remove", Event: Remove }], { IsRowNumber: true, IsSelected: true });

    });


}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveActivity", { "id": id }, function () {
            delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);
        });
    }));
}
function SearchAdminActivity() {
    LoadListAdmin();
}
function RemoveAll() {

 
    if ($("#OkRemoveAllButtun").click(function () {
        
        listRoles = [];
        selectedRole = {};
        $("#adminmelk tbody tr").each(function () {

            var id = $(this).attr("data-id");
            item = {};
            item["ID"] = id;
            listRoles.push(item);
        });
        selectedRole["listRoles"] = listRoles;

        $.ajax({
            type: "Post",
            url: "RemoveAllRecordsForMelkAdminActivity",
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



    
        //item = {};
        //item["ID"] = id;

        //listRoles.push(item);
  
    //selectedRole["listRoles"] = listRoles;
    //selectedRole["AdminID"] = id;

    //$.ajax({
    //    type: "Post",
    //    url: "AdminUserRolesRegister",
    //    dataType: 'json',
    //    data: selectedRole,
    //    success: function () {
    //        alert("عملیات انجام شد!");

    //    }
    //});

}