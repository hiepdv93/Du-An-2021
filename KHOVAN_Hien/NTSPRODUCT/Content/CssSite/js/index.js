$('#owl-pro-home').owlCarousel({
    loop: true,
    margin: 10,
    items: 3,
    dots: false,
    autoplay: 1000,
    nav: true,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        480: {
            items: 1
        },
        768: {
            items: 1
        },
        1024: {
            items: 4
        }
    }
});
$('#owl-product-relate-detail').owlCarousel({
    loop: true,
    margin: 10,
    items: 3,
    dots: false,
    autoplay: 1000,
    nav: true,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        480: {
            items: 1
        },
        768: {
            items: 1
        },
        1024: {
            items: 4
        }
    }
});
$('#owl-news-home').owlCarousel({
    loop: true,
    margin: 10,
    items: 3,
    dots: false,
    nav: true,
    autoplay: 1000,
    responsiveClass: true,
    responsive: {
        0: {
            items: 3
        },
        480: {
            items: 3
        },
        768: {
            items: 3
        }
    }
});
$('#owl-news-team-home').owlCarousel({
    loop: true,
    margin: 10,
    items: 1,
    navText: false,
    nav: true,
    autoplay: 1000,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        480: {
            items: 1
        },
        768: {
            items: 1
        }
    }
});
$('#owl-faedback-home').owlCarousel({
    loop: true,
    margin: 10,
    navText: false,
    nav: true,
    autoplay: 1000,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1
        }
    }
});
$('#owl-partner-home').owlCarousel({
    loop: true,
    margin: 10,
    dots: false,
    autoplay: 1000,
    nav: true,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        480: {
            items: 2
        },
        768: {
            items: 4
        },
        1024: {
            items: 5
        }
    }
});
//js tab-home
$(document).ready(function(){
    
    $('ul.tabs li').click(function(){
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs li').removeClass('current');
        $('.tab-contents').removeClass('current');

        $(this).addClass('current');
        $("#"+tab_id).addClass('current');
    })

})
//js tab-detail-product
$(document).ready(function(){
    
    $('ul.tabs-product li').click(function(){
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs-product li').removeClass('current');
        $('.info-product-tab-content').removeClass('current');

        $(this).addClass('current');
        $("#"+tab_id).addClass('current');
    })

})
// js menu mobile
$(document).ready(function() {
    $('div.submit-form').click(function() {
        $('html, body').animate({
            scrollTop: $("#regist").offset().top
        }, 1000)
    })
});
$(document).ready(function() {
    $("#content-slider").lightSlider({
        loop:true,
        keyPress:true
    });
    $('#image-gallery').lightSlider({
        gallery:true,
        item:1,
        thumbItem:5,
        slideMargin: 0,
        speed:500,
        auto:true,
        loop:true,
        onSliderLoad: function() {
            $('#image-gallery').removeClass('cS-hidden');
        }  
    });
});
(function($) {
    var ico = $('<i class="fa fa-caret-right"></i>');
    $('nav#menu li:has(ul) > a').append(ico);

    $('nav#menu li:has(ul)').on('click', function() {
        $(this).toggleClass('open');
    });

    $('a#toggle').on('click', function(e) {
        $('html').toggleClass('open-menu');
        return false;
    });


    $('div#overlay').on('click', function() {
        $('html').removeClass('open-menu');
    })

})(jQuery)