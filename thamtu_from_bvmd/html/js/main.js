/**
 * Created by DucNguyen on 2/13/2017.
 */
$(document).ready(function () {



    var owl_news_products = $("#owl1");
    owl_news_products.owlCarousel({
        animateOut: false,
        loop:true, // vong lap cac items
        margin:10,
        autoplay: true,
        autoplayTimeout: 7000,
        nav:false, // cai nay de hien thanh bam navition mac dinh
        dots: false, // cai nay hien dau cham o duoi slide
        customPaging : function(slider, i) {
            var thumb = $(slider.$slides[i]).data();
            return '<a>'+i+'</a>';
        },
        responsive:{
            0:{
                items:1,
                nav:false
            },
            600:{
                items:1,
                nav:false
            },
            1000:{
                items:1
            }
        }
    });
    //custom function showing current slide
    var $status = $('.pagingInfo');
    var $slickElement = $('#owl1');

    $slickElement.on('init reInit afterChange', function (event, slick, currentSlide, nextSlide) {
        //currentSlide is undefined on init -- set it to 0 in this case (currentSlide is 0 based)
        var i = (currentSlide ? currentSlide : 0) + 1;
        $status.text(i + '/' + slick.slideCount);
    });

    $(".navition.one .nextt").click(function(){
        owl_news_products.trigger('next.owl.carousel');
    });
    $(".navition.one .privew").click(function(){
        owl_news_products.trigger('prev.owl.carousel');
    });





    var owl_news_products1 = $("#owl2");
    owl_news_products1.owlCarousel({
        animateOut: false,
        loop:true, // vong lap cac items
        margin:10,
        autoplay: true,
        autoplayTimeout: 7000,
        nav:false, // cai nay de hien thanh bam navition mac dinh
        dots: false, // cai nay hien dau cham o duoi slide
        customPaging : function(slider, i) {
            var thumb = $(slider.$slides[i]).data();
            return '<a>'+i+'</a>';
        },
        responsive:{
            0:{
                items:2,
                nav:false
            },
            600:{
                items:3,
                nav:false
            },
            1000:{
                items:3
            }
        }
    });
    $(".navition.two .nextt").click(function(){
        owl_news_products1.trigger('next.owl.carousel');
    });
    $(".navition.two .privew").click(function(){
        owl_news_products1.trigger('prev.owl.carousel');
    });



    $(function () {
        $('.kid-dc').hide();
        $('.pro-dc > .top-pro-dc > span').click(function () {
            $(this).parent().next().slideToggle('slow');
            this.classList.toggle("active");
            return false;
        });
    });


    $(function () {
        $('.button1').on('click', function () {
            $('.menu-mobile').toggleClass("active-dc");
            $('body').append('<div class="append-body"></div>');
            $('.append-body').on('click', function () {
                $('.menu-mobile').toggleClass("active-dc");
                $(this).hide();
            });
        });
    });
});