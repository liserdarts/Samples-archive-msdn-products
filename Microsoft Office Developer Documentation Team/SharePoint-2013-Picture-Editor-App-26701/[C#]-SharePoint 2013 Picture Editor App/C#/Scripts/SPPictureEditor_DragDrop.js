/*
    Attach the drag and drop event with the identified zone.
*/
function attachDragDropEvents() {
    var dropZone = document.getElementById('drop_zone');

    if (isValid.dnd) {
        dropZone.ondragover = function () { this.className = 'hover'; return false; };
        dropZone.ondragend = function () { this.className = ''; return false; };
        dropZone.ondrop = function (e) {
            this.className = '';
            e.preventDefault();
            jQuery('#drop_zonecontainer').css('background-image', 'url(../_layouts/images/loading16.gif)');
            readDroppedFile(e.dataTransfer.files);
        }
    }

    /*
        Read the dropped file.
    */
    function readDroppedFile(files) {
        var file = files[0];
        if (isValid.filereader === true && acceptedTypes[file.type] === true) {
            var reader = new FileReader();
            reader.onload = function (event) {
                selectfromDragDrop(file.name, reader.result, file.type);
                jQuery('#drop_zonecontainer').css('background-image', 'url(../_layouts/images/dnd.png)');
            };

            reader.readAsDataURL(file);
        } else {
            alert('Please select a valid image file.');
        }
    }
}