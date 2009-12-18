$(document).ready(function() {

	var zoomSpeed = 500;
	var timlineScrollPixels = 500;


	//Corners
	//	$(".search, .button-sml, .button-lrg, #category-selector, .tl, .event-teaser, #modal-container, #controls, .refresh-timeline, .add-find, .advanced-search-table, .button").corner();


	$(".off").click(function() {
		var originalHeight = $(".tl-container").css("height");
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
		var windowHeight = $(window).height();
		var scrollTop = $(window).scrollTop();
		$("#modal-container").css("top", (((windowHeight / 2) - 250) + scrollTop) + "px");
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src", modalURL);
		$("#modal-container, #modal-background").fadeIn();
		return false;
	});
	//Use livequery to bind modal to AJAX-created content links
	$(".modal").livequery("click", function(event) {
		var windowHeight = $(window).height();
		var scrollTop = $(window).scrollTop();
		$("#modal-container").css("top", (((windowHeight / 2) - 250) + scrollTop) + "px");
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src", modalURL);
		$("#modal-container, #modal-background").fadeIn();
		return false;
	});


	$(".refresh-timeline").livequery("click", function(event) {
		reloadPage();
	});


	//Hide all modals if clicking outside
	$("body").click(function() {
		$("#modal-container, #modal-background").fadeOut();
	});



	//***PHIL*** The following code checks what the current value of the #what and #what2 boxes is.  If the value is blank it puts a demo suggested value in.  However, this seems to create a problem in that the boxes are then both populated so the Find search acts as an Add.  I've put code in to disable the input boxes if nothing is entered but this still doesn't solve it.  I've commented this all out for the time being, but Megan is keen for this default text to be present so it looks like this will need to be handled server side.

	//Search form
	//Style input box text and remove sample value	
	/*
	var whatValue = $("#what").attr("value");
	var what2Value = $("#what2").attr("value");

	if (whatValue == "") {
	$("#what").attr("value", "e.g. climb Everest");
	$("#what").attr("disabled", "disabled");
	}
	if (what2Value == "") {
	$("#what2").attr("value", "e.g. climb Everest");
	$("#what2").attr("disabled", "disabled");
	}

	$("#what, #what2").focus(function() {
	var currentValue = $(this).attr("value");
	if (currentValue = "e.g. climb Everest") {
	$(this).attr("value", "");
	$(this).attr("disabled", "");
	}
	});
	$("#what, #what2").blur(function() {
	var currentValue = $(this).attr("value");
	if (currentValue == "") {
	$(this).attr("value", "e.g. climb Everest");
	$(this).attr("disabled", "disabled");
	}
	});
	*/




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