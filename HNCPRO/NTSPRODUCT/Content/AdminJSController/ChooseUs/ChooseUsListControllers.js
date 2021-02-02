
var modelSayWes =
{
    Name: '',
  
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSayWes.Name = $('#nameSearch').val();
 
}

function SearchSayWes() {
    GetModelSearch();
    OpenWaiting();
    $.post("/WhyChooseUss/GetList", modelSayWes, function (result) {
        modelSayWes.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchSayWes();
        return false;
    }
});

function ChangeSize() {
    modelSayWes.PageNumber = 1;
    modelSayWes.PageSize = $('#pageSize').val();
    SearchSayWes();
}
function phantrang(PageNumber) {
    modelSayWes.PageNumber = PageNumber;
    SearchSayWes();
}

function RefreshSayWes() {
    $('#nameSearch').val('');

    modelSayWes.PageSize = 15;
    modelSayWes.PageNumber = 1;
    SearchSayWes();
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
        url: "/WhyChooseUss/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchSayWes();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

function ChangeStatus(id) {
    $.ajax({
        url: "/WhyChooseUss/ChangeStatus?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Cập nhật trạng thái thành công', { timeOut: 5000 });
                SearchSayWes();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//goi modam them moi, sua
function CreateSayWe() {
    OpenWaiting();
    $.post("/WhyChooseUss/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSayWe').html('Thêm mới vì sao chọn chúng tôi');
        $('#modamSayWe').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSayWe(id) {
    OpenWaiting();
    $.post("/WhyChooseUss/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('.titleSayWe').html('Cập nhật vì sao chọn chúng tôi');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/WhyChooseUss/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#title').val(data.title);
            $('#image').val(data.image);
            $('#contents').val(data.contents);
            $('#numberOder').val(data.numberOder);
            if (data.active == true) {
                document.getElementById('active').checked = "checked";
            }
            $('#modamSayWe').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
//lay du lieu them moi, sua
function CreateOrUpdate() {
    var getActive = document.getElementById('active');
    var model =
    {
        id: $('#idUpdate').val(),
        title: $('#title').val(),
        image: $('#image').val(),
        contents: $('#contents').val(),
        numberOder: $('#numberOder').val()
    };
    if (getActive.checked) {
        model.active = true;
    } else {
        model.active = false;
    }
    //validate
    if (model.title == '') {
        toastr.error('Nhập tiêu đề!'); return false;
    }
    if (isNaN(model.numberOder)) {
        toastr.error('Thứ tự hiển thị phải là số!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/WhyChooseUss/Update";
    } else {
        url = "/WhyChooseUss/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.id.length > 0) {
                    toastr.success('Cập nhật  thành công');
                } else {
                    toastr.success('Thêm mới  thành công');
                }
                CloseWaiting();
                $('#modamSayWe').modal("hide");
                SearchSayWes();
            } else {
                toastr.error(data.mess);
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}

//tự chạy hàm tìm kiếm
SearchSayWes();