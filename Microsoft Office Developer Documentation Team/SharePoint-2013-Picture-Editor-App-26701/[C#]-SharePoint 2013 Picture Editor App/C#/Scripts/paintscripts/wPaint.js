/******************************************
 * Websanova.com
 *
 * Resources for web entrepreneurs
 *
 * @author          Websanova
 * @copyright       Copyright (c) 2012 Websanova.
 * @license         This wPaint jQuery plug-in is dual licensed under the MIT and GPL licenses.
 * @link            http://www.websanova.com
 * @github			http://github.com/websanova/wPaint
 * @version         Version 1.7.0
 *
 ******************************************/

(function ($) {

    var iMouseX, iMouseY = 1;
    var theSelection = null;
    var iscrop = false;
    var orignalImg = null;
    var cropProgressImg = null;
    var isfirstload = false;

    $.fn.wPaint = function (option, settings) {
        if (typeof option === 'object') {
            settings = option;
        }
        else if (typeof option == 'string') {
            var values = [];

            var elements = this.each(function () {
                var data = $(this).data('_wPaint');

                if (data) {
                    if (option == 'clear') { data.clearAll(); }
                    else if (option == 'image' && settings === undefined) { values.push(data.getImage()); }
                    else if (option == 'image' && settings !== undefined) { data.setImage(settings, true); }
                    else if ($.fn.wPaint.defaultSettings[option] !== undefined) {
                        if (settings !== undefined) { data.settings[option] = settings; }
                        else { values.push(data.settings[option]); }
                    }
                }
            });

            if (values.length === 1) { return values[0]; }
            if (values.length > 0) { return values; }
            else { return elements; }
        }

        //clean up some variables
        settings = $.extend({}, $.fn.wPaint.defaultSettings, settings || {});
        settings.lineWidthMin = parseInt(settings.lineWidthMin);
        settings.lineWidthMax = parseInt(settings.lineWidthMax);
        settings.lineWidth = parseInt(settings.lineWidth);
        settings.fontSizeMin = parseInt(settings.fontSizeMin);
        settings.fontSizeMax = parseInt(settings.fontSizeMax);
        settings.fontSize = parseInt(settings.fontSize);

        return this.each(function () {
            var elem = $(this);
            var $settings = jQuery.extend(true, {}, settings);

            //test for HTML5 canvas
            var test = document.createElement('canvas');
            if (!test.getContext) {
                elem.html("Browser does not support HTML5 canvas, please upgrade to a more modern browser.");
                return false;
            }


            theSelection = null;
            iscrop = false;
            orignalImg = null;
            cropProgressImg = null;

            var canvas = new Canvas($settings, elem);
            canvas.mainMenu = new MainMenu();
            canvas.textMenu = new TextMenu();
            canvas.cropMenu = new CropMenu();
            canvas.effectMenu = new EffectsMenu();

            elem.append(canvas.generate(elem.width(), elem.height()));
            elem.append(canvas.generateTemp());
            elem.append(canvas.generateTextInput());
            //canvas.drawCropFrame();



            $('body')
			.append(canvas.mainMenu.generate(canvas, canvas.textMenu, canvas.cropMenu, canvas.effectMenu))
			.append(canvas.textMenu.generate(canvas, canvas.mainMenu))
            .append(canvas.cropMenu.generate(canvas, canvas.mainMenu))
            .append(canvas.effectMenu.generate(canvas, canvas.mainMenu));

            //init the snap on the text menu
            canvas.mainMenu.moveTextMenu(canvas.mainMenu, canvas.textMenu);

            canvas.mainMenu.moveCropMenu(canvas.mainMenu, canvas.cropMenu);

            canvas.mainMenu.moveEffectMenu(canvas.mainMenu, canvas.effectMenu);

            isfirstload = true;
            theSelection = new Selection(200, 200, 200, 200, canvas.ctx);

            //init mode
            canvas.mainMenu.set_mode(canvas.mainMenu, canvas, $settings.mode);

            //pull from css so that it is dynamic
            var buttonSize = $("._wPaint_icon").outerHeight(true) - (parseInt($("._wPaint_icon").css('paddingTop').split('px')[0]) + parseInt($("._wPaint_icon").css('paddingBottom').split('px')[0]));

            canvas.mainMenu.menu.find("._wPaint_fillColorPicker").wColorPicker({
                mode: "click",
                initColor: $settings.fillStyle,
                buttonSize: buttonSize,
                showSpeed: 300,
                hideSpeed: 300,
                onSelect: function (color) {
                    canvas.settings.fillStyle = color;
                    canvas.textInput.css({ color: color });
                }
            });

            canvas.mainMenu.menu.find("._wPaint_strokeColorPicker").wColorPicker({
                mode: "click",
                initColor: $settings.strokeStyle,
                buttonSize: buttonSize,
                showSpeed: 300,
                hideSpeed: 300,
                onSelect: function (color) {
                    canvas.settings.strokeStyle = color;
                }
            });

            if ($settings.image) {
                canvas.setImage($settings.image, true);
            }
            else {
                canvas.addUndo();
            }

            elem.data('_wPaint', canvas);
        });
    }

    var shapes = ['Rectangle', 'Ellipse', 'Line', 'Text'];

    $.fn.wPaint.defaultSettings = {
        mode: 'Pencil',			// drawing mode - Rectangle, Ellipse, Line, Pencil, Eraser
        lineWidthMin: '0', 				// line width min for select drop down
        lineWidthMax: '10',				// line widh max for select drop down
        lineWidth: '2', 				// starting line width
        fillStyle: '#FFFFFF',		// starting fill style
        strokeStyle: '#FFFF00',		// start stroke style
        fontSizeMin: '8',				// min font size in px
        fontSizeMax: '20',				// max font size in px
        fontSize: '12',				// current font size for text input
        fontFamilyOptions: ['Arial', 'Courier', 'Times', 'Trebuchet', 'Verdana'], // available font families
        fontFamily: 'Arial',			// active font family for text input
        fontTypeBold: false,			// text input bold enable/disable
        fontTypeItalic: false,			// text input italic enable/disable
        fontTypeUnderline: false,			// text input italic enable/disable
        image: null,				// preload image - base64 encoded data
        drawDown: null,				// function to call when start a draw
        drawMove: null,				// function to call during a draw
        drawUp: null,				// function to call at end of draw
        menu: ['undo', 'crop', 'effect', 'rotateLeft', 'rotateRight', 'rectangle', 'ellipse', 'line', 'pencil', 'text', 'eraser', 'fillColor', 'lineWidth', 'strokeColor'] // menu items - appear in order they are set
    };

    /**
	 * Canvas class definition
	 */
    function Canvas(settings, elem) {
        this.settings = settings;
        this.$elem = elem;
        this.mainMenu = null;
        this.textMenu = null;
        this.cropMenu = null;
        this.effectMenu = null;

        this.undoArray = [];
        this.undoCurrent = -1;
        this.undoMax = 10;

        this.draw = false;

        this.canvas = null;
        this.ctx = null;

        this.canvasTemp = null;
        this.ctxTemp = null;

        this.canvasTempLeftOriginal = null;
        this.canvasTempTopOriginal = null;

        this.canvasTempLeftNew = null;
        this.canvasTempTopNew = null;

        this.textInput = null;

        return this;
    }

    Canvas.prototype =
	{
	    /*******************************************************************************
		 * Generate canvases and events
		 *******************************************************************************/
	    generate: function (width, height) {
	        this.canvas = document.createElement('canvas');
	        this.ctx = this.canvas.getContext('2d');

	        //create local reference
	        var $this = this;

	        $(this.canvas)
			.attr('width', width + 'px')
			.attr('height', height + 'px')
			.css({ position: 'absolute', left: 0, top: 0 })
			.mousedown(function (e) {
			    e.preventDefault();
			    e.stopPropagation();
			    if ($this.settings.mode == 'Crop') {
			        $this.cropMouseDown(e, $this);
			    }
			    else {
			        $this.draw = true;
			        $this.callFunc(e, $this, 'Down');
			    }
			});

	        $(document)
			.mousemove(function (e) {
			    if ($this.settings.mode == 'Crop') {
			        $this.cropMouseMove(e, $this);
			    }
			    else {
			        if ($this.draw) $this.callFunc(e, $this, 'Move');
			    }
			})
			.mouseup(function (e) {
			    //make sure we are in draw mode otherwise this will fire on any mouse up.
			    if ($this.settings.mode == 'Crop') {
			        $this.cropMouseUp(e, $this);
			    } else if ($this.draw) {
			        $this.draw = false;
			        $this.callFunc(e, $this, 'Up');
			    }
			});

	        this.bindMobile();

	        return $(this.canvas);
	    },

	    cropMouseDown: function (e, $this) {
	        if (iscrop) {
	            var canvasOffset = jQuery($this.canvas).offset();
	            iMouseX = Math.floor(e.pageX - canvasOffset.left);
	            iMouseY = Math.floor(e.pageY - canvasOffset.top);

	            theSelection.px = iMouseX - theSelection.x;
	            theSelection.py = iMouseY - theSelection.y;

	            if (theSelection.bHow[0]) {
	                theSelection.px = iMouseX - theSelection.x;
	                theSelection.py = iMouseY - theSelection.y;
	            }
	            if (theSelection.bHow[1]) {
	                theSelection.px = iMouseX - theSelection.x - theSelection.w;
	                theSelection.py = iMouseY - theSelection.y;
	            }
	            if (theSelection.bHow[2]) {
	                theSelection.px = iMouseX - theSelection.x - theSelection.w;
	                theSelection.py = iMouseY - theSelection.y - theSelection.h;
	            }
	            if (theSelection.bHow[3]) {
	                theSelection.px = iMouseX - theSelection.x;
	                theSelection.py = iMouseY - theSelection.y - theSelection.h;
	            }


	            if (iMouseX > theSelection.x + theSelection.csizeh && iMouseX < theSelection.x + theSelection.w - theSelection.csizeh &&
                    iMouseY > theSelection.y + theSelection.csizeh && iMouseY < theSelection.y + theSelection.h - theSelection.csizeh) {

	                theSelection.bDragAll = true;
	            }

	            for (i = 0; i < 4; i++) {
	                if (theSelection.bHow[i]) {
	                    theSelection.bDrag[i] = true;
	                }
	            }
	        }
	    },

	    cropMouseMove: function (e, $this) {

	        if (iscrop) {

	            var canvasOffset = jQuery($this.canvas).offset();
	            iMouseX = Math.floor(e.pageX - canvasOffset.left);
	            iMouseY = Math.floor(e.pageY - canvasOffset.top);

	            // in case of drag of whole selector
	            if (theSelection.bDragAll) {
	                theSelection.x = iMouseX - theSelection.px;
	                theSelection.y = iMouseY - theSelection.py;
	            }

	            for (i = 0; i < 4; i++) {
	                theSelection.bHow[i] = false;
	                theSelection.iCSize[i] = theSelection.csize;
	            }

	            // hovering over resize cubes
	            if (iMouseX > theSelection.x - theSelection.csizeh && iMouseX < theSelection.x + theSelection.csizeh &&
                    iMouseY > theSelection.y - theSelection.csizeh && iMouseY < theSelection.y + theSelection.csizeh) {

	                theSelection.bHow[0] = true;
	                theSelection.iCSize[0] = theSelection.csizeh;
	            }
	            if (iMouseX > theSelection.x + theSelection.w - theSelection.csizeh && iMouseX < theSelection.x + theSelection.w + theSelection.csizeh &&
                    iMouseY > theSelection.y - theSelection.csizeh && iMouseY < theSelection.y + theSelection.csizeh) {

	                theSelection.bHow[1] = true;
	                theSelection.iCSize[1] = theSelection.csizeh;
	            }
	            if (iMouseX > theSelection.x + theSelection.w - theSelection.csizeh && iMouseX < theSelection.x + theSelection.w + theSelection.csizeh &&
                    iMouseY > theSelection.y + theSelection.h - theSelection.csizeh && iMouseY < theSelection.y + theSelection.h + theSelection.csizeh) {

	                theSelection.bHow[2] = true;
	                theSelection.iCSize[2] = theSelection.csizeh;
	            }
	            if (iMouseX > theSelection.x - theSelection.csizeh && iMouseX < theSelection.x + theSelection.csizeh &&
                    iMouseY > theSelection.y + theSelection.h - theSelection.csizeh && iMouseY < theSelection.y + theSelection.h + theSelection.csizeh) {

	                theSelection.bHow[3] = true;
	                theSelection.iCSize[3] = theSelection.csizeh;
	            }

	            // in case of dragging of resize cubes
	            var iFW, iFH;
	            if (theSelection.bDrag[0]) {
	                var iFX = iMouseX - theSelection.px;
	                var iFY = iMouseY - theSelection.py;
	                iFW = theSelection.w + theSelection.x - iFX;
	                iFH = theSelection.h + theSelection.y - iFY;
	            }
	            if (theSelection.bDrag[1]) {
	                var iFX = theSelection.x;
	                var iFY = iMouseY - theSelection.py;
	                iFW = iMouseX - theSelection.px - iFX;
	                iFH = theSelection.h + theSelection.y - iFY;
	            }
	            if (theSelection.bDrag[2]) {
	                var iFX = theSelection.x;
	                var iFY = theSelection.y;
	                iFW = iMouseX - theSelection.px - iFX;
	                iFH = iMouseY - theSelection.py - iFY;
	            }
	            if (theSelection.bDrag[3]) {
	                var iFX = iMouseX - theSelection.px;
	                var iFY = theSelection.y;
	                iFW = theSelection.w + theSelection.x - iFX;
	                iFH = iMouseY - theSelection.py - iFY;
	            }

	            if (iFW > theSelection.csizeh * 2 && iFH > theSelection.csizeh * 2) {
	                theSelection.w = iFW;
	                theSelection.h = iFH;

	                theSelection.x = iFX;
	                theSelection.y = iFY;
	            }

	            var myImage = new Image();
	            myImage.src = $this.canvas.toDataURL();
	            $(myImage).load(function () {
	                drawScene($this.ctx, cropProgressImg, true);
	            });
	        }
	    },

	    cropMouseUp: function (e, $this) {
	        if (iscrop) {
	            theSelection.bDragAll = false;

	            for (i = 0; i < 4; i++) {
	                theSelection.bDrag[i] = false;
	            }
	            theSelection.px = 0;
	            theSelection.py = 0;
	            drawScene($this.ctx, cropProgressImg, true);
	        }
	    },

	    bindMobile: function () {
	        $(this.canvas).bind('touchstart touchmove touchend touchcancel', function () {
	            var touches = event.changedTouches, first = touches[0], type = "";

	            switch (event.type) {
	                case "touchstart": type = "mousedown"; break;
	                case "touchmove": type = "mousemove"; break;
	                case "touchend": type = "mouseup"; break;
	                default: return;
	            }

	            var simulatedEvent = document.createEvent("MouseEvent");

	            simulatedEvent.initMouseEvent(type, true, true, window, 1, first.screenX, first.screenY, first.clientX, first.clientY, false, false, false, false, 0/*left*/, null);
	            first.target.dispatchEvent(simulatedEvent);
	            event.preventDefault();
	        });
	    },

	    generateTemp: function () {
	        this.canvasTemp = document.createElement('canvas');
	        this.ctxTemp = this.canvasTemp.getContext('2d');

	        $(this.canvasTemp).css({ position: 'absolute' }).hide();

	        return $(this.canvasTemp);
	    },

	    generateTextInput: function () {
	        var $this = this;

	        $this.textCalc = $('<div></div>').css({ display: 'none', fontSize: this.settings.fontSize, lineHeight: this.settings.fontSize + 'px', fontFamily: this.settings.fontFamily });

	        $this.textInput =
            $('<textarea class="_wPaint_textInput" spellcheck="false"></textarea>')
            .css({ display: 'none', position: 'absolute', color: this.settings.fillStyle, fontSize: this.settings.fontSize, lineHeight: this.settings.fontSize + 'px', fontFamily: this.settings.fontFamily })

	        if ($this.settings.fontTypeBold) { $this.textInput.css('fontWeight', 'bold'); $this.textCalc.css('fontWeight', 'bold'); }
	        if ($this.settings.fontTypeItalic) { $this.textInput.css('fontStyle', 'italic'); $this.textCalc.css('fontStyle', 'italic'); }
	        if ($this.settings.fontTypeUnderline) { $this.textInput.css('textDecoration', 'underline'); $this.textCalc.css('textDecoration', 'underline'); }

	        $('body').append($this.textCalc);

	        return $this.textInput;
	    },

	    callFunc: function (e, $this, event) {
	        $e = jQuery.extend(true, {}, e);

	        var canvas_offset = $($this.canvas).offset();

	        $e.pageX = Math.floor($e.pageX - canvas_offset.left);
	        $e.pageY = Math.floor($e.pageY - canvas_offset.top);

	        var mode = $.inArray($this.settings.mode, shapes) > -1 ? 'Shape' : $this.settings.mode;
	        var func = $this['draw' + mode + '' + event];

	        if (func) func($e, $this);

	        if ($this.settings['draw' + event]) $this.settings['draw' + event].apply($this, [e, mode]);

	        if ($this.settings.mode !== 'Text' && event === 'Up') { this.addUndo(); }
	    },

	    /*******************************************************************************
         * draw any shape
         *******************************************************************************/
	    drawShapeDown: function (e, $this) {
	        if ($this.settings.mode == 'Text') {
	            //draw current text before resizing next text box
	            if ($this.textInput.val() != '') $this.drawTextUp(e, $this);

	            $this.textInput.css({ left: e.pageX - 1, top: e.pageY - 1, width: 0, height: 0 });
	        }

	        $($this.canvasTemp)
            .css({ left: e.pageX, top: e.pageY })
            .attr('width', 0)
            .attr('height', 0)
            .show();

	        $this.canvasTempLeftOriginal = e.pageX;
	        $this.canvasTempTopOriginal = e.pageY;

	        var func = $this['draw' + $this.settings.mode + 'Down'];

	        if (func) func(e, $this);
	    },

	    drawShapeMove: function (e, $this) {
	        var xo = $this.canvasTempLeftOriginal;
	        var yo = $this.canvasTempTopOriginal;

	        var half_line_width = $this.settings.lineWidth / 2;

	        var left = (e.pageX < xo ? e.pageX : xo) - ($this.settings.mode == 'Line' ? Math.floor(half_line_width) : 0);
	        var top = (e.pageY < yo ? e.pageY : yo) - ($this.settings.mode == 'Line' ? Math.floor(half_line_width) : 0);
	        var width = Math.abs(e.pageX - xo) + ($this.settings.mode == 'Line' ? $this.settings.lineWidth : 0);
	        var height = Math.abs(e.pageY - yo) + ($this.settings.mode == 'Line' ? $this.settings.lineWidth : 0);

	        $($this.canvasTemp)
            .css({ left: left, top: top })
            .attr('width', width)
            .attr('height', height)

	        if ($this.settings.mode == 'Text') $this.textInput.css({ left: left - 1, top: top - 1, width: width, height: height });

	        $this.canvasTempLeftNew = left;
	        $this.canvasTempTopNew = top;

	        var func = $this['draw' + $this.settings.mode + 'Move'];

	        if (func) {
	            var factor = $this.settings.mode == 'Line' ? 1 : 2;

	            e.x = half_line_width * factor;
	            e.y = half_line_width * factor;
	            e.w = width - $this.settings.lineWidth * factor;
	            e.h = height - $this.settings.lineWidth * factor;

	            $this.ctxTemp.fillStyle = $this.settings.fillStyle;
	            $this.ctxTemp.strokeStyle = $this.settings.strokeStyle;
	            $this.ctxTemp.lineWidth = $this.settings.lineWidth * factor;

	            func(e, $this);
	        }
	    },

	    drawShapeUp: function (e, $this) {
	        if ($this.settings.mode != 'Text') {
	            $this.ctx.drawImage($this.canvasTemp, $this.canvasTempLeftNew, $this.canvasTempTopNew);

	            $($this.canvasTemp).hide();

	            var func = $this['draw' + $this.settings.mode + 'Up'];
	            if (func) func(e, $this);
	        }
	    },

	    /*******************************************************************************
         * draw rectangle
         *******************************************************************************/
	    drawRectangleMove: function (e, $this) {
	        $this.ctxTemp.beginPath();
	        $this.ctxTemp.rect(e.x, e.y, e.w, e.h)
	        $this.ctxTemp.closePath();
	        $this.ctxTemp.stroke();
	        $this.ctxTemp.fill();
	    },

	    /*******************************************************************************
         * draw ellipse
         *******************************************************************************/
	    drawEllipseMove: function (e, $this) {
	        var kappa = .5522848;
	        var ox = (e.w / 2) * kappa; 	// control point offset horizontal
	        var oy = (e.h / 2) * kappa; 	// control point offset vertical
	        var xe = e.x + e.w;           	// x-end
	        var ye = e.y + e.h;           	// y-end
	        var xm = e.x + e.w / 2;       	// x-middle
	        var ym = e.y + e.h / 2;       	// y-middle

	        $this.ctxTemp.beginPath();
	        $this.ctxTemp.moveTo(e.x, ym);
	        $this.ctxTemp.bezierCurveTo(e.x, ym - oy, xm - ox, e.y, xm, e.y);
	        $this.ctxTemp.bezierCurveTo(xm + ox, e.y, xe, ym - oy, xe, ym);
	        $this.ctxTemp.bezierCurveTo(xe, ym + oy, xm + ox, ye, xm, ye);
	        $this.ctxTemp.bezierCurveTo(xm - ox, ye, e.x, ym + oy, e.x, ym);
	        $this.ctxTemp.closePath();
	        if ($this.settings.lineWidth > 0) $this.ctxTemp.stroke();
	        $this.ctxTemp.fill();
	    },

	    /*******************************************************************************
         * draw line
         *******************************************************************************/
	    drawLineMove: function (e, $this) {
	        var xo = $this.canvasTempLeftOriginal;
	        var yo = $this.canvasTempTopOriginal;

	        if (e.pageX < xo) { e.x = e.x + e.w; e.w = e.w * -1 }
	        if (e.pageY < yo) { e.y = e.y + e.h; e.h = e.h * -1 }

	        $this.ctxTemp.lineJoin = "round";
	        $this.ctxTemp.beginPath();
	        $this.ctxTemp.moveTo(e.x, e.y);
	        $this.ctxTemp.lineTo(e.x + e.w, e.y + e.h);
	        $this.ctxTemp.closePath();
	        $this.ctxTemp.stroke();
	    },

	    /*******************************************************************************
         * draw pencil
         *******************************************************************************/
	    drawPencilDown: function (e, $this) {
	        $this.ctx.lineJoin = "round";
	        $this.ctx.lineCap = "round";
	        $this.ctx.strokeStyle = $this.settings.strokeStyle;
	        $this.ctx.fillStyle = $this.settings.strokeStyle;
	        $this.ctx.lineWidth = $this.settings.lineWidth;

	        //draw single dot in case of a click without a move
	        $this.ctx.beginPath();
	        $this.ctx.arc(e.pageX, e.pageY, $this.settings.lineWidth / 2, 0, Math.PI * 2, true);
	        $this.ctx.closePath();
	        $this.ctx.fill();

	        //start the path for a drag
	        $this.ctx.beginPath();
	        $this.ctx.moveTo(e.pageX, e.pageY);
	    },

	    drawPencilMove: function (e, $this) {
	        $this.ctx.lineTo(e.pageX, e.pageY);
	        $this.ctx.stroke();
	    },

	    drawPencilUp: function (e, $this) {
	        $this.ctx.closePath();
	    },


	    /*******************************************************************************
         * draw text
         *******************************************************************************/

	    drawTextDown: function (e, $this) {
	        $this.textInput.val('').show().focus();
	    },

	    drawTextUp: function (e, $this) {
	        if (e) { this.addUndo(); }

	        var fontString = '';
	        if ($this.settings.fontTypeItalic) fontString += 'italic ';
	        //if($this.settings.fontTypeUnderline) fontString += 'underline ';
	        if ($this.settings.fontTypeBold) fontString += 'bold ';

	        fontString += $this.settings.fontSize + 'px ' + $this.settings.fontFamily;

	        //setup lines
	        var lines = $this.textInput.val().split('\n');
	        var linesNew = [];
	        var textInputWidth = $this.textInput.width() - 2;

	        var width = 0;
	        var lastj = 0;

	        for (var i = 0, ii = lines.length; i < ii; i++) {
	            $this.textCalc.html('');
	            lastj = 0;

	            for (var j = 0, jj = lines[0].length; j < jj; j++) {
	                width = $this.textCalc.append(lines[i][j]).width();

	                if (width > textInputWidth) {
	                    linesNew.push(lines[i].substring(lastj, j));
	                    lastj = j;
	                    $this.textCalc.html(lines[i][j]);
	                }
	            }

	            if (lastj != j) linesNew.push(lines[i].substring(lastj, j));
	        }

	        lines = $this.textInput.val(linesNew.join('\n')).val().split('\n');

	        var offset = $this.textInput.position();
	        var left = offset.left;// + parseInt($this.fontOffsets[$this.settings.fontFamily][$this.settings.fontSize][0] || 0);
	        var top = offset.top;// + parseInt($this.fontOffsets[$this.settings.fontFamily][$this.settings.fontSize][1] || 0);
	        var underlineOffset = 0;// = parseInt($this.fontOffsets[$this.settings.fontFamily][$this.settings.fontSize][2] || 0);

	        for (var i = 0, ii = lines.length; i < ii; i++) {
	            $this.ctx.fillStyle = $this.settings.fillStyle;

	            $this.ctx.textBaseline = 'top';
	            $this.ctx.font = fontString;
	            $this.ctx.fillText(lines[i], left, top);

	            top += $this.settings.fontSize;

	            if (lines[i] != '' && $this.settings.fontTypeUnderline) {
	                width = $this.textCalc.html(lines[i]).width();

	                //manually set pixels for underline since to avoid antialiasing 1px issue, and lack of support for underline in canvas
	                var imgData = $this.ctx.getImageData(0, top + underlineOffset, width, 1);

	                for (j = 0; j < imgData.width * imgData.height * 4; j += 4) {
	                    imgData.data[j] = parseInt($this.settings.fillStyle.substring(1, 3), 16);
	                    imgData.data[j + 1] = parseInt($this.settings.fillStyle.substring(3, 5), 16);
	                    imgData.data[j + 2] = parseInt($this.settings.fillStyle.substring(5, 7), 16);
	                    imgData.data[j + 3] = 255;
	                }

	                $this.ctx.putImageData(imgData, left, top + underlineOffset);
	            }
	        }
	    },

	    /*******************************************************************************
         * eraser
         *******************************************************************************/
	    drawEraserDown: function (e, $this) {
	        $this.ctx.save();
	        $this.ctx.globalCompositeOperation = 'destination-out';
	        $this.drawPencilDown(e, $this);
	    },

	    drawEraserMove: function (e, $this) {
	        $this.drawPencilMove(e, $this);
	    },

	    drawEraserUp: function (e, $this) {
	        $this.drawPencilUp(e, $this);
	        $this.ctx.restore();
	    },

	    /*******************************************************************************
         * save / load data
         *******************************************************************************/
	    getImage: function () {
	        return this.canvas.toDataURL();
	    },

	    setImage: function (data, addUndo) {
	        var $this = this;
	        var myImage = new Image();
	        myImage.src = data.toString();
	        $this.ctx.clearRect(0, 0, $this.canvas.width, $this.canvas.height);
	        // draw source image	        
	        $(myImage).load(function () {
	            $this.canvas.width = myImage.width;
	            $this.canvas.height = myImage.height;
	            $this.ctx.clearRect(0, 0, $this.canvas.width, $this.canvas.height);
	            $this.ctx.drawImage(myImage, 0, 0);
	            if (addUndo) { $this.addUndo(); }
	            $("#wPaint").width(myImage.width);
	            $("#wPaint").height(myImage.height);
	        });
	    },

	    /*******************************************************************************
         * undo / redo
         *******************************************************************************/

	    addUndo: function () {
	        //if it's not at the end of the array we need to repalce the current array position
	        if (this.undoCurrent < this.undoArray.length - 1) {
	            this.undoArray[++this.undoCurrent] = this.getImage();
	        }
	        else // owtherwise we push normally here
	        {
	            this.undoArray.push(this.getImage());

	            //if we're at the end of the array we need to slice off the front - in increment required
	            if (this.undoArray.length > this.undoMax) { this.undoArray = this.undoArray.slice(1, this.undoArray.length); }
	                //if we're NOT at the end of the array, we just increment
	            else { this.undoCurrent++; }
	        }

	        //for undo's then a new draw we want to remove everything afterwards - in most cases nothing will happen here
	        while (this.undoCurrent != this.undoArray.length - 1) { this.undoArray.pop(); }

	        this.undoToggleIcons();
	    },

	    setUndoImage: function () {
	        this.setImage(this.undoArray[this.undoCurrent]);
	    },

	    undoNext: function () {
	        if (this.undoArray[this.undoCurrent + 1]) { this.undoCurrent++; this.setUndoImage(); }

	        this.undoToggleIcons();
	    },

	    undoPrev: function () {
	        if (this.undoArray[this.undoCurrent - 1]) { this.undoCurrent--; this.setUndoImage(); }

	        this.undoToggleIcons();
	    },

	    undoToggleIcons: function () {
	        var iconUndo = this.mainMenu.menu.find("._wPaint_undo");
	        var iconRedo = this.mainMenu.menu.find("._wPaint_redo");

	        if (this.undoCurrent > 0 && this.undoArray.length > 1) {
	            if (!iconUndo.hasClass('uactive')) { iconUndo.addClass('uactive'); }
	        }
	        else { iconUndo.removeClass('uactive'); }

	        if (this.undoCurrent < this.undoArray.length - 1) {
	            if (!iconRedo.hasClass('uactive')) { iconRedo.addClass('uactive'); }
	        }
	        else { iconRedo.removeClass('uactive'); }
	    },

	    /*******************************************************************************
         * Functions
         *******************************************************************************/
	    clearAll: function () {
	        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

	        this.addUndo();
	    },
	}



    /**
     * Main Menu
     */
    function MainMenu() {
        this.menu = null;

        return this;
    }

    MainMenu.prototype =
    {
        generate: function (canvas, textMenu, cropMenu, effectMenu) {
            var $canvas = canvas;
            this.textMenu = textMenu;
            this.cropMenu = cropMenu;
            this.effectMenu = effectMenu;

            var $this = this;

            //setup the line width select
            var options = '';
            for (var i = $canvas.settings.lineWidthMin; i <= $canvas.settings.lineWidthMax; i++) options += '<option value="' + i + '" ' + ($canvas.settings.lineWidth == i ? 'selected="selected"' : '') + '>' + i + '</option>';

            var lineWidth = $('<div class="_wPaint_lineWidth _wPaint_dropDown" title="line width"></div>').append(
                $('<select>' + options + '</select>')
                .change(function (e) { $canvas.settings.lineWidth = parseInt($(this).val()); })
            )

            //content
            var menuContent = $('<div class="_wPaint_options"></div>');

            $.each($canvas.settings.menu, function (i, item) {
                switch (item) {
                    case 'undo': menuContent.append($('<div class="_wPaint_icon _wPaint_undo" title="undo"></div>').click(function () { $canvas.undoPrev(); })).append($('<div class="_wPaint_icon _wPaint_redo" title="redo"></div>').click(function () { $canvas.undoNext(); })); break;
                    case 'clear': menuContent.append($('<div class="_wPaint_icon _wPaint_clear" title="clear"></div>').click(function () { $canvas.clearAll(); })); break;
                    case 'crop': menuContent.append($('<div class="_wPaint_icon _wPaint_crop" title="crop"></div>').click(function () { $this.set_mode($this, $canvas, 'Crop'); })); break;
                    case 'effect': menuContent.append($('<div class="_wPaint_icon _wPaint_effect" title="effects"></div>').click(function () { $this.set_mode($this, $canvas, 'Effect'); })); break;
                    case 'rotateLeft': menuContent.append($('<div class="_wPaint_icon _wPaint_rotateleft" title="rotate left"></div>').click(function () { $this.set_mode($this, $canvas, 'RotateLeft'); })); break;
                    case 'rotateRight': menuContent.append($('<div class="_wPaint_icon _wPaint_rotateright" title="rotate right"></div>').click(function () { $this.set_mode($this, $canvas, 'RotateRight'); })); break;
                    case 'rectangle': menuContent.append($('<div class="_wPaint_icon _wPaint_rectangle" title="rectangle"></div>').click(function () { $this.set_mode($this, $canvas, 'Rectangle'); })); break;
                    case 'ellipse': menuContent.append($('<div class="_wPaint_icon _wPaint_ellipse" title="ellipse"></div>').click(function () { $this.set_mode($this, $canvas, 'Ellipse'); })); break;
                    case 'line': menuContent.append($('<div class="_wPaint_icon _wPaint_line" title="line"></div>').click(function () { $this.set_mode($this, $canvas, 'Line'); })); break;
                    case 'pencil': menuContent.append($('<div class="_wPaint_icon _wPaint_pencil" title="pencil"></div>').click(function () { $this.set_mode($this, $canvas, 'Pencil'); })); break;
                    case 'text': menuContent.append($('<div class="_wPaint_icon _wPaint_text" title="text"></div>').click(function () { $this.set_mode($this, $canvas, 'Text'); })); break;
                    case 'eraser': menuContent.append($('<div class="_wPaint_icon _wPaint_eraser" title="eraser"></div>').click(function (e) { $this.set_mode($this, $canvas, 'Eraser'); })); break;
                    case 'fillColor': menuContent.append($('<div class="_wPaint_fillColorPicker _wPaint_colorPicker" title="fill color"></div>')); break;
                    case 'lineWidth': menuContent.append(lineWidth); break;
                    case 'strokeColor': menuContent.append($('<div class="_wPaint_strokeColorPicker _wPaint_colorPicker" title="stroke color"></div>')); break;
                }
            });

            //handle
            var menuHandle = $('<div id="frontHandle" class="_wPaint_handle"></div>');
            var menuHandlerear = $('<div id="readHandle" class="_wPaint_handle"></div>');

            //get position of canvas
            var offset = $($canvas.canvas).offset();

            //menu
            return this.menu =
            $('<div id="mainmenu" class="_wPaint_menu"></div>')
            .css({ position: 'absolute', left: offset.left + 5, top: offset.top + 5 })
            .draggable({
                //handle: menuHandle,
                handle: '#frontHandle, #readHandle',
                drag: function () {
                    $this.moveTextMenu($this, $this.textMenu);
                    $this.moveCropMenu($this, $this.cropMenu);
                    $this.moveEffectMenu($this, $this.effectMenu)
                },
                stop: function () {
                    $this.moveTextMenu($this, $this.textMenu);
                    $this.moveCropMenu($this, $this.cropMenu);
                    $this.moveEffectMenu($this, $this.effectMenu)
                }
            })
            //.resizable()
            .append(menuHandle)
            .append(menuContent)
            .append(menuHandlerear);
        },

        moveTextMenu: function (mainMenu, textMenu) {
            if (textMenu.docked) {
                textMenu.menu.css({ left: parseInt(mainMenu.menu.css('left')) + textMenu.dockOffsetLeft, top: parseInt(mainMenu.menu.css('top')) + textMenu.dockOffsetTop });
            }
        },

        moveCropMenu: function (mainMenu, cropMenu) {
            if (cropMenu.docked) {
                cropMenu.menu.css({ left: parseInt(mainMenu.menu.css('left')) + cropMenu.dockOffsetLeft, top: parseInt(mainMenu.menu.css('top')) + cropMenu.dockOffsetTop });
            }
        },

        moveEffectMenu: function (mainMenu, effectMenu) {
            if (effectMenu.docked) {
                effectMenu.menu.css({ left: parseInt(mainMenu.menu.css('left')) + effectMenu.dockOffsetLeft, top: parseInt(mainMenu.menu.css('top')) + effectMenu.dockOffsetTop });
            }
        },

        set_mode: function ($this, $canvas, mode) {
            $canvas.settings.mode = mode;

            if (mode == 'Text') {
                if (iscrop) {
                    iscrop = false;
                    drawScene($canvas.ctx, orignalImg, false);
                }

                $this.textMenu.menu.show();
                $this.cropMenu.menu.hide();
                $this.effectMenu.menu.hide();
            }
            else if (mode == 'Crop') {
                if (!iscrop) {
                    iscrop = true;
                    theSelection = new Selection(200, 200, 200, 200, $canvas.ctx);
                    var myImage = new Image();
                    myImage.src = $canvas.canvas.toDataURL();
                    $(myImage).load(function () {
                        orignalImg = myImage;
                        cropProgressImg = myImage;
                        drawScene($canvas.ctx, cropProgressImg, true);

                        $this.cropMenu.menu.show();
                        $this.effectMenu.menu.hide();
                        $canvas.drawTextUp(null, $canvas);
                        $this.textMenu.menu.hide();
                        $canvas.textInput.hide();
                    });
                }
            }
            else if (mode == 'Effect') {
                if (iscrop && !isfirstload) {
                    iscrop = false;
                    drawScene($canvas.ctx, orignalImg, false);
                }

                $this.effectMenu.menu.show();
                $this.cropMenu.menu.hide();
                $canvas.drawTextUp(null, $canvas);
                $this.textMenu.menu.hide();
                $canvas.textInput.hide();
            }
            else if (mode == 'RotateLeft' || mode == 'RotateRight') {
                if (iscrop && !isfirstload) {
                    iscrop = false;
                    drawScene($canvas.ctx, orignalImg, false);
                }

                var myImage = new Image();
                myImage.src = $canvas.canvas.toDataURL();
                $(myImage).load(function () {
                    direction = null;
                    if (mode == 'RotateLeft') {
                        direction = 'left';
                    }
                    else {
                        direction = 'right';
                    }

                    rotateImage($canvas, myImage, direction);

                    $this.cropMenu.menu.hide();
                    $this.effectMenu.menu.hide();
                    $canvas.drawTextUp(null, $canvas);
                    $this.textMenu.menu.hide();
                    $canvas.textInput.hide();
                });
            }
            else {
                if (iscrop && !isfirstload) {
                    iscrop = false;
                    drawScene($canvas.ctx, orignalImg, false);
                }

                $canvas.drawTextUp(null, $canvas);
                $this.textMenu.menu.hide();
                $canvas.textInput.hide();
                $this.cropMenu.menu.hide();
                $this.effectMenu.menu.hide();
            }

            $this.menu.find("._wPaint_icon").removeClass('active');
            $this.menu.find("._wPaint_" + mode.toLowerCase()).addClass('active');

            if (isfirstload) {
                isfirstload = false;
            }

        }
    }

    function Selection(x, y, w, h, ctx) {
        this.x = x; // initial positions
        this.y = y;
        this.w = w; // and size
        this.h = h;

        this.px = x; // extra variables to dragging calculations
        this.py = y;

        this.csize = 6; // resize cubes size
        this.csizeh = 10; // resize cubes size (on hover)

        this.bHow = [false, false, false, false]; // hover statuses
        this.iCSize = [this.csize, this.csize, this.csize, this.csize]; // resize cubes sizes
        this.bDrag = [false, false, false, false]; // drag statuses
        this.bDragAll = false; // drag whole selection

        this.ctx1 = ctx;
    }

    // define Selection draw method
    Selection.prototype =
    {
        draw: function (image) {
            this.ctx1.strokeStyle = '#000';
            this.ctx1.lineWidth = 2;
            this.ctx1.strokeRect(this.x, this.y, this.w, this.h);

            // draw part of original image
            if (this.w > 0 && this.h > 0) {
                this.ctx1.drawImage(image, this.x, this.y, this.w, this.h, this.x, this.y, this.w, this.h);
            }

            // draw resize cubes
            this.ctx1.fillStyle = '#fff';
            this.ctx1.fillRect(this.x - this.iCSize[0], this.y - this.iCSize[0], this.iCSize[0] * 2, this.iCSize[0] * 2);
            this.ctx1.fillRect(this.x + this.w - this.iCSize[1], this.y - this.iCSize[1], this.iCSize[1] * 2, this.iCSize[1] * 2);
            this.ctx1.fillRect(this.x + this.w - this.iCSize[2], this.y + this.h - this.iCSize[2], this.iCSize[2] * 2, this.iCSize[2] * 2);
            this.ctx1.fillRect(this.x - this.iCSize[3], this.y + this.h - this.iCSize[3], this.iCSize[3] * 2, this.iCSize[3] * 2);
        }
    }

    function drawScene(ctx, image, isdraw) { // main drawScene function
        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height); // clear canvas

        // draw source image
        ctx.drawImage(image, 0, 0, ctx.canvas.width, ctx.canvas.height);

        // draw selection
        if (isdraw) {
            // and make it darker
            ctx.fillStyle = 'rgba(0, 0, 0, 0.5)';
            ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
            theSelection.draw(image);
        };
    }

    function rotateImage($this, image) {
        $this.ctx.clearRect(0, 0, $this.ctx.canvas.width, $this.ctx.canvas.height); // clear canvas
        $this.ctx.save();

        switch (direction) {
            case 'right':
                $("#wPaint").width(image.height);
                $("#wPaint").height(image.width);
                $this.ctx.canvas.width = image.height;
                $this.ctx.canvas.height = image.width;
                $this.ctx.rotate(90 * Math.PI / 180);
                $this.ctx.drawImage(image, 0, -image.height);
                break;

            case 'left':
                $("#wPaint").width(image.height);
                $("#wPaint").height(image.width);
                $this.ctx.canvas.width = image.height;
                $this.ctx.canvas.height = image.width;
                $this.ctx.rotate(270 * Math.PI / 180);
                $this.ctx.drawImage(image, -image.width, 0);
                break;
        }

        $this.ctx.restore();
        var mytemp = new Image();
        mytemp.src = $this.canvas.toDataURL();
        $(mytemp).load(function () {
            $this.canvas.width = mytemp.width;
            $this.canvas.height = mytemp.height;
            $this.ctx.clearRect(0, 0, mytemp.width, mytemp.height);
            $this.ctx.drawImage(mytemp, 0, 0);
        });
    }

    function CropMenu() {
        this.menu = null;

        this.docked = true;

        this.dockOffsetLeft = 0;
        this.dockOffsetTop = 36;

        return this;
    }

    CropMenu.prototype =
    {
        generate: function (canvas, mainMenu) {
            var $canvas = canvas;
            var $this = this;

            var menuContent =
           $('<div class="_wPaint_options"></div>')
           .append($('<div class="_wPaint_icon _wPaint_cropcheck" title="accept change"></div>').click(function () {
               //var resule = confirm("This operation cannot be undone. Do you want to continue?");
               //if (resule == true) {
               $this.setCropType($this, $canvas, 'Crop');
               //}
           }))

            //handle
            var menuHandle = $('<div class="_wPaint_handle"></div>')

            //get position of canvas
            var offset = $($canvas.canvas).offset();

            //menu
            return this.menu =
            $('<div id="paintmainmenu" class="_wPaint_menu"></div>')
            .css({ display: 'none', position: 'absolute' })
            .draggable({
                snap: '._wPaint_menu',
                handle: menuHandle,
                stop: function () {
                    $.each($(this).data('draggable').snapElements, function (index, element) {
                        $this.dockOffsetLeft = $this.menu.offset().left - mainMenu.menu.offset().left;
                        $this.dockOffsetTop = $this.menu.offset().top - mainMenu.menu.offset().top;
                        $this.docked = element.snapping;
                    });
                }
            })
            .append(menuHandle)
            .append(menuContent);
        },

        setCropType: function ($this, $canvas, mode) {
            var element = $this.menu.find("._wPaint_" + mode.toLowerCase());
            var thismenu = $this.menu.find("._wPaint_cropcheck");

            $canvas.ctx.drawImage(orignalImg, 0, 0);
            $canvas.addUndo();

            var temp_ctx, temp_canvas;
            temp_canvas = document.createElement('canvas');
            temp_ctx = temp_canvas.getContext('2d');
            temp_canvas.width = theSelection.w;
            temp_canvas.height = theSelection.h;
            temp_ctx.drawImage(cropProgressImg, theSelection.x, theSelection.y, theSelection.w, theSelection.h, 0, 0, theSelection.w, theSelection.h);
            var vData = temp_canvas.toDataURL();

            $canvas.setImage(vData, true);
            $canvas.settings.mode = 'Pencil';

            thismenu.parent().parent().hide();
            $("._wPaint_" + mode.toLowerCase()).removeClass('active');
            $("._wPaint_" + 'Pencil'.toLowerCase()).addClass('active');

            var croppedImage = new Image();
            croppedImage.src = vData;
            $(croppedImage).load(function () {
                orignalImg = croppedImage;
                cropProgressImg = null;
                iscrop = false;
            });
        }
    }

    function EffectsMenu() {
        this.menu = null;

        this.docked = true;

        this.dockOffsetLeft = 0;
        this.dockOffsetTop = 36;

        return this;
    }

    EffectsMenu.prototype =
    {
        generate: function (canvas, mainMenu) {
            var $canvas = canvas;
            var $this = this;

            var menuContent =
           $('<div class="_wPaint_options"></div>')
           .append($('<div class="_wPaint_icon _wPaint_effectGrey" title="grayscale"></div>').click(function () {
               $this.setEffect($this, $canvas, 'Crop', 'grayscale');
           }))
           .append($('<div class="_wPaint_icon _wPaint_effectSepia" title="sepia"></div>').click(function () {
               $this.setEffect($this, $canvas, 'Effect', 'sepia');
           }))
           .append($('<div class="_wPaint_icon _wPaint_effectBrighter" title="brighter"></div>').click(function () {
               $this.setEffect($this, $canvas, 'Effect', 'brighter');
           }))
            .append($('<div class="_wPaint_icon _wPaint_effectDarker" title="darker"></div>').click(function () {
                $this.setEffect($this, $canvas, 'Effect', 'darker');
            }))


            //handle
            var menuHandle = $('<div class="_wPaint_handle"></div>')

            //get position of canvas
            var offset = $($canvas.canvas).offset();

            //menu
            return this.menu =
            $('<div class="_wPaint_menu"></div>')
            .css({ display: 'none', position: 'absolute' })
            .draggable({
                snap: '._wPaint_menu',
                handle: menuHandle,
                stop: function () {
                    $.each($(this).data('draggable').snapElements, function (index, element) {
                        $this.dockOffsetLeft = $this.menu.offset().left - mainMenu.menu.offset().left;
                        $this.dockOffsetTop = $this.menu.offset().top - mainMenu.menu.offset().top;
                        $this.docked = element.snapping;
                    });
                }
            })
            .append(menuHandle)
            .append(menuContent);
        },

        setEffect: function ($this, $canvas, mode, effect) {
            var element = $this.menu.find("._wPaint_" + mode.toLowerCase());
            $canvas.addUndo();
            var imageData = $canvas.ctx.getImageData(0, 0, $canvas.canvas.width, $canvas.canvas.height);
            switch (effect) {
                case 'grayscale':
                    var d = imageData.data;
                    for (var i = 0; i < d.length; i += 4) {
                        var r = d[i];
                        var g = d[i + 1];
                        var b = d[i + 2];
                        d[i] = d[i + 1] = d[i + 2] = (r + g + b) / 3;
                    }

                    break;

                case 'sepia':
                    var d = imageData.data;
                    for (var i = 0; i < d.length; i += 4) {
                        var r = d[i];
                        var g = d[i + 1];
                        var b = d[i + 2];
                        d[i] = (r * 0.393) + (g * 0.769) + (b * 0.189); // red
                        d[i + 1] = (r * 0.349) + (g * 0.686) + (b * 0.168); // green
                        d[i + 2] = (r * 0.272) + (g * 0.534) + (b * 0.131); // blue
                    }

                    break;

                case 'brighter':
                    delta = 30;
                    var d = imageData.data;
                    for (var i = 0; i < d.length; i += 4) {
                        d[i] += delta;     // red
                        d[i + 1] += delta; // green
                        d[i + 2] += delta; // blue   
                    }

                    break;

                case 'darker':
                    delta = -30;
                    var d = imageData.data;
                    for (var i = 0; i < d.length; i += 4) {
                        d[i] += delta;     // red
                        d[i + 1] += delta; // green
                        d[i + 2] += delta; // blue   
                    }

                    break;
            }

            $canvas.ctx.putImageData(imageData, 0, 0);
        }
    }

    /**
     * Text Helper
     */
    function TextMenu() {
        this.menu = null;

        this.docked = true;

        this.dockOffsetLeft = 0;
        this.dockOffsetTop = 36;

        return this;
    }

    TextMenu.prototype =
    {
        generate: function (canvas, mainMenu) {
            var $canvas = canvas;
            var $this = this;

            //setup font sizes
            var options = '';
            for (var i = $canvas.settings.fontSizeMin; i <= $canvas.settings.fontSizeMax; i++) options += '<option value="' + i + '" ' + ($canvas.settings.fontSize == i ? 'selected="selected"' : '') + '>' + i + '</option>';

            var fontSize = $('<div class="_wPaint_fontSize _wPaint_dropDown" title="font size"></div>').append(
                $('<select>' + options + '</select>')
                .change(function (e) {
                    var fontSize = parseInt($(this).val());
                    $canvas.settings.fontSize = fontSize;
                    $canvas.textInput.css({ fontSize: fontSize, lineHeight: fontSize + 'px' });
                    $canvas.textCalc.css({ fontSize: fontSize, lineHeight: fontSize + 'px' });
                })
            )

            //setup font family
            var options = '';
            for (var i = 0, ii = $canvas.settings.fontFamilyOptions.length; i < ii; i++) options += '<option value="' + $canvas.settings.fontFamilyOptions[i] + '" ' + ($canvas.settings.fontFamily == $canvas.settings.fontFamilyOptions[i] ? 'selected="selected"' : '') + '>' + $canvas.settings.fontFamilyOptions[i] + '</option>';

            var fontFamily = $('<div class="_wPaint_fontFamily _wPaint_dropDown" title="font family"></div>').append(
                $('<select>' + options + '</select>')
                .change(function (e) {
                    var fontFamily = $(this).val();
                    $canvas.settings.fontFamily = fontFamily;
                    $canvas.textInput.css({ fontFamily: fontFamily });
                    $canvas.textCalc.css({ fontFamily: fontFamily });
                })
            )

            //content
            var menuContent =
            $('<div class="_wPaint_options"></div>')
            .append($('<div class="_wPaint_icon _wPaint_bold ' + ($canvas.settings.fontTypeBold ? 'active' : '') + '" title="bold"></div>').click(function () { $this.setType($this, $canvas, 'Bold'); }))
            .append($('<div class="_wPaint_icon _wPaint_italic ' + ($canvas.settings.fontTypeItalic ? 'active' : '') + '" title="italic"></div>').click(function () { $this.setType($this, $canvas, 'Italic'); }))
            .append($('<div class="_wPaint_icon _wPaint_underline ' + ($canvas.settings.fontTypeUnderline ? 'active' : '') + '" title="underline"></div>').click(function () { $this.setType($this, $canvas, 'Underline'); }))
            .append(fontSize)
            .append(fontFamily)

            //handle
            var menuHandle = $('<div class="_wPaint_handle"></div>')

            //get position of canvas
            var offset = $($canvas.canvas).offset();

            //menu
            return this.menu =
            $('<div class="_wPaint_menu"></div>')
            .css({ display: 'none', position: 'absolute' })
            .draggable({
                snap: '._wPaint_menu',
                handle: menuHandle,
                stop: function () {
                    $.each($(this).data('draggable').snapElements, function (index, element) {
                        $this.dockOffsetLeft = $this.menu.offset().left - mainMenu.menu.offset().left;
                        $this.dockOffsetTop = $this.menu.offset().top - mainMenu.menu.offset().top;
                        $this.docked = element.snapping;
                    });
                }
            })
            .append(menuHandle)
            .append(menuContent);
        },

        setType: function ($this, $canvas, mode) {
            var element = $this.menu.find("._wPaint_" + mode.toLowerCase());
            var isActive = element.hasClass('active')

            $canvas.settings['fontType' + mode] = !isActive;

            isActive ? element.removeClass('active') : element.addClass('active');

            fontTypeBold = $canvas.settings.fontTypeBold ? 'bold' : 'normal';
            fontTypeItalic = $canvas.settings.fontTypeItalic ? 'italic' : 'normal';
            fontTypeUnderline = $canvas.settings.fontTypeUnderline ? 'underline' : 'none';

            $canvas.textInput.css({ fontWeight: fontTypeBold }); $canvas.textCalc.css({ fontWeight: fontTypeBold });
            $canvas.textInput.css({ fontStyle: fontTypeItalic }); $canvas.textCalc.css({ fontStyle: fontTypeItalic });
            $canvas.textInput.css({ textDecoration: fontTypeUnderline }); $canvas.textCalc.css({ textDecoration: fontTypeUnderline });
        }
    }
})(jQuery);
