
var model
 =
{
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSlides.Name = $('#nameSearch').val();
}

function SearchSlides() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Admins/GetList", modelSlides, function (result) {
        modelSlides.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSlides();
        return false;
    }
});

function ChangeSize() {
    modelSlides.PageNumber = 1;
    modelSlides.PageSize = $('#pageSize').val();
    SearchSlides();
}
function phantrang(PageNumber) {
    modelSlides.PageNumber = PageNumber;
    SearchSlides();
}

function RefreshSlides() {
    $('#nameSearch').val('');

    modelSlides.PageSize = 15;
    modelSlides.PageNumber = 1;
    SearchSlides();
}

function DeleteConfim(id) {
    $('#valueDelete').val(id);
    $('#labelDelete').html('Bạn có chắc chắn muốn xóa tài khoản này không?');
    $('#modamDelete').modal({
        show: 'true'
    });
}

function Delete() {
    OpenWaiting();
    var id = $('#valueDelete').val();
    $.ajax({
        url: "/Admins/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa tài khoản thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchSlides();
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

function ChangeStatus(id) {
    OpenWaiting();
    $.ajax({
        url: "/Admins/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái tài khoản thành công', { timeOut: 5000 });
                SearchSlides();
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
function CreateSlide() {
    OpenWaiting();
    $.post("/Admins/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSlide').html('Thêm mới tài khoản');
        $('#modamSlide').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSlide(id) {
    OpenWaiting();
    $.post("/Admins/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleSlide').html('Cập nhật tài khoản');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Admins/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#fullName').val(data.fullName);
            $('#addresss').val(data.addresss);
            $('#phone').val(data.phone);
            $('#email').val(data.email);
            if (data.active == true) {
                document.getElementById('active').checked = "checked";
            }
            $('#modamSlide').modal({
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
    var getActive = document.getElementById('active');
    var model =
    {
        id: $('#id').val(),
        fullName: $('#fullName').val(),
        addresss: $('#addresss').val(),
        phone: $('#phone').val(),
        email: $('#email').val(),
        active: false
    };
    if (getActive.checked) {
        model.active = true;
    } else {
        model.active = false;
    }
    //validate
    if (model.fullName == '') {
        toastr.error('Nhập tên admin!'); return false;
    } else if (model.email == '') {
        toastr.error('Nhập email!'); return false;
    } else if (!is_Email(model.email)) {
        toastr.error('Email sai định dạng!'); return false;
    } else if (model.phone == '') {
        toastr.error('Nhập điện thoại!'); return false;
    }

    var url = '';
    if (model.id.length > 0) {
        url = "/Admins/Update";
    } else {
        url = "/Admins/Add";
    }
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật tài khoản thành công');
            } else {
                toastr.success('Thêm mới tài khoản thành công');
            }
            SearchSlides();
            $('#modamSlide').modal("hide");
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchSlides();