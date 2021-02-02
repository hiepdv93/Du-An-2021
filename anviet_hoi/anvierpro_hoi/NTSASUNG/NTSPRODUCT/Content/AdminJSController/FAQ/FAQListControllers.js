
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
    $.post("/FAQ/GetList", modelSayWes, function (result) {
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
        url: "/FAQ/Delete",
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

//goi modam them moi, sua
function CreateSayWe() {
    OpenWaiting();
    $.post("/FAQ/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSayWe').html('Thêm mới hỏi đáp sản phẩm');
        $('#modamSayWe').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSayWe(id) {
    OpenWaiting();
    $.post("/FAQ/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('.titleSayWe').html('Cập nhật hỏi đáp sản phẩm');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/FAQ/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#title').val(data.name);
            $('#contents').val(data.contents);
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
        name: $('#title').val(),
        contents: $('#contents').val()
    };
    //validate
    if (model.name == '') {
        toastr.error('Nhập tiêu đề!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/FAQ/Update";
    } else {
        url = "/FAQ/Add";
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