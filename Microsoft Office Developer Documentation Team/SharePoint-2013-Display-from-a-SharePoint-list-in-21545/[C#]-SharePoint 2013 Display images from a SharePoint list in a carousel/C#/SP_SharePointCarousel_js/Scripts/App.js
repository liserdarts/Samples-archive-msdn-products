// The solution deploys 15 images to the images module (i.e. file storage in SharePoint)
// and the following code adds an unordered list (<ul>) to the page, with a list item (<li>)
// for each image.
// Then it simply calls the liquidcarousel method for the div with an id of productCarousel
// which creates the carousel based on the open source jQuery libraries provided in 
// ../Scripts/jquery.liquidcarousel.pack.js and ../Scripts/jquery.liquidcarousel.js
// This code runs when the DOM is ready becuase it's wrapped in the 'ready' event handler
$(document).ready(function () {
    var dvds = document.createElement("ul");
    for (var counter = 0; counter < 15; counter++) {
        var dvd = document.createElement("li");
        var dvdImage = document.createElement("img");
        dvdImage.setAttribute("src", "../images/" + (counter + 1).toString() + ".jpg");
        dvdImage.setAttribute("width", "80");
        dvdImage.setAttribute("height", "125");
        dvd.appendChild(dvdImage);
        dvds.appendChild(dvd);
    }

    // Default.aspx has a <div> element with an ID of 'wrapper'. This is where we want to add the
    // unordered list.
    document.getElementById("gallery").appendChild(dvds);

    // Then we can render the list as a carousel
    $('#productCarousel').liquidcarousel({ height: 150, duration: 100, hidearrows: true });
});

