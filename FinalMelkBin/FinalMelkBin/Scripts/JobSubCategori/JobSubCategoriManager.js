function theSubJobCategori(namee, jobCategoriId, id) {

    this.namee = namee;

    this.jobCategoriId = jobCategoriId;
    this.id = id;
}


var _theSubJobCategori = new theSubJobCategori("", 0, 0);




function jobSubcategoriregister() {

    if ($(JobSubCategori).val() === '' || !CheckValidation(FormRegisterJobSubCategori)) {
        $(JobSubCategoriEmpty).fadeIn();
        $(JobSubCategoriEmpty).fadeOut(4000);

        return;
    }
    theSubJobCategori.namee = JobSubCategori.value;
    theSubJobCategori.jobCategoriId = jobCategoriId.value;
    theSubJobCategori.id = ID.value;

    MyAjax("Post", "JobSubCategoriRegister", theSubJobCategori, function (Data) {
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
function ResetRemoveModal() {
    location.reload();
}