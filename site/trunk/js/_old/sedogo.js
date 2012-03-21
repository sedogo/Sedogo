function highlightSave()
{
    try
    {
        var oElement = document.getElementById('ctl00_ContentMain_saveButton');
        oElement.className = 'highlightButton';
    }
    catch(e) {}
}
function noMasterPageHighlightSave()
{
    var oElement = document.getElementById('saveButton');
    oElement.className = 'highlightButton';
}
function formatString(value, decPlaces)
{
	if( !isNaN(value) )
	{
		value = parseFloat(value);
	}
	if( isNaN(value) )
	{
		value = 0;
	}
	return value.toFixed(decPlaces);
}
function stripBlanks(inputStr) 
{
    var szInputString = inputStr;
    var szNewString = "";
    var nStartPos = 0;
    var nEndPos = szInputString.length-1;
    var nLength = szInputString.length-1;
    var nFound = 0;

    for( var i=0 ; i<szInputString.length ; i++ )
    {
        if( (nFound == 0) && (szInputString.charAt(i) != " ") )
        {
            nStartPos = i;        
            nFound = 1;
        }
    }
    nFound = 0;
    for( var j=nLength ; j>=0 ; j-- )
    {
        if( (nFound == 0) && (szInputString.charAt(j) != " ") )
        {
            nEndPos = j;        
            nFound = 1;
        }
    }
    if( nFound == 0 )
        szNewString = "";
    else
        szNewString = szInputString.substring(nStartPos,nEndPos+1);
    return szNewString;
}
// general purpose function to see if an input value has been entered at all
function isEmpty(inputStr) 
{
	if (inputStr == null || stripBlanks(inputStr) == "") {
		return true
	}
	return false
}
// general purpose function to see if a suspected numeric input is a positive integer
function isPosInteger(inputVal) {
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// general purpose function to see if a suspected numeric input is a positive or negative integer
function isInteger(inputVal) {
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (i == 0 && oneChar == "-") {
			continue
		}
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// general purpose function to see if a suspected numeric input is a positive or negative number
function isNumber(inputVal) {
	oneDecimal = false
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (i == 0 && oneChar == "-") {
			continue
		}
		if (oneChar == "." && !oneDecimal) {
			oneDecimal = true
			continue
		}
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// check the entered month for too high a value
function checkMonthLength(mm,dd) 
{
	var months = new Array("","January","February","March","April","May","June","July","August","September","October","November","December")
	if ((mm == 4 || mm == 6 || mm == 9 || mm == 11) && dd > 30)
	{
		//alert(months[mm] + " has only 30 days.")
		return false
	}
	else if (dd > 31)
	{
		//alert(months[mm] + " has only 31 days.")
		return false
	}
	return true
}

// check the entered February date for too high a value 
function checkLeapMonth(mm,dd,yyyy)
{
	if (yyyy % 4 > 0 && dd > 28)
	{
		//alert("February of " + yyyy + " has only 28 days.")
		return false
	}
	else if (dd > 29) 
	{
		//alert("February of " + yyyy + " has only 29 days.")
		return false
	}
	return true
}
// Validate a date
function validateDate( szDate, szDateFormat )
{
	if( isEmpty(szDate) )
	{
		// No value entered for date or format
		return false
	}
	else
	{
		// convert hyphen delimiters to slashes
		while (szDate.indexOf("-") != -1)
		{
			szDate = replaceString(szDate,"-","/")
		}
		var delim1 = szDate.indexOf("/")
		var delim2 = szDate.lastIndexOf("/")
		if (delim1 != -1 && delim1 == delim2) 
		{
			// there is only one delimiter in the string
			return false
		}
		if( szDateFormat == "d/m/y" || szDateFormat == "dmy" )
		{
			var dd = parseInt(szDate.substring(0,delim1),10)
			var mm = parseInt(szDate.substring(delim1 + 1,delim2),10)
			var yyyy = parseInt(szDate.substring(delim2 + 1, szDate.length),10)
		}
		else
		{
			var mm = parseInt(szDate.substring(0,delim1),10)
			var dd = parseInt(szDate.substring(delim1 + 1,delim2),10)
			var yyyy = parseInt(szDate.substring(delim2 + 1, szDate.length),10)
		}
		//alert(dd + " " + mm + " " + yyyy);
		if (isNaN(mm) || isNaN(dd) || isNaN(yyyy)) {
			// there is a non-numeric character in one of the component values
			return false
		}
		if (mm < 1 || mm > 12) {
			// month value is not 1 thru 12
			return false
		}
		if (dd < 1 || dd > 31) {
			// date value is not 1 thru 31
			return false
		}
		// validate year
		if (yyyy < 100) 
		{
			// entered value is two digits, which we allow for 1930-2029
			if (yyyy >= 30)
			{
				yyyy += 1900
			}
			else
			{
				yyyy += 2000
			}
		}
		if (!checkMonthLength(mm,dd)) 
		{
			return false
		}
		if (mm == 2) 
		{
			if (!checkLeapMonth(mm,dd,yyyy)) 
			{
				return false
			}
		}
		//alert("Checking year range");
		if ((yyyy < 1980) || (yyyy > 2100)) 
		{
			return false
		}
	}
	return true
}
// Validate a time
function validateTime( szTime )
{
	if( isEmpty(szTime) )
	{
		// No value entered for date or format
		return false
	}
	else
	{
		// convert dot delimiters to colons
		while (szTime.indexOf(".") != -1)
		{
			szTime = replaceString(szTime,".",":")
		}
		var delim1 = szTime.indexOf(":")
		var hh = parseInt(szTime.substring(0,delim1),10)
		var mm = parseInt(szTime.substring(delim1 + 1, szTime.length),10)
		if (isNaN(hh) || isNaN(mm)) {
			// there is a non-numeric character in one of the component values
			return false
		}
		if (hh < 0 || hh > 23) {
			// hour value is not 0 thru 23
			return false
		}
		if (mm < 0 || mm > 59) {
			// date value is not 0 thru 59
			return false
		}
	}
	return true
}
// Validate an IP address
function validateIPAddress( szIPAddress )
{
    var arIPAddress = szIPAddress.split(".");
    if( arIPAddress.length != 4 )
    {
        return false;
    }
    else
    {
        for( i=0 ; i<4 ; i++ )
        {
            if( (arIPAddress[i] < 0) || (arIPAddress[i] > 255) )
            {
                return false;
            }
        }
    }
	return true
}
// ***********************************************
// FORMATTING FUNCTIONS
// ***********************************************
// Format number
function formatNumber( expr, decplaces )
{
	var str = "" + Math.round( eval(expr) * Math.pow(10,decplaces) )
	while( str.length <= decplaces )
	{
		str = "0" + str
	}
	var decpoint = str.length - decplaces
	return str.substring(0,decpoint) + "." + str.substring( decpoint, str.length );
}
// ***********************************************
// MISC FUNCTIONS
// ***********************************************
function getBrowser()
{
	if( navigator.appName.indexOf("Netscape") > -1 )
	{
		return "Netscape";
	}
	if( navigator.appName.indexOf("Explorer") > -1 )
	{
		return "Explorer";
	}
}
function getVersion()
{
	return parseFloat( navigator.appVersion );
}
function getPlatform()
{
	if( navigator.appVersion.indexOf("Windows") > -1 )
	{
		return "Windows";
	}
	if( navigator.appVersion.indexOf("Macintosh") > -1 )
	{
		return "Macintosh";
	}
	if( navigator.appVersion.indexOf("X11") > -1 )
	{
		return "Unix";
	}
	if( navigator.appVersion.indexOf("Unix") > -1 )
	{
		return "Unix";
	}
	return "Unknown";
}
function replaceString(mainStr,searchStr,replaceStr) 
{ 
	var front = getFront(mainStr,searchStr); 
	var end = getEnd(mainStr,searchStr);
	if (front != null && end != null) 
	{ 
		return front + replaceStr + end 
	} 
	return null
}
function getFront(mainStr,searchStr)
{
	foundOffset = mainStr.indexOf(searchStr)
	if (foundOffset == -1)
	{
		return null;
	}
	return mainStr.substring(0,foundOffset);
}
function getEnd(mainStr,searchStr)
{
	foundOffset = mainStr.indexOf(searchStr)
	if (foundOffset == -1) 
	{
		return null;
	}
	return mainStr.substring(foundOffset+searchStr.length,mainStr.length)
}





function stripBlanks(inputStr) 
{
    var szInputString = inputStr;
    var szNewString = "";
    var nStartPos = 0;
    var nEndPos = szInputString.length-1;
    var nLength = szInputString.length-1;
    var nFound = 0;

    for( var i=0 ; i<szInputString.length ; i++ )
    {
        if( (nFound == 0) && (szInputString.charAt(i) != " ") )
        {
            nStartPos = i;        
            nFound = 1;
        }
    }
    nFound = 0;
    for( var j=nLength ; j>=0 ; j-- )
    {
        if( (nFound == 0) && (szInputString.charAt(j) != " ") )
        {
            nEndPos = j;        
            nFound = 1;
        }
    }
    if( nFound == 0 )
        szNewString = "";
    else
        szNewString = szInputString.substring(nStartPos,nEndPos+1);
    return szNewString;
}
// general purpose function to see if an input value has been entered at all
function isEmpty(inputStr) 
{
	if (inputStr == null || stripBlanks(inputStr) == "") {
		return true
	}
	return false
}
// general purpose function to see if a suspected numeric input is a positive integer
function isPosInteger(inputVal) {
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// general purpose function to see if a suspected numeric input is a positive or negative integer
function isInteger(inputVal) {
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (i == 0 && oneChar == "-") {
			continue
		}
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// general purpose function to see if a suspected numeric input is a positive or negative number
function isNumber(inputVal) {
	oneDecimal = false
	inputStr = inputVal.toString()
	for (var i = 0; i < inputStr.length; i++) {
		var oneChar = inputStr.charAt(i)
		if (i == 0 && oneChar == "-") {
			continue
		}
		if (oneChar == "." && !oneDecimal) {
			oneDecimal = true
			continue
		}
		if (oneChar < "0" || oneChar > "9") {
			return false
		}
	}
	return true
}
// check the entered month for too high a value
function checkMonthLength(mm,dd) 
{
	var months = new Array("","January","February","March","April","May","June","July","August","September","October","November","December")
	if ((mm == 4 || mm == 6 || mm == 9 || mm == 11) && dd > 30)
	{
		//alert(months[mm] + " has only 30 days.")
		return false
	}
	else if (dd > 31)
	{
		//alert(months[mm] + " has only 31 days.")
		return false
	}
	return true
}

// check the entered February date for too high a value 
function checkLeapMonth(mm,dd,yyyy)
{
	if (yyyy % 4 > 0 && dd > 28)
	{
		//alert("February of " + yyyy + " has only 28 days.")
		return false
	}
	else if (dd > 29) 
	{
		//alert("February of " + yyyy + " has only 29 days.")
		return false
	}
	return true
}
// Validate a date
function validateDate( szDate, szDateFormat )
{
	if( isEmpty(szDate) )
	{
		// No value entered for date or format
		return false
	}
	else
	{
		// convert hyphen delimiters to slashes
		while (szDate.indexOf("-") != -1)
		{
			szDate = replaceString(szDate,"-","/")
		}
		var delim1 = szDate.indexOf("/")
		var delim2 = szDate.lastIndexOf("/")
		if (delim1 != -1 && delim1 == delim2) 
		{
			// there is only one delimiter in the string
			return false
		}
		if( szDateFormat == "d/m/y" || szDateFormat == "dmy" )
		{
			var dd = parseInt(szDate.substring(0,delim1),10)
			var mm = parseInt(szDate.substring(delim1 + 1,delim2),10)
			var yyyy = parseInt(szDate.substring(delim2 + 1, szDate.length),10)
		}
		else
		{
			var mm = parseInt(szDate.substring(0,delim1),10)
			var dd = parseInt(szDate.substring(delim1 + 1,delim2),10)
			var yyyy = parseInt(szDate.substring(delim2 + 1, szDate.length),10)
		}
		//alert(dd + " " + mm + " " + yyyy);
		if (isNaN(mm) || isNaN(dd) || isNaN(yyyy)) {
			// there is a non-numeric character in one of the component values
			return false
		}
		if (mm < 1 || mm > 12) {
			// month value is not 1 thru 12
			return false
		}
		if (dd < 1 || dd > 31) {
			// date value is not 1 thru 31
			return false
		}
		// validate year
		if (yyyy < 100) 
		{
			// entered value is two digits, which we allow for 1930-2029
			if (yyyy >= 30)
			{
				yyyy += 1900
			}
			else
			{
				yyyy += 2000
			}
		}
		if (!checkMonthLength(mm,dd)) 
		{
			return false
		}
		if (mm == 2) 
		{
			if (!checkLeapMonth(mm,dd,yyyy)) 
			{
				return false
			}
		}
		//alert("Checking year range");
		if ((yyyy < 1980) || (yyyy > 2100)) 
		{
			return false
		}
	}
	return true
}
// Validate a time
function validateTime( szTime )
{
	if( isEmpty(szTime) )
	{
		// No value entered for date or format
		return false
	}
	else
	{
		// convert dot delimiters to colons
		while (szTime.indexOf(".") != -1)
		{
			szTime = replaceString(szTime,".",":")
		}
		var delim1 = szTime.indexOf(":")
		var hh = parseInt(szTime.substring(0,delim1),10)
		var mm = parseInt(szTime.substring(delim1 + 1, szTime.length),10)
		if (isNaN(hh) || isNaN(mm)) {
			// there is a non-numeric character in one of the component values
			return false
		}
		if (hh < 0 || hh > 23) {
			// hour value is not 0 thru 23
			return false
		}
		if (mm < 0 || mm > 59) {
			// date value is not 0 thru 59
			return false
		}
	}
	return true
}
// Validate an IP address
function validateIPAddress( szIPAddress )
{
    var arIPAddress = szIPAddress.split(".");
    if( arIPAddress.length != 4 )
    {
        return false;
    }
    else
    {
        for( i=0 ; i<4 ; i++ )
        {
            if( (arIPAddress[i] < 0) || (arIPAddress[i] > 255) )
            {
                return false;
            }
        }
    }
	return true
}
// ***********************************************
// FORMATTING FUNCTIONS
// ***********************************************
// Format number
function formatNumber( expr, decplaces )
{
	var str = "" + Math.round( eval(expr) * Math.pow(10,decplaces) )
	while( str.length <= decplaces )
	{
		str = "0" + str
	}
	var decpoint = str.length - decplaces
	return str.substring(0,decpoint) + "." + str.substring( decpoint, str.length );
}
// ***********************************************
// MISC FUNCTIONS
// ***********************************************
function getBrowser()
{
	if( navigator.appName.indexOf("Netscape") > -1 )
	{
		return "Netscape";
	}
	if( navigator.appName.indexOf("Explorer") > -1 )
	{
		return "Explorer";
	}
}
function getVersion()
{
	return parseFloat( navigator.appVersion );
}
function getPlatform()
{
	if( navigator.appVersion.indexOf("Windows") > -1 )
	{
		return "Windows";
	}
	if( navigator.appVersion.indexOf("Macintosh") > -1 )
	{
		return "Macintosh";
	}
	if( navigator.appVersion.indexOf("X11") > -1 )
	{
		return "Unix";
	}
	if( navigator.appVersion.indexOf("Unix") > -1 )
	{
		return "Unix";
	}
	return "Unknown";
}
function replaceString(mainStr,searchStr,replaceStr) 
{ 
	var front = getFront(mainStr,searchStr); 
	var end = getEnd(mainStr,searchStr);
	if (front != null && end != null) 
	{ 
		return front + replaceStr + end 
	} 
	return null
}
function getFront(mainStr,searchStr)
{
	foundOffset = mainStr.indexOf(searchStr)
	if (foundOffset == -1)
	{
		return null;
	}
	return mainStr.substring(0,foundOffset);
}
function getEnd(mainStr,searchStr)
{
	foundOffset = mainStr.indexOf(searchStr)
	if (foundOffset == -1) 
	{
		return null;
	}
	return mainStr.substring(foundOffset+searchStr.length,mainStr.length)
}




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



/**
* DD_roundies, this adds rounded-corner CSS in standard browsers and VML sublayers in IE that accomplish a similar appearance when comparing said browsers.
* Author: Drew Diller
* Email: drew.diller@gmail.com
* URL: http://www.dillerdesign.com/experiment/DD_roundies/
* Version: 0.0.2a -  preview 2008.12.26
* Licensed under the MIT License: http://dillerdesign.com/experiment/DD_roundies/#license
*
* Usage:
* DD_roundies.addRule('#doc .container', '10px 5px'); // selector and multiple radii
* DD_roundies.addRule('.box', 5, true); // selector, radius, and optional addition of border-radius code for standard browsers.
* 
* Just want the PNG fixing effect for IE6, and don't want to also use the DD_belatedPNG library?  Don't give any additional arguments after the CSS selector.
* DD_roundies.addRule('.your .example img');
**/

eval(function(p,a,c,k,e,r){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)r[e(c)]=k[c]||e(c);k=[function(e){return r[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('t K={16:\'K\',1L:G,1M:G,1d:G,2f:y(){u(D.2g!=8&&D.1N&&!D.1N[q.16]){q.1L=M;q.1M=M}17 u(D.2g==8){q.1d=M}},2h:D.2i,1O:[],1b:{},2j:y(){u(q.1L||q.1M){D.1N.2L(q.16,\'2M:2N-2O-2P:x\')}u(q.1d){D.2Q(\'<?2R 2S="\'+q.16+\'" 2T="#1P#2k" ?>\')}},2l:y(){t a=D.1k(\'z\');D.2m.1w.1Q(a,D.2m.1w.1w);u(a.12){2n{t b=a.12;b.1x(q.16+\'\\\\:*\',\'{1l:2U(#1P#2k)}\');q.12=b}2o(2p){}}17{q.12=a}},1x:y(a,b,c){u(1R b==\'1S\'||b===2V){b=0}u(b.2W.2q().1y(\'2X\')==-1){b=b.2q().2Y(/[^0-9 ]/g,\'\').1T(\' \')}H(t i=0;i<4;i++){b[i]=(!b[i]&&b[i]!==0)?b[C.1e((i-2),0)]:b[i]}u(q.12){u(q.12.1x){t d=a.1T(\',\');H(t i=0;i<d.1U;i++){q.12.1x(d[i],\'1l:2Z(K.1V.2r(q, [\'+b.1W(\',\')+\']))\')}}17 u(c){t e=b.1W(\'F \')+\'F\';q.12.1z(D.2s(a+\' {Q-1f:\'+e+\'; -30-Q-1f:\'+e+\';}\'));q.12.1z(D.2s(a+\' {-1A-Q-1m-1n-1f:\'+b[0]+\'F \'+b[0]+\'F; -1A-Q-1m-1X-1f:\'+b[1]+\'F \'+b[1]+\'F; -1A-Q-1Y-1X-1f:\'+b[2]+\'F \'+b[2]+\'F; -1A-Q-1Y-1n-1f:\'+b[3]+\'F \'+b[3]+\'F;}\'))}}17 u(q.1d){q.1O.31({\'2t\':a,\'2u\':b})}},2v:y(a){2w(32.33){I\'z.Q\':I\'z.34\':I\'z.1B\':q.1o(a);13;I\'z.2x\':q.1Z(a);13;I\'z.1p\':I\'z.2y\':I\'z.2z\':q.1o(a);13;I\'z.20\':a.18.z.20=(a.z.20==\'S\')?\'S\':\'35\';13;I\'z.21\':q.22(a);13;I\'z.1c\':a.18.z.1c=a.z.1c;13}},1o:y(a){a.14.23=\'\';q.2A(a);q.1Z(a);q.1C(a);q.1D(a);q.24(a);q.2B(a);q.22(a)},22:y(a){u(a.W.21.1y(\'36\')!=-1){t b=a.W.21;b=1g(b.37(b.25(\'=\')+1,b.25(\')\')),10)/2C;H(t v 1h a.x){a.x[v].1i.38=b}}},2A:y(a){u(!a.W){1q}17{t b=a.W}a.14.1p=\'\';a.14.1E=\'\';t c=(b.1p==\'2D\');t d=M;u(b.1E!=\'S\'||a.1F){u(!a.1F){a.J=b.1E;a.J=a.J.39(5,a.J.25(\'")\')-5)}17{a.J=a.26}t e=q;u(!e.1b[a.J]){t f=D.1k(\'3a\');f.1r(\'3b\',y(){q.1s=q.3c;q.1t=q.3d;e.1D(a)});f.3e=e.16+\'3f\';f.14.23=\'1l:S; 1j:27; 1m:-2E; 1n:-2E; Q:S;\';f.26=a.J;f.2F(\'1s\');f.2F(\'1t\');D.2G.1Q(f,D.2G.1w);e.1b[a.J]=f}a.x.Z.1i.26=a.J;d=G}a.x.Z.2H=!d;a.x.Z.1G=\'S\';a.x.1u.2H=!c;a.x.1u.1G=b.1p;a.14.1E=\'S\';a.14.1p=\'2D\'},1Z:y(a){a.x.1H.1G=a.W.2x},1C:y(a){t c=[\'N\',\'19\',\'1a\',\'O\'];a.P={};H(t b=0;b<4;b++){a.P[c[b]]=1g(a.W[\'Q\'+c[b]+\'U\'],10)||0}},1D:y(c){t e=[\'O\',\'N\',\'U\',\'V\'];H(t d=0;d<4;d++){c.E[e[d]]=c[\'3g\'+e[d]]}t f=y(a,b){a.z.1n=(b?0:c.E.O)+\'F\';a.z.1m=(b?0:c.E.N)+\'F\';a.z.1s=c.E.U+\'F\';a.z.1t=c.E.V+\'F\'};H(t v 1h c.x){t g=(v==\'Z\')?1:2;c.x[v].3h=(c.E.U*g)+\', \'+(c.E.V*g);f(c.x[v],M)}f(c.18,G);u(K.1d){c.x.1H.z.28=\'-3i\';u(1R c.P==\'1S\'){q.1C(c)}c.x.1u.z.28=(c.P.N-1)+\'F \'+(c.P.O-1)+\'F\'}},24:y(j){t k=y(a,w,h,r,b,c,d){t e=a?[\'m\',\'1I\',\'l\',\'1J\',\'l\',\'1I\',\'l\',\'1J\',\'l\']:[\'1J\',\'l\',\'1I\',\'l\',\'1J\',\'l\',\'1I\',\'l\',\'m\'];b*=d;c*=d;w*=d;h*=d;t R=r.2I();H(t i=0;i<4;i++){R[i]*=d;R[i]=C.3j(w/2,h/2,R[i])}t f=[e[0]+C.11(0+b)+\',\'+C.11(R[0]+c),e[1]+C.11(R[0]+b)+\',\'+C.11(0+c),e[2]+C.15(w-R[1]+b)+\',\'+C.11(0+c),e[3]+C.15(w+b)+\',\'+C.11(R[1]+c),e[4]+C.15(w+b)+\',\'+C.15(h-R[2]+c),e[5]+C.15(w-R[2]+b)+\',\'+C.15(h+c),e[6]+C.11(R[3]+b)+\',\'+C.15(h+c),e[7]+C.11(0+b)+\',\'+C.15(h-R[3]+c),e[8]+C.11(0+b)+\',\'+C.11(R[0]+c)];u(!a){f.3k()}t g=f.1W(\'\');1q g};u(1R j.P==\'1S\'){q.1C(j)}t l=j.P;t m=j.2J.2I();t n=k(M,j.E.U,j.E.V,m,0,0,2);m[0]-=C.1e(l.O,l.N);m[1]-=C.1e(l.N,l.19);m[2]-=C.1e(l.19,l.1a);m[3]-=C.1e(l.1a,l.O);H(t i=0;i<4;i++){m[i]=C.1e(m[i],0)}t o=k(G,j.E.U-l.O-l.19,j.E.V-l.N-l.1a,m,l.O,l.N,2);t p=k(M,j.E.U-l.O-l.19+1,j.E.V-l.N-l.1a+1,m,l.O,l.N,1);j.x.1u.29=o;j.x.Z.29=p;j.x.1H.29=n+o;q.2K(j)},2B:y(a){t s=a.W;t b=[\'N\',\'O\',\'19\',\'1a\'];H(t i=0;i<4;i++){a.14[\'1B\'+b[i]]=(1g(s[\'1B\'+b[i]],10)||0)+(1g(s[\'Q\'+b[i]+\'U\'],10)||0)+\'F\'}a.14.Q=\'S\'},2K:y(e){t f=K;u(!e.J||!f.1b[e.J]){1q}t g=e.W;t h={\'X\':0,\'Y\':0};t i=y(a,b){t c=M;2w(b){I\'1n\':I\'1m\':h[a]=0;13;I\'3l\':h[a]=0.5;13;I\'1X\':I\'1Y\':h[a]=1;13;1P:u(b.1y(\'%\')!=-1){h[a]=1g(b,10)*0.3m}17{c=G}}t d=(a==\'X\');h[a]=C.15(c?((e.E[d?\'U\':\'V\']-(e.P[d?\'O\':\'N\']+e.P[d?\'19\':\'1a\']))*h[a])-(f.1b[e.J][d?\'1s\':\'1t\']*h[a]):1g(b,10));h[a]+=1};H(t b 1h h){i(b,g[\'2y\'+b])}e.x.Z.1i.1j=(h.X/(e.E.U-e.P.O-e.P.19+1))+\',\'+(h.Y/(e.E.V-e.P.N-e.P.1a+1));t j=g.2z;t c={\'T\':1,\'R\':e.E.U+1,\'B\':e.E.V+1,\'L\':1};t k={\'X\':{\'2a\':\'L\',\'2b\':\'R\',\'d\':\'U\'},\'Y\':{\'2a\':\'T\',\'2b\':\'B\',\'d\':\'V\'}};u(j!=\'2c\'){c={\'T\':(h.Y),\'R\':(h.X+f.1b[e.J].1s),\'B\':(h.Y+f.1b[e.J].1t),\'L\':(h.X)};u(j.1y(\'2c-\')!=-1){t v=j.1T(\'2c-\')[1].3n();c[k[v].2a]=1;c[k[v].2b]=e.E[k[v].d]+1}u(c.B>e.E.V){c.B=e.E.V+1}}e.x.Z.z.3o=\'3p(\'+c.T+\'F \'+c.R+\'F \'+c.B+\'F \'+c.L+\'F)\'},1v:y(a){t b=q;2d(y(){b.1o(a)},1)},2e:y(a){q.1D(a);q.24(a)},1V:y(b){q.z.1l=\'S\';u(!q.W){1q}17{t c=q.W}t d={3q:G,3r:G,3s:G,3t:G,3u:G,3v:G,3w:G};u(d[q.1K]===G){1q}t e=q;t f=K;q.2J=b;q.E={};t g={3x:\'2e\',3y:\'2e\'};u(q.1K==\'A\'){t i={3z:\'1v\',3A:\'1v\',3B:\'1v\',3C:\'1v\'};H(t a 1h i){g[a]=i[a]}}H(t h 1h g){q.1r(\'3D\'+h,y(){f[g[h]](e)})}q.1r(\'3E\',y(){f.2v(e)});t j=y(a){a.z.3F=1;u(a.W.1j==\'3G\'){a.z.1j=\'3H\'}};j(q.3I);j(q);q.18=D.1k(\'3J\');q.18.14.23=\'1l:S; 1j:27; 28:0; 1B:0; Q:0; 3K:S;\';q.18.z.1c=c.1c;q.x={\'1u\':M,\'Z\':M,\'1H\':M};H(t v 1h q.x){q.x[v]=D.1k(f.16+\':3L\');q.x[v].1i=D.1k(f.16+\':3M\');q.x[v].1z(q.x[v].1i);q.x[v].3N=G;q.x[v].z.1j=\'27\';q.x[v].z.1c=c.1c;q.x[v].3O=\'1,1\';q.18.1z(q.x[v])}q.x.Z.1G=\'S\';q.x.Z.1i.3P=\'3Q\';q.3R.1Q(q.18,q);q.1F=G;u(q.1K==\'3S\'){q.1F=M;q.z.3T=\'3U\'}2d(y(){f.1o(e)},1)}};2n{D.3V("3W",G,M)}2o(2p){}K.2f();K.2j();K.2l();u(K.1d&&D.1r&&K.2h){D.1r(\'3X\',y(){u(D.3Y==\'3Z\'){t d=K.1O;t e=d.1U;t f=y(a,b,c){2d(y(){K.1V.2r(a,b)},c*2C)};H(t i=0;i<e;i++){t g=D.2i(d[i].2t);t h=g.1U;H(t r=0;r<h;r++){u(g[r].1K!=\'40\'){f(g[r],d[i].2u,r)}}}}})}',62,249,'||||||||||||||||||||||||||this|||var|if|||vml|function|style|||Math|document|dim|px|false|for|case|vmlBg|DD_roundies||true|Top|Left|bW|border||none||Width|Height|currentStyle|||image||floor|styleSheet|break|runtimeStyle|ceil|ns|else|vmlBox|Right|Bottom|imgSize|zIndex|IE8|max|radius|parseInt|in|filler|position|createElement|behavior|top|left|applyVML|backgroundColor|return|attachEvent|width|height|color|pseudoClass|firstChild|addRule|search|appendChild|webkit|padding|vmlStrokeWeight|vmlOffsets|backgroundImage|isImg|fillcolor|stroke|qy|qx|nodeName|IE6|IE7|namespaces|selectorsToProcess|default|insertBefore|typeof|undefined|split|length|roundify|join|right|bottom|vmlStrokeColor|display|filter|vmlOpacity|cssText|vmlPath|lastIndexOf|src|absolute|margin|path|b1|b2|repeat|setTimeout|reposition|IEversion|documentMode|querySelector|querySelectorAll|createVmlNameSpace|VML|createVmlStyleSheet|documentElement|try|catch|err|toString|call|createTextNode|selector|radii|readPropertyChanges|switch|borderColor|backgroundPosition|backgroundRepeat|vmlFill|nixBorder|100|transparent|10000px|removeAttribute|body|filled|slice|DD_radii|clipImage|add|urn|schemas|microsoft|com|writeln|import|namespace|implementation|url|null|constructor|Array|replace|expression|moz|push|event|propertyName|borderWidth|block|lpha|substring|opacity|substr|img|onload|offsetWidth|offsetHeight|className|_sizeFinder|offset|coordsize|1px|min|reverse|center|01|toUpperCase|clip|rect|BODY|TABLE|TR|TD|SELECT|OPTION|TEXTAREA|resize|move|mouseleave|mouseenter|focus|blur|on|onpropertychange|zoom|static|relative|offsetParent|ignore|background|shape|fill|stroked|coordorigin|type|tile|parentNode|IMG|visibility|hidden|execCommand|BackgroundImageCache|onreadystatechange|readyState|complete|INPUT'.split('|'),0,{}))




/*
This license text has to stay intact at all times:
fleXcroll Public License Version
Cross Browser Custom Scroll Bar Script by Hesido.
Public version - Free for non-commercial uses.

This script cannot be used in any commercially built
web sites, or in sites that relates to commercial
activities. This script is not for re-distribution.
For licensing options:
Contact Emrah BASKAYA @ www.hesido.com

Derivative works are only allowed for personal uses,
and they cannot be redistributed.

FleXcroll Public Key Code: 20050907122003339
MD5 hash for this license: 9ada3be4d7496200ab2665160807745d

End of license text---
*/
//fleXcroll v1.9.5f
var fleXenv={
//*New
//fleXcrollInit:function(){if (SimileAjax.Platform.browser.isIE) {this.addTrggr(window,'load',this.globalInit)}else{this.addTrggr(window,'load',setTimeout(this.globalInit,2000))};},
fleXcrollInit:function(){this.addTrggr(window,'load',this.globalInit);},

fleXcrollMain:function(dDv){

var sPath = window.location.pathname;
var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
if(sPage=="search2.aspx" || sPage=="userTimeline.aspx")
{
    if(dDv.id=="divouter-5")
    {
        $('#imgMngT').click();
    }
}
//*New
//Main code beg
var dC=document,wD=window,nV=navigator;
if(!dC.getElementById||!dC.createElement)return;
if (typeof(dDv)=='string')dDv=document.getElementById(dDv);
if(dDv==null||nV.userAgent.indexOf('OmniWeb')!=-1||((nV.userAgent.indexOf('AppleWebKit')!=-1||nV.userAgent.indexOf('Safari')!=-1)&&!(typeof(HTMLElement)!="undefined"&&HTMLElement.prototype))||nV.vendor=='KDE'||(nV.platform.indexOf('Mac')!=-1&&nV.userAgent.indexOf('MSIE')!=-1))return;
if(dDv.scrollUpdate){dDv.scrollUpdate();return;};
if(!dDv.id||dDv.id==''){var sTid="flex__",c=1;while(document.getElementById(sTid+c)!=null){c++};dDv.id=sTid+c;}
var targetId=dDv.id;
dDv.fleXdata=new Object();var sC=dDv.fleXdata;
sC.keyAct={_37:['-1s',0],_38:[0,'-1s'],_39:['1s',0],_40:[0,'1s'],_33:[0,'-1p'],_34:[0,'1p'],_36:[0,'-100p'],_35:[0,'+100p']};
sC.wheelAct=["-2s","2s"];
sC.baseAct=["-2s","2s"];
var cDv=createDiv('contentwrapper',true),mDv=createDiv('mcontentwrapper',true),tDv=createDiv('scrollwrapper',true),pDv=createDiv('copyholder',true);
var iDv=createDiv('domfixdiv',true),fDv=createDiv('zoomdetectdiv',true),stdMode=false;
pDv.sY.border='1px solid blue';pDv.fHide();
dDv.style.overflow='hidden';
fDv.sY.fontSize="12px";fDv.sY.height="1em";fDv.sY.width="1em";fDv.sY.position="absolute";fDv.sY.zIndex="-999";fDv.fHide();
var brdHeight=dDv.offsetHeight,brdWidth=dDv.offsetWidth;
copyStyles(dDv,pDv,'0px',['border-left-width','border-right-width','border-top-width','border-bottom-width']);
var intlHeight=dDv.offsetHeight,intlWidth=dDv.offsetWidth,brdWidthLoss=brdWidth-intlWidth,brdHeightLoss=brdHeight-intlHeight;
var oScrollY=(dDv.scrollTop)?dDv.scrollTop:0,oScrollX=(dDv.scrollLeft)?dDv.scrollLeft:0;
var urlBase=document.location.href,uReg=/#([^#.]*)$/;
var focusProtectList=['textarea','input','select'];
sC.scroller=[];sC.forcedBar=[];sC.containerSize=sC.cntRSize=[];sC.contentSize=sC.cntSize=[];sC.edge=[false,false];
sC.reqS=[];sC.barSpace=[0,0];sC.forcedHide=[];sC.forcedPos=[];sC.paddings=[];
while (dDv.firstChild) {cDv.appendChild(dDv.firstChild)};
cDv.appendChild(iDv);dDv.appendChild(mDv);dDv.appendChild(pDv);
if(getStyle(dDv,'position')!='absolute') dDv.style.position="relative";

var dAlign=getStyle(dDv,'text-align');dDv.style.textAlign='left';
mDv.sY.width="100px";mDv.sY.height="100px";mDv.sY.top="0px";mDv.sY.left="0px";
copyStyles(dDv,pDv,"0px",['padding-left','padding-top','padding-right','padding-bottom']);
var postWidth=dDv.offsetWidth,postHeight=dDv.offsetHeight,mHeight;
mHeight=mDv.offsetHeight;mDv.sY.borderBottom="2px solid black";
if(mDv.offsetHeight>mHeight)stdMode=true;mDv.sY.borderBottomWidth="0px";
copyStyles(pDv,dDv,false,['padding-left','padding-top','padding-right','padding-bottom']);
findPos(mDv);findPos(dDv);
sC.paddings[0]=mDv.yPos-dDv.yPos;sC.paddings[2]=mDv.xPos-dDv.xPos;
dDv.style.paddingTop=getStyle(dDv,"padding-bottom");dDv.style.paddingLeft=getStyle(dDv,"padding-right");
findPos(mDv);findPos(dDv);
sC.paddings[1]=mDv.yPos-dDv.yPos;sC.paddings[3]=mDv.xPos-dDv.xPos;
dDv.style.paddingTop=getStyle(pDv,"padding-top");dDv.style.paddingLeft=getStyle(pDv,"padding-left");
var padWidthComp=sC.paddings[2]+sC.paddings[3],padHeightComp=sC.paddings[0]+sC.paddings[1];

mDv.style.textAlign=dAlign;
copyStyles(dDv,mDv,false,['padding-left','padding-right','padding-top','padding-bottom']);
tDv.sY.width=dDv.offsetWidth+'px';tDv.sY.height=dDv.offsetHeight+'px';
mDv.sY.width=postWidth+'px'; mDv.sY.height=postHeight+'px';
tDv.sY.position='absolute';tDv.sY.top='0px';tDv.sY.left='0px';
//tDv.fHide();

mDv.appendChild(cDv);dDv.appendChild(tDv);tDv.appendChild(fDv);

cDv.sY.position='relative';mDv.sY.position='relative';
cDv.sY.top="0";cDv.sY.width="100%";//fix IE7
mDv.sY.overflow='hidden';
mDv.sY.left="-"+sC.paddings[2]+"px";
mDv.sY.top="-"+sC.paddings[0]+"px";
sC.zTHeight=fDv.offsetHeight;

sC.getContentWidth=function(){
	var cChilds=cDv.childNodes,maxCWidth=compPad=0;
	for(var i=0;i<cChilds.length;i++){if(cChilds[i].offsetWidth){maxCWidth=Math.max(cChilds[i].offsetWidth,maxCWidth)}};
	sC.cntRSize[0]=((sC.reqS[1]&&!sC.forcedHide[1])||sC.forcedBar[1])?dDv.offsetWidth-sC.barSpace[0]:dDv.offsetWidth;
	sC.cntSize[0]=maxCWidth+padWidthComp;
	return sC.cntSize[0];
	};
sC.getContentHeight=function(){
	sC.cntRSize[1]=((sC.reqS[0]&&!sC.forcedHide[0])||sC.forcedBar[0])?dDv.offsetHeight-sC.barSpace[1]:dDv.offsetHeight;
	sC.cntSize[1]=cDv.offsetHeight+padHeightComp-2;
	return sC.cntSize[1];
	};

sC.fixIEDispBug=function(){cDv.sY.display='none';cDv.sY.display='block';};
sC.setWidth=function(){mDv.sY.width=(stdMode)?(sC.cntRSize[0]-padWidthComp-brdWidthLoss)+'px':sC.cntRSize[0]+'px';};
sC.setHeight=function(){mDv.sY.height=(stdMode)?(sC.cntRSize[1]-padHeightComp-brdHeightLoss)+'px':sC.cntRSize[1]+'px';};

sC.createScrollBars=function(){
	sC.getContentWidth();sC.getContentHeight();
	//vert
	tDv.vrt=new Array();var vrT=tDv.vrt;
	createScrollBars(vrT,'vscroller');
	vrT.barPadding=[parseInt(getStyle(vrT.sBr,'padding-top')),parseInt(getStyle(vrT.sBr,'padding-bottom'))];
	vrT.sBr.sY.padding='0px';vrT.sBr.curPos=0;vrT.sBr.vertical=true;
	vrT.sBr.indx=1; cDv.vBar=vrT.sBr;
	prepareScroll(vrT,0);sC.barSpace[0]=vrT.sDv.offsetWidth;sC.setWidth();
	//horiz
	tDv.hrz=new Array();var hrZ=tDv.hrz;
	createScrollBars(hrZ,'hscroller');
	hrZ.barPadding=[parseInt(getStyle(hrZ.sBr,'padding-left')),parseInt(getStyle(hrZ.sBr,'padding-right'))];
	hrZ.sBr.sY.padding='0px';hrZ.sBr.curPos=0;hrZ.sBr.vertical=false;
	hrZ.sBr.indx=0; cDv.hBar=hrZ.sBr;
	if(wD.opera) hrZ.sBr.sY.position='relative';
	prepareScroll(hrZ,0);
	sC.barSpace[1]=hrZ.sDv.offsetHeight;sC.setHeight();
	tDv.sY.height=dDv.offsetHeight+'px';
	// jog
	hrZ.jBox=createDiv('scrollerjogbox');
	tDv.appendChild(hrZ.jBox);hrZ.jBox.onmousedown=function(){
		hrZ.sBr.scrollBoth=true;sC.goScroll=hrZ.sBr;hrZ.sBr.clicked=true;
		hrZ.sBr.moved=false;tDv.vrt.sBr.moved=false;
		fleXenv.addTrggr(dC,'selectstart',retFalse);fleXenv.addTrggr(dC,'mousemove',mMoveBar);fleXenv.addTrggr(dC,'mouseup',mMouseUp);
		return false;
	};	
	
};

sC.goScroll=null;
sC.createScrollBars();
cDv.removeChild(iDv);

if(!this.addChckTrggr(dDv,'mousewheel',mWheelProc)||!this.addChckTrggr(dDv,'DOMMouseScroll',mWheelProc)){dDv.onmousewheel=mWheelProc;};
this.addChckTrggr(dDv,'mousewheel',mWheelProc);
this.addChckTrggr(dDv,'DOMMouseScroll',mWheelProc);
dDv.setAttribute('tabIndex','0');

this.addTrggr(dDv,'keydown',function(e){
	if(dDv.focusProtect) return;
	if(!e){var e=wD.event;};var pK=e.keyCode; sC.pkeY=pK;
	sC.mDPosFix();
	if(sC.keyAct['_'+pK]&&!window.opera){dDv.contentScroll(sC.keyAct['_'+pK][0],sC.keyAct['_'+pK][1],true);if(e.preventDefault) e.preventDefault();return false;};
	});
this.addTrggr(dDv,'keypress',function(e){//make Opera Happy
	if(dDv.focusProtect) return;
	if(!e){var e=wD.event;};var pK=e.keyCode;
	if(sC.keyAct['_'+pK]){dDv.contentScroll(sC.keyAct['_'+pK][0],sC.keyAct['_'+pK][1],true);if(e.preventDefault) e.preventDefault();return false;};
});

this.addTrggr(dDv,'keyup',function(){sC.pkeY=false});

this.addTrggr(dC,'mouseup',intClear);
this.addTrggr(dDv,'mousedown',function(e){
	if(!e) e=wD.event;
	var cTrgt=(e.target)?e.target:(e.srcElement)?e.srcElement:false;
	if(!cTrgt||(cTrgt.className&&cTrgt.className.match(RegExp("\\bscrollgeneric\\b")))) return;
	sC.inMposX=e.clientX;sC.inMposY=e.clientY;
	pageScrolled();findPos(dDv);intClear();
	fleXenv.addTrggr(dC,'mousemove',tSelectMouse);
	sC.mTBox=[dDv.xPos+10,dDv.xPos+sC.cntRSize[0]-10,dDv.yPos+10,dDv.yPos+sC.cntRSize[1]-10];
});

function tSelectMouse(e){if(!e) e=wD.event;
	var mX=e.clientX,mY=e.clientY,mdX=mX+sC.xScrld,mdY=mY+sC.yScrld;
	sC.mOnXEdge=(mdX<sC.mTBox[0]||mdX>sC.mTBox[1])?1:0;
	sC.mOnYEdge=(mdY<sC.mTBox[2]||mdY>sC.mTBox[3])?1:0;
	sC.xAw=mX-sC.inMposX;sC.yAw=mY-sC.inMposY;
	sC.sXdir=(sC.xAw>40)?1:(sC.xAw<-40)?-1:0;sC.sYdir=(sC.yAw>40)?1:(sC.yAw<-40)?-1:0;
	if((sC.sXdir!=0||sC.sYdir!=0)&&!sC.tSelectFunc) sC.tSelectFunc=wD.setInterval(function(){
		if(sC.sXdir==0&&sC.sYdir==0){wD.clearInterval(sC.tSelectFunc);sC.tSelectFunc=false;return;};pageScrolled();
		if(sC.mOnXEdge==1||sC.mOnYEdge==1) dDv.contentScroll((sC.sXdir*sC.mOnXEdge)+"s",(sC.sYdir*sC.mOnYEdge)+"s",true);
	},45)
};
function intClear(){
	fleXenv.remTrggr(dC,'mousemove',tSelectMouse);if(sC.tSelectFunc) wD.clearInterval(sC.tSelectFunc);sC.tSelectFunc=false;
	if(sC.barClickRetard) wD.clearTimeout(sC.barClickRetard); if(sC.barClickScroll) wD.clearInterval(sC.barClickScroll);
};
function pageScrolled(){
	sC.xScrld=(wD.pageXOffset)?wD.pageXOffset:(dC.documentElement&&dC.documentElement.scrollLeft)?dC.documentElement.scrollLeft:0;
	sC.yScrld=(wD.pageYOffset)?wD.pageYOffset:(dC.documentElement&&dC.documentElement.scrollTop)?dC.documentElement.scrollTop:0;
};

dDv.scrollUpdate=function(recurse){
	tDv.fShow();
	if(tDv.getSize[1]()===0||tDv.getSize[0]()===0) return;
	cDv.sY.padding='1px';var reqH=sC.reqS[0],reqV=sC.reqS[1],vBr=tDv.vrt,hBr=tDv.hrz,vUpReq,hUpReq,cPSize=[];
	tDv.sY.width=dDv.offsetWidth-brdWidthLoss+'px';tDv.sY.height=dDv.offsetHeight-brdHeightLoss+'px';
	cPSize[0]=sC.cntRSize[0];cPSize[1]=sC.cntRSize[1];
	sC.reqS[0]=sC.getContentWidth()>sC.cntRSize[0];
	sC.reqS[1]=sC.getContentHeight()>sC.cntRSize[1];
	var stateChange=(reqH!=sC.reqS[0]||reqV!=sC.reqS[1]||cPSize[0]!=sC.cntRSize[0]||cPSize[1]!=sC.cntRSize[1])?true:false;
	vBr.sDv.setVisibility(sC.reqS[1]);hBr.sDv.setVisibility(sC.reqS[0]);
	vUpReq=(sC.reqS[1]||sC.forcedBar[1]);hUpReq=(sC.reqS[0]||sC.forcedBar[0]);
	sC.getContentWidth();sC.getContentHeight();sC.setHeight();sC.setWidth();
	if(!sC.reqS[0]||!sC.reqS[1]||sC.forcedHide[0]||sC.forcedHide[1]) hBr.jBox.fHide();
	else hBr.jBox.fShow();
	if(vUpReq) updateScroll(vBr,(hUpReq&&!sC.forcedHide[0])?sC.barSpace[1]:0);else cDv.sY.top="0";
	if(hUpReq) updateScroll(hBr,(vUpReq&&!sC.forcedHide[1])?sC.barSpace[0]:0);else cDv.sY.left="0";
	if(stateChange&&!recurse) dDv.scrollUpdate(true);
	cDv.sY.padding='0px';
	sC.edge[0]=sC.edge[1]=false;
};

dDv.commitScroll=dDv.contentScroll=function(xPos,yPos,relative){
	var reT=[[false,false],[false,false]],Bar;
	if((xPos||xPos===0)&&sC.scroller[0]){xPos=calcCScrollVal(xPos,0);Bar=tDv.hrz.sBr;Bar.trgtScrll=(relative)?Math.min(Math.max(Bar.mxScroll,Bar.trgtScrll-xPos),0):-xPos;Bar.contentScrollPos();reT[0]=[-Bar.trgtScrll-Bar.targetSkew,-Bar.mxScroll]}
	if((yPos||yPos===0)&&sC.scroller[1]){yPos=calcCScrollVal(yPos,1);Bar=tDv.vrt.sBr;Bar.trgtScrll=(relative)?Math.min(Math.max(Bar.mxScroll,Bar.trgtScrll-yPos),0):-yPos;Bar.contentScrollPos();reT[1]=[-Bar.trgtScrll-Bar.targetSkew,-Bar.mxScroll]}
	if(!relative) sC.edge[0]=sC.edge[1]=false;
	
	return reT;
};

if(sPage == "HomeMoreDetail.aspx" || sPage == "MoreDetail.aspx")
    {
        $('#imgMngT').click();        
    }
if (sPage!="search2.aspx" || sPage!="userTimeline.aspx")   
    {
     try {
      if (MyTimelineClose)
       {
        if(MyTimelineClose == 'yes')
         {
         $('#imgMngT').click();  
         }
       }
     }
     catch (err)
     {   
        
     } 
   }   

dDv.scrollToElement=function(tEM){
if(tEM==null||!isddvChild(tEM)) return;
var sPos=findRCpos(tEM);
dDv.contentScroll(sPos[0]+sC.paddings[2],sPos[1]+sC.paddings[0],false);
dDv.contentScroll(0,0,true);
};

copyStyles(pDv,dDv,'0px',['border-left-width','border-right-width','border-top-width','border-bottom-width']);

dDv.removeChild(pDv);
dDv.scrollTop=0;dDv.scrollLeft=0;
dDv.fleXcroll=true;

classChange(dDv,'flexcrollactive',false);
dDv.scrollUpdate();
dDv.contentScroll(oScrollX,oScrollY,true);
if(urlBase.match(uReg)) {dDv.scrollToElement(dC.getElementById(urlBase.match(uReg)[1]));};
//tDv.fShow();

sC.sizeChangeDetect=wD.setInterval(function(){
var n=fDv.offsetHeight;if(n!=sC.zTHeight){dDv.scrollUpdate();sC.zTHeight=n};
},2500);

function calcCScrollVal(v,i){
	var stR=v.toString();v=parseFloat(stR);
	return parseInt((stR.match(/p$/))?v*sC.cntRSize[i]*0.9:(stR.match(/s$/))?v*sC.cntRSize[i]*0.1:v);
}
function camelConv(spL){
	var spL=spL.split('-'),reT=spL[0],i;
	for(i=1;parT=spL[i];i++) {reT +=parT.charAt(0).toUpperCase()+parT.substr(1);}
	return reT;
}
function getStyle(elem,style){
	if(wD.getComputedStyle) return wD.getComputedStyle(elem,null).getPropertyValue(style);
	if(elem.currentStyle) return elem.currentStyle[camelConv(style)];
	return false;
};

function copyStyles(src,dest,replaceStr,sList){
	var camelList = new Array();
	for (var i=0;i<sList.length;i++){
		camelList[i]=camelConv(sList[i]);
		dest.style[camelList[i]] = getStyle(src,sList[i],camelList[i]);
		if(replaceStr) src.style[camelList[i]] = replaceStr;
	}
};
function createDiv(typeName,noGenericClass){
	var nDiv=dC.createElement('div');//,pTx=dC.createTextNode('\u00a0');
	nDiv.id=targetId+'_'+typeName;
	nDiv.className=(noGenericClass)?typeName:typeName+' scrollgeneric';
	nDiv.getSize=[function(){return nDiv.offsetWidth;},function(){return nDiv.offsetHeight;}];
	nDiv.setSize=[function(sVal){nDiv.sY.width=sVal;},function(sVal){nDiv.sY.height=sVal;}];
	nDiv.getPos=[function(){return getStyle(nDiv,"left");},function(){return getStyle(nDiv,"top");}];
	nDiv.setPos=[function(sVal){nDiv.sY.left=sVal;},function(sVal){nDiv.sY.top=sVal;}];
	nDiv.fHide=function(){nDiv.sY.visibility="hidden"};
	nDiv.fShow=function(coPy){nDiv.sY.visibility=(coPy)?getStyle(coPy,'visibility'):"visible"};
	nDiv.sY=nDiv.style;
//	if(!noGenericClass) nDiv.appendChild(pTx);
	return nDiv;
	
};
function createScrollBars(ary,bse){
	ary.sDv=createDiv(bse+'base');ary.sFDv=createDiv(bse+'basebeg');
	ary.sSDv=createDiv(bse+'baseend');ary.sBr=createDiv(bse+'bar');
	ary.sFBr=createDiv(bse+'barbeg');ary.sSBr=createDiv(bse+'barend');
	tDv.appendChild(ary.sDv);ary.sDv.appendChild(ary.sBr);
	ary.sDv.appendChild(ary.sFDv);ary.sDv.appendChild(ary.sSDv);
	ary.sBr.appendChild(ary.sFBr);ary.sBr.appendChild(ary.sSBr);
};
function prepareScroll(bAr,reqSpace){
	var sDv=bAr.sDv,sBr=bAr.sBr,i=sBr.indx;
	sBr.minPos=bAr.barPadding[0];
	sBr.ofstParent=sDv;
	sBr.mDv=mDv;
	sBr.scrlTrgt=cDv;
	sBr.targetSkew=0;
	updateScroll(bAr,reqSpace,true);
	
	sBr.doScrollPos=function(){
		sBr.curPos=(Math.min(Math.max(sBr.curPos,0),sBr.maxPos));
		sBr.trgtScrll=parseInt((sBr.curPos/sBr.sRange)*sBr.mxScroll);
		sBr.targetSkew=(sBr.curPos==0)?0:(sBr.curPos==sBr.maxPos)?0:sBr.targetSkew;
		sBr.setPos[i](sBr.curPos+sBr.minPos+"px");
		cDv.setPos[i](sBr.trgtScrll+sBr.targetSkew+"px");
	};
	
	sBr.contentScrollPos=function(){
		sBr.curPos=parseInt((sBr.trgtScrll*sBr.sRange)/sBr.mxScroll);
		sBr.targetSkew=sBr.trgtScrll-parseInt((sBr.curPos/sBr.sRange)*sBr.mxScroll);
		sBr.curPos=(Math.min(Math.max(sBr.curPos,0),sBr.maxPos));
		sBr.setPos[i](sBr.curPos+sBr.minPos+"px");
		sBr.setPos[i](sBr.curPos+sBr.minPos+"px");
		cDv.setPos[i](sBr.trgtScrll+"px");
	};
	
	sC.barZ=getStyle(sBr,'z-index');
	sBr.sY.zIndex=(sC.barZ=="auto"||sC.barZ=="0"||sC.barZ=='normal')?2:sC.barZ;
	mDv.sY.zIndex=getStyle(sBr,'z-index');

	sBr.onmousedown=function(){
		sBr.clicked=true;sC.goScroll=sBr;sBr.scrollBoth=false;sBr.moved=false;
		fleXenv.addTrggr(dC,'selectstart',retFalse);
		fleXenv.addTrggr(dC,'mousemove',mMoveBar);
		fleXenv.addTrggr(dC,'mouseup',mMouseUp);
		return false;
		};
	
	sBr.onmouseover=intClear;
	
	 sDv.onmousedown=sDv.ondblclick=function(e){
		if(!e){var e=wD.event;}
		if(e.target&&(e.target==bAr.sFBr||e.target==bAr.sSBr||e.target==bAr.sBr)) return;
		if(e.srcElement&&(e.srcElement==bAr.sFBr||e.srcElement==bAr.sSBr||e.srcElement==bAr.sBr)) return;
		var relPos,mV=[];pageScrolled();
		sC.mDPosFix();
		findPos(sBr);
		relPos=(sBr.vertical)?e.clientY+sC.yScrld-sBr.yPos:e.clientX+sC.xScrld-sBr.xPos;
		mV[sBr.indx]=(relPos<0)?sC.baseAct[0]:sC.baseAct[1];mV[1-sBr.indx]=0;
		dDv.contentScroll(mV[0],mV[1],true);
		if(e.type!="dblclick") {
		intClear();
		sC.barClickRetard=wD.setTimeout(function(){
		sC.barClickScroll=wD.setInterval(function(){
		dDv.contentScroll(mV[0],mV[1],true);},80)},425);
		}
		return false;
	};
	sDv.setVisibility=function(r){
		if(r){sDv.fShow(dDv);
		sC.forcedHide[i]=(getStyle(sDv,"visibility")=="hidden")?true:false;
		if(!sC.forcedHide[i]) sBr.fShow(dDv); else sBr.fHide();
		sC.scroller[i]=true;classChange(sDv,"","flexinactive");
		}
		else{sDv.fHide();sBr.fHide();
		sC.forcedBar[i]=(getStyle(sDv,"visibility")!="hidden")?true:false;
		sC.scroller[i]=false;sBr.curPos=0;cDv.setPos[i]('0px');
		classChange(sDv,"flexinactive","");}
		mDv.setPos[1-i]((sC.forcedPos[i]&&(r||sC.forcedBar[i])&&!sC.forcedHide[i])?sC.barSpace[1-i]-sC.paddings[i*2]+"px":"-"+sC.paddings[i*2]+"px");
	};
	sDv.onmouseclick = retFalse;
};

function updateScroll(bAr,reqSpace,firstRun){
	var sDv=bAr.sDv,sBr=bAr.sBr,sFDv=bAr.sFDv,sFBr=bAr.sFBr,sSDv=bAr.sSDv,sSBr=bAr.sSBr,i=sBr.indx;
	sDv.setSize[i](tDv.getSize[i]()-reqSpace+'px');sDv.setPos[1-i](tDv.getSize[1-i]()-sDv.getSize[1-i]()+'px');
	sC.forcedPos[i]=(parseInt(sDv.getPos[1-i]())===0)?true:false;
	bAr.padLoss=bAr.barPadding[0]+bAr.barPadding[1];bAr.baseProp=parseInt((sDv.getSize[i]()-bAr.padLoss)*0.75);
	sBr.aSize=Math.min(Math.max(Math.min(parseInt(sC.cntRSize[i]/sC.cntSize[i]*sDv.getSize[i]()),bAr.baseProp),45),bAr.baseProp);
	sBr.setSize[i](sBr.aSize+'px');sBr.maxPos=sDv.getSize[i]()-sBr.getSize[i]()-bAr.padLoss;
	sBr.curPos=Math.min(Math.max(0,sBr.curPos),sBr.maxPos);
	sBr.setPos[i](sBr.curPos+sBr.minPos+'px');sBr.mxScroll=mDv.getSize[i]()-sC.cntSize[i];
	sBr.sRange=sBr.maxPos;
	sFDv.setSize[i](sDv.getSize[i]()-sSDv.getSize[i]()+'px');
	sFBr.setSize[i](sBr.getSize[i]()-sSBr.getSize[i]()+'px');
	sSBr.setPos[i](sBr.getSize[i]()-sSBr.getSize[i]()+'px');
	sSDv.setPos[i](sDv.getSize[i]()-sSDv.getSize[i]()+'px');
	if(!firstRun) sBr.doScrollPos();
	sC.fixIEDispBug();
};

sC.mDPosFix=function(){mDv.scrollTop=0;mDv.scrollLeft=0;dDv.scrollTop=0;dDv.scrollLeft=0;};

this.addTrggr(wD,'load',function(){if(dDv.fleXcroll) dDv.scrollUpdate();});
this.addTrggr(wD,'resize',function(){
if(dDv.refreshTimeout) wD.clearTimeout(dDv.refreshTimeout);
dDv.refreshTimeout=wD.setTimeout(function(){if(dDv.fleXcroll) dDv.scrollUpdate();},80);
});

for(var j=0,inputName;inputName=focusProtectList[j];j++){
	var inputList=dDv.getElementsByTagName(inputName);
	for(var i=0,formItem;formItem=inputList[i];i++){
	fleXenv.addTrggr(formItem,'focus',function(){dDv.focusProtect=true;});
	fleXenv.addTrggr(formItem,'blur',onblur=function(){dDv.focusProtect=false;});
}};

function retFalse(){return false;};
function mMoveBar(e){
if(!e){var e=wD.event;};
var FCBar=sC.goScroll,movBr,maxx,xScroll,yScroll;
if(FCBar==null) return;
if(!fleXenv.w3events&&!e.button) mMouseUp();
maxx=(FCBar.scrollBoth)?2:1;
for (var i=0;i<maxx;i++){
	movBr=(i==1)?FCBar.scrlTrgt.vBar:FCBar;
	if(FCBar.clicked){
	if(!movBr.moved){
	sC.mDPosFix();
	findPos(movBr);findPos(movBr.ofstParent);movBr.pointerOffsetY=e.clientY-movBr.yPos;
	movBr.pointerOffsetX=e.clientX-movBr.xPos;movBr.inCurPos=movBr.curPos;movBr.moved=true;
	};
	movBr.curPos=(movBr.vertical)?e.clientY-movBr.pointerOffsetY-movBr.ofstParent.yPos-movBr.minPos:e.clientX-movBr.pointerOffsetX-movBr.ofstParent.xPos-movBr.minPos;
	if(FCBar.scrollBoth) movBr.curPos=movBr.curPos+(movBr.curPos-movBr.inCurPos);
	movBr.doScrollPos();
	} else movBr.moved=false;
	};
};

function mMouseUp(){
	if(sC.goScroll!=null){sC.goScroll.clicked=false;}
	sC.goScroll=null;
	fleXenv.remTrggr(dC,'selectstart',retFalse);
	fleXenv.remTrggr(dC,'mousemove',mMoveBar);
	fleXenv.remTrggr(dC,'mouseup',mMouseUp);
};

function mWheelProc(e){
	if(!e) e=wD.event;
	if(!this.fleXcroll) return;
	var scrDv=this,vEdge,hEdge,hoverH=false,delta=0,iNDx;
	sC.mDPosFix();
	hElem=(e.target)?e.target:(e.srcElement)?e.srcElement:this;
	if(hElem.id&&hElem.id.match(/_hscroller/)) hoverH=true;
	if(e.wheelDelta) delta=-e.wheelDelta;if(e.detail) delta=e.detail;
	delta=(delta<0)?-1:+1;iNDx=(delta<0)?0:1;sC.edge[1-iNDx]=false;
	if((sC.edge[iNDx]&&!hoverH)||(!sC.scroller[0]&&!sC.scroller[1])) return;
	if(sC.scroller[1]&&!hoverH) scrollState=dDv.contentScroll(false,sC.wheelAct[iNDx],true);
	vEdge=!sC.scroller[1]||hoverH||(sC.scroller[1]&&((scrollState[1][0]==scrollState[1][1]&&delta>0)||(scrollState[1][0]==0&&delta<0)));
	//if(sC.scroller[0]&&(!sC.scroller[1]||hoverH)) scrollState=dDv.contentScroll(sC.wheelAct[iNDx],false,true);
	//*New
	if(sC.scroller[0]&&(!sC.scroller[1]||hoverH)) scrollState=dDv.contentScroll(false,false,true);
	//
	hEdge=!sC.scroller[0]||(sC.scroller[0]&&sC.scroller[1]&&vEdge&&!hoverH)||(sC.scroller[0]&&((scrollState[0][0]==scrollState[0][1]&&delta>0)||(scrollState[0][0]==0&&delta<0)));
	if(vEdge&&hEdge&&!hoverH) sC.edge[iNDx]=true; else sC.edge[iNDx]=false;
	if(e.preventDefault) e.preventDefault();
	return false;
};

function isddvChild(elem){while(elem.parentNode){elem=elem.parentNode;if(elem==dDv) return true;}	return false;};

function findPos(elem){ 
//modified from firetree.net
	var obj=elem,curleft=curtop=0;
	var monc="";
	if(obj.offsetParent){while(obj){curleft+=obj.offsetLeft;curtop+=obj.offsetTop;obj=obj.offsetParent; monc+=curtop+" ";}}
	else if(obj.x){curleft+=obj.x;curtop+=obj.y;}
	elem.xPos=curleft;elem.yPos=curtop;
};

function findRCpos(elem){
	var obj=elem;curleft=curtop=0;
	while(!obj.offsetHeight&&obj.parentNode&&obj!=cDv&&getStyle(obj,'display')=="inline"){obj=obj.parentNode;}
	if(obj.offsetParent){while(obj!=cDv){curleft+=obj.offsetLeft;curtop+=obj.offsetTop;obj=obj.offsetParent;}}
	return [curleft,curtop];
};

function classChange(elem,addClass,remClass) {
	if (!elem.className) elem.className = '';
	var clsnm = elem.className;
	if (addClass && !clsnm.match(RegExp("(^|\\s)"+addClass+"($|\\s)"))) clsnm = clsnm.replace(/(\S$)/,'$1 ')+addClass;
	if (remClass) clsnm = clsnm.replace(RegExp("((^|\\s)+"+remClass+")+($|\\s)","g"),'$2').replace(/\s$/,'');
	elem.className=clsnm;
	};
	
if (!SimileAjax.Platform.browser.isIE)
    {
     setTimeout("maxWindow();",200);
    }	
	
},
//main code end
globalInit:function(){

if (!SimileAjax.Platform.browser.isIE)
{
    //window.resizeTo(screen.width, parseInt(screen.height)- 25);
}

if(fleXenv.catchFastInit) window.clearInterval(fleXenv.catchFastInit);
var regg=/#([^#.]*)$/,urlExt=/(.*)#.*$/,matcH,i,anchoR,anchorList=document.getElementsByTagName("a"),urlBase=document.location.href;
if(urlBase.match(urlExt)) urlBase=urlBase.match(urlExt)[1];
for(i=0;anchoR=anchorList[i];i++){
if(anchoR.href&&anchoR.href.match(regg)&&anchoR.href.match(urlExt)&&urlBase===anchoR.href.match(urlExt)[1]) {
	anchoR.fleXanchor=true;
	fleXenv.addTrggr(anchoR,'click',function(e){
		if(!e) e=window.event;
		var clickeD=(e.srcElement)?e.srcElement:this;
		while(!clickeD.fleXanchor&&clickeD.parentNode){clickeD=clickeD.parentNode};
		if(!clickeD.fleXanchor) return;
		var tEL=document.getElementById(clickeD.href.match(regg)[1]),eScroll=false;
		if(tEL==null) tEL=(tEL=document.getElementsByName(clickeD.href.match(regg)[1])[0])?tEL:null;
		if(tEL!=null){
		var elem=tEL;
		while(elem.parentNode){
			elem=elem.parentNode;if(elem.scrollToElement){
				elem.scrollToElement(tEL);eScroll=elem;
				};
			};
		if(eScroll) {if(e.preventDefault) e.preventDefault();document.location.href="#"+clickeD.href.match(regg)[1];eScroll.fleXdata.mDPosFix();return false;}
		};
		});
	};
};
fleXenv.initByClass();
if(window.onfleXcrollRun) window.onfleXcrollRun();    
},

initByClass:function(){
if(fleXenv.initialized) return;
fleXenv.initialized=true;
var fleXlist=fleXenv.getByClassName(document.getElementsByTagName("body")[0],"div",'flexcroll');
for (var i=0,tgDiv;tgDiv=fleXlist[i];i++) fleXenv.fleXcrollMain(tgDiv);
},

getByClassName:function(elem,elType,classString){
//v1.1fleX
	if(typeof(elem)=='string') elem=document.getElementById(elem);
	if(elem==null)return false;
	var regExer=new RegExp("(^|\\s)"+classString+"($|\\s)"),clsnm,retArray=[],key=0;
	var elems=elem.getElementsByTagName(elType);
	for(var i=0,pusher;pusher=elems[i];i++){
	if(pusher.className && pusher.className.match(regExer)){
		retArray[key]=pusher;key++;
		}
	};
return retArray;
},

catchFastInit:window.setInterval(function(){
	var dElem=document.getElementById('flexcroll-init');
	if(dElem!=null) {fleXenv.initByClass();window.clearInterval(fleXenv.catchFastInit);}
	},100),

addTrggr:function(elm,eventname,func){if(!fleXenv.addChckTrggr(elm,eventname,func)&&elm.attachEvent) {elm.attachEvent('on'+eventname,func);}},

addChckTrggr:function(elm,eventname,func){if(elm.addEventListener){elm.addEventListener(eventname,func,false);fleXenv.w3events=true;window.addEventListener("unload",function(){fleXenv.remTrggr(elm,eventname,func)},false);return true;} else return false;},

remTrggr:function(elm,eventname,func){if(!fleXenv.remChckTrggr(elm,eventname,func)&&elm.detachEvent) elm.detachEvent('on'+eventname,func);},

remChckTrggr:function(elm,eventname,func){if(elm.removeEventListener){elm.removeEventListener(eventname,func,false);return true;} else return false;}

};

function CSBfleXcroll(targetId){fleXenv.fleXcrollMain(targetId)};
fleXenv.fleXcrollInit();

function maxWindow()
{
//window.moveTo(0,0);
//window.resizeTo(screen.width, screen.height);
}



/**************************************************
 * dom-drag.js
 * 09.25.2001
 * www.youngpup.net
 * Script featured on Dynamic Drive (http://www.dynamicdrive.com) 12.08.2005
 **************************************************
 * 10.28.2001 - fixed minor bug where events
 * sometimes fired off the handle, not the root.
 **************************************************/

var Drag = {

	obj : null,

	init : function(o, oRoot, minX, maxX, minY, maxY, bSwapHorzRef, bSwapVertRef, fXMapper, fYMapper)
	{
		o.onmousedown	= Drag.start;

		o.hmode			= bSwapHorzRef ? false : true ;
		o.vmode			= bSwapVertRef ? false : true ;

		o.root = oRoot && oRoot != null ? oRoot : o ;

		if (o.hmode  && isNaN(parseInt(o.root.style.left  ))) o.root.style.left   = "0px";
		if (o.vmode  && isNaN(parseInt(o.root.style.top   ))) o.root.style.top    = "0px";
		if (!o.hmode && isNaN(parseInt(o.root.style.right ))) o.root.style.right  = "0px";
		if (!o.vmode && isNaN(parseInt(o.root.style.bottom))) o.root.style.bottom = "0px";

		o.minX	= typeof minX != 'undefined' ? minX : null;
		o.minY	= typeof minY != 'undefined' ? minY : null;
		o.maxX	= typeof maxX != 'undefined' ? maxX : null;
		o.maxY	= typeof maxY != 'undefined' ? maxY : null;

		o.xMapper = fXMapper ? fXMapper : null;
		o.yMapper = fYMapper ? fYMapper : null;

		o.root.onDragStart	= new Function();
		o.root.onDragEnd	= new Function();
		o.root.onDrag		= new Function();
	},

	start : function(e)
	{
		var o = Drag.obj = this;
		e = Drag.fixE(e);
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		o.root.onDragStart(x, y);

		o.lastMouseX	= e.clientX;
		o.lastMouseY	= e.clientY;

		if (o.hmode) {
			if (o.minX != null)	o.minMouseX	= e.clientX - x + o.minX;
			if (o.maxX != null)	o.maxMouseX	= o.minMouseX + o.maxX - o.minX;
		} else {
			if (o.minX != null) o.maxMouseX = -o.minX + e.clientX + x;
			if (o.maxX != null) o.minMouseX = -o.maxX + e.clientX + x;
		}

		if (o.vmode) {
			if (o.minY != null)	o.minMouseY	= e.clientY - y + o.minY;
			if (o.maxY != null)	o.maxMouseY	= o.minMouseY + o.maxY - o.minY;
		} else {
			if (o.minY != null) o.maxMouseY = -o.minY + e.clientY + y;
			if (o.maxY != null) o.minMouseY = -o.maxY + e.clientY + y;
		}

		document.onmousemove	= Drag.drag;
		document.onmouseup		= Drag.end;

		return false;
	},

	drag : function(e)
	{
		e = Drag.fixE(e);
		var o = Drag.obj;

		var ey	= e.clientY;
		var ex	= e.clientX;
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		var nx, ny;

		if (o.minX != null) ex = o.hmode ? Math.max(ex, o.minMouseX) : Math.min(ex, o.maxMouseX);
		if (o.maxX != null) ex = o.hmode ? Math.min(ex, o.maxMouseX) : Math.max(ex, o.minMouseX);
		if (o.minY != null) ey = o.vmode ? Math.max(ey, o.minMouseY) : Math.min(ey, o.maxMouseY);
		if (o.maxY != null) ey = o.vmode ? Math.min(ey, o.maxMouseY) : Math.max(ey, o.minMouseY);

		nx = x + ((ex - o.lastMouseX) * (o.hmode ? 1 : -1));
		ny = y + ((ey - o.lastMouseY) * (o.vmode ? 1 : -1));

		if (o.xMapper)		nx = o.xMapper(y)
		else if (o.yMapper)	ny = o.yMapper(x)

		Drag.obj.root.style[o.hmode ? "left" : "right"] = nx + "px";
		Drag.obj.root.style[o.vmode ? "top" : "bottom"] = ny + "px";
		Drag.obj.lastMouseX	= ex;
		Drag.obj.lastMouseY	= ey;

		Drag.obj.root.onDrag(nx, ny);
		return false;
	},

	end : function()
	{
		document.onmousemove = null;
		document.onmouseup   = null;
		Drag.obj.root.onDragEnd(	parseInt(Drag.obj.root.style[Drag.obj.hmode ? "left" : "right"]), 
									parseInt(Drag.obj.root.style[Drag.obj.vmode ? "top" : "bottom"]));
		Drag.obj = null;
	},

	fixE : function(e)
	{
		if (typeof e == 'undefined') e = window.event;
		if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
		if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
		return e;
	}
};






var Drag1 = {

	obj : null,

	init : function(o, oRoot, minX, maxX, minY, maxY, bSwapHorzRef, bSwapVertRef, fXMapper, fYMapper)
	{
		o.onmousedown	= Drag1.start;

		o.hmode			= bSwapHorzRef ? false : true ;
		o.vmode			= bSwapVertRef ? false : true ;

		o.root = oRoot && oRoot != null ? oRoot : o ;

		if (o.hmode  && isNaN(parseInt(o.root.style.left  ))) o.root.style.left   = "0px";
		if (o.vmode  && isNaN(parseInt(o.root.style.top   ))) o.root.style.top    = "0px";
		if (!o.hmode && isNaN(parseInt(o.root.style.right ))) o.root.style.right  = "0px";
		if (!o.vmode && isNaN(parseInt(o.root.style.bottom))) o.root.style.bottom = "0px";

		o.minX	= typeof minX != 'undefined' ? minX : null;
		o.minY	= typeof minY != 'undefined' ? minY : null;
		o.maxX	= typeof maxX != 'undefined' ? maxX : null;
		o.maxY	= typeof maxY != 'undefined' ? maxY : null;

		o.xMapper = fXMapper ? fXMapper : null;
		o.yMapper = fYMapper ? fYMapper : null;

		o.root.onDragStart	= new Function();
		o.root.onDragEnd	= new Function();
		o.root.onDrag		= new Function();
	},

	start : function(e)
	{
		var o = Drag1.obj = this;
		e = Drag1.fixE(e);
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		o.root.onDragStart(x, y);

		o.lastMouseX	= e.clientX;
		o.lastMouseY	= e.clientY;

		if (o.hmode) {
			if (o.minX != null)	o.minMouseX	= e.clientX - x + o.minX;
			if (o.maxX != null)	o.maxMouseX	= o.minMouseX + o.maxX - o.minX;
		} else {
			if (o.minX != null) o.maxMouseX = -o.minX + e.clientX + x;
			if (o.maxX != null) o.minMouseX = -o.maxX + e.clientX + x;
		}

		if (o.vmode) {
			if (o.minY != null)	o.minMouseY	= e.clientY - y + o.minY;
			if (o.maxY != null)	o.maxMouseY	= o.minMouseY + o.maxY - o.minY;
		} else {
			if (o.minY != null) o.maxMouseY = -o.minY + e.clientY + y;
			if (o.maxY != null) o.minMouseY = -o.maxY + e.clientY + y;
		}

		document.onmousemove	= Drag1.drag;
		document.onmouseup		= Drag1.end;

		return false;
	},

	drag : function(e)
	{
		e = Drag1.fixE(e);
		var o = Drag1.obj;

		var ey	= e.clientY;
		var ex	= e.clientX;
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		var nx, ny;

		if (o.minX != null) ex = o.hmode ? Math.max(ex, o.minMouseX) : Math.min(ex, o.maxMouseX);
		if (o.maxX != null) ex = o.hmode ? Math.min(ex, o.maxMouseX) : Math.max(ex, o.minMouseX);
		if (o.minY != null) ey = o.vmode ? Math.max(ey, o.minMouseY) : Math.min(ey, o.maxMouseY);
		if (o.maxY != null) ey = o.vmode ? Math.min(ey, o.maxMouseY) : Math.max(ey, o.minMouseY);

		nx = x + ((ex - o.lastMouseX) * (o.hmode ? 1 : -1));
		ny = y + ((ey - o.lastMouseY) * (o.vmode ? 1 : -1));

		if (o.xMapper)		nx = o.xMapper(y)
		else if (o.yMapper)	ny = o.yMapper(x)

		Drag1.obj.root.style[o.hmode ? "left" : "right"] = nx + "px";
		Drag1.obj.root.style[o.vmode ? "top" : "bottom"] = ny + "px";
		Drag1.obj.lastMouseX	= ex;
		Drag1.obj.lastMouseY	= ey;

		Drag1.obj.root.onDrag(nx, ny);
		return false;
	},

	end : function()
	{
		document.onmousemove = null;
		document.onmouseup   = null;
		Drag1.obj.root.onDragEnd(	parseInt(Drag1.obj.root.style[Drag1.obj.hmode ? "left" : "right"]), 
									parseInt(Drag1.obj.root.style[Drag1.obj.vmode ? "top" : "bottom"]));
		Drag1.obj = null;
	},

	fixE : function(e)
	{
		if (typeof e == 'undefined') e = window.event;
		if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
		if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
		return e;
	}
};

function CloseMyTimeline()
    {    
	 $('#imgMngT').click();	
    }





//*New
var browser=navigator.appName;
//*
function centerSimileAjax(date) {
	tl.getBand(0).setCenterVisibleDate(SimileAjax.DateTime.parseGregorianDateTime(date));
}

//Function to reload page (to force timeline height to refresh on category selection)
function reloadPage() {
	setTimeout("location.reload(true);", 0);
}

//var numOfFilters = 14;
var numOfFilters = 15;



//If user hasn't selected any categories, set cookies for all as selected
var blnSelectionMade = false;
for (var i = 0; i < numOfFilters; i++) {
	var thisCookie = readCookie("filter" + i) + "";
	if (thisCookie != "null") {
		blnSelectionMade = true;
	}
}
if (blnSelectionMade == false) {
	for (var i = 0; i < numOfFilters; i++) {		
		createCookie('filter' + i, '1', 365);
	}    
}



function setupFilterHighlightControls(div, timeline, bandIndices, theme) {   
    //*new
    var allHTMLTags=document.getElementsByTagName("a");
    for (i=0; i<allHTMLTags.length; i++){
        if (allHTMLTags[i].className=="close-controls")
        {
        allHTMLTags[i].style.display='none';
        }
    }
    //*

	// Init Handler
	var handler = function(elmt, evt, target) {
		onKeyPress(timeline, bandIndices, table);
	
	//*New			
	if (SimileAjax.Platform.browser.isIE) {
		if(elmt.innerHTML=="View All")
		{
		    document.getElementById("filter" + 0).checked = true;		
		}
		else if(elmt.innerHTML=="Personal")
		{
		    document.getElementById("filter" + 1).checked = true;
		}
		else if(elmt.innerHTML=="Travel")
		{
		    document.getElementById("filter" + 2).checked = true;
		}
		else if(elmt.innerHTML=="Friends")
		{
		    document.getElementById("filter" + 3).checked = true;
		}
		else if(elmt.innerHTML=="Family")
		{
		    document.getElementById("filter" + 4).checked = true;
		}
		else if(elmt.innerHTML=="General")
		{
		    document.getElementById("filter" + 5).checked = true;
		}
		else if(elmt.innerHTML=="Health")
		{
		    document.getElementById("filter" + 6).checked = true;
		}
		else if(elmt.innerHTML=="Money")
		{
		    document.getElementById("filter" + 7).checked = true;
		}
		else if(elmt.innerHTML=="Education")
		{
		    document.getElementById("filter" + 8).checked = true;
		}
		else if(elmt.innerHTML=="Hobbies")
		{
		    document.getElementById("filter" + 9).checked = true;
		}
		else if(elmt.innerHTML=="Work")
		{
		    document.getElementById("filter" + 10).checked = true;
		}
		else if(elmt.innerHTML=="Culture")
		{
		    document.getElementById("filter" + 11).checked = true;
		}
		else if(elmt.innerHTML=="Charity")
		{
		    document.getElementById("filter" + 12).checked = true;
		}
		else if(elmt.innerHTML=="Green")
		{
		    document.getElementById("filter" + 13).checked = true;
		}
		else if(elmt.innerHTML=="Misc")
		{
		    document.getElementById("filter" + 14).checked = true;
		}
	}//*		
	};

	// Create Table
	var table = document.createElement("table");
	table.id = "table-filter";
	SimileAjax.DOM.registerEvent(table, "click", handler);
	
    //*New
    tr = table.insertRow(0);
	tr.style.verticalAlign = "middle";	
	td = tr.insertCell(0);
	//td.id="tdcatlogo";
	td.style.cursor="pointer";
	td.innerHTML = '<div class="catlogo"><img src="images/category.jpg" title="" alt="" /></div>';
        
	/* Create the text inputs for the filters and add eventListeners */
	for (var i = 0; i < numOfFilters; i++) {
		//Get cookie value for this input
		var thisCookieValue = readCookie("filter" + i) + "";
		tr = table.insertRow(i+1);
		td = tr.insertCell(0);
		//*New
		if (SimileAjax.Platform.browser.isIE) {
		    input = document.createElement("<input name=' '>");}
		 else{
		    input = document.createElement("input");
		 }
		
		//input.type = "checkbox";
		input.name = "grp"
		input.type = "radio";		
		input.style.display="none";		
		input.className = "filter";
		
	    if(readCookie("filter" + 0) + ""!== 'null')
	    {
	        if(i==0)
	        {
	        input.checked = 1;}
	    }
	    else
	    {
		    if (thisCookieValue !== 'null') {
			    input.checked = 1;
		    }
	    }//*
	    	
		SimileAjax.DOM.registerEvent(input, "click", handler);		
		td.appendChild(input);
		input.id = "filter" + i;
		var valueOfCheckbox = "";
//		switch (i) {
//			case 0:
//				valueOfCheckbox = "Personal"
//				break;
//			case 1:
//				valueOfCheckbox = "Travel"
//				break;
//			case 2:
//				valueOfCheckbox = "Friends"
//				break;
//			case 3:
//				valueOfCheckbox = "Family"
//				break;
//			case 4:
//				valueOfCheckbox = "General"
//				break;
//			case 5:
//				valueOfCheckbox = "Health"
//				break;
//			case 6:
//				valueOfCheckbox = "Money"
//				break;
//			case 7:
//				valueOfCheckbox = "Education"
//				break;
//			case 8:
//				valueOfCheckbox = "Hobbies"
//				break;
//			case 9:
//				valueOfCheckbox = "Work"
//				break;
//			case 10:
//				valueOfCheckbox = "Culture"
//				break;
//			case 11:
//				valueOfCheckbox = "Charity"
//				break;
//			case 12:
//				valueOfCheckbox = "Green"
//				break;
//			case 13:
//				valueOfCheckbox = "Misc"
//				break;
//		}

        //*
        switch (i) {
            case 0:
                valueOfCheckbox = "View All"
				break;
			case 1:
				valueOfCheckbox = "Personal"
				break;
			case 2:
				valueOfCheckbox = "Travel"
				break;
			case 3:
				valueOfCheckbox = "Friends"
				break;
			case 4:
				valueOfCheckbox = "Family"
				break;
			case 5:
				valueOfCheckbox = "General"
				break;
			case 6:
				valueOfCheckbox = "Health"
				break;
			case 7:
				valueOfCheckbox = "Money"
				break;
			case 8:
				valueOfCheckbox = "Education"
				break;
			case 9:
				valueOfCheckbox = "Hobbies"
				break;
			case 10:
				valueOfCheckbox = "Work"
				break;
			case 11:
				valueOfCheckbox = "Culture"
				break;
			case 12:
				valueOfCheckbox = "Charity"
				break;
			case 13:
				valueOfCheckbox = "Green"
				break;
			case 14:
				valueOfCheckbox = "Misc"
				break;
		}

		input.value = valueOfCheckbox;

		var label = document.createElement("label");
		label.htmlFor = "filter" + i;
		//label.className = "filter" + (i + 1);
		//*
		label.className = "filter" + (i);
		//*
		label.innerHTML = valueOfCheckbox;		
        label.style.cursor = "pointer";
	       
		SimileAjax.DOM.registerEvent(label, "click", handler);
		td.appendChild(label);
	}
    //*
	//tr = table.insertRow(numOfFilters);
	//tr.style.verticalAlign = "middle";	
	//td = tr.insertCell(0);
	//td.innerHTML = '<div class="refresh-timeline"><a href="#" onclick=""><img src="images/refresh.png" title="" alt="" /> Refresh timeline</a></div>';
    //*
	// Append the table to the div
	div.appendChild(table);

}

var timerID = null;
var filterMatcherGlobal = null;
var highlightMatcherGlobal = null;

function onKeyPress(timeline, bandIndices, table) {
	if (timerID != null) {
		window.clearTimeout(timerID);
	}
	timerID = window.setTimeout(function() {
		performFiltering(timeline, bandIndices, table);
	}, 300);
}

function cleanString(s) {
	return s.replace(/^\s+/, '').replace(/\s+$/, '');
}

function performFiltering(timeline, bandIndices, table) {
	timerID = null;

	// Add all filter inputs to a new array
	var filterInputs = new Array();
	for (var i = 0; i < numOfFilters; i++) {
		filterInputs.push(cleanString(table.rows[i+1].cells[0].firstChild.value));
	}

	var filterMatcher = null;
	var filterRegExes = new Array();
	for (var i = 0; i < filterInputs.length; i++) {
		/* if the filterInputs are not empty create a new regex for each one and add them to an array */

		//var checkboxValue = document.getElementById("filter" + i).checked;
		//*New
		if(document.getElementById("filter" + 0).checked)
		{
		    var checkboxValue = true;
		}
		else
		{
		    var checkboxValue = document.getElementById("filter" + i).checked;
		}
		//*
		if (checkboxValue == true) {
			createCookie('filter' + i, '1', 365);
			filterRegExes.push(new RegExp(filterInputs[i], "i"));
		} else {
			eraseCookie('filter' + i, '1', 365);
		}

		filterMatcher = function(evt) {
		
			/* iterate through the regex's and check them against the evtText if match return true, if not found return false */
			if (filterRegExes.length != 0) {
			
				for (var j = 0; j < filterRegExes.length; j++) {
					if (filterRegExes[j].test(evt.getProperty("category")) == true) {
						return true;
					}
				}
			}
			else if (filterRegExes.length == 0) {
				return true;
			}
			return false;
		};
	}

	// Set the matchers and repaint the timeline
	filterMatcherGlobal = filterMatcher;
	for (var i = 0; i < bandIndices.length; i++) {
		var bandIndex = bandIndices[i];
		timeline.getBand(bandIndex).getEventPainter().setFilterMatcher(filterMatcher);
	}
	timeline.paint();
	//*new
	document.getElementById("controls").style.display="none";
	//*
}



function clearAll(timeline, bandIndices, table) {

	for (var i = 0; i < numOfFilters; i++) {
		eraseCookie("filter" + i);
		document.getElementById("filter" + i).checked = 0;
		reloadPage();
	}

	// First clear the filters
	var tr = table.rows[1];
	for (var x = 0; x < table.rows.length; x++) {
		table.rows[x].cells[0].firstChild.value = "";
	}

	// Then re-init the filters and repaint the timeline
	for (var i = 0; i < bandIndices.length; i++) {
		var bandIndex = bandIndices[i];
		timeline.getBand(bandIndex).getEventPainter().setFilterMatcher(null);
	}
	timeline.paint();
}