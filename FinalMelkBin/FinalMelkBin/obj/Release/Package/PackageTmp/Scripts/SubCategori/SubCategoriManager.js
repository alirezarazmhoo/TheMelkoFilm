
function theDownCategoriName(subCategoriName, categoriId,id) {

    this.subCategoriName = subCategoriName;

    this.categoriId = categoriId;
    this.id = id;
}


var _downCategori = new theDownCategoriName("", 0,0);




function downcategoriregister() {

    if ($(downCategori).val() === '' || !CheckValidation(FormRegisterSubCategori)) {
        $(SubCategoriEmpty).fadeIn();
        $(SubCategoriEmpty).fadeOut(4000);

        return;
    }
    theDownCategoriName.subCategoriName = $('#downCategori').val();
    theDownCategoriName.categoriId = $('#SubCat').val();
    theDownCategoriName.id = $('#ID').val();

    MyAjax("Post", "SubCategoriRegister", theDownCategoriName, function(Data) {
        if (Data != null) {
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
function ResetSubCategoriModal() {
    location.reload();
}
