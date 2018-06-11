"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.StickyNote = function (id, body, canvas) {

    var _id = id,
        _body = body,
        _canvas = canvas,
        get_id = function () { return _id; },
        set_id = function (v) { _id = v; },
        get_body = function () { return _body; },
        set_body = function (v) { _body = v; },
        get_canvas = function () { return _canvas; },
        set_canvas = function (v) { _canvas = v; };


    return {
        set_id: set_id,
        get_id: get_id,
        set_body: set_body,
        get_body: get_body,
        set_canvas: set_canvas,
        get_canvas: get_canvas
    }

};

Wingtip.StickyNoteCanvas = function (x, y, width, height, radius, text, canvas) {
    
    this.noteX = x;
    this.noteY = y;
    this.noteWidth = width;
    this.noteHeight = height;
    this.noteRadius = radius;
    this.noteText = text;
    this.canvas = canvas;
    this.context = this.canvas.getContext("2d");
    this.typingOn = false;
    var that = this;

};

Wingtip.StickyNoteCanvas.prototype =
{
    clear: function () {
        this.context.clearRect(0, 0, 200, 200);
    },

    active: function (flag) {
        this.typingOn = flag;
        this.draw();
    },

    keypress: function (charStr) {
        this.context.clearRect(0, 0, 200, 200);
        this.noteText = this.noteText + charStr;
        Wingtip.ViewModel.get_activeNote().set_body(this.noteText);
        this.draw();
    },

    keydown: function (charCode) {
        if (charCode === 8) {
            this.context.clearRect(0, 0, 200, 200);
            this.noteText = this.noteText.slice(0, -1);
            Wingtip.ViewModel.get_activeNote().set_body(this.noteText);
            this.draw();
        }
    },

    draw: function () {

        this.context.clearRect(0, 0, 200, 200);

        //Note
        this.context.save();
        var x = this.noteX;
        var y = this.noteY;
        var width = this.noteWidth;
        var height = this.noteHeight;
        var radius = this.noteRadius;

        this.context.shadowColor = '#999';
        this.context.shadowBlur = 20;
        this.context.shadowOffsetX = 5;
        this.context.shadowOffsetY = 5;

        this.context.fillStyle = "#FFD700";

        this.context.beginPath();

        this.context.moveTo(x + radius, y);
        this.context.arcTo(x + width, y, x + width, y + radius, radius);

        this.context.arcTo(x + width, y + height, x + width - radius, y + height, radius);

        this.context.arcTo(x, y + height, x, y + height - radius, radius)
        this.context.arcTo(x, y, x + radius, y, radius);

        this.context.closePath();

        this.context.fill();
        this.context.stroke();

        this.context.restore();

        //Lines
        this.context.save;

        this.context.strokeStyle = 'rgb(100, 100, 100)';

        this.context.moveTo(x, y + 33);
        this.context.lineTo(x + width, y + 33);
        this.context.stroke();

        this.context.moveTo(x, y + 53);
        this.context.lineTo(x + width, y + 53);
        this.context.stroke();

        this.context.moveTo(x, y + 73);
        this.context.lineTo(x + width, y + 73);
        this.context.stroke();

        this.context.moveTo(x, y + 93);
        this.context.lineTo(x + width, y + 93);
        this.context.stroke();

        this.context.moveTo(x, y + 113);
        this.context.lineTo(x + width, y + 113);
        this.context.stroke();

        //Text
        this.context.save();

        if (this.typingOn) {
            this.context.fillStyle = 'black';
        }
        else {
            this.context.fillStyle = '#777';
        }
        this.context.font = '20pt calibri';

        var words = this.noteText.split(' ');
        var line = '';
        var x = this.noteX + 7.5;
        var y = this.noteY + 32.5;
        var returns = 0;

        for (var n = 0; n < words.length; n++) {
            var testLine = line + words[n] + ' ';
            var metrics = this.context.measureText(testLine);
            var testWidth = metrics.width;

            if (returns < 4) {
                if (testWidth > this.noteWidth - 2.5) {
                    this.context.fillText(line, x, y);
                    line = words[n] + ' ';
                    y += 20;
                    returns = returns + 1;
                }
                else { line = testLine; }
            }
        }

        this.context.fillText(line, x, y);

        this.context.restore();
    }

};







