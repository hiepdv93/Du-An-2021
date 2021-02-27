//js count down
function getTimeRemaining(endtime) {
  var t = Date.parse(endtime) - Date.parse(new Date());
  var seconds = Math.floor((t / 1000) % 60);
  var minutes = Math.floor((t / 1000 / 60) % 60);
  var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
  var days = Math.floor(t / (1000 * 60 * 60 * 24));
  return {
    total: t,
    days: days,
    hours: hours,
    minutes: minutes,
    seconds: seconds,
  };
}

function initializeClock(id, endtime) {
  var clock = document.getElementById(id);
  var daysSpan = clock.querySelector(".days");
  var hoursSpan = clock.querySelector(".hours");
  var minutesSpan = clock.querySelector(".minutes");
  var secondsSpan = clock.querySelector(".seconds");

  function updateClock() {
    var t = getTimeRemaining(endtime);

    daysSpan.innerHTML = t.days;
    hoursSpan.innerHTML = ("0" + t.hours).slice(-2);
    minutesSpan.innerHTML = ("0" + t.minutes).slice(-2);
    secondsSpan.innerHTML = ("0" + t.seconds).slice(-2);

    if (t.total <= 0) {
      clearInterval(timeinterval);
    }
  }

  updateClock();
  var timeinterval = setInterval(updateClock, 1000);
}

var dateToStr = null;
var dateTo = null;
for (var i = 1; i <= 4; i++) {
  if ($("#clockdiv_" + i).length) {
    dateToStr = $("#dateTo_" + i)
      .val()
      .split(":");

    dateTo = new Date(
      parseInt(dateToStr[0]),
      parseInt(dateToStr[1]) - 1,
      parseInt(dateToStr[2]),
      parseInt(dateToStr[3]),
      parseInt(dateToStr[4]),
      parseInt(dateToStr[5])
    );
    initializeClock("clockdiv_" + i, dateTo);
  }
}
//end js count down
$("#owl-pro-home").owlCarousel({
  loop: true,
  margin: 10,
  items: 5,
  dots: false,
  autoplay: 1000,
  nav: true,
  responsiveClass: true,
  responsive: {
    0: {
      items: 1,
    },
    480: {
      items: 1,
    },
    768: {
      items: 1,
    },
    1024: {
      items: 5,
    },
  },
});
$("#owl-pro-detail").owlCarousel({
  loop: true,
  margin: 10,
  items: 5,
  dots: false,
  autoplay: 1000,
  nav: true,
  responsiveClass: true,
  responsive: {
    0: {
      items: 1,
    },
    480: {
      items: 1,
    },
    768: {
      items: 3,
    },
    1024: {
      items: 3,
    },
  },
});
$("#owl-pro-home2").owlCarousel({
  loop: true,
  margin: 10,
  items: 5,
  dots: false,
  autoplay: 1000,
  nav: true,
  responsiveClass: true,
  responsive: {
    0: {
      items: 1,
    },
    480: {
      items: 1,
    },
    768: {
      items: 1,
    },
    1024: {
      items: 5,
    },
  },
});
$("#owl-pro-home3").owlCarousel({
  loop: true,
  margin: 10,
  items: 5,
  dots: false,
  autoplay: 1000,
  nav: true,
  responsiveClass: true,
  responsive: {
    0: {
      items: 1,
    },
    480: {
      items: 1,
    },
    768: {
      items: 1,
    },
    1024: {
      items: 5,
    },
  },
});

$("#owl-partner-home").owlCarousel({
  loop: true,
  margin: 10,
  items: 5,
  dots: false,
  autoplay: false,
  nav: true,
  responsiveClass: true,
  responsive: {
    0: {
      items: 1,
    },
    480: {
      items: 1,
    },
    768: {
      items: 1,
    },
    1024: {
      items: 5,
    },
  },
});
jQuery("document").ready(function ($) {
  var nav = $('#navBar');
  var gotop = $("#gotop");
  $(window).scroll(function () {
    if ($(this).scrollTop() > 150) {
      nav.addClass("div_banner_fixed");
    } else {
      nav.removeClass("div_banner_fixed");
    }

    if ($(this).scrollTop() > 300) {
      gotop.addClass("go-top-show");
    } else {
      gotop.removeClass("go-top-show");
    }
  });

  gotop.click(function () {
    $("html, body").animate({scrollTop: 0}, 300);
  });
});

//js tab-home
$(document).ready(function () {
  $(".circle-container .learning-item").click(function () {
    var tab_id = $(this).attr("data-tab");
    $(".circle-container .learning-item").removeClass("current");
    $(".for-tab").removeClass("current");

    $(this).addClass("current");
    $("#" + tab_id).addClass("current");
  });
});
//js tab-maps
$(document).ready(function () {
  $(".tabs-maps li").click(function () {
    var tab_id_map = $(this).attr("data-tab");
    $(".tabs-maps li").removeClass("current");
    $(".tab-content-maps").removeClass("current");

    $(this).addClass("current");
    $("#" + tab_id_map).addClass("current");
  });
});
//js tab-detail-product
$(document).ready(function () {
  $("ul.tabs-product li").click(function () {
    var tab_id = $(this).attr("data-tab");

    $("ul.tabs-product li").removeClass("current");
    $(".info-product-tab-content").removeClass("current");

    $(this).addClass("current");
    $("#" + tab_id).addClass("current");
  });
});
// js menu mobile
$(document).ready(function () {
  $("div.submit-form").click(function () {
    $("html, body").animate(
      {
        scrollTop: $("#regist").offset().top,
      },
      1000
    );
  });
});
$(document).ready(function () {
  $("#content-slider").lightSlider({
    loop: true,
    keyPress: true,
  });
  $("#image-gallery").lightSlider({
    gallery: true,
    item: 1,
    thumbItem: 5,
    slideMargin: 0,
    speed: 500,
    auto: true,
    loop: true,
    onSliderLoad: function () {
      $("#image-gallery").removeClass("cS-hidden");
    },
  });
});
(function ($) {
  var ico = $('<i class="fa fa-caret-right"></i>');
  $("nav#menu li:has(ul) > a").append(ico);

  $("nav#menu li:has(ul)").on("click", function () {
    $(this).toggleClass("open");
  });

  $("a#toggle").on("click", function (e) {
    $("html").toggleClass("open-menu");
    return false;
  });

  $("div#overlay").on("click", function () {
    $("html").removeClass("open-menu");
  });

  $(document).ready(function () {
    switchLayout()
  });

  $( window ).resize(function() {
    switchLayout()
  });

  function switchLayout() {
    if ($(window).width() < 767) {
      $("#left-panel").before($("#right-panel"));
    } else {
      $("#right-panel").before($("#left-panel"));
    }
  }
})(jQuery);
