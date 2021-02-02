
var modelSlides =
{
    Name: '',
    Status :'',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSlides.Name = $('#nameSearch').val();
    modelSlides.Status = $('#Status').val();
}

function SearchSlides() {
    GetModelSearch();
    OpenWaiting();
    $.post("/GroupPartner/GetList", modelSlides, function (result) {
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
    $('#Status').val('');

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
        url: "/GroupPartner/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa nhóm thành công', { timeOut: 5000 });
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
    $.post("/GroupPartner/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSlide').html('Thêm mới nhóm khách hàng - đối tác');
        $('#modamSlide').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSlide(id) {
    OpenWaiting();
    $.post("/GroupPartner/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleSlide').html('Cập nhật nhóm khách hàng - đối tác');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/GroupPartner/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#name').val(data.name);
            $('#type').val(data.gtype );
            $('#number').val(data.number+'');
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
        name: $('#name').val(),
        number: $('#number').val(),
        gtype: $('#type').val(),
    };
 
    //validate
    if (model.name == '') {
        toastr.error('Nhập tên nhóm!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/GroupPartner/Update";
    } else {
        url = "/GroupPartner/Add";
    }
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật nhóm thành công');
            } else {
                toastr.success('Thêm mới nhóm thành công');
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