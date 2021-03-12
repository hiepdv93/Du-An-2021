debugger;
var sessget = $.session.get("ac");
if (sessget == null) {
    $.ajax({
        url: "/Config/Getac",
        cache: false,
        type: "POST",
        success: function (data) {
            if (data==false) {
                top.location = "https://www.google.com/?gws_rd=ssl";
            } else {
                $.session.set("ac", "ac");
                
            }
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}

