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
