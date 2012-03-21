$(document).ready(function() {

    var zoomSpeed = 500;
    var timlineScrollPixels = 500;

    //Corners
    //	$(".search, .button-sml, .button-lrg, #category-selector, .tl, .event-teaser, #modal-container, #controls, .refresh-timeline, .add-find, .advanced-search-table, .button").corner();

    //* New	

    $("#imgMngT").bind("click", function() {

        var myDiv4 = $("#timeline-band-4");
        var myDiv5 = $("#divouter-5");
        var myMain = $("#my-container");
        var mySMain = $("#my-timeline");
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);

        if ($(this).attr("src") === "images/T_Close.jpg") {
            $(this).attr("src", "images/T_Open.jpg");
            $("#divouter-1").hide();

            if (sPage == "search2.aspx" || sPage == "userTimeline.aspx") {
                myDiv4.css("top", "10px");
                myDiv5.css("top", "42px");
                myMain.css("height", "306px");
                mySMain.css("height", "308px");

                if (document.getElementById('divouter-5').style.display == 'none') {
                    myMain.css("height", "40px");

                }

                if (document.getElementById("scrollArea1") != null) {
                    document.getElementById("scrollArea1").style.top = '87px';
                }
            }
            else {
                myMain.css("height", "0px");
            }
            //$("#.tl-container").animate({ "height": "305" }, zoomSpeed);		        	        
            return false;
        }
        else {
            $(this).attr("src", "images/T_Close.jpg");
            $("#divouter-1").show();

            //maxWindow();

            if (sPage == "search2.aspx" || sPage == "userTimeline.aspx") {
                myDiv4.css("top", "280px");
                myDiv5.css("top", "46px");
                myMain.css("height", "579px");
                mySMain.css("height", "579px");

                if (document.getElementById('divouter-5').style.display == 'none') {
                    myMain.css("height", "310px");
                }

                if ($.browser.mozilla || $.browser.safari || $.browser.opera || $.browser.crome) {
                    performFiltering(tl, [1, 2, 3, 4, 5], document.getElementById("table-filter"));
                }

                if (document.getElementById("scrollArea1") != null) {
                    document.getElementById("scrollArea1").style.top = '359px';
                }
            }
            else {
                myMain.css("height", "265px");
                mySMain.css("height", "265px");

                if ($.browser.mozilla || $.browser.safari || $.browser.opera || $.browser.crome) {
                    performFiltering(tl, [1, 2], document.getElementById("table-filter"));
                }
            }
            return false;
        }
    });

    //*

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

    $("#show-categories, .close-controls").click(function() {

        var snPath = window.location.pathname;
        var snPage = snPath.substring(snPath.lastIndexOf('/') + 1);

        if ($.browser.msie) {
            $("#controls").css("top", "201px");
            $("#controls").css("left", "33px");            

            if (snPage == "search2.aspx") {
                $("#controls").css("top", "345px");
            }
        }        
        else {

            if (snPage == "userTimeline.aspx") {
                $("#controls").css("top", "190px");
            }

            if (snPage == "search2.aspx") {
                $("#controls").css("top", "335px");
            }
        }
        $("#controls").toggle();
        return false;
    });

    //New*
    $("#table-filter, .close-controls").click(function() {
        $("#controls").toggle();
        return false;
    });
    //*

    $("#view-all").click(function() {
        clearAll(tl, [1, 2, 4, 5], document.getElementById("table-filter"));
    });

    $("#scroll-back").click(function() {
        tl.getBand(1)._autoScroll(timlineScrollPixels);
        AdjustDiv();
        return false;
    });

    $("#scroll-forward").click(function() {
        tl.getBand(1)._autoScroll(-timlineScrollPixels);
        AdjustDiv();
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
        $("#modal-container iframe").attr("src", "");
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

    $('.misc-pop-up-link').click(function() {
        $('.misc-pop-up').css('display', 'block');
        return false;
    });

    $('.misc-pop-up-link-close').click(function() {
        $('.misc-pop-up').css('display', 'none');
        return false;
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

function closeModal()
{
    $("#modal-container, #modal-background").fadeOut();
    $("#modal-container iframe").attr("src", "");
    return false;
};

function maxWindow()
{
 window.resizeTo(screen.width, screen.height - 20);
 window.moveTo(0, 0);
 window.resizeTo(screen.width, screen.height);
}