
var modelSayWes =
{
    Name: '',
    Email: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSayWes.Name = $('#nameSearch').val();
    modelSayWes.Email = $('#emailSearch').val();
}

function SearchSayWes() {
    GetModelSearch();
    OpenWaiting();
    $.post("/SayWe/GetList", modelSayWes, function (result) {
        modelSayWes.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSayWes();
        return false;
    }
});
$("#emailSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSayWes();
        return false;
    }
});
function ChangeSize() {
    modelSayWes.PageNumber = 1;
    modelSayWes.PageSize = $('#pageSize').val();
    SearchSayWes();
}
function phantrang(PageNumber) {
    modelSayWes.PageNumber = PageNumber;
    SearchSayWes();
}

function RefreshSayWes() {
    $('#nameSearch').val('');
    $('#emailSearch').val('');

    modelSayWes.PageSize = 15;
    modelSayWes.PageNumber = 1;
    SearchSayWes();
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
        url: "/SayWe/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa ý kiến thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchSayWes();
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
    $.ajax({
        url: "/SayWe/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái ý kiến thành công', { timeOut: 5000 });
                SearchSayWes();
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
function CreateSayWe() {
    OpenWaiting();
    $.post("/SayWe/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSayWe').html('Thêm mới ý kiến');
        $('#modamSayWe').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSayWe(id) {
    OpenWaiting();
    $.post("/SayWe/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('.titleSayWe').html('Cập nhật ý kiến');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/SayWe/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#sType').val(data.type);
            $('#fullName').val(data.fullName);
            $('#email').val(data.email);
            $('#phone').val(data.phone);
            $('#addresss').val(data.addresss);
            $('#job').val(data.job);
            $('#avata').val(data.avata);
            $('#contents').val(data.contents);
            $('#numberOder').val(data.numberOder);
            if (data.active == true) {
                document.getElementById('active').checked = "checked";
            }
            $('#modamSayWe').modal({
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
    var getActive = document.getElementById('active');
    var model =
    {
        id: $('#idUpdate').val(),
        fullName: $('#fullName').val(),
        type: $('#sType').val(),
        email: $('#email').val(),
        phone: $('#phone').val(),
        addresss: $('#addresss').val(),
        job: $('#job').val(),
        avata: $('#avata').val(),
        contents: $('#contents').val(),
        numberOder: $('#numberOder').val()
    };
    if (getActive.checked) {
        model.active = true;
    } else {
        model.active = false;
    }
    //validate
    if (model.fullName == '') {
        toastr.error('Nhập họ tên!'); return false;
    }
    if (model.phone != '' && isNaN(model.phone)) {
        toastr.error('Số điện thoại phải là số!'); return false;
    }
    if (isNaN(model.numberOder)) {
        toastr.error('Thứ tự hiển thị phải là số!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/SayWe/Update";
    } else {
        url = "/SayWe/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.id.length > 0) {
                    toastr.success('Cập nhật ý kiến thành công');
                } else {
                    toastr.success('Thêm mới ý kiến thành công');
                }
                CloseWaiting();
                $('#modamSayWe').modal("hide");
                SearchSayWes();
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
SearchSayWes();