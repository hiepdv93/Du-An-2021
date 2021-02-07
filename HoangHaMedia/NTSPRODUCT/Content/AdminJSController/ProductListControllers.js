
var modelProduct =
{
    CateId: '',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelProduct.Name = $('#nameSearch').val();
    modelProduct.CateId = $('#cateSearch').val();
}

function SearchProduct() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Prodcuts/GetList", modelProduct, function (result) {
        modelProduct.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchProduct();
        return false;
    }
});

function ChangeSize() {
    modelProduct.PageNumber = 1;
    modelProduct.PageSize = $('#pageSize').val();
    SearchProduct();
}
function phantrang(PageNumber) {
    modelProduct.PageNumber = PageNumber;
    SearchProduct();
}

function RefreshProduct() {
    $('#nameSearch').val('');
    $('#cateSearch').val('');

    modelProduct.PageSize = 15;
    modelProduct.PageNumber = 1;
    SearchProduct();
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
    $.ajax({
        url: "/Prodcuts/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa sự kiện thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchProduct();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeProductNew(id) {
    $.ajax({
        url: "/Prodcuts/ChangeProductNew?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sự kiện mới thành công', { timeOut: 5000 });
                SearchProduct();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeProductHot(id) {
    $.ajax({
        url: "/Prodcuts/ChangeProductHot?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sự kiện hot thành công', { timeOut: 5000 });
                SearchProduct();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeProductSale(id) {
    //hiện trang chủ
    $.ajax({
        url: "/Prodcuts/ChangeProductSale?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sự kiện khuyến mại thành công', { timeOut: 5000 });
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
function ChangeStatus(id) {
    $.ajax({
        url: "/Prodcuts/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị sự kiện thành công', { timeOut: 5000 });
                SearchProduct();
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
SearchProduct();