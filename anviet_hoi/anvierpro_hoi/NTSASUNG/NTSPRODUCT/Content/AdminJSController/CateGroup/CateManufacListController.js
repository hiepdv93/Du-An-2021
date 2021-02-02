
var modelCatepro =
{
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelCatepro.Name = $('#nameSearch').val();
}

function SearchCatepro() {
    GetModelSearch();
    OpenWaiting();
    $.post("/CateGroup/GetListCateManufac", modelCatepro, function (result) {
        modelCatepro.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCatepro();
        return false;
    }
});

function ChangeSize() {
    modelCatepro.PageNumber = 1;
    modelCatepro.PageSize = $('#pageSize').val();
    SearchCatepro();
}
function phantrang(PageNumber) {
    modelCatepro.PageNumber = PageNumber;
    SearchCatepro();
}

function RefreshCatepro() {
    $('#nameSearch').val('');

    modelCatepro.PageSize = 10;
    modelCatepro.PageNumber = 1;
    SearchCatepro();
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
        url: "/CateGroup/DeleteManufac?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa giải pháp thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchCatepro();
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
function ChangeStatus(id) {
    OpenWaiting();
    $.ajax({
        url: "/CateGroup/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchCatepro();
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

SearchCatepro();