function AdminUserModel(id,fullName,telephone,mobile,userName,password) {
    this.id = id;
    this.fullName = fullName;
    this.telephone = telephone;
    this.mobile = mobile;
    this.userName = userName;
    this.password = password;
}

var _AdminUserModel = AdminUserModel(0, "","","","","");

function AdminUserRegister() {

    if ($(AdminFullName).val() === '' || $(AdminUserTelephone).val() === '' || $(AdminUserMobile).val() === '' || $(AdminUserName).val() === '' || $(AdminUserPassword).val() ==='') {
        $(AdminUserempty).fadeIn();
        $(AdminUserempty).fadeOut(4000);

        return;
    }
    AdminUserModel.fullName = AdminFullName.value;
    AdminUserModel.telephone = AdminUserTelephone.value;
    AdminUserModel.mobile = AdminUserMobile.value;
    AdminUserModel.userName = AdminUserName.value;
    AdminUserModel.password = AdminUserPassword.value;


    AdminUserModel.id = ID.value;

   

        MyAjax("Post", "AdminUserRegister", AdminUserModel, function () {
            CloseModal.click();
            $(AdmibUserSuuccess).fadeIn();
            $(AdmibUserSuuccess).fadeOut(3000);

            var delayInMilliseconds = 2000;

            setTimeout(function () {
                window.location.reload();
            }, delayInMilliseconds);
            $('#AdminUserModal').on('hidden', function () {
                $.clearInput();
            });
        }),
    
            $('#AdminUserModal').on('hidden', function () {
        $.clearInput();
    });
}