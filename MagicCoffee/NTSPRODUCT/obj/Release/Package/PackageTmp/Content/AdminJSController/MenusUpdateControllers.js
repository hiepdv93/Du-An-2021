//code them moi
$('#loaimenu').on('change', function () {
    var gt = $('#loaimenu').val();
    if (gt == "1") {
        document.getElementById("menu_link").style.display = "block";
        document.getElementById("menu_lienket").style.display = "none";
        document.getElementById("menu_noidung").style.display = "none";
    } else if (gt == "2") {
        document.getElementById("menu_link").style.display = "none";
        document.getElementById("menu_lienket").style.display = "none";
        document.getElementById("menu_noidung").style.display = "block";
    } else {
        document.getElementById("menu_link").style.display = "none";
        document.getElementById("menu_lienket").style.display = "block";
        document.getElementById("menu_noidung").style.display = "none";
    }
});
//phan 2
$('#typecate').on('change', function () {
    var gt = $(this).val();
    if (gt == "1" || gt == "2" || gt == "3") {
        $.post("/Menus/Nhomdanhmuc/" + gt, function (result) {
            $("#cateid").html(result);
        });
        document.getElementById("idchon_nhom").style.display = "block";
    }
    else if (gt == "4") {
        $.post("/Menus/Giaiphap" , function (result) {
            $("#cateid").html(result);
            document.getElementById("idchon_nhom").style.display = "block";
        });
    }
    else {
        document.getElementById("idchon_nhom").style.display = "none";
    }
});

function SetDisplay() {
    var type_hid = $('#type_hid').val();
    if (type_hid == "1") {
        document.getElementById("menu_link").style.display = "block";
        document.getElementById("menu_lienket").style.display = "none";
        document.getElementById("menu_noidung").style.display = "none";
    } else if (type_hid == "2") {
        document.getElementById("menu_link").style.display = "none";
        document.getElementById("menu_lienket").style.display = "none";
        document.getElementById("menu_noidung").style.display = "block";
    } else {
        document.getElementById("menu_link").style.display = "none";
        document.getElementById("menu_lienket").style.display = "block";
        document.getElementById("menu_noidung").style.display = "none";
    }
}
//phan 3
$('#catep').val($('#pid_hid').val());
$('#loaimenu').val($('#type_hid').val());
$('#typecate').val($('#typecate_hid').val());
$('#vitri').val($('#vitri_hid').val());
SetDisplay();
var cateid_hid = $('#cateid_hid').val();
if (cateid_hid != "" && cateid_hid != " ") {
    $('#cateid').val(cateid_hid);
}