var giabd = 50;
var giakt = 1000;
function Searchproduct()
{
    var moi = $('#idck_moi').is(":checked");
    var km = $('#idck_km').is(":checked");
    var bc = $('#idck_bc').is(":checked");
    var sapxep = $('.id_sapxep').val();

    var selected = [];
    $('#scrollbar input:checked').each(function () {
        selected.push($(this).attr('value'));
    });
    var url = "/SiteProduct/GetListPro?page=1";
    $.get(url + "&sx=" + sapxep + "&moi=" + moi + "&km=" + km + "&bc=" + bc + "&tu=" + giabd + "&den=" + giakt + "&mang=" + selected.join(';'), function (data) {
        $("#idnoidung").html(data);
    });
}
function phantrang(url) {
    var moi = $('#idck_moi').is(":checked");
    var km = $('#idck_km').is(":checked");
    var bc = $('#idck_bc').is(":checked");
    var sapxep = $('.id_sapxep').val();
    var selected = [];
    $('#scrollbar input:checked').each(function () {
        selected.push($(this).attr('value'));
    });
    $.get(url + "&sx=" + sapxep + "&moi=" + moi + "&km=" + km + "&bc=" + bc + "&tu=" + giabd + "&den=" + giakt + "&mang=" + selected.join(';'), function (data) {
        $("#idnoidung").html(data);
    });
}
function ChangeGia()
{
    var giahtml = $('.tooltip-inner').html();
    var mang = giahtml.split(':');
    var rsbd = parseInt(mang[0]);
    var rskt = parseInt(mang[1]);
    if (giabd != rsbd || giakt != rskt) {
        giabd = rsbd;
        giakt = rskt;
        Searchproduct();
    }
}