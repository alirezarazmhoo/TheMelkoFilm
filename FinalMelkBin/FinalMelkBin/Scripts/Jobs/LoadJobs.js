function LoadJob() {
    MyAjax("Get", "GetJobs", { 'description': Description.value, 'StatusCode': JobStatusCode.value }, function (listJobs) {
        
        tableJobs(listJobs, divMelk, [{ Data: "ID", Title: "ID" }, { Data: "Title", Title: " عنوان" }, { Data: "FullName", Title: "نام شخص" }, { Data: "City", Title: "استان" }, { Data: "ExpireDate", Title: "انقضا" }, { Data: "StatusID", Title: "وضعیت" }], [{ Text: "Removemelk", classname: "danger", Icon: "remove", Event: Remove }, { Text: "ثبت وضعیت", classname: "primary", Icon: "edit", Event: edit }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: informations }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: StatusRegister }], { IsRowNumber: true, IsSelected: true });
    });
}
function informations(id) {
    popupWindow = window.open("../Admin/ShowJobInformation/" + id + ""
        , 'popUpWindow', 'height=500,width=1000,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes')
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveJob", { "id": id }, function (Data) {

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

        if ($("#JobStatus:checked").val() != null) {

            var statusvalue = $("#JobStatus:checked").val();
            var adminComment = $("#adminComment").val();
            MyAjax("Post", "JobStatusRegister", { 'statusvalue': statusvalue, 'id': id, 'adminComment': adminComment }, function () {
                $('#adminComment').val('');

                location.reload();
                _Done = true;
                if (_Done) {
                    MyAjax("Post", "JobUserActivity", { 'id': id, 'Puser': Puser, 'statusvalue': statusvalue }, function () {

                    });
                }

            });
        }
        else {
            alert("موردی را انتخاب کنید");
            location.reload();
            return;
        }
    }));







    //var Puser = $("#userInformation").val();

    //var _Done = false;


    //var statusvalue = $("#JobStatus:checked").val();
    //MyAjax("Post", "JobStatusRegister", { 'statusvalue': statusvalue, 'id': id }, function () {
    //    location.reload();
    //    _Done = true;
    //    if (_Done) {
    //        MyAjax("Post", "JobUserActivity", { 'id': id, 'Puser': Puser, 'statusvalue': statusvalue }, function () {

    //        });
    //    }

    //});


}
function SearchJob() {
    LoadJob();
}
function resetStatusModal() {
    $('#adminComment').val('');
    location.reload();
}