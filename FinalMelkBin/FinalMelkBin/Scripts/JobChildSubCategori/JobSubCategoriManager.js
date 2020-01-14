
function theChildCategoriName(childJobSubCategoriName, jobSubCategoriId, id) {

    this.childJobSubCategoriName = childJobSubCategoriName;

    this.jobSubCategoriId = jobSubCategoriId;
    this.id = id;
}


var _theChildCategoriName = new theChildCategoriName("", 0, 0);




function ChildSubcategoriregister() {

    if ($(childSubCategoriName).val() === '' ) {
        $(SubCategoriEmpty).fadeIn();
        $(SubCategoriEmpty).fadeOut(4000);

        return;
    }
    theChildCategoriName.childJobSubCategoriName = childSubCategoriName.value;
    theChildCategoriName.jobSubCategoriId = SubCat.value;
    theChildCategoriName.id = ID.value;

    MyAjax("Post", "JobChildSubCategoriRegister", theChildCategoriName, function (Data) {
        if (Data != null) {
            CloseModal.click();
            $('#errorCityRepat').click();
            var delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);

        }

        else {
            CloseModal.click();

            location.reload();
        }


    });

}
function ResetSubCategoriModal() {
    location.reload();
}
