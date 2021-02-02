
var modelContactLocations =
{
    cateName: '',
    Phone: '',
    Email: '',
    Employee: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelContactLocations.cateName = $('#cateSearch').val();
    modelContactLocations.Phone = $('#phoneSearch').val();
    modelContactLocations.Email = $('#emailSearch').val();
    modelContactLocations.Employee = $('#employeelSearch').val();
}

function SearchContactLocations() {
    GetModelSearch();
    debugger
    OpenWaiting();
    $.post("/ContactLocation/GetList", modelContactLocations, function (result) {
        modelContactLocations.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#cateSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContactLocations();
        return false;
    }
});
$("#phoneSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContactLocations();
        return false;
    }
});
$("#emailSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContactLocations();
        return false;
    }
});
$("#employeelSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContactLocations();
        return false;
    }
});

function ChangeSize() {
    modelContactLocations.PageNumber = 1;
    modelContactLocations.PageSize = $('#pageSize').val();
    SearchContactLocations();
}
function phantrang(PageNumber) {
    modelContactLocations.PageNumber = PageNumber;
    SearchContactLocations();
}

function RefreshContactLocations() {
    $('#cateSearch').val('');
    $('#phoneSearch').val('');
    $('#emailSearch').val('');
    $('#employeelSearch').val('');

    modelContactLocations.PageSize = 15;
    modelContactLocations.PageNumber = 1;
    SearchContactLocations();
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
        url: "/ContactLocation/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa liên hệ thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchContactLocations();
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

function ChangeStatus(id) {
    OpenWaiting();
    $.ajax({
        url: "/ContactLocation/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái liên hệ thành công', { timeOut: 5000 });
                SearchContactLocations();
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
function CreateContactLocation() {
    OpenWaiting();
    $.post("/ContactLocation/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleContactLocation').html('Thêm mới chi nhánh liên hệ');
        $('#modamContactLocation').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateContactLocation(id) {
    OpenWaiting();
    $.post("/ContactLocation/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleContactLocation').html('Cập nhật chi nhánh liên hệ');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/ContactLocation/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#cateName').val(data.cateName);
            $('#location').val(data.location);
            $('#employeeName').val(data.employeeName);
            $('#phone').val(data.phone);
            $('#email').val(data.email);
            $('#number').val(data.number);
            //if (data.active == true) {
            //    document.getElementById('active').checked = true;
            //} else {
            //    document.getElementById('active').checked = false;
            //} 
            $('#modamContactLocation').modal({
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
    //var getActive = document.getElementById('active');
    var model =
    {
        id: $('#id').val(),
        cateName: $('#cateName').val(),
        location: $('#location').val(),
        employeeName: $('#employeeName').val(),
        phone: $('#phone').val(),
        email: $('#email').val(),
        number: $('#number').val()
    };
    //if (getActive.checked) {
    //    model.active = true;
    //} else {
    //    model.active = false;
    //}
    //validate
    if (model.cateName == '') {
        toastr.error('Nhập tên ngành nghề!'); return false;
    }
    if (model.location == '') {
        toastr.error('Nhập tên đơn vị!'); return false;
    }
    if (model.employeeName == '') {
        toastr.error('Nhập tên nhân viên!'); return false;
    }
    if (model.email == '') {
        toastr.error('Nhập tên email!'); return false;
    }
    if (model.phone != '' && isNaN(model.phone)) {
        toastr.error('Số điện thoại phải là số!'); return false;
    }
    if (isNaN(model.number)) {
        toastr.error('Thứ tự hiển thị phải là số!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/ContactLocation/Update";
    } else {
        url = "/ContactLocation/Add";
    }
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật liên hệ thành công');
            } else {
                toastr.success('Thêm mới liên hệ thành công');
            }
            SearchContactLocations();
            $('#modamContactLocation').modal("hide");
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchContactLocations();