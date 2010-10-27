$(document).ready(function() {


    //Global variables
    var zoomSpeed = 600; 							//Zoom/scroll animation speed (milliseconds)
    var zoomFactor = 40; 							//Zoom scale factor (%)
    var scrollFactor = 120; 							//Amount of movement on left-scroll or right-scroll click (pixels)
    var cookieOptions = { path: "/templates/", expires: 365 }; 	//General cookie path and expiry (days) settings
    var totalCategories = 13; 						//Total number of timeline categories (various loops performed)


    //Useful functions
    jQuery.fn.fadeToggle = function(speed, easing, callback) {
        return this.animate({ opacity: "toggle" }, speed, easing, callback);
    };


    //Control display of categories based on cookie values
    for (i = 1; i <= totalCategories; i++) {
        if ($.cookie("category-" + i) == "selected") {
            $(".category-" + i).css("display", "block");
            $(".category-btn-" + i + " a").attr("class", "selected");
        }
    }


    //Round corners
    //Use standard corners plug-in for timelines
    //$(".tl").corners("6px");
    //$("h2").corners("8px");
    //$(".button").corners("12px");
    //$(".button-sml").corners("8px");
    //$("#modal").corners("12px");
    //Use curvy corners plug-in where pixel borders are required
    //$(".search").corner();
    //$("#category-selector").corner();
    //$("#modal").corner();


    //Make timelines draggable along x axis only.
    $(".tl").draggable({
        axis: "x",
        opacity: 0.5,
        stop: function() {
            var leftPos = $(this).css("left");
            //To do: save leftPos via AJAX call to update start date of event
        }
    });


    //Vertical line which traces cursor relative to tl-container
    /*
    $(".tl-container").mousemove(function(e) {
    $(".x-axis-tracker").css("left", e.pageX - this.offsetLeft + 1 + "px");
    });
    */

    //Build timeline scale bar
    varScaleContent = '<div class="year-scale">today</div>';
    varCurrentYear = 2009;
    varEndYear = 2030;
    for (i = varCurrentYear; i <= varEndYear; i = i + 1) {
        varScaleContent += '<div class="year-scale">' + i + '</div>';
    }
    $(".tl-scale").prepend(varScaleContent);


    //Category view options
    $("#view-all").click(function() {
        for (i = 1; i <= totalCategories; i++) {
            $(".category-btn-" + i + " a").attr("class", "selected")
            if ($.cookie("category-" + i) != "selected") {
                $(".category-" + i).fadeToggle();
            }
        }
        return false;
    });
    $("#show-categories,#hide-categories").click(function() {
        $("#category-selector-container").fadeToggle();
        return false;
    });
    for (i = 1; i <= totalCategories; i++) {
        $(".category-btn-" + i + " a").click(function() {
            $(this).toggleClass("selected");
            var categoryId = $(this).parent().attr("class").replace("category-btn-", "");
            $(".category-" + categoryId).fadeToggle();
            $.cookie("category-" + categoryId, $(this).attr("class"), cookieOptions);
            return false;
        });
    }


    //Zoom buttons
    $("#zoom-in").click(function() {
        $(".tl").each(function() {
            varCurrentWidth = $(this).css("width").replace(/px/, "");
            varNewWidth = (varCurrentWidth / 100) * (100 + zoomFactor);
            varCurrentLeft = $(this).css("left").replace(/px/, "");
            varNewLeft = (varCurrentLeft / 100) * (100 + zoomFactor);
            $(this).animate({
                width: varNewWidth,
                left: varNewLeft
            }, zoomSpeed);
        });
        $(".year-scale").each(function() {
            varCurrentWidth = $(this).css("width").replace(/px/, "");
            varNewWidth = (varCurrentWidth / 100) * (100 + zoomFactor);
            varCurrentLeft = $(this).css("left").replace(/px/, "");
            varNewLeft = (varCurrentLeft / 100) * (100 + zoomFactor);
            $(this).animate({
                width: varNewWidth,
                left: varNewLeft
            }, zoomSpeed);
            if (varNewWidth >= 360) {
                $(this).prepend('<div class="month-scale">Jan</div><div class="month-scale">Feb</div><div class="month-scale">Mar</div><div class="month-scale">Apr</div><div class="month-scale">May</div><div class="month-scale">Jun</div><div class="month-scale">Jul</div><div class="month-scale">Aug</div><div class="month-scale">Sep</div><div class="month-scale">Oct</div><div class="month-scale">Nov</div><div class="month-scale">Dec</div>');
            }
        });
        return false;
    });

    $("#zoom-out").click(function() {
        $(".tl").each(function() {
            varCurrentWidth = $(this).css("width").replace(/px/, "");
            varNewWidth = (varCurrentWidth / 100) * (100 - zoomFactor);
            varCurrentLeft = $(this).css("left").replace(/px/, "");
            varNewLeft = (varCurrentLeft / 100) * (100 - zoomFactor);
            $(this).animate({
                width: varNewWidth,
                left: varNewLeft
            }, zoomSpeed);
        });
        $(".year-scale").each(function() {
            varCurrentWidth = $(this).css("width").replace(/px/, "");
            varNewWidth = (varCurrentWidth / 100) * (100 - zoomFactor);
            varCurrentLeft = $(this).css("left").replace(/px/, "");
            varNewLeft = (varCurrentLeft / 100) * (100 - zoomFactor);
            $(this).animate({
                width: varNewWidth,
                left: varNewLeft
            }, zoomSpeed);
            if (varNewWidth < 360) {
                $(".month-scale").remove();
            }
        });
        return false;
    });


    //Scroll buttons
    $("#scroll-back").click(function() {
        $(".tl").each(function() {
            varCurrentLeft = parseInt($(this).css("left").replace(/px/, ""));
            varNewLeft = (varCurrentLeft + scrollFactor);
            $(this).animate({
                left: varNewLeft
            }, zoomSpeed);
        });
        $(".year-scale").each(function() {
            varCurrentLeft = parseInt($(this).css("left").replace(/px/, ""));
            varNewLeft = (varCurrentLeft + scrollFactor);
            $(this).animate({
                left: varNewLeft
            }, zoomSpeed);
        });
        return false;
    });

    $("#scroll-forward").click(function() {
        $(".tl").each(function() {
            varCurrentLeft = parseInt($(this).css("left").replace(/px/, ""));
            varNewLeft = (varCurrentLeft - scrollFactor);
            $(this).animate({
                left: varNewLeft
            }, zoomSpeed);
        });
        $(".year-scale").each(function() {
            varCurrentLeft = parseInt($(this).css("left").replace(/px/, ""));
            varNewLeft = (varCurrentLeft - scrollFactor);
            $(this).animate({
                left: varNewLeft
            }, zoomSpeed);
        });
        return false;
    });


    //Load event details with AJAX and present in tooltip
    $(".tl").hover(function(e) {
        var timelineTopOffset = 2;
        var eventWidth = 219;   //Width of event 'pop-up' to calculate offset if near to being off page
        var eventHeight = 96;   //Height of event 'pop-up' to calculate offset if near to being off page
        var xEffectiveLimit = 756;
        var yEffectiveLimit = $(".tl-container").height() - eventHeight;
        var timelineContainer = $(".tl-container").position();
        var masterContainer = $("#container").position();
        var masterContainerLeft = masterContainer.left;
        var timelineContainerTop = timelineContainer.top;
        var xPosRelToPage = e.pageX;
        var yPosRelToPage = e.pageY;
        var xOffset = this.offsetLeft
        var yOffset = this.offsetTop
        var x = xPosRelToPage - xOffset - masterContainerLeft;
        var y = yPosRelToPage - yOffset - timelineContainerTop - timelineTopOffset;
        var xLimit = xPosRelToPage - masterContainerLeft;
        var yLimit = yPosRelToPage - timelineContainerTop;
        var noCacheValue = Math.random();

        if (xLimit > xEffectiveLimit) {
            x = x - eventWidth;
        }
        if (yLimit > yEffectiveLimit) {
            y = y - eventHeight;
        }

        $(this).append('<div class="event-teaser" style="left: ' + x + 'px; top: ' + y + 'px"></div>');
        $(".event-teaser",this).load("event.htm");
        $(".event-teaser",this).fadeIn();
    }, function() {
        $(".event-teaser", this).fadeOut().remove();
    });


	//Modal windows
    //Create modal window from regular links with class '.modal'
    $(".modal").click(function() {
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src",modalURL);
        $("#modal-container, #modal-background").fadeIn();
        return false;
    });
	//Use livequery to bind modal to AJAX-created content links
	$('.modal').livequery('click', function(event) { 
		var modalURL = $(this).attr("href");
		$("#modal-container iframe").attr("src",modalURL);
        $("#modal-container, #modal-background").fadeIn();
        return false;
    });
	

    //Search form
    //Style input box text and remove sample value
    $("#what").focus(function() {
        var currentValue = $(this).attr("value");
        if (currentValue = "e.g. climb Everest") {
            $(this).attr("value", "");
        }
        $(this).css("color", "#9b9885")
    });
    $("#what").blur(function() {
        var currentValue = $(this).attr("value");
        if (currentValue = "") {
            $(this).attr("value", "e.g. climb Everest");
        }
        $(this).css("color", "#ccc")
    });


    //Hide all modals if clicking outside
    $("body").click(function() {
        $("#modal-container, #modal-background").fadeOut();
        $("#category-selector-container").fadeOut();
    });
});