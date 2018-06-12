///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.0.js"/>
///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.0-vsdoc.js"/>

$(function () {
    $('#baseUnit').val($('#hiddenfieldId').val().substring(0,3));
    $('#targUnit').val($('#hiddenfieldId').val().substring(3,6));
    $('#baseDis').text($('#txtBase').val() + ' ' + $('#baseUnit option:selected').text() + ' equals');
    $('#targDis').text($('#txtTarg').val() + ' ' + $('#targUnit option:selected').text());
    $('#exchangeForm input[type=text]').keyup(function () {
        var txtBase = $('#txtBase'),
            txtTarg = $('#txtTarg'),
            rate = parseFloat($('#hiddenfieldRate').val());

        if ($.isNumeric($(this).val())) {
            if ($(this).attr('id') === 'txtBase') {
                txtTarg.val((parseFloat(txtBase.val()) * rate).toFixed(2));
            }
            else {
                txtBase.val((parseFloat(txtTarg.val()) / rate).toFixed(2));
            }
            $('#baseDis').text($('#txtBase').val() + ' ' + $('#baseUnit option:selected').text() + ' equals');
            $('#targDis').text($('#txtTarg').val() + ' ' + $('#targUnit option:selected').text());
            $('#baseUnit').prop('disabled', false);
            $('#targUnit').prop('disabled', false);
        }
        else {
            if ($(this).attr('id') === 'txtBase')
            {
                txtTarg.val('');
            }
            else
            {
                txtBase.val('');
            }
            $('#baseUnit').prop('disabled', true);
            $('#targUnit').prop('disabled', true);
        }
    });

    $('#exchangeForm select').change(function () {
        if ($.isNumeric($('#txtBase').val())) {
            $('form').attr('action', '/home/exchange/' + $('#baseUnit').val() + $('#targUnit').val());
            $('form').submit();
        }
    });
});