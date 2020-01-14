
function Categorimodel(id, categoriName) {
    this.id = id;
    this.categoriName = categoriName;
}
var theCategori = new Categorimodel(0, "");




function CategoriRegister() {

    if ($(Categori).val() === '' || !CheckValidation(FormRegisterCategori)) {
        $(Categoriempty).fadeIn();
        $(Categoriempty).fadeOut(4000);

        return;
    }
    Categorimodel.categoriName = Categori.value;
    Categorimodel.id = ID.value;
    if (CheckValidation(FormRegisterCategori)) {
        MyAjax("Post", "CategoriRegister", Categorimodel, function (Data) {
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



            //CloseModal.click();
            //$(CategoriSuuccess).fadeIn();
            //$(CategoriSuuccess).fadeOut(3000);
            //var delayInMilliseconds = 2000;

            //setTimeout(function () {
            //    window.location.reload();
            //}, delayInMilliseconds);

        });
    }

}