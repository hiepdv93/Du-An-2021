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
function PushProduct() {
    var numberPro = $('#number_pro').val();
    var idPro = $('#idPro').val();
    if (isNaN(numberPro)) {
        toastr.error(GetNotifyByKey('Error_numberPro')); return;
    } else {
        window.location = '/Carts/Addcart/' + idPro + '?number=' + numberPro;
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
    var url = "/HomeSite/CheckPage";
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        success: function (data) {
            if (data.page == 'n') {
                window.location = '/tim-kiem/' + value;
            } else {
                window.location = '/tim-kiem/' + value;
            }
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });

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

        document.getElementById("btnContactUs").style.disabled = true;
        $.ajax({
            url: "/Carts/Dathang",
            type: "POST",
            data: model,
            success: function (data) {
                if (data.ok === 1) {
                    toastr.success('Gửi đơn hàng thành công, xin cám ơn quý khách', { timeOut: 5000 });
                    setTimeout(function () { window.location = '/'; }, 1000);
                } else if (data.ok === 0) {
                    toastr.error('Đã xảy ra lỗi vui lòn thử lại', { timeOut: 5000 });
                } else {
                    setTimeout(function () { window.location = '/'; }, 1000);
                }
                CloseWaiting();
                document.getElementById("btnContactUs").style.disabled = false;
            },
            error: function (response) {
                CloseWaiting();
                document.getElementById("btnContactUs").style.disabled = false;
                toastr.error(GetNotifyByKey('rs_Cart_error'), { timeOut: 5000 });
            }
        });
    }
}

function ActionPush() {
    try {
        $.ajax({
            url: "/HomeSite/PushCount",
            type: "POST",
            success: function (data) {
                if (data.ok === true) {
                    console.log('add ls ok');
                } else {
                    console.log(data.mess);
                }
            },
            error: function (response) {
                console.log('Đã xảy ra lỗi');
            },
        });
    } catch (e) { }
}
function PushCount() {
    setTimeout(function () { ActionPush(); }, 2000);
}

function changeNum() {
    var x = $('#proPrice_sale').val();
    if (x !== undefined && x !== '' && x !== '0') {
        var sl = $('#number_pro').val();
        $('#price_calc').html(formatMoney(x, sl));
    }
}

function formatMoney(price, number) {
    var rs = parseInt(price) * parseInt(number);
    let txtView = rs.toLocaleString('vi', { style: 'currency', currency: 'VND' });
    return txtView.replaceAll('.', ',');
}
function showPushPro(idPro, namePro, price, priceSale, donvi) {
    $('#pro_number_base').val('1');
    $('#pro_id_base').val(idPro);
    $('#pro_name_base').html(namePro);
    $('#pro_price_base').html(formatMoney(price, 1) + '/' + donvi);
    $('#pro_pricesale_base').html(formatMoney(priceSale, 1) + '/' + donvi);

    $('#pro_dongia_base').val(priceSale);
    $('#pro_money_base').html(formatMoney(priceSale, 1));

    $("#modamPro").modal();
}

function changeNumList() {
    var x = $('#pro_dongia_base').val();
    var sl = $('#pro_number_base').val();

    if (x !== undefined && x !== '' && x !== '0') {
        $('#pro_money_base').html(formatMoney(x, sl));
    }
}
function PushProductList() {
    var numberPro = $('#pro_number_base').val();
    var idPro = $('#pro_id_base').val();
    if (isNaN(numberPro)) {
        toastr.error("Nhập số lượng sản phẩm"); return;
    } else {
        var url = '/Carts/Addcarts/' + idPro + '?number=' + numberPro;
        OpenWaiting();
        $.ajax({
            url: url,
            // data: { lang: lang },
            cache: false,
            type: "POST",
            success: function (data) {
                if (data.ok === 1) {
                    $('#total_count_cart').html(data.countcart + '');
                    $('#modamPro').modal('hide');

                    toastr.success('Thêm sản phẩm vào giỏ hàng thành công!', { timeOut: 5000 });
                }
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
        CloseWaiting();
    }
}