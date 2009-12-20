$(document).ready(function() {

	var zoomSpeed = 500;
	var timlineScrollPixels = 500;


	//Corners
	//	$(".search, .button-sml, .button-lrg, #category-selector, .tl, .event-teaser, #modal-container, #controls, .refresh-timeline, .add-find, .advanced-search-table, .button").corner();


	$(".off").click(function() {
		var originalHeight = $(".tl-container").height();
		createCookie("originalHeight", originalHeight, 365);
		$(".tl-container").animate({ "height": "0" }, zoomSpeed);
		//		$(this).text("On");
		return false;
	});
	$(".on").click(function() {
		var tlContainerHeight = readCookie("originalHeight");
		$(".tl-container").animate({ "height": tlContainerHeight }, zoomSpeed);
		return false;
	});


	$("#show-categories").click(function() {
		$("#controls").toggle();
		return false;
	});


	$("#view-all").click(function() {
		clearAll(tl, [1, 2, 4, 5], document.getElementById("table-filter"));
	});


	$("#scroll-back").click(function() {
		tl.getBand(1)._autoScroll(timlineScrollPixels);
		return false;
	});


	$("#scroll-forward").click(function() {
		tl.getBand(1)._autoScroll(-timlineScrollPixels);
		return false;
	});


	//Modal windows
	//Create modal window from regular links with class '.modal'
	$(".modal").click(function() {
		//		var windowHeight = $(window).height();
		//		var scrollTop = $(window).scrollTop();
		//		$("#modal-container").css("top", (((windowHeight / 2) - 250) + scrollTop) + "px");
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src", modalURL);
		$("#modal-container, #modal-background").fadeIn();
		return false;
	});
	//Use livequery to bind modal to AJAX-created content links
	$(".modal").livequery("click", function(event) {
		//		var windowHeight = $(window).height();
		//		var scrollTop = $(window).scrollTop();
		//		$("#modal-container").css("top", (((windowHeight / 2) - 250) + scrollTop) + "px");
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src", modalURL);
		$("#modal-container, #modal-background").fadeIn();
		return false;
	});


	$(".refresh-timeline").livequery("click", function(event) {
		reloadPage();
	});


	//Hide all modals if clicking outside
	$("body, .close-modal").click(function() {
		$("#modal-container, #modal-background").fadeOut();
	});

	//Search form
	//Style input box text and remove sample value	
	var whatValue = $("#what").attr("value");
	var what2Value = $("#what2").attr("value");

	if (whatValue == "") {
		$("#what").attr("value", "e.g. climb Everest");
	}
	if (what2Value == "") {
		$("#what2").attr("value", "e.g. climb Everest");
	}

	$("#what, #what2").focus(function() {
		var currentValue = $(this).attr("value");
		if (currentValue = "e.g. climb Everest") {
			$(this).attr("value", "");
		}
	});
	$("#what, #what2").blur(function() {
		var currentValue = $(this).attr("value");
		if (currentValue == "") {
			$(this).attr("value", "e.g. climb Everest");
		}
	});
});

function createCookie(name, value, days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		var expires = "; expires=" + date.toGMTString();
	}
	else var expires = "";
	document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') c = c.substring(1, c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name, "", -1);
}

function openModal(modalURL) {
	$(".simileAjax-bubble-container").remove();
	$("#modal-container iframe").attr("src", modalURL);
	$("#modal-container, #modal-background").fadeIn();
	return false;
};