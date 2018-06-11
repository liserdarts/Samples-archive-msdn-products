/*
iso date format, in UTC: yyyy-mm-ddThh:mm:ssZ
sample date: 2006-12-11T16:37:24Z
*/

function GetElapsedHMS( isoUTC ) {

	var fmt = isoUTC.substring(5,10) + '-' + isoUTC.substring(0,4) + ' ' + isoUTC.substring(11,19);

	var current = new Date();  // local time
	var old = new Date( fmt ); // server time = UTC
	var diff = new Date();
	
	var currentUTC = current.getTime() + (current.getTimezoneOffset() * 6e4);
	
	diff.setTime(Math.abs(currentUTC - old.getTime()));
	
	timediff = diff.getTime();
			
	hours = Math.floor(timediff / 36e5); 
	timediff -= hours * 36e5;
	
	mins = Math.floor(timediff / 6e4); 
	timediff -= mins * 6e4;
	
	secs = Math.floor(timediff / 1e3); 
	timediff -= secs * 1e3;

	return [hours, mins, secs];
}

function ShowElapsedHours( isoUTC ) {

	var arr = GetElapsedHMS( isoUTC );
		
	document.write(arr[0] + ' hours, ' + arr[1] + ' minutes');
}

function ShowElapsedHoursDiv( isoUTC, cssClass ) {

	var arr = GetElapsedHMS( isoUTC );
		
	width = Math.round((arr[0] + arr[1]/60) * 50);
	if (width > 600) {
		width = 600; // clip width of rendered div
		cssClass = "ms-WPSelected"; // change style to indicate clipping
	}
	
	title = arr[0] + ' hours, ' + arr[1] + ' minutes';
	
	str = "<div class='" + cssClass + "' style='width:" + width + "px;height=6px;' title='" + title + "'></div>";
	
	document.write( str  );
}
