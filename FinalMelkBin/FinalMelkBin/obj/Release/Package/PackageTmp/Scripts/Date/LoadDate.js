function loadDate() {
    MyAjax("Get", "GetDate", { 'description': Description.value }, function (listdata) {
       

        tableDate(listdata, divDate, [{ Data: "ID", Title: "ID" }, { Data: "Date", Title: "تاریخ" }], [{ Text: "RemoveCategori", classname: "danger", Icon: "remove", Event: Remove }], { IsRowNumber: true, IsSelected: true });

    });


}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post","RemoveDate",{ "id": id }, function (Data) {
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
function SearchDate() {
    loadDate();
}
function ResetRemoveModalDate() {
    location.reload();
}