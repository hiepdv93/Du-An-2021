
var modelSlides =
{
    Name: '',

    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSlides.Name = $('#nameSearch').val();
}

function SearchSlides() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Slide/GetList", modelSlides, function (result) {
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
        url: "/Slide/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa slide thành công', { timeOut: 5000 });
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

function ChangeStatus(id) {
    OpenWaiting();
    $.ajax({
        url: "/Slide/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái slide thành công', { timeOut: 5000 });
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
    $.post("/Slide/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSlide').html('Thêm mới slide');
        $('#modamSlide').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSlide(id) {
    OpenWaiting();
    $.post("/Slide/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleSlide').html('Cập nhật slide');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Slide/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#name').val(data.name);
            $('#slink').val(data.sLink);
            $('#images').val(data.images);
            $('#contents').val(data.contents);
            $('#numberOder').val(data.numberOder);
            if (data.active == true) {
                document.getElementById('active').checked = true;
            } else {
                document.getElementById('active').checked = false;
            } 
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
    var getActive = document.getElementById('active');
    var model =
    {
        id: $('#id').val(),
        name: $('#name').val(),
        sLink: $('#slink').val(),
        images: $('#images').val(),
        contents: $('#contents').val(),
        numberOder: $('#numberOder').val()
    };
    if (getActive.checked) {
        model.active = true;
    } else {
        model.active = false;
    }
    //validate
    if (model.name == '') {
        toastr.error('Nhập tên slide!'); return false;
    }

    var url = '';
    if (model.id.length > 0) {
        url = "/Slide/Update";
    } else {
        url = "/Slide/Add";
    }
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.id.length > 0) {
                toastr.success('Cập nhật slide thành công');
            } else {
                toastr.success('Thêm mới slide thành công');
            }
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