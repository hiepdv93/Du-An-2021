
var modelAdvs =
{
    Type: '-1',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelAdvs.Name = $('#nameSearch').val();
    modelAdvs.Type = $('#typeSearch').val();
}

function SearchAdvs() {
    GetModelSearch();
    OpenWaiting();
    $.post("/HinhAnh/GetList", modelAdvs, function (result) {
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
    $('#typeSearch').val('-1');

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
        url: "/HinhAnh/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa hình ảnh thành công', { timeOut: 5000 });
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
        url: "/HinhAnh/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hình ảnh thành công', { timeOut: 5000 });
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
    $.post("/HinhAnh/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleAdv').html('Thêm mới hình ảnh');
        $('#modamAdv').modal({
            show: 'true'
        });
        CloseWaiting();

    });
}
function UpdateAdv(id) {
    OpenWaiting();
    $.post("/HinhAnh/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idAdv").val(id);
        $('.titleAdv').html('Cập nhật hình ảnh');
        GetbyId(id);

    });
}
function GetbyId(id) {
    OpenWaiting();
    $.ajax({
        url: "/HinhAnh/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#advName').val(data.advName);
            $('#advLink').val(data.advLink);
            $('#advType').val(data.advType);
            $('#advNote').val(data.advNote);
            $('#advOrder').val(data.advOrder);
            $('#advImage').val(data.advImage);
            if (data.advActive == true) {
                document.getElementById('advActive').checked = true;
            } else {
                document.getElementById('advActive').checked = false;
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
    var getActive = document.getElementById('advActive');
    var model =
    {
        id: $('#idAdv').val(),
        advName: $('#advName').val(),
        advLink: $('#advLink').val(),
        advType: $('#advType').val(),
        advNote: $('#advNote').val(),
        advOrder: $('#advOrder').val(),
        advImage: $('#advImage').val(),
    };
    if (getActive.checked) {
        model.advActive = true;
    } else {
        model.advActive = false;
    }
    //validate
    if (model.advName == '') {
        toastr.error('Nhập tên hình ảnh!'); return false;
    }
    if (model.advOrder == '') {
        toastr.error('Nhập thứ tự!'); return false;
    }
    if (model.advType == '') {
        toastr.error('Chọn loại hình ảnh!'); return false;
    }

    var url = '';
    if (model.id.length > 0) {
        url = "/HinhAnh/Update";
    } else {
        url = "/HinhAnh/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật hình ảnh thành công');
            } else {
                toastr.success('Thêm mới hình ảnh thành công');
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