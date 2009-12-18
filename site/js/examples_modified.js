function centerSimileAjax(date) {
    tl.getBand(0).setCenterVisibleDate(SimileAjax.DateTime.parseGregorianDateTime(date));
}

//Function to reload page (to force timeline height to refresh on category selection)
function reloadPage() {
	setTimeout("location.reload(true);", 0);
}

var numOfFilters = 13;



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

	// Init Handler
	var handler = function(elmt, evt, target) {
		onKeyPress(timeline, bandIndices, table);
	};

	// Create Table
	var table = document.createElement("table");
	table.id = "table-filter";

	/* Create the text inputs for the filters and add eventListeners */
	for (var i = 0; i < numOfFilters; i++) {
		//Get cookie value for this input
		var thisCookieValue = readCookie("filter" + i) + "";
		tr = table.insertRow(i);
		td = tr.insertCell(0);
		var input = document.createElement("input");
		input.type = "checkbox";
		input.className = "filter";
		if (thisCookieValue !== 'null') {
			input.checked = 1;
		}
		SimileAjax.DOM.registerEvent(input, "click", handler);
		td.appendChild(input);
		input.id = "filter" + i;
		var valueOfCheckbox = "";
		switch (i) {
			case 0:
				valueOfCheckbox = "Personal"
				break;
			case 1:
				valueOfCheckbox = "Travel"
				break;
			case 2:
				valueOfCheckbox = "Friends"
				break;
			case 3:
				valueOfCheckbox = "Family"
				break;
			case 4:
				valueOfCheckbox = "General"
				break;
			case 5:
				valueOfCheckbox = "Health"
				break;
			case 6:
				valueOfCheckbox = "Money"
				break;
			case 7:
				valueOfCheckbox = "Education"
				break;
			case 8:
				valueOfCheckbox = "Hobbies"
				break;
			case 9:
				valueOfCheckbox = "Culture"
				break;
			case 10:
				valueOfCheckbox = "Charity"
				break;
			case 11:
				valueOfCheckbox = "Green"
				break;
			case 12:
				valueOfCheckbox = "Misc"
				break;
		}
		input.value = valueOfCheckbox;

		var label = document.createElement("label");
		label.htmlFor = "filter" + i;
		label.className = "filter" + (i + 1);
		label.innerHTML = valueOfCheckbox;
		SimileAjax.DOM.registerEvent(label, "click", handler);
		td.appendChild(label);
	}

	tr = table.insertRow(13);
	tr.style.verticalAlign = "middle";
	td = tr.insertCell(0);
	td.innerHTML = '<div class="refresh-timeline"><a href="#" onclick=""><img src="images/refresh.png" title="" alt="" /> Refresh timeline</a></div>';

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
		filterInputs.push(cleanString(table.rows[i].cells[0].firstChild.value));
	}

	var filterMatcher = null;
	var filterRegExes = new Array();
	for (var i = 0; i < filterInputs.length; i++) {
		/* if the filterInputs are not empty create a new regex for each one and add them to an array */

		var checkboxValue = document.getElementById("filter" + i).checked;
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