
var modelAdvs =
{
    Type: '-1',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelAdvs.Name = $('#nameSearch').val();
}

function SearchAdvs() {
    GetModelSearch();
    OpenWaiting();
    $.post("/FieldActivity/GetList", modelAdvs, function (result) {
        modelAdvs.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchAdvs();
        return false;
    }
});

function ChangeSize() {
    modelAdvs.PageNumber = 1;
    modelAdvs.PageSize = $('#pageSize').val();
    SearchAdvs();
}
function phantrang(PageNumber) {
    modelAdvs.PageNumber = PageNumber;
    SearchAdvs();
}

function RefreshAdvs() {
    $('#nameSearch').val('');

    modelAdvs.PageSize = 15;
    modelAdvs.PageNumber = 1;
    SearchAdvs();
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
    OpenWaiting();
    $.ajax({
        url: "/FieldActivity/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa lĩnh vực ứng dụng thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchAdvs();
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
        url: "/FieldActivity/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái lĩnh vực ứng dụng thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchAdvs();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//goi modam them moi, sua
function CreateAdv() {
    OpenWaiting();
    $.post("/FieldActivity/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleAdv').html('Thêm mới lĩnh vực ứng dụng');
        $('#modamAdv').modal({
            show: 'true'
        });
        CloseWaiting();

    });
}
function UpdateAdv(id) {
    OpenWaiting();
    $.post("/FieldActivity/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idAdv").val(id);
        $('.titleAdv').html('Cập nhật lĩnh vực ứng dụng');
        GetbyId(id);

    });
}
function GetbyId(id) {
    OpenWaiting();
    $.ajax({
        url: "/FieldActivity/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#name').val(data.name);
            $('#contents').val(data.contents);
            $('#numberOder').val(data.numberOder);
            $('#image').val(data.image);
            if (data.active == true) {
                document.getElementById('active').checked = true;
            } else {
                document.getElementById('active').checked = false;
            }
            $('#modamAdv').modal({
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
        id: $('#idAdv').val(),
        name: $('#name').val(),
        contents: $('#contents').val(),
        numberOder: $('#numberOder').val(),
        image: $('#image').val(),
    };
    if (getActive.checked) {
        model.active = true;
    } else {
        model.active = false;
    }
    //validate
    if (model.name == '') {
        toastr.error('Nhập tên lĩnh vực ứng dụng!'); return false;
    }
    if (model.numberOder == '') {
        toastr.error('Nhập thứ tự!'); return false;
    }

    var url = '';
    if (model.id.length > 0) {
        url = "/FieldActivity/Update";
    } else {
        url = "/FieldActivity/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật lĩnh vực ứng dụng thành công');
            } else {
                toastr.success('Thêm mới lĩnh vực ứng dụng thành công');
            }
            CloseWaiting();
            SearchAdvs();
            $('#modamAdv').modal("hide");
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchAdvs();