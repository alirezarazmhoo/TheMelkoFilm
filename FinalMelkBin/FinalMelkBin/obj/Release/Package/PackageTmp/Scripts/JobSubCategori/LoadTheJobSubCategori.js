
function JobSubCategoriModel(id, namee, jobCategoriId) {
    this.id = id;
    this.namee = namee;
    this.jobCategoriId = jobCategoriId;
}
var _JobSubCategoriModel = new JobSubCategoriModel(0, "", 0);
var ListJobSubCategoriCategori = [];




function LoadJobSubCategori() {
    MyAjax("Get", "GetJobSubCategori", { 'description': Description.value }, function (listdata) {

        ListJobSubCategoriCategori = listdata;
        tableJobSubCategori(listdata, divdownCat, [{ Data: "ID", Title: "ID" }, { Data: "NameJoBbCategori", Title: "دسته بندی" }, { Data: "Name", Title: "زیردسته" }], [{ Text: "RemoveCity", classname: "danger", Icon: "remove", Event: Removeee }, { Text: "EditCity", classname: "primary", Icon: "edit", Event: edit }], { IsRowNumber: true, IsSelected: true });

    });
}
function Removeee(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "jobSubCategoriRemove", { "id": id }, function (Data) {
            if (Data == "fail") {
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
    $('#btnRegister').click();
    //btnRegister.click();
    var _find = false;
    for (var i in ListJobSubCategoriCategori) {
        if (ListJobSubCategoriCategori[i].ID == id) {
            _find = true;
            JobSubCategoriModel.id = ListJobSubCategoriCategori[i].ID;
            JobSubCategoriModel.namee = ListJobSubCategoriCategori[i].Name;
            JobSubCategoriModel.jobCategoriId = ListJobSubCategoriCategori[i].JobCategoriId;
            break;
        }
    }
    if (_find) {
        JobSubCategori.value = JobSubCategoriModel.namee;
        ID.value = JobSubCategoriModel.id;
        jobCategoriId.value = JobSubCategoriModel.jobCategoriId ;


    }
}
function SearchJobSubCategori() {
    LoadJobSubCategori();
}