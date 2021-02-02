var modelSolution =
{
    Name: '',
    Status: '',
    PageSize: 15,
    PageNumber: 1
};
function GetModelSearch() {
    modelSolution.Name = $('#nameSearch').val();
    modelSolution.Status = $('#sttSearch').val();
}
function SearchSolution() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Solution/GetList", modelSolution, function (result) {
        modelSolution.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}
$("#nameSearch").keydown(function (Solution) {
    if (Solution.keyCode === 13) {
        SearchSolution();
        return false;
    }
});
function ChangeSize() {
    modelSolution.PageNumber = 1;
    modelSolution.PageSize = $('#pageSize').val();
    SearchSolution();
}
function phantrang(PageNumber) {
    modelSolution.PageNumber = PageNumber;
    SearchSolution();
}
function RefreshSolution() {
    $('#nameSearch').val('');
    $('#sttSearch').val('');
    modelSolution.PageSize = 10;
    modelSolution.PageNumber = 1;
    SearchSolution();
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
        url: "/Solution/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa giải pháp thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchSolution();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        }
    });
}
function ChangeStatus(id) {
    OpenWaiting();
    $.ajax({
        url: "/Solution/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái giải pháp thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchTeacher();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        }
    });
}
//tự chạy hàm tìm kiếm
SearchSolution();