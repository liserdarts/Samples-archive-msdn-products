// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {
    $(document).ready(function () {
        // If setSelectedDataAsync method is supported by the host application
        // the UI buttons are hooked up to call the method else the buttons are removed

        if (Office.context.document.setSelectedDataAsync) {

            clickHandler();

        }

        else {
            $('#setFText').remove();
            $('#setSText').remove();
            $('#setImage').remove();
            $('#setBox').remove();
            $('#setShape').remove();
            $('#setControl').remove();
            $('#setFTable').remove();
            $('#setSTable').remove();
            $('#setSmartArt').remove();
            $('#setChart').remove();
        }
    });
};


function writeContent(fileName) {

    var myOOXMLRequest = new XMLHttpRequest();
    var myXML;
    myOOXMLRequest.open('GET', fileName, false);
    myOOXMLRequest.send();
    if (myOOXMLRequest.status === 200) {
        myXML = myOOXMLRequest.responseText;
    }
    Office.context.document.setSelectedDataAsync(myXML, { coercionType: 'ooxml' });

}

function writeMarkup(fileName) {

    var myOOXMLRequest = new XMLHttpRequest();
    var myXML;

    //Set the format for the markup
    myOOXMLRequest.open('GET', '../../OOXMLSamples/FormatForMarkup.xml', false);
    myOOXMLRequest.send();
    if (myOOXMLRequest.status === 200) {
        myXML = myOOXMLRequest.responseText;
    }
    Office.context.document.setSelectedDataAsync(myXML, { coercionType: 'ooxml' });

    //Insert the markup as text
    myOOXMLRequest.open('GET', fileName, false);
    myOOXMLRequest.send();
    if (myOOXMLRequest.status === 200) {
        myXML = myOOXMLRequest.responseText;
    }
    Office.context.document.setSelectedDataAsync(myXML, { coercionType: 'text' });

}

function clickHandler() {

    //This function resets the event handlers for the eleven buttons in the task pane to the function that matches the user's radio button selection
    //See more info on this in the file LoadingAndWritingOOXML.html
    clearButtons();

    if ($('#setOOXMLContent').is(':checked')) {

        $('#setFText').click(function () { writeContent('../../OOXMLSamples/TextWithDirectFormat.xml'); });
        $('#setSText').click(function () { writeContent('../../OOXMLSamples/TextWithStyle.xml'); });
        $('#setImage').click(function () { writeContent('../../OOXMLSamples/SimpleImage.xml'); });
        $('#setPhoto').click(function () { writeContent('../../OOXMLSamples/FormattedImage.xml'); });
        $('#setBox').click(function () { writeContent('../../OOXMLSamples/TextBoxWordArt.xml'); });
        $('#setShape').click(function () { writeContent('../../OOXMLSamples/ShapeWithText.xml'); });
        $('#setControl').click(function () { writeContent('../../OOXMLSamples/ContentControl.xml'); });
        $('#setFTable').click(function () { writeContent('../../OOXMLSamples/TableWithDirectFormat.xml'); });2
        $('#setSTable').click(function () { writeContent('../../OOXMLSamples/TableStyled.xml'); });
        $('#setSmartArt').click(function () { writeContent('../../OOXMLSamples/SmartArt.xml'); });
        $('#setChart').click(function () { writeContent('../../OOXMLSamples/Chart.xml'); });
    }

    else {
        $('#setFText').click(function () { writeMarkup('../../OOXMLSamples/TextWithDirectFormat.xml'); });
        $('#setSText').click(function () { writeMarkup('../../OOXMLSamples/TextWithStyle.xml'); });
        $('#setImage').click(function () { writeMarkup('../../OOXMLSamples/SimpleImage.xml'); });
        $('#setPhoto').click(function () { writeMarkup('../../OOXMLSamples/FormattedImageMarkup.xml'); });
        $('#setBox').click(function () { writeMarkup('../../OOXMLSamples/TextBoxWordArt.xml'); });
        $('#setShape').click(function () { writeMarkup('../../OOXMLSamples/ShapeWithText.xml'); });
        $('#setControl').click(function () { writeMarkup('../../OOXMLSamples/ContentControl.xml'); });
        $('#setFTable').click(function () { writeMarkup('../../OOXMLSamples/TableWithDirectFormat.xml'); });
        $('#setSTable').click(function () { writeMarkup('../../OOXMLSamples/TableStyled.xml'); });
        $('#setSmartArt').click(function () { writeMarkup('../../OOXMLSamples/SmartArt.xml'); });
        $('#setChart').click(function () { writeMarkup('../../OOXMLSamples/ChartMarkup.xml'); })
    }
}

function clearButtons() {

    $('#setFText').unbind('click');
    $('#setSText').unbind('click');
    $('#setImage').unbind('click');
    $('#setPhoto').unbind('click');
    $('#setBox').unbind('click');
    $('#setShape').unbind('click');
    $('#setControl').unbind('click');
    $('#setFTable').unbind('click');
    $('#setSTable').unbind('click');
    $('#setSmartArt').unbind('click');
    $('#setChart').unbind('click');

}