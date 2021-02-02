
var modelNews =
{
    Name: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelNews.Name = $('#nameSearch').val();
}

function SearchNews() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Department/GetList", modelNews, function (result) {
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
        url: "/Department/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa trung tâm đào tạo thành công', { timeOut: 5000 });
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
SearchNews();