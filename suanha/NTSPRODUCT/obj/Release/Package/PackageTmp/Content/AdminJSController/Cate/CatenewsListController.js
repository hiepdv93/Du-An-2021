
var modelCatenews =
{
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelCatenews.Name = $('#nameSearch').val();
}

function SearchCatenews() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Categorys/GetListCateNews", modelCatenews, function (result) {
        modelCatenews.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchCatenews();
        return false;
    }
});

function ChangeSize() {
    modelCatenews.PageNumber = 1;
    modelCatenews.PageSize = $('#pageSize').val();
    SearchCatenews();
}
function phantrang(PageNumber) {
    modelCatenews.PageNumber = PageNumber;
    SearchCatenews();
}

function RefreshCatenews() {
    $('#nameSearch').val('');

    modelCatenews.PageSize = 10;
    modelCatenews.PageNumber = 1;
    SearchCatenews();
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
        url: "/Categorys/Deletenew?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa nhóm tin tức thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchCatenews();
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
        url: "/Categorys/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchCatenews();
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
SearchCatenews();