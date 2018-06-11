"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.Paint = function () {

    var canvasMain,
        canvasPicker,
        ctxMain,
        ctxPicker,
        activeTool = 'pencil',
        bMouseDown = false,
        x1 = 0,
        y1 = 0,
        xp = 0,
        yp = 0,
        wp = 0,
        hp = 0,
        selColorR = 0,
        selColorG = 0,
        selColorB = 0,
        selColorVal = 'white',

        init = function () {

            //create canvas objects
            canvasMain = document.getElementById('main');
            ctxMain = canvasMain.getContext('2d');
            canvasPicker = document.getElementById('picker');
            ctxPicker = canvasPicker.getContext('2d');

            //draw color picker gradients
            var grad = ctxPicker.createLinearGradient(20, 0, canvasPicker.width - 20, 0);
            grad.addColorStop(0, 'red');
            grad.addColorStop(1 / 6, 'orange');
            grad.addColorStop(2 / 6, 'yellow');
            grad.addColorStop(3 / 6, 'green')
            grad.addColorStop(4 / 6, 'aqua');
            grad.addColorStop(5 / 6, 'blue');
            grad.addColorStop(1, 'purple');
            ctxPicker.fillStyle = grad;
            ctxPicker.fillRect(0, 0, canvasPicker.width, canvasPicker.height);

            //set main canvas color to white
            ctxMain.fillStyle = "white";
            ctxMain.fillRect(0, 0, canvasMain.width, canvasMain.height);

            //Tool selection
            $('#pencil').click(function (e) {
                activeTool = 'pencil'
            });

            $('#line').click(function (e) {
                activeTool = 'line';
            });

            $('#rectangle').click(function (e) {
                activeTool = 'rectangle';
            });

            //mouse move over color picker
            $('#picker').mousemove(function (e) {
                var canvasOffset = $(canvasPicker).offset();
                var canvasX = Math.floor(e.pageX - canvasOffset.left);
                var canvasY = Math.floor(e.pageY - canvasOffset.top);

                var imageData = ctxPicker.getImageData(canvasX, canvasY, 1, 1);
                var pixel = imageData.data;

                var pixelColor = "rgba(" + pixel[0] + ", " + pixel[1] + ", " + pixel[2] + ", " + pixel[3] + ")";
                $('#preview').css('backgroundColor', pixelColor);
            });

            //mouse click on color picker
            $('#picker').click(function (e) {
                var canvasOffset = $(canvasPicker).offset();
                var canvasX = Math.floor(e.pageX - canvasOffset.left);
                var canvasY = Math.floor(e.pageY - canvasOffset.top);

                var imageData = ctxPicker.getImageData(canvasX, canvasY, 1, 1);
                var pixel = imageData.data;

                $('#rgbVal').val(pixel[0] + ',' + pixel[1] + ',' + pixel[2]);

                selColorVal = "rgba(" + pixel[0] + ", " + pixel[1] + ", " + pixel[2] + ", " + pixel[3] + ")";
                $('#pick').css('backgroundColor', selColorVal);

                selColorR = pixel[0];
                selColorG = pixel[1];
                selColorB = pixel[2];
            });

            //mousedown on main canvas
            $('#main').mousedown(function (e) {
                bMouseDown = true;
                var canvasOffset = $(canvasMain).offset();
                var canvasX = Math.floor(e.pageX - canvasOffset.left);
                var canvasY = Math.floor(e.pageY - canvasOffset.top);
                x1 = canvasX;
                y1 = canvasY;
                xp = 0;
                yp = 0;
                wp = 0;
                hp = 0;
            });

            //mouse up on main canvas
            $('#main').mouseup(function (e) {


                bMouseDown = false;
                var canvasOffset = $(canvasMain).offset();
                var canvasX = Math.floor(e.pageX - canvasOffset.left);
                var canvasY = Math.floor(e.pageY - canvasOffset.top);
                var x = Math.min(canvasX, x1);
                var y = Math.min(canvasY, y1);
                var w = Math.abs(canvasX - x1);
                var h = Math.abs(canvasY - y1);

                switch (activeTool) {

                    case 'pencil':
                        bMouseDown = false;
                        break;

                    case 'line':
                        if (w && h) {

                            ctxMain.fillStyle = "white";
                            ctxMain.fillRect(xp - 1, yp - 1, wp + 2, hp + 2);
                            ctxMain.strokeStyle = selColorVal;
                            ctxMain.beginPath();
                            ctxMain.moveTo(x1, y1);
                            ctxMain.lineTo(canvasX, canvasY);
                            ctxMain.stroke();
                            ctxMain.closePath();
                        }

                        break;

                    case 'rectangle':
                        bMouseDown = false;
                        var canvasOffset = $(canvasMain).offset();
                        var canvasX = Math.floor(e.pageX - canvasOffset.left);
                        var canvasY = Math.floor(e.pageY - canvasOffset.top);
                        var x = Math.min(canvasX, x1);
                        var y = Math.min(canvasY, y1);
                        var w = Math.abs(canvasX - x1);
                        var h = Math.abs(canvasY - y1);

                        if (w && h) {

                            ctxMain.fillStyle = "white";
                            ctxMain.fillRect(xp - 1, yp - 1, wp + 2, hp + 2);
                            ctxMain.strokeStyle = selColorVal;
                            ctxMain.strokeRect(x, y, w, h);
                        }
                        break;
                }
            });

            //mousemove on main canvas
            $('#main').mousemove(function (e) {

                var canvasOffset = $(canvasMain).offset();
                var canvasX = Math.floor(e.pageX - canvasOffset.left);
                var canvasY = Math.floor(e.pageY - canvasOffset.top);

                switch (activeTool) {

                    case 'pencil':
                        if (bMouseDown) {

                            var imageData = ctxMain.getImageData(canvasX, canvasY, 1, 1);
                            var pixel = imageData.data;
                            pixel[0] = selColorR;
                            pixel[1] = selColorG;
                            pixel[2] = selColorB;
                            pixel[3] = 255;

                            ctxMain.putImageData(imageData, canvasX, canvasY);
                        }
                        break;

                    case 'line':
                        if (bMouseDown) {
                            var x = Math.min(canvasX, x1);
                            var y = Math.min(canvasY, y1);
                            var w = Math.abs(canvasX - x1);
                            var h = Math.abs(canvasY - y1);

                            if (w && h) {

                                ctxMain.fillStyle = "white";
                                ctxMain.fillRect(xp - 1, yp - 1, wp + 2, hp + 2);
                                ctxMain.strokeStyle = selColorVal;
                                ctxMain.beginPath();
                                ctxMain.moveTo(x1, y1);
                                ctxMain.lineTo(canvasX, canvasY);
                                ctxMain.stroke();
                                ctxMain.closePath();

                                xp = x;
                                yp = y;
                                wp = w;
                                hp = h;
                            }
                        }
                        break;

                    case 'rectangle':
                        if (bMouseDown) {

                            var x = Math.min(canvasX, x1);
                            var y = Math.min(canvasY, y1);
                            var w = Math.abs(canvasX - x1);
                            var h = Math.abs(canvasY - y1);

                            if (w && h) {

                                ctxMain.fillStyle = "white";
                                ctxMain.fillRect(xp - 1, yp - 1, wp + 2, hp + 2);
                                ctxMain.strokeStyle = selColorVal;
                                ctxMain.strokeRect(x, y, w, h);
                                xp = x;
                                yp = y;
                                wp = w;
                                hp = h;
                            }
                        }
                        break;
                }

            });

            //Reset image
            $('#clearImage').click(function (e) {
                ctxMain.fillStyle = "white";
                ctxMain.fillRect(0, 0, canvasMain.width, canvasMain.height);
            });

        };

    return {
        init: init
    }

}();

