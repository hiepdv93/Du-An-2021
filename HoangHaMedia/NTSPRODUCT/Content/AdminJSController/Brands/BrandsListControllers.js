
var modelBrands =
{
    Name: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelBrands.Name = $('#nameSearch').val();
}

function SearchBrands() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Brands/GetList", modelBrands, function (result) {
        modelBrands.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchBrands();
        return false;
    }
});

function ChangeSize() {
    modelBrands.PageNumber = 1;
    modelBrands.PageSize = $('#pageSize').val();
    SearchBrands();
}
function phantrang(PageNumber) {
    modelBrands.PageNumber = PageNumber;
    SearchBrands();
}

function RefreshBrands() {
    $('#nameSearch').val('');

    modelBrands.PageSize = 10;
    modelBrands.PageNumber = 1;
    SearchBrands();
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
        url: "/Brands/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa thương hiệu thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchBrands();
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
function CreateBrands() {
    OpenWaiting();
    $.post("/Brands/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleBrand').html('Thêm mới thương hiệu');
        $('#modamBrand').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateBrands(id) {
    OpenWaiting();
    $.post("/Brands/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        GetbyId(id);
        $("#idBrands").val(id);
        $('.titleBrand').html('Cập nhật thương hiệu');

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Brands/Getbyid?id=" + id,
        type: "POST",
        success: function (data) {
            $('#bkName').val(data.bkName);
            $('#bkImage').val(data.bkImage);
            $('#note').val(data.note);
            $('#numberOder').val(data.numberOder);
            $('#modamBrand').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//lay du lieu them moi, sua

function CreateOrUpdate() {
    var model =
    {
        id: $('#idBrands').val(),
        bkName: $('#bkName').val(),
        bkImage: $('#bkImage').val(),
        note: $('#note').val(),
        numberOder: $('#numberOder').val(),

    };
    //validate
    if (model.bkName == '') {
        toastr.error('Nhập tên!'); return false;
    } else
        if (model.bkImage == '') {
            toastr.error('Chọn hình ảnh!'); return false;
        }
    var url = '';
    if (model.id.length > 0) {
        url = "/Brands/Update";
    } else {
        url = "/Brands/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.id.length > 0) {
                    toastr.success('Cập nhật thương hiệu thành công');
                } else {
                    toastr.success('Thêm mới thương hiệu thành công');
                }
                $('#modamBrand').modal("hide");
                CloseWaiting();
                SearchBrands();
                
            } else {
                CloseWaiting();
                toastr.error(data.mess);
            }

        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchBrands();