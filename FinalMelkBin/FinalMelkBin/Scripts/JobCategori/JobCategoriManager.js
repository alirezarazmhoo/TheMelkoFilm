
function jobCategorimodel(id, namee) {
    this.id = id;
    this.namee = namee;
}
var _jobCategorimodel = new jobCategorimodel(0, "");

function JobCategoriRegister() {

    if ($(JobCategori).val() === '' || !CheckValidation(FormRegisterJobcategori)) {
        $(JobCategoriempty).fadeIn();
        $(JobCategoriempty).fadeOut(4000);

        return;
    }
    if (CheckValidation(FormRegisterJobcategori)) {
        jobCategorimodel.namee = JobCategori.value;
        jobCategorimodel.id = ID.value;

        MyAjax("Post", "JobCategoriRegister", jobCategorimodel, function (Data) {
            if (Data == "fail") {
                CloseModal.click();
                $('#errorCityRepat').click();
                var delayInMilliseconds = 2000;

                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);

            }

            else {

                location.reload();
            }

        });
    }
}