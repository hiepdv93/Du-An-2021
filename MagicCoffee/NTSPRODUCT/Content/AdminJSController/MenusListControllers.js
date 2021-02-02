
var modelNews =
{
    Type: '-1',
    Position: '-1',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelNews.Name = $('#nameSearch').val();
    modelNews.Type = $('#typeSearch').val();
    modelNews.Position = $('#positionSearch').val();
}

function SearchMenu() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Menus/GetList", modelNews, function (result) {
        modelNews.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchMenu();
        return false;
    }
});

function ChangeSize() {
    modelNews.PageNumber = 1;
    modelNews.PageSize = $('#pageSize').val();
    SearchMenu();
}
function phantrang(PageNumber) {
    modelNews.PageNumber = PageNumber;
    SearchMenu();
}

function RefreshNews() {
    $('#nameSearch').val('');
    $('#positionSearch').val('-1');
    $('#typeSearch').val('-1');

    modelNews.PageSize = 15;
    modelNews.PageNumber = 1;
    SearchMenu();
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
        url: "/Menus/Delete?id="+id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa menu thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchMenu();
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
        url: "/Menus/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái menu thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchMenu();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//tự chạy hàm tìm kiếm
SearchMenu();