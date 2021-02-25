function OpenWaiting() {
    $('#loader_id').addClass("loader");
    document.getElementById("overlay").style.display = "block";
}
function CloseWaiting() {
    $('#loader_id').removeClass("loader");
    document.getElementById("overlay").style.display = "none";
}
function changPass() {
    OpenWaiting();
    $.post("/Admins/ChangPass", function (result) {
        $("#div_user").html(result);
        $('.titleUser').html('Đối mật khẩu');
        $('#modamUser').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdatePass() {
    var model =
    {
        Password: $('#pass1').val(),
        PasswordNew: $('#pass2').val(),
        PasswordNew2: $('#pass3').val()
    };
    //validate
    if (model.Password == '') {
        toastr.error('Nhập mật khẩu cũ!'); return false;
    } else if (model.PasswordNew == '') {
        toastr.error('Nhập mật khẩu mới!'); return false;
    } else if (model.PasswordNew2 == '') {
        toastr.error('Nhập lại mật khẩu mới!'); return false;
    }
    else if (model.PasswordNew2 != model.PasswordNew) {
        toastr.error('Mật khẩu mới phải trùng nhau!'); return false;
    }
    OpenWaiting();
    $.ajax({
        url: "/Admins/UpdatePass",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == 1) {
                toastr.success('Cập nhật mật khẩu thành công');
                $('#modamUser').modal('hide');
            } else if (data.ok == 0) {
                toastr.error(data.mess, { timeOut: 5000 });
            } else {
                window.location.href = '/Admins/Login';
            }
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function FogotChange()
{
    var model =
    {
        Password: $('#passwordNew').val(),
        Password2: $('#passwordNew2').val(),
        SecurityKey: $('#SecurityKey').val()
    };
    //validate
    if (model.Password == '') {
        toastr.error('Nhập mật khẩu cũ!'); return false;
    } else if (model.Password2 == '') {
        toastr.error('Nhập lại mật khẩu mới!'); return false;
    }
    else if (model.Password2 != model.Password) {
        toastr.error('Mật khẩu mới phải trùng nhau!'); return false;
    }
    else if (model.SecurityKey =='') {
        toastr.error('Không tìm thấy yêu cầu lấy lại mật khẩu!'); return false;
    }
    OpenWaiting();
    $.ajax({
        url: "/Admins/UpdateFogotChange",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                window.location.href = '/Admins/Login';
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
$('#slNgonngu').on('change', function () {
    var lang = $(this).val();
    var url = "/Config/ChangeLangAdmin";
    $.ajax({
        url: url,
        data: { lang: lang },
        cache: false,
        type: "POST",
        success: function (data) {
            window.location.reload();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
});