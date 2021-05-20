
var modelSlides =
{
    Name: '',
    CateId: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSlides.Name = $('#nameSearch').val();
    modelSlides.CateId = $('#CateId').val();
}

function SearchSlides() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Partner/GetList", modelSlides, function (result) {
        modelSlides.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSlides();
        return false;
    }
});

function ChangeSize() {
    modelSlides.PageNumber = 1;
    modelSlides.PageSize = $('#pageSize').val();
    SearchSlides();
}
function phantrang(PageNumber) {
    modelSlides.PageNumber = PageNumber;
    SearchSlides();
}

function RefreshSlides() {
    $('#nameSearch').val('');
    $('#CateId').val('');

    modelSlides.PageSize = 15;
    modelSlides.PageNumber = 1;
    SearchSlides();
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
        url: "/Partner/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa bản ghi thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchSlides();
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

//goi modam them moi, sua
function CreateSlide() {
    OpenWaiting();
    $.post("/Partner/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSlide').html('Thêm mới thông tin ngân hàng');
        $('#modamSlide').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSlide(id) {
    OpenWaiting();
    $.post("/Partner/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleSlide').html('Cập nhật thông tin ngân hàng');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Partner/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#pName').val(data.pName);
            $('#pImage').val(data.pImage);
            $('#pNote').val(data.pNote);
            $('#bankNumber').val(data.bankNumber);
            $('#bankName').val(data.bankName);
            $('#pLink').val(data.pLink);
            $('#numberOder').val(data.numberOder);
            $('#groupId').val(data.groupId);

            $('#modamSlide').modal({
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
        id: $('#id').val(),
        pName: $('#pName').val(),
        pImage: $('#pImage').val(),
        pNote: $('#pNote').val(),
        bankNumber: $('#bankNumber').val(),
        bankName: $('#bankName').val(),
        pLink: $('#pLink').val(),
        numberOder: $('#numberOder').val(),
        groupId: $('#groupId').val()
    };

    //validate
    if (model.pName == '') {
        toastr.error('Nhập tên thông tin ngân hàng!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/Partner/Update";
    } else {
        url = "/Partner/Add";
    }
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật thông tin ngân hàng thành công');
            } else {
                toastr.success('Thêm mới thông tin ngân hàng thành công');
            }
            CloseWaiting();
            SearchSlides();
            $('#modamSlide').modal("hide");
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchSlides();