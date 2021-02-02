
var modelEmails =
{
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelEmails.Name = $('#nameSearch').val();
}

function SearchEmails() {
    var keyMess = sessionStorage.getItem("keyMess");
    if (keyMess !== null && keyMess !== '') {
        toastr.success(keyMess, { timeOut: 5000 });
        sessionStorage.removeItem("keyMess");
    }
    GetModelSearch();
    OpenWaiting();
    $.post("/Email/GetList", modelEmails, function (result) {
        modelEmails.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchEmails();
        return false;
    }
});

function ChangeSize() {
    modelEmails.PageNumber = 1;
    modelEmails.PageSize = $('#pageSize').val();
    SearchEmails();
}
function phantrang(PageNumber) {
    modelEmails.PageNumber = PageNumber;
    SearchEmails();
}

function RefreshEmails() {
    $('#nameSearch').val('');

    modelEmails.PageSize = 10;
    modelEmails.PageNumber = 1;
    SearchEmails();
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
    $.ajax({
        url: "/Email/Delete?id="+id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa bài tin thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchEmails();
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
SearchEmails();