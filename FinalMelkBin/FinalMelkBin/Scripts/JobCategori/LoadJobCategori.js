
function JobCategoriModel(id, namee) {
    this.id = id;
    this.namee = namee;
}
var _JobCategoriModel = new JobCategoriModel(0, "");
var ListJobCategories = [];


function LoadTheJobCategories() {
    MyAjax("Get", "GetJobCategoriName", { 'description': Description.value }, function (listdata) {
        ListJobCategories = listdata;

        tableJobCategori(listdata, divCategori, [{ Data: "ID", Title: "ID" }, { Data: "Name", Title: "نام دسته شغلی" }], [{ Text: "RemoveJobCategori", classname: "danger", Icon: "remove", Event: Remove }, { Text: "EditJobCategori", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveJobCategori", { "id": id }, function (Data) {
            if (Data == "failed") {
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
    //$('#btnRegister').click();
    btnRegister.click();
    var _find = false;
    for (var i in ListJobCategories) {
        if (ListJobCategories[i].ID == id) {
            _find = true;
            JobCategoriModel.id = ListJobCategories[i].ID;
            JobCategoriModel.namee = ListJobCategories[i].Name;
            break;
        }
    }
    if (_find) {
        ID.value = JobCategoriModel.id;
        JobCategori.value = JobCategoriModel.namee;

    }

}
function SearchJobCategori() {
    LoadTheJobCategories();
}
function ResetRemoveModal() {
    location.reload();
}