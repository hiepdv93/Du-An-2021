
var modelSupport =
{
    Nick: '',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSupport.Name = $('#nameSearch').val();
    modelSupport.Nick = $('#nick').val();
}

function SearchSupport() {
    var keyMess = sessionStorage.getItem("keyMess");
    if (keyMess !== null && keyMess !== '') {
        toastr.success(keyMess, { timeOut: 5000 });
        sessionStorage.removeItem("keyMess");
    }
    GetModelSearch();
    OpenWaiting();
    $.post("/Support/GetList", modelSupport, function (result) {
        modelSupport.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSupport();
        return false;
    }
});
$("#nick").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSupport();
        return false;
    }
});
function ChangeSize() {
    modelSupport.PageNumber = 1;
    modelSupport.PageSize = $('#pageSize').val();
    SearchSupport();
}
function phantrang(PageNumber) {
    modelSupport.PageNumber = PageNumber;
    SearchSupport();
}

function RefreshNews() {
    $('#nameSearch').val('');
    $('#nick').val('');

    modelSupport.PageSize = 15;
    modelSupport.PageNumber = 1;
    SearchSupport();
}

function DeleteConfim(id) {
    $('#valueDelete').val(id);
    $('#labelDelete').html('Bạn có chắc chắn muốn xóa bản ghi này không?');
    $('#modamDelete').modal({
        show: 'true'
    });
}
//cap nhat nhanh trang thai
function Delete() {
    var id = $('#valueDelete').val();
    OpenWaiting();
    $.ajax({
        url: "/Support/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa hỗ trợ online thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchSupport();
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

//goi modam them moi, sua
function CreateSuport() {
    OpenWaiting();
    $.post("/Support/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSuport').html('Thêm mới hỗ trợ online');
        $('#modamSuport').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSuport(id) {
    OpenWaiting();
    $.post("/Support/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idSuport").val(id);
        $('.titleSuport').html('Cập nhật hỗ trợ online');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Support/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {

            $('#fullName').val(data.fullName);
            $('#insnick').val(data.nick);
            $('#phone').val(data.phone);
            $('#addresss').val(data.addresss);
            $('#sType').val(data.sType);
            $('#email').val(data.email);
            $('#numberOder').val(data.numberOder);
            $('#modamSuport').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//lay du lieu them moi, sua

function CreateOrUpdate() {
    var model =
    {
        id: $('#idSuport').val(),
        Nick: $('#insnick').val(),
        fullName: $('#fullName').val(),
        phone: $('#phone').val(),
        addresss: $('#addresss').val(),
        sType: $('#sType').val(),
        email: $('#email').val(),
        numberOder: $('#numberOder').val()
    };
    //validate
    if (model.fullName == '') {
        toastr.error('Nhập họ tên!'); return false;
    }
    if (model.Nick == '') {
        toastr.error('Nhập tên nick!'); return false;
    }
    if (model.email == '') {
        toastr.error('Nhập email!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/Support/Update";
    } else {
        url = "/Support/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật hỗ trợ online thành công');
            } else {
                toastr.success('Thêm mới hỗ trợ online thành công');
            }
            $('#modamSuport').modal("hide");
            CloseWaiting();
            SearchSupport();
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//tự chạy hàm tìm kiếm
SearchSupport();