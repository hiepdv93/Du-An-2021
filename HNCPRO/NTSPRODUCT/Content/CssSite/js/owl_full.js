$(document).ready(function()
{
	"use strict";
    /* SlideShow OWL */
	$(".customers_owl").owlCarousel({
		navigation: true,
		items:2,
		slideSpeed : 200,
		paginationSpeed : 800,
		rewindSpeed : 1000,
		//Autoplay
		autoPlay : true,
		itemsCustom:[[480,1],[320,1],[768,1],[767,1],[1024,2],[1200,2]],
		responsive:true,
		navigationText: [
			"<a class='flex-prev-slideshow'><i class='fa fa-chevron-left'></i></a>",
			"<a class='flex-next-slideshow'><i class='fa fa-chevron-right'></i></a>"
		],

	})
	$(".brand_owl").owlCarousel({
		navigation: true,
		items:9,
		slideSpeed : 200,
		paginationSpeed : 800,
		rewindSpeed : 1000,
		//Autoplay
		autoPlay : true,
		itemsCustom:[[480,2],[320,2],[768,4],[767,3],[991,4],[1200,9]],
		responsive:true,
		navigationText: [
			"<a class='flex-prev-slideshow'><i class='fa fa-chevron-left'></i></a>",
			"<a class='flex-next-slideshow'><i class='fa fa-chevron-right'></i></a>"
		],

	})
	$(".similar_products_slideshow").owlCarousel({
		navigation: true,
		items:5,
		slideSpeed : 200,
		paginationSpeed : 800,
		rewindSpeed : 1000,
		//Autoplay
		autoPlay : true,
		itemsCustom:[[480,2],[320,1],[768,4],[767,3],[991,4],[1200,5]],
		responsive:true,
		navigationText: [
			"<a class='flex-prev-slideshow'><i class='fa fa-chevron-left'></i></a>",
			"<a class='flex-next-slideshow'><i class='fa fa-chevron-right'></i></a>"
		],

	});		
});