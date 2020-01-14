function AdminUserModel(id, fullName, telephone, mobile, userName, password) {
    this.id = id;
    this.fullName = fullName;
    this.telephone = telephone;
    this.mobile = mobile;
    this.userName = userName;
    this.password = password;
}
var _AdminUserModel = AdminUserModel(0, "", "", "", "", "");
var listAdminUser = [];






function LoadAdminUser() {
    MyAjax("Get", "GetAdminUserInfo", { 'description': Description.value }, function (list) {
        listAdminUser = list;

        tableAdminUser(list, divMelk, [{ Data: "ID", Title: "ID" }, { Data: "fullName", Title: "نام و نام خانوادگی" }, { Data: "Telephone", Title: "تلفن" }, { Data: "Mobile", Title: "موبایل" }, { Data: "UserName", Title: "نام کاربری" }, { Data: "Password", Title: "رمزعبور" }], [{ Text: "Removemelk", classname: "danger", Icon: "remove", Event: Remove }, { Text: "ویرایش", classname: "primary", Icon: "edit", Event: edit }, { Text: "نقش های فعلی", classname: "primary", Icon: "edit", Event: informations }, { Text: "جزییات", classname: "primary", Icon: "edit", Event: StatusRegister }, { Text: "تخصیص نقش", classname: "primary", Icon: "edit", Event: null }], { IsRowNumber: true, IsSelected: true });
    });
}

//function SetAdminActivityRoll(id) {
//    $('#OkStatusButtun').click(function () {
//        alert('h')
//    })
//}


function informations(id) {
    popupWindow = window.open("../Admin/ShowMelkInformation/" + id + ""
        , 'popUpWindow', 'height=500,width=1000,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes')
}

function Remove(id) {
    if ($('#OkButtun').click(function () {
        MyAjax("Post", "RemoveAdminUser", { "id": id }, function (Data) {
            location.reload();
        });

    }));


}
function edit(id) {
    $("#btnRegister").click();
    var _find = false;
    for (var i in listAdminUser) {
        if (listAdminUser[i].ID == id) {
            _find = true;
            AdminUserModel.id = listAdminUser[i].ID;
            AdminUserModel.fullName = listAdminUser[i].fullName;
            AdminUserModel.telephone = listAdminUser[i].Telephone;
            AdminUserModel.mobile = listAdminUser[i].Mobile;
            AdminUserModel.userName = listAdminUser[i].UserName;
            AdminUserModel.password = listAdminUser[i].Password;
            break;
        }
    }
    if (_find) {
        ID.value = AdminUserModel.id;
        AdminFullName.value = AdminUserModel.fullName;
        AdminUserTelephone.value = AdminUserModel.telephone;
        AdminUserMobile.value = AdminUserModel.mobile;
        AdminUserName.value = AdminUserModel.userName;
        AdminUserPassword.value = AdminUserModel.password;

    }


}
function StatusRegister(id) {

    MyAjax("Post", "AdminUser", { "id": id }, function (Data) {

    });




    if ($('#OkStatusButtun').click(function () {


        listRoles = [];
        selectedRole = {};
        $(':checkbox:checked').each(function (i) {

            var id = $(this).val();
           
            item = {};
            item["ID"] = id;

            listRoles.push(item);
        });
        selectedRole["listRoles"] = listRoles;
        selectedRole["AdminID"] = id;
     
        $.ajax({
            type: "Post",
            url: "AdminUserRolesRegister",
            dataType: 'json',
            data: selectedRole,
            success: function () {
                alert("عملیات انجام شد!");

            }
        });


        //MyAjax("Post", "AdminUserRolesRegister", { 'selectedRole': selectedRole,'idAdmin':id }, function () {

        //    alert('ok');

        //});





        //var Puser = $("#userInformation").val();

        //var _Done = false;
        //if ($("#melkStatus:checked").val() != null) {

    
        //        }


        //else {
        //    alert("موردی را انتخاب کنید");
        //    location.reload();
        //}

    }));



}
function SearchMelk() {
    LoadMelk();
}