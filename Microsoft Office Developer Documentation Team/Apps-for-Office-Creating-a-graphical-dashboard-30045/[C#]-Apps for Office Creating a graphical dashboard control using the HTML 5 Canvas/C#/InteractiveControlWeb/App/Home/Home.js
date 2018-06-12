/// <reference path="../App.js" />

(function () {
    "use strict";

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();
        });
    };

    $(document).ready(function () {
        // get element
        var canvas = document.getElementById('control');
        canvas.onpointermove = TouchHitTest;
        canvas.onpointerdown = TouchHitTest;
        canvas.onpointerup = TouchHitTest;
        window.onload = initSelectionHandler;
        drawControl(.4);
    });

    function initSelectionHandler(){
        Office.context.document.addHandlerAsync(Office.EventType.DocumentSelectionChanged, SelectionChanged);
    }

    function SelectionChanged(e) {
        var dataString = "uninitialized";

        // get data at selection
        Office.context.document.getSelectedDataAsync(Office.CoercionType.Text, 
            { valueFormat: "unformatted", filterType: "all" },
            function (asyncResult) {
                var error = asyncResult.error;
                if (asyncResult.status === Office.AsyncResultStatus.Failed) {
                    dataString = error.name + ": " + error.message;
                } 
                else {
                    // Get selected data.
                    dataString = asyncResult.value;

                    // if there is no data in the cell update the value from the guage
                    if (dataString == "") {
                        // update the data in the cell with the data from the indicator
                        Office.context.document.setSelectedDataAsync(intIndicator);
                    }
                    else {
                        //update the guage with the value in the cell
                        drawGauge((100-dataString)/100);
                    }
                }            
            });
    }

    var intIndicator;

    function drawControl(indicatorValue) {
        if (indicatorValue > 1) indicatorValue = 1;
        if (indicatorValue < 0) indicatorValue = 0;

        intIndicator = parseInt(100 - (100 * indicatorValue));

        // get element
        var canvas = document.getElementById('control');

        // get context
        var context = canvas.getContext('2d');

        // set the color to draw with
        context.strokeStyle = "rgb(0, 162, 232)";
        context.fillStyle = "rgb(0, 162, 232)";
        context.lineWidth = "20";
        context.lineCap = "round";
        context.font = "20px tahoma";

        // (re)set the identity matrix.
        context.setTransform(1, 0, 0, 1, 0, 0);

        // erase the canvas each draw cycle
        context.clearRect(0,0,200,200);

        // draw the numerical value to the canvas
        var textCenter = context.measureText(intIndicator).width/2;
        //context.fillText(intIndicator.toString(10), (canvas.width / 2) - (textWidth/2), canvas.height / 2);
        context.fillText(intIndicator.toString(10), 100 - textCenter, 165);

        //the arc function takes a bool that determines the direction that the arc is drawn in.  
        //It may be easier to create descriptivly named values like these for this bool.
        var clockwise = false;   // this is the direction that the degrees are counted in
        var anticlockwise = true;

        //we want to center the arc in the canvas.
        var cx = canvas.clientWidth /2;
        var cy = canvas.clientHeight /2;

        // A gap of 60 gives a 300 degree gauge
        // A gap of 90 gives a 270 degree gauge
        // A gap of 120 gives a 240 degree gauge
        // A gap of 180 gives a 180 degree gauge
        // A gap of 240 gives a 120 degree gauge
        var gap = 80;

        // draw a dot at the center of the gauge
        var radius = 5;
        context.beginPath();
        context.arc(cx, cy, radius, ConvertDegrees(0), ConvertDegrees(360), clockwise);
        context.fill();
        context.stroke();
        //Rotate the context so that the arc function has it's origion at 3:00.  This requires rotating by 90 degrees to put it at 6:00.
        //for more details see http://msdn.microsoft.com/en-us/library/ie/ff975430(v=vs.85).aspx 
        context.setTransform(0, 1, 1, 0, 0, 0);  // rotate by 90 degrees

        // you could also use rotate instead of setTransform:
        //context.translate(cx, cy);          //set the center of rotation
        //context.rotate(ConvertDegrees(90)); //rotate
        //context.translate(-cx, -cy);        //reset

        //draw the gauge background
        context.beginPath();

        // draw the circle.
        // make radius a little less than half the width of the canvas
        var radius = cx * .75;
        // call the arc function
        context.arc(cx, cy, radius, ConvertDegrees(0 + gap / 2), ConvertDegrees(360 - gap / 2), clockwise);
        context.stroke();

        // draw the tick marks
        context.lineCap = "round";
        context.lineWidth = "5";
        context.strokeStyle = "white";

        //Number of ticks
        var ticks = 10;
        var tickThickness = .1; // degrees
        var spacing = (360 - gap) / ticks;
        for (var loop = 0; loop <= ticks; loop++) {
            var ticDegree = gap / 2 + spacing * loop;
            context.beginPath();
            context.arc(cx, cy, radius, ConvertDegrees(ticDegree - tickThickness / 2), ConvertDegrees(ticDegree + tickThickness / 2, clockwise));
            context.stroke();
        }

        // draw the indicator
        context.strokeStyle = "white";
        context.lineWidth = "10";
        context.beginPath();
        context.arc(cx, cy, radius, ConvertDegrees((0 + gap / 2) + (360 - gap) * indicatorValue), ConvertDegrees(360 - gap / 2), clockwise);
        context.stroke();
    }

    // arc function requires radians, Most people think in terms of degrees.
    function ConvertDegrees(Degrees) {
        return Degrees * Math.PI / 180;
    }

    var PointerDown = false;
    function TouchHitTest(e) {

        // stop default behavior/propagation of event to avoid selecting other elemements when mouse exits canvas and button is down.
        e.preventDefault();
        e.stopImmediatePropagation();

        var canvas = document.getElementById('control');
        canvas.setPointerCapture(e.pointerId); 
        if (e.type == "pointerdown")
            PointerDown = true;
        else if (e.type == "pointerup")
            PointerDown = false;

        if (PointerDown == true) {
            updateValues(e);
        }
    }

    function ConvertWindowToCanvasCoords(e) {
        var canvas = document.getElementById('control');
        var point = document.getElementById('pointer');
        var rect = canvas.getBoundingClientRect();
        var x = e.x - rect.left;
        var y = e.y - rect.top;
    
        //var indicator = 1 - (x / canvas.clientWidth);
        return 1 - (x / canvas.clientWidth);
    }

    function updateValues(e) {
            var indicator = ConvertWindowToCanvasCoords(e);
            drawControl(indicator);

            //intIndicator is a member of the parent class
            Office.context.document.setSelectedDataAsync(intIndicator);
    }
})();
