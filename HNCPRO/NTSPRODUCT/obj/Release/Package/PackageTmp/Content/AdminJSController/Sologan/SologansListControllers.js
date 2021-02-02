
var modelSayWes =
{
    Name: '',
    Description: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelSayWes.Name = $('#nameSearch').val();
    modelSayWes.Description = $('#emaildesSearchSearch').val();
}

function SearchSayWes() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Sologan/GetList", modelSayWes, function (result) {
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
$("#desSearch").keydown(function (event) {
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
    $('#desSearch').val('');

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
        url: "/Sologan/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa sologan thành công', { timeOut: 5000 });
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
        url: "/Sologan/ChangeStatus?id=" + id,
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
    $.post("/Sologan/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleSayWe').html('Thêm mới ý kiến');
        $('#modamSayWe').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateSayWe(id) {
    OpenWaiting();
    $.post("/Sologan/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#idUpdate").val(id);
        $('.titleSayWe').html('Cập nhật sologan');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Sologan/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#name').val(data.name);
            $('#descript').val(data.descript);
            $('#avata').val(data.avata);
            $('#dOrder').val(data.dOrder);
            if (data.status == true) {
                document.getElementById('status').checked  = true;
            } else {
                document.getElementById('status').checked = false;
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
    var getActive = document.getElementById('status');
    var model =
    {
        id: $('#idUpdate').val(),
        name: $('#name').val(),
        avata: $('#avata').val(),
        descript: $('#descript').val(),
        status: false,
        dOrder: $('#dOrder').val()
    };
    if (getActive.checked) {
        model.status = true;
    } else {
        model.status = false;
    }
    //validate
    if (model.name == '') {
        toastr.error('Nhập họ tên!'); return false;
    }
    if (isNaN(model.dOrder)) {
        toastr.error('Thứ tự hiển thị phải là số!'); return false;
    }
    var url = '';
    if (model.id.length > 0) {
        url = "/Sologan/Update";
    } else {
        url = "/Sologan/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.id.length > 0) {
                    toastr.success('Cập nhật sologan thành công');
                } else {
                    toastr.success('Thêm mới sologan thành công');
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