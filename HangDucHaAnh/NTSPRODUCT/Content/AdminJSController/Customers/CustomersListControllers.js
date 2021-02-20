
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

function ChangeStatus(id) {
    $.ajax({
        url: "/Customers/ChangeStatus?id=" + id,
        type: "POST",
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
//function CreateCustomers() {
//    $.post("/Customers/UpdateOrCreate", function (result) {
//        $("#divModam").html(result);
//        $('.titleCustomers').html('Thêm mới khách hàng');
//        $('#modamCustomers').modal({
//            show: 'true'
//        });


//    });
//}
//function UpdateCustomers(id) {
//    $.post("/Customers/UpdateOrCreate", function (result) {
//        $("#divModam").html(result);
//        $("#id").val(id);
//        $('.titleCustomers').html('Cập nhật khách hàng');
//        GetbyId(id);

//    });
//}
function GetbyId(id) {
    $.ajax({
        url: "/Customers/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#fullName').val(data.fullName);
            $('#phone').val(data.phone);
            $('#email').val(data.email);
            $('#pass').val(data.pass);
            $('#addresss').val(data.addresss);
            $('#type').val(data.type);
            if (data.active == true) {
                document.getElementById('active').checked = "checked";
            }
            $('#modamCustomers').modal({
                show: 'true'
            });
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//lay du lieu them moi, sua
//function CreateOrUpdate() {
//    var getActive = document.getElementById('active');
//    var model =
//    {
//        id: $('#id').val(),
//        fullName: $('#fullName').val(),
//        phone: $('#phone').val(),
//        email: $('#email').val(),
//        pass: $('#pass').val(),
//        addresss: $('#addresss').val(),
//        type: $('#type').val(),
//    };
//    if (getActive.checked) {
//        model.active = true;
//    } else {
//        model.active = false;
//    }
//    //validate
//    if (model.name == '') {
//        toastr.error('Nhập tên khách hàng!'); return false;
//    }

//    var url = '';
//    if (model.id.length > 0) {
//        url = "/Customers/Update";
//    } else {
//        url = "/Customers/Add";
//    }
//    $.ajax({
//        url: url,
//        type: "POST",
//        data: model,
//        success: function (data) {
//            if (model.id.length > 0) {
//                toastr.success('Cập nhật thành công');
//            } else {
//                toastr.success('Thêm mới thành công');
//            }
//            SearchCustomers();
//            $('#modamCustomers').modal("hide");
//        },
//        error: function (response) {
//            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
//        },
//    });
//}

//tự chạy hàm tìm kiếm
SearchCustomers();