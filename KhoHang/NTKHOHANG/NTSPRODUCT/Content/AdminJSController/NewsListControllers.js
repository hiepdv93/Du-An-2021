
var modelNews =
{
    CateId: '',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelNews.Name = $('#nameSearch').val();
    modelNews.CateId = $('#cateSearch').val();
}

function SearchNews() {
    GetModelSearch();
    OpenWaiting();
    $.post("/News/GetList", modelNews, function (result) {
        modelNews.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchNews();
        return false;
    }
});

function ChangeSize() {
    modelNews.PageNumber = 1;
    modelNews.PageSize = $('#pageSize').val();
    SearchNews();
}
function phantrang(PageNumber) {
    modelNews.PageNumber = PageNumber;
    SearchNews();
}

function RefreshNews() {
    $('#nameSearch').val('');
    $('#cateSearch').val('');

    modelNews.PageSize = 10;
    modelNews.PageNumber = 1;
    SearchNews();
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
        url: "/News/Delete?id="+id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa bài tin thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchNews();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeNewNew(id) {
    OpenWaiting();
    $.ajax({
        url: "/News/ChangeNewNew?id="+id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái tin hiện trang chủ thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchNews();
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
function ChangeNewHot(id) {
    OpenWaiting();
    $.ajax({
        url: "/News/ChangeNewHot?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái tin giới thiệu thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchNews();
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
        url: "/News/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị tin thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchNews();
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
//tự chạy hàm tìm kiếm
SearchNews();