var dataLang =
    [
        { key: "Error_name", valueVi: "Bạn chưa nhập họ tên!", valueEn: "You have not entered your name!" },
        { key: "Error_phone", valueVi: "Bạn chưa nhập số điện thoại!", valueEn: "You did not enter a phone number!" },
        { key: "Error_email", valueVi: "Bạn chưa nhập email!", valueEn: "You have not entered an email!" },
        { key: "Error_mess", valueVi: "Bạn chưa nhập tin nhắn!", valueEn: "You have not entered a message!" },
        { key: "Error_email_validate", valueVi: "Email sai định dạng!", valueEn: "Email wrong format!" },
        { key: "rs_ok", valueVi: "Gửi liên hệ thành công, xin cảm ơn!", valueEn: "You sent the contact successfully, thank you!" },
        { key: "rs_error", valueVi: "Gửi liên hệ thất bại, vui lòng thử lại!", valueEn: "Send contact failed, please try again!" },
        { key: "Error_numberPro", valueVi: "Số lương  mua phải là số!", valueEn: "Purchase quantity must be a number!" },
        { key: "rs_Comment_ok", valueVi: "Gửi bình luận thành công, xin cảm ơn!", valueEn: "You sent the comment successfully, thank you!" },
        { key: "rs_Comment_error", valueVi: "Gửi  bình luận thất bại, vui lòng thử lại!", valueEn: "Send comment failed, please try again!" },
        { key: "rs_Cart_ok", valueVi: "Gửi đơn hàng thành công, xin cảm ơn!", valueEn: "You sent the order successfully, thank you!" },
        { key: "rs_Cart_error", valueVi: "Gửi đơn hàng thất bại, vui lòng thử lại!", valueEn: "Send order failed, please try again!" },
        { key: "delete_Cart_ok", valueVi: "Xóa sản phẩm giỏ hàng thành công!", valueEn: "Delete the cart product successfully!" },
        { key: "delete_Cart_error", valueVi: "Xóa sản phẩm lỗi, vui lòng thử lại!", valueEn: "Delete the error, please try again!" },
        { key: "update_Cart_ok", valueVi: "Cập nhật số lượng sản phẩm thành công!", valueEn: "Update the number of successful products!" },
        { key: "update_Cart_error", valueVi: "Cập nhật số lượng sản phẩm thất bại, vui lòng thử lại!", valueEn: "Update the number of products failed, please try again!" },
        { key: "Error_search", valueVi: "Mời bạn nhập thông tin tìm kiếm!", valueEn: "Please enter your search information!" },
    ];
function OpenWaiting() {
    $('#loader_id').addClass("loader");
    document.getElementById("overlay").style.display = "block";
}
function CloseWaiting() {
    $('#loader_id').removeClass("loader");
    document.getElementById("overlay").style.display = "none";
}
function GetNotifyByKey(key) {
    var langUse = $('#langUse').val();
    var index = dataLang.findIndex(x => x.key === key);
    if (langUse === 'en') {
        return dataLang[index].valueEn;
    } else {
        return dataLang[index].valueVi;
    }
}
function SendContact() {
    var ctName = $('#ctName').val();
    var ctEmail = $('#ctEmail').val();
    var ctPhone = $('#ctPhone').val();
    var ctAddress = $('#ctAddress').val();
    var ctNote = $('#ctNote').val();
    if (ctName == '') {
        toastr.error(GetNotifyByKey('Error_name')); return;
    } else if (ctPhone == '') {
        toastr.error(GetNotifyByKey('Error_phone')); return;
    }
    else if (ctEmail != '' && !is_Email(ctEmail)) {
        toastr.error(GetNotifyByKey('Error_email_validate')); return;
    } else if (ctNote == '') {
        toastr.error(GetNotifyByKey('Error_mess')); return;
    }
    else {
        var model = {
            fullName: ctName,
            email: ctEmail,
            phone: ctPhone,
            contents: ctNote,
            address: ctAddress,
            referencesId: $('#referencesId').val()
        };
        OpenWaiting();
        $.ajax({
            url: "/HomeSite/AddContact",
            type: "POST",
            data: model,
            success: function (data) {
                if (data.ok === true) {
                    toastr.success(GetNotifyByKey('rs_ok'), { timeOut: 5000 });
                    ResetContact();
                } else {
                    toastr.error(GetNotifyByKey('rs_error'), { timeOut: 5000 });
                }
                CloseWaiting();
            },
            error: function (response) {
                CloseWaiting();
                toastr.error(GetNotifyByKey('rs_error'), { timeOut: 5000 });
            }
        });
    }
}
function SendComment() {
    var commentName = $('#commentName').val();
    var commentEmail = $('#commentEmail').val();
    var commentPhone = $('#commentPhone').val();
    var commentNote = $('#commentNote').val();
    if (commentName == '') {
        toastr.error(GetNotifyByKey('Error_name')); return;
    } else if (commentEmail == '') {
        toastr.error(GetNotifyByKey('Error_email')); return;
    } else if (!is_Email(commentEmail)) {
        toastr.error(GetNotifyByKey('Error_email_validate')); return;
    } else if (commentNote == '') {
        toastr.error(GetNotifyByKey('Error_mess')); return;
    }
    else {
        var model = {
            address: $('#typeComment').val(),
            proId: $('#idObject').val(),
            fullName: commentName,
            email: commentEmail,
            phone: commentPhone,
            contents: commentNote,
        };
        OpenWaiting();
        $.ajax({
            url: "/HomeSite/AddComment",
            type: "POST",
            data: model,
            success: function (data) {
                if (data.ok === true) {
                    toastr.success(GetNotifyByKey('rs_Comment_ok'), { timeOut: 5000 });
                    ResetCcomment();
                } else {
                    toastr.error(GetNotifyByKey('rs_Comment_error'), { timeOut: 5000 });
                }
                CloseWaiting();
            },
            error: function (response) {
                CloseWaiting();
                toastr.error(GetNotifyByKey('rs_Comment_error'), { timeOut: 5000 });
            }
        });
    }
}
function ResetContact() {
    $('#ctName').val('');
    $('#ctEmail').val('');
    $('#ctPhone').val('');
    $('#ctAddress').val('');
    $('#ctNote').val('');
}

