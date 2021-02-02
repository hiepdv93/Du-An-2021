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
$('#typecate').on('change', function () {
    var gt = $(this).val();
    if (gt == "1" || gt == "2") {
        $('#cateid').load("/Menus/Nhomdanhmuc/" + gt);
        document.getElementById("idchon_nhom").style.display = "block";
    } else {
        document.getElementById("idchon_nhom").style.display = "none";
    }
});
