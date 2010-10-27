//_________________________________________________________________________________
//popupWindow launches a popup window (funny that)
//_________________________________________________________________________________
  function popupWindow(nWidth, nHeight, szURL, szWinName, szStatus, nLeft, nTop){
    //Get top and left, ensuring popup will be centered
    if (isNaN(nTop))
      nTop = (screen.height - nHeight)/2;
    if (isNaN(nLeft))
      nLeft = (screen.width - nWidth)/2;

    var szStatusVal;
    if (arguments.length > 4)
      szStatusVal = szStatus;
    else
      szStatusVal = '0';
      
    //get window attributes
    var szWinAttribs = 'toolbar=0,' +
                      'location=0,' +
                      'directories=0,' +
                      'status=' + szStatusVal + ',' +
                      'menubar=0,' +
                      'scrollbars=0,' +
                      'resizable=0,' +
                      'width=' + nWidth + ',' +
                      'height=' + nHeight + ',' +
                      'top=' + nTop + ',' +
                      'left=' + nLeft;

    //Launch window, return reference
    return window.open(szURL, szWinName, szWinAttribs);
  }
//_________________________________________________________________________________
//validateDate function takes a string, and a date format, returning true if the 
//date is valid and consistent with the date format supplied ... false otherwise
//_________________________________________________________________________________
  function validateDate(szDate, szDateFormat){
  //check date is of correct format
    if (szDate.search(/^\d{1,2}\/\d{1,2}\/\d{4}$/) != 0)
      return false;
  
  //get day, month, year
    var nDay, nMonth, nYear;
    var aDateArr = szDate.split("/");
    switch(szDateFormat){
      case "dmy":
        nDay = aDateArr[0];
        nMonth = aDateArr[1];
        nYear = aDateArr[2];
        break;
      case "mdy":
        nDay = aDateArr[1];
        nMonth = aDateArr[0];
        nYear = aDateArr[2];
        break;
      default:
        nDay = 0;
        nMonth = 0;
        nYear = 0;
        break;
      }
      
  //check date is valid
    var bValid = 
      ((nDay > 0) && (
        ((nMonth == 1) && (nDay < 32)) ||
        ((nMonth == 2) && (((nDay < 29) && (nYear % 4 > 0)) || ((nDay < 30) && (nYear % 4 == 0)))) ||
        ((nMonth == 3) && (nDay < 32)) ||
        ((nMonth == 4) && (nDay < 31)) ||
        ((nMonth == 5) && (nDay < 32)) ||
        ((nMonth == 6) && (nDay < 31)) ||
        ((nMonth == 7) && (nDay < 32)) ||
        ((nMonth == 8) && (nDay < 32)) ||
        ((nMonth == 9) && (nDay < 31)) ||
        ((nMonth == 10) && (nDay < 32)) ||
        ((nMonth == 11) && (nDay < 31)) ||
        ((nMonth == 12) && (nDay < 32))
      ));

    return bValid;
  }
//_________________________________________________________________________________
//trim function removes leading and trailing spaces from a string
//_________________________________________________________________________________
  function trim(szTrimStr){
    return szTrimStr.replace(/(^\s*|\s*$)/g, "");
  }
//_________________________________________________________________________________
//highlightButton function turns a button red with white text.  Intended for use
//highlighting Save buttons on forms.
//_________________________________________________________________________________
  function highlightButton(oButton){
    oButton.style.backgroundColor = "red";
    oButton.style.color = "white";
  }
//_________________________________________________________________________________
//unhighlightButton is opposite of highlightButton function.
//_________________________________________________________________________________
  function unhighlightButton(oButton){
    oButton.style.backgroundColor = "";
    oButton.style.color = "";
  }
//_________________________________________________________________________________
//queryString function gets a value off the querystring
//_________________________________________________________________________________
  function queryString(strValueName, strNotFoundVal){

    var aQS = window.location.search.substr(1).split('&');
    for (var i = 0; i < aQS.length; i++){
      var aQSValue = aQS[i].split('=');
      if (aQSValue[0] == strValueName)
        return aQSValue[1];
    }
   
  //if code reaches this point, the querystring value has not been found.
    return strNotFoundVal;
  }
//_________________________________________________________________________________


