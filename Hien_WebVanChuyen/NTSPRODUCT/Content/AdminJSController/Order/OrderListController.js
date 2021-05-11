
var modelOrder =
{
    Name: '',
    Status: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelOrder.Name = $('#nameSearch').val();
    modelOrder.Status = $('#statusSearch').val();
    modelOrder.dateFrom = $('#dateFrom').val();
    modelOrder.DateTo = $('#dateTo').val();
}

function SearchOrder() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Oders/GetListOrder", modelOrder, function (result) {
        modelOrder.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchOrder();
        return false;
    }
});

function ChangeSize() {
    modelOrder.PageNumber = 1;
    modelOrder.PageSize = $('#pageSize').val();
    SearchOrder();
}
function phantrang(PageNumber) {
    modelOrder.PageNumber = PageNumber;
    SearchOrder();
}

function Lammoi() {
    $('#nameSearch').val('');
    $('#statusSearch').val('');
    $('#dateFrom').val('');
    $('#dateTo').val('');
    modelOrder.PageSize = 15;
    modelOrder.PageNumber = 1;
    SearchOrder();
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
        url: "/Oders/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa đơn hàng thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchOrder();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
           
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function Update() {
    var model =
    {
        id: $('#idupdate').val(),
        status: $('#trangthai').val(),
        noteSiteAdmin: $('#note').val()
    };
    OpenWaiting();
    $.ajax({
        url: "/Oders/Update",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái thành công', { timeOut: 5000 });
                $('#editmodam').modal('hide');
                CloseWaiting();
                SearchOrder();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function viewUpdate(id) {
    $('#note').val('');
    var url = "/Oders/Getbyid";
    OpenWaiting();
    $.ajax({
        url: url,
        data: { id: id },
        cache: false,
        type: "POST",
        success: function (data) {
            $("#idupdate").val(data.id);
            $('#note').val(data.noteSiteAdmin);
            $('#trangthai').val(data.status + '');
            $('#editmodam').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (reponse) {
            CloseWaiting();
            alert("error : " + reponse);
        }
    });
}

function AutoUpdate(_id) {
    var model =
    {
        id: _id,
        status: $('#status_detailDH').val(),
        noteSiteAdmin: $('#noteSiteAdmin_detailDH').val()
    };
    OpenWaiting();
    $.ajax({
        url: "/Oders/Update",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái thành công', { timeOut: 5000 });
                CloseWaiting();
                window.location.reload();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function xacNhanDonHang(_id) {
    var model =
    {
        id: _id,
    };
    OpenWaiting();
    $.ajax({
        url: "/Oders/xacNhanDonHang",
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái thành công', { timeOut: 5000 });
                CloseWaiting();
                window.location.reload();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//SearchOrder();