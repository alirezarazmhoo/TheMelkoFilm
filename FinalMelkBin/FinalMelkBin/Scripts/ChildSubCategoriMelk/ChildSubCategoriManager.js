
function theChildCategoriName(childSubCategoriName, subCategoriId, id) {

    this.childSubCategoriName = childSubCategoriName;

    this.subCategoriId = subCategoriId;
    this.id = id;
}


var _theChildCategoriName = new theChildCategoriName("", 0, 0);




function ChildSubcategoriregister() {

    if ($(childSubCategoriName).val() === '' ) {
        $(SubCategoriEmpty).fadeIn();
        $(SubCategoriEmpty).fadeOut(4000);

        return;
    }
    theChildCategoriName.childSubCategoriName = childSubCategoriName.value;
    theChildCategoriName.subCategoriId = SubCat.value;
    theChildCategoriName.id = ID.value;

    MyAjax("Post", "ChildSubCategoriRegister", theChildCategoriName, function (Data) {
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
