
var modelTeacher =
{
    Title: '',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelTeacher.Name = $('#nameSearch').val();
    modelTeacher.Title = $('#nameTitle').val();
}

function SearchTeacher() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Teacher/GetList", modelTeacher, function (result) {
        modelTeacher.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchTeacher();
        return false;
    }
});
$("#nameTitle").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchTeacher();
        return false;
    }
});

function ChangeSize() {
    modelTeacher.PageNumber = 1;
    modelTeacher.PageSize = $('#pageSize').val();
    SearchTeacher();
}
function phantrang(PageNumber) {
    modelTeacher.PageNumber = PageNumber;
    SearchTeacher();
}

function RefreshTeacher() {
    $('#nameSearch').val('');
    $('#nameTitle').val('');

    modelTeacher.PageSize = 10;
    modelTeacher.PageNumber = 1;
    SearchTeacher();
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
        url: "/Teacher/Delete?id="+id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa giảng viên thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchTeacher();
            } else {
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
        url: "/Teacher/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị giảng viên thành công', { timeOut: 5000 });
                CloseWaiting();
                SearchTeacher();
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
SearchTeacher();