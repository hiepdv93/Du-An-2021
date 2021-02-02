
var modelLibs =
{
    Name: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelLibs.Name = $('#nameSearch').val();
}

function SearchLibs() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Lib/GetList", modelLibs, function (result) {
        modelLibs.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchLibs();
        return false;
    }
});

function ChangeSize() {
    modelLibs.PageNumber = 1;
    modelLibs.PageSize = $('#pageSize').val();
    SearchLibs();
}
function phantrang(PageNumber) {
    modelLibs.PageNumber = PageNumber;
    SearchLibs();
}

function RefreshLibs() {
    $('#nameSearch').val('');
    $('#typeSearch').val('-1');

    modelLibs.PageSize = 15;
    modelLibs.PageNumber = 1;
    SearchLibs();
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
        url: "/Lib/Delete",
        type: "POST",
        data: { Id: id },
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa thư viện thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                CloseWaiting();
                SearchLibs();
            } else {
                CloseWaiting();
                toastr.error(data.mess, { timeOut: 5000 });
            }
        },
        error: function (response) {
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        },
    });
}
function ChangeTypeCate() {
    OpenWaiting();
    $.post("/ImportView/GetCateForProduct?type=" + $('#type').val(), function (result) {
        $("#cateId").html(result);
        CloseWaiting();
    });
}
function CreateLib() {
    OpenWaiting();
    $.post("/Lib/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $('.titleLib').html('Thêm mới thư viện');
        $.post("/ImportView/GetCateForProduct?type=" + $('#type').val(), function (result2) {
            $("#cateId").html(result2);
        });
        $('#modamLib').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateLib(libId) {
    OpenWaiting();
    $.post("/Lib/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#libId").val(libId);
        $('.titleLib').html('Cập nhật thư viện');
      
        GetbyId(libId);

    });
}
function GetbyId(id) {

    $.ajax({
        url: "/Lib/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
            $('#linName').val(data.linName);
            $('#libLink').val(data.libLink);
            $('#libImage').val(data.libImage);
            $('#libNote').val(data.libNote);
            $('#libOrder').val(data.libOrder);
            $('#type').val(data.libType + '');
           
            $.post("/ImportView/GetCateForProduct?type=" + $('#type').val(), function (result2) {
                $("#cateId").html(result2);
                $('#cateId').val(data.cateId);
            });
            $('#modamLib').modal({
                show: 'true'
            });
            CloseWaiting();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        }
    });
}
//lay du lieu them moi, sua
function CreateOrUpdate() {
    var getActive = document.getElementById('active');
    var model =
    {
        libId: $('#libId').val(),
        linName: $('#linName').val(),
        libLink: $('#libLink').val(),
        libImage: $('#libImage').val(),
        libNote: $('#libNote').val(),
        libOrder: $('#libOrder').val(),
        libType: $('#type').val(),
        cateId: $('#cateId').val()
    };
    //validate
    if (model.name == '') {
        toastr.error('Nhập tên Thư viện!'); return false;
    }
    if (isNaN(model.libOrder)) {
        toastr.error('Thứ tự hiển thị phải là số!'); return false;
    }
    var url = '';
    if (model.libId.length > 0) {
        url = "/Lib/Update";
    } else {
        url = "/Lib/Add";
    }
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (model.libId.length > 0) {
                toastr.success('Cập nhật thư viện thành công');
            } else {
                toastr.success('Thêm mới thư viện thành công');
            }
            $('#modamLib').modal("hide");
            CloseWaiting();
            SearchLibs();
        },
        error: function (response) {
            CloseWaiting();
            toastr.error("Đã xảy ra lỗi!", { timeOut: 5000 });
        }
    });
}

//tự chạy hàm tìm kiếm
SearchLibs();