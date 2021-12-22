
var modelTagpros =
{
    Name: '',
    PageSize: 15,
    PageNumber: 1
};

function GetModelSearch() {
    modelTagpros.Name = $('#nameSearch').val();
}

function SearchTagpros() {
    GetModelSearch();
    OpenWaiting();
    $.post("/Tagpro/GetList", modelTagpros, function (result) {
        modelTagpros.PageNumber = 1;
        $("#row_data").html(result);
        CloseWaiting();
    });
}

$("#nameSearch").keydown(function (event) {
    if (event.keyCode === 13) {
        SearchTagpros();
        return false;
    }
});

function ChangeSize() {
    modelTagpros.PageNumber = 1;
    modelTagpros.PageSize = $('#pageSize').val();
    SearchTagpros();
}
function phantrang(PageNumber) {
    modelTagpros.PageNumber = PageNumber;
    SearchTagpros();
}

function RefreshTagpros() {
    $('#nameSearch').val('');

    modelTagpros.PageSize = 15;
    modelTagpros.PageNumber = 1;
    SearchTagpros();
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
    $.ajax({
        url: "/Tagpro/Delete?id=" + id,
        type: "POST",
        success: function (data) {
            if (data.ok === true) {
                toastr.success('Xóa thẻ tag thành công', { timeOut: 5000 });
                $('#modamDelete').modal('hide');
                SearchTagpros();
            } else {
                toastr.error(data.mess, { timeOut: 5000 });
            }
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
        tagId: $('#id').val(),
      
        tagName: $('#tagName').val(),
        tagOrder: $('#tagOrder').val()
    };
    
    //validate
    if (model.tagName == '') {
        toastr.error('Nhập tên thẻ tag!'); return false;
    }

    var url = '';
    url = "/Tagpro/Update";
    OpenWaiting();
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (data) {
            if (data.ok == true) {
                if (model.tagId.length > 0) {
                    toastr.success('Cập nhật thẻ tag thành công');
                } else {
                    toastr.success('Thêm mới thẻ tag thành công');
                }
                CloseWaiting();
                $('#modamSlide').modal("hide");
                SearchTagpros();
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
function CreateTag() {
    OpenWaiting();
    $.post("/Tagpro/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val('');
        $('.titleSlide').html('Thêm mới thẻ tag');
        $('#modamSlide').modal({
            show: 'true'
        });
        CloseWaiting();
    });
}
function UpdateTag(id) {
    OpenWaiting();
    $.post("/Tagpro/UpdateOrCreate", function (result) {
        $("#divModam").html(result);
        $("#id").val(id);
        $('.titleSlide').html('Cập nhật  thẻ tag');
        GetbyId(id);

    });
}
function GetbyId(id) {
    $.ajax({
        url: "/Tagpro/GetbyId?id=" + id,
        type: "POST",
        success: function (data) {
           
            $('#tagName').val(data.tagName);
            $('#tagOrder').val(data.tagOrder);
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
//tự chạy hàm tìm kiếm
SearchTagpros();