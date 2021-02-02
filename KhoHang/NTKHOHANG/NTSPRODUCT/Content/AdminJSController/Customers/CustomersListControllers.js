
var modelCustomers =
{
    Name: '',
    Phone: '',
    Email: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelCustomers.Name = $('#nameSearch').val();
    modelCustomers.Email = $('#email').val();
    modelCustomers.Phone = $('#phone').val();
}

function SearchCustomers() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Customers/GetList", modelCustomers, function (result) {
        modelCustomers.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCustomers();
        return false;
    }
});

$("#email").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCustomers();
        return false;
    }
});
$("#phone").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCustomers();
        return false;
    }
});
function ChangeSize() {
    modelCustomers.PageNumber = 1;
    modelCustomers.PageSize = $('#pageSize').val();
    SearchCustomers();
}
function phantrang(PageNumber) {
    modelCustomers.PageNumber = PageNumber;
    SearchCustomers();
}

function RefreshCustomers() {
    $('#nameSearch').val('');
    $('#email').val('');
    $('#phone').val('');

    modelCustomers.PageSize = 15;
    modelCustomers.PageNumber = 1;
    SearchCustomers();
}

function DeleteConfim(id) {
    $('#valueDelete').val(id);
    $('#labelDelete').html('Bạn có chắc chắn muốn xóa bản ghi này không?');
    $('#modamDelete').modal({
        show: 'true'
    });
}

function Delete() {
    var id = $('#valueDelete').val();
    $.ajax({
        url: "/Customers/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa khách hàng thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchCustomers();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeStatusCode(id, activeCode, activeAcount) {
    var model =
    {
        id: id,
        activeAcount: Boolean(activeAcount),
        activeCode: Boolean(activeCode),
    };
    $.ajax({
        url: "/Customers/ChangeStatus",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái khách hàng thành công', { timeOut: 5000 });
                SearchCustomers();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeStatus(id, activeCode, activeAcount) {
    var model =
    {
        id: id,
        activeAcount: Boolean(activeAcount),
        activeCode: Boolean(activeCode),
    };
    $.ajax({
        url: "/Customers/ChangeStatus",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái khách hàng thành công', { timeOut: 5000 });
                SearchCustomers();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//goi modam them moi, sua
function ShowUpdateCustomers(id) {
    OpenWaiting();
    $.post("/Customers/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('.titleCustomer').html('Cập nhật tài khoản khách hàng');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Customers/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#fullName').val(data.fullName);
            $('#cemail').val(data.email);
            if (data.activeAcount == true) {
                document.getElementById('activeAcount').checked = "checked";
            }
            if (data.activeCode == true) {
                document.getElementById('activeCode').checked = "checked";
            }
            $('#modamCustomer').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//lay du lieu them moi, sua
function CreateOrUpdate() {
    var getActive = document.getElementById('activeAcount');
    var getActiveCode = document.getElementById('activeCode');
    var model =
    {
        id: $('#idUpdate').val(),
    };
    if (getActive.checked) {
        model.activeAcount = true;
    } else {
        model.activeAcount = false;
    }
    //code
    if (getActiveCode.checked) {
        model.activeCode = true;
    } else {
        model.activeCode = false;
    }
    var url = '';
    url = "/Customers/ChangeStatus";

    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.id.length > 0) {
                    toastr.success('Cập nhật tài khoản khách hàng thành công');
                } else {
                    toastr.success('Thêm mới khách hàng thành công');
                }
                CloseWaiting();
                $('#modamCustomer').modal("hide");
                SearchCustomers();
            } else {
                toastr.error(data.mess);
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

function ShowOnResetPass(id) {
    OpenWaiting();
    $.post("/Customers/ShowResetPass", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('#modamResetPass').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function OnResetPass() {
    var model =
    {
        id: $('#idUpdate').val(),
    };
    model.pass = $('#rspass').val();
    var re_rspass = $('#re_rspass').val();
    if (model.pass == '' || re_rspass == '') {
        toastr.error('Nhập đầy đủ mật khẩu'); return;
    } else if (model.pass != re_rspass) {
        toastr.error('Mật khẩu phải trùng nhau'); return;
    }
    $.ajax({
        url: "/Customers/ResetPass",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Reset mật khẩu thành công', { timeOut: 5000 });
                $('#modamResetPass').modal("hide");
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

SearchCustomers();