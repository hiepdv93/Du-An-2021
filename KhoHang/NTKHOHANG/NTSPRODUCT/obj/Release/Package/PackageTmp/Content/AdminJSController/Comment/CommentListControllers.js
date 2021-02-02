
var modelContacts =
{
    Type: '1',
    ProName: '',
    Name: '',
    Email: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelContacts.Name = $('#nameSearch').val();
    modelContacts.Email = $('#emailSearch').val();
    modelContacts.ProName = $('#ProName').val();
    modelContacts.Type = $('#typeSearch').val();
}

function SearchCustomers() {

    GetModelSearch();
    OpenWaiting();
    $.post("/Comments/GetList", modelContacts, function (result) {
        modelContacts.PageNumber = 1;
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
$("#emailSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCustomers();
        return false;
    }
});
$("#ProName").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCustomers();
        return false;
    }
});
function ChangeSize() {
    modelContacts.PageNumber = 1;
    modelContacts.PageSize = $('#pageSize').val();
    SearchCustomers();
}
function phantrang(PageNumber) {
    modelContacts.PageNumber = PageNumber;
    SearchCustomers();
}

function RefreshContacts() {
    $('#nameSearch').val('');
    $('#emailSearch').val('');
    $('#ProName').val('');
    $('#typeSearch').val('1');
    modelContacts.PageSize = 15;
    modelContacts.PageNumber = 1;
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
    OpenWaiting();
    var id = $('#valueDelete').val();
    $.ajax({
        url: "/Comments/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa bình luận sản phẩm thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchCustomers();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
            CloseWaiting();
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ShowDetail(id) {
    OpenWaiting();
    $.ajax({
        url: "/Comments/Getbyid?id=" + id,
        type: "POST",
        success: function (data) {
            $('#namev').html(data.fullName);
            $('#phonev').html(data.phone);
            $('#emailv').html(data.email);
            $('#notev').html(data.contents);
            $('#modamDelete').modal('hide');
            $('#modamDetail').modal({
                show: 'true'
            });
            CloseWaiting();

        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//goi modam them moi, sua
function UpdateSayWe(id) {
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
            $('#email').val(data.email);
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


//tự chạy hàm tìm kiếm
SearchCustomers();