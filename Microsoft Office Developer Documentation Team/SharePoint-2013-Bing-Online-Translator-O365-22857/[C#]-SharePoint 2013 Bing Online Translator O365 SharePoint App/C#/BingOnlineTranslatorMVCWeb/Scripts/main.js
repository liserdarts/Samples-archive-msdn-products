///<reference paht="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.0.js"/>
///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.0-vsdoc.js"/>
$(function () {
    $('.langSel button').click(function () {
        if ($.trim($('#txtSource').val()).length == 0) {
            return;
        }
        $.ajax({
            type: 'POST',
            url: 'Translate',
            data: 'from=' + $('#sourceList').val() + '&to=' + $('#toList').val() + '&text=' + $.trim($('#txtSource').val()),
            dataType: 'json',
            success: function (result, status, xhr) {
                $('#txtTo').val(result);
                if ($('#toList option:selected').attr('speak')) {
                    $('#speak').css('visibility', 'visible');
                }
                else {
                    $('#speak').css('visibility', 'hidden');
                }
            },
            error: function (xhr, status, error) {
                alert(status);
            }
        });
    });

    $('#speak').click(function () {
        if ($.trim($('#txtTo').val()).length == 0) {
            return;
        }
        $('audio').remove();
        $('body').append('<audio autoplay><source src="' + 'Speak?text=' + encodeURI($.trim($('#txtTo').val())) + '&language=' + $('#toList').val() + '" type="audio/mpeg"></audio>');
    });

    $('#toList').change(function () {
        $('#txtTo').val('');
        $('#speak').css('visibility', 'hidden');
    });
});