//jQuery(function ($) {});
function GetNotifyByKey2(key) {
    var langUse = $('#langUse').val();
    var index = dataLang.findIndex(x => x.key === key);
    if (langUse === 'en') {
        return dataLang[index].valueEn;
    } else {
        return dataLang[index].valueVi;
    }
}

$("#homeSearch1").keydown(function (event) {
    if (event.keyCode === 13) {
        var homeSearch1 = $("#homeSearch1").val();
        if (homeSearch1 == '') {
            return false;
        } else {
            seachHome(homeSearch1);
        }
    }
});
$('#btnHomeSearch1').on('click', function () {
    var homeSearch1 = $("#homeSearch1").val();
    if (homeSearch1 == '') {
        return false;
    } else {
        seachHome(homeSearch1);
    }
});

function seachHome(value) {
    window.location = '/tim-kiem/' + value;
}
$('#btnLangVN').on('click', function () {
    ChangLangSite('vi');
});
$('#btnLangEN').on('click', function () {
    ChangLangSite('en');
});
function ChangLangSite(lang) {
    var url = "/Config/ChangeLangSite";
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
}
function PushProduct() {
    var numberPro = $('#qty').val();
    var idPro = $('#idPro').val();
    if (isNaN(numberPro)) {
        toastr.error(GetNotifyByKey('Error_numberPro')); return;
    } else {
        window.location = '/Carts/Addcart/' + idPro + '?number=' + numberPro;
    }
}

function AddCart() {
    var cartName = $('#cartName').val();
    var cartEmail = $('#cartEmail').val();
    var cartPhone = $('#cartPhone').val();
    var cartNote = $('#cartNote').val();
    var cartAddress = $('#cartAddress').val();

    if (cartName == '') {
        toastr.error(GetNotifyByKey('Error_name')); return;
    } else if (cartEmail == '') {
        toastr.error(GetNotifyByKey('Error_email')); return;
    } else if (!is_Email(cartEmail)) {
        toastr.error(GetNotifyByKey('Error_email_validate')); return;
    }
    else {
        var model = {
            fullName: cartName,
            email: cartEmail,
            phone: cartPhone,
            noteSite: cartNote,
            Address: cartAddress
        };
        OpenWaiting();
        $.ajax({
            url: "/Carts/Dathang",
            type: "POST",
            data: model,
            success: function (data) {
                if (data.ok === 1) {
                    sessionStorage.setItem("okCart", GetNotifyByKey('rs_Cart_ok'));
                    window.location = '/';
                } else if (data.ok === 0) {
                    toastr.error(GetNotifyByKey('rs_Cart_error'), { timeOut: 5000 });
                } else {
                    window.location = '/danh-muc/';
                }
                CloseWaiting();
            },
            error: function (response) {
                CloseWaiting();
                toastr.error(GetNotifyByKey('rs_Cart_error'), { timeOut: 5000 });
            }
        });
    }
}
