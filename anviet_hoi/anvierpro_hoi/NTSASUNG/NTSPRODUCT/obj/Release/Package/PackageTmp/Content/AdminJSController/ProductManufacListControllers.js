﻿
var modelProduct =
{
    CateId: '',
    CateId2: '',
    BrandId: '',
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelProduct.Name = $('#nameSearch').val();
    modelProduct.CateId = $('#cateSearch').val();
    modelProduct.CateId2 = $('#cateSearch2').val();
    modelProduct.BrandId = $('#brandSearch').val();
}

function SearchProduct() {
    GetModelSearch();
    OpenWaiting();
    $.post("/ProductManufac/GetList", modelProduct, function (result) {
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
    $('#cateSearch2').val('');
    $('#brandSearch').val('');

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
        url: "/ProductManufac/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa sản phẩm thành công', { timeOut: 5000 });
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
        url: "/ProductManufac/ChangeProductNew?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sản phẩm mới thành công', { timeOut: 5000 });
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
        url: "/ProductManufac/ChangeProductHot?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sản phẩm hot thành công', { timeOut: 5000 });
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
    $.ajax({
        url: "/ProductManufac/ChangeProductSale?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái sản phẩm khuyến mại thành công', { timeOut: 5000 });
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
        url: "/ProductManufac/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái hiển thị sản phẩm thành công', { timeOut: 5000 });
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