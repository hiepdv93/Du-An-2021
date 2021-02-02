
var modelContacts =
{
    Name: '',
    Email: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelContacts.Name = $('#nameSearch').val();
    modelContacts.Email = $('#emailSearch').val();
}

function SearchContacts() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Contacts/GetList", modelContacts, function (result) {
        modelContacts.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContacts();
        return false;
    }
});
$("#emailSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchContacts();
        return false;
    }
});

function ChangeSize() {
    modelContacts.PageNumber = 1;
    modelContacts.PageSize = $('#pageSize').val();
    SearchContacts();
}
function phantrang(PageNumber) {
    modelContacts.PageNumber = PageNumber;
    SearchContacts();
}

function RefreshContacts() {
    $('#nameSearch').val('');
    $('#emailSearch').val('');

    modelContacts.PageSize = 15;
    modelContacts.PageNumber = 1;
    SearchContacts();
}

function DeleteConfim(id) {
    $('#valueDelete').val(id);
    $('#labelDelete').html('Bạn có chắc chắn muốn xóa bản ghi này không?');
    $('#modamDelete').modal({
        show: 'true'
    });
}

function Delete() {
    OpenWaiting();
    var id = $('#valueDelete').val();
    $.ajax({
        url: "/Contacts/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa liên hệ thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchContacts();
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
function ShowDetail(id) {
    OpenWaiting();
    $.ajax({
        url: "/Contacts/Getbyid?id=" + id,
        type: "POST",
        success: function (data) {
            $('#namev').html(data.fullName);
            $('#phonev').html(data.phone);
            $('#emailv').html(data.email);
            $('#addv').html(data.address);
            $('#notev').html(data.contents);
            $('#modamDelete').modal('hide');
            $('#modamDetail').modal({
                show: 'true'
            });
            CloseWaiting();

        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchContacts();