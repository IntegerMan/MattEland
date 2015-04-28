
var infobox = null;

function getIcon(typeId, severityId) {

    /// <summary>
    /// Gets the icon path for a map shape given the type and severity.
    /// </summary>
    /// <param name="typeId">The type identifier.</param>
    /// <param name="severityId">The severity identifier.</param>

    // Determine Shape Name based on type of item
    var shapeName;
    if (typeId === 1 || typeId === 3 || typeId === 8) { // Accidents
        shapeName = "BowTie";
    } else if (typeId === 5) {  // For misc. shapes
        shapeName = "HalfDiamond";
    } else { // All other shapes - primarily construction
        shapeName = "Diamond";
    }

    // Determine Color based on severity
    var color;
    if (severityId <= 1) {
        color = "Blue";
    } else if (severityId >= 3) {
        color = "Red";
    } else {
        color = "Yellow";
    }

    // Put it all together in an icon
    return "/Images/" + color + shapeName + ".png";

}

function addPin(map, lat, lng, severityId, severityName, title, description, start, end, typeId) {

    /// <summary>
    /// Builds and adds a pin to the map.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="lat">The latitude.</param>
    /// <param name="lng">The longitude.</param>
    /// <param name="severityId">The severity identifier.</param>
    /// <param name="severityName">Name of the severity.</param>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <param name="start">The start date/time.</param>
    /// <param name="end">The end date/time.</param>
    /// <param name="typeId">The type identifier.</param>

    // Build the location of the pin
    var location = new Microsoft.Maps.Location(lat, lng);


    // Create a pin object
    var pin = new Microsoft.Maps.Pushpin(location,
    {
        text: "",
        icon: getIcon(typeId, severityId),
        width: 16,
        height: 16,
        draggable: false
    });

    pin.Title = title;
    pin.Description = description;
    pin.SeverityName = severityName;
    pin.Start = start;
    pin.End = end;

    // Add handler for the pushpin click event.
    Microsoft.Maps.Events.addHandler(pin, 'click', displayInfobox);

    // Add the items to the map
    map.entities.push(pin);
}

function initializeMap(mapKey, lat, lng, zoom) {

    /// <summary>
    /// Initializes the map control.
    /// </summary>
    /// <param name="mapKey">The Bing map key.</param>
    /// <param name="lat">The latitude.</param>
    /// <param name="lng">The longitude.</param>
    /// <param name="zoom">The zoom level.</param>

    // Set up mapping options
    var mapOptions = {
        credentials: mapKey,
        center: new Microsoft.Maps.Location(lat, lng),
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: zoom,
        showScalebar: false
    }

    // Create the map control
    var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), mapOptions);

    initializeInfoBox(map);

    // Hide the infobox when the map is moved.
    Microsoft.Maps.Events.addHandler(map, 'viewchange', hideInfobox);

    return map;
}


function displayInfobox(e) {

    /// <summary>
    /// Displays the info box for the selected pushpin.
    /// </summary>
    /// <param name="e">The event arguments.</param>

    if (e.targetType == 'pushpin') {
        infobox.setLocation(e.target.getLocation());
        infobox.setOptions({
            visible: true, title: e.target.SeverityName + ": " + e.target.Title,
            description: e.target.Description + " " + e.target.Start + " - " + e.target.End
        });
    } else {
        hideInfobox(e);
    }
}

function hideInfobox(e) {

    /// <summary>
    /// Hides the info box.
    /// </summary>
    /// <param name="e">The event arguments.</param>

    infobox.setOptions({ visible: false });
}

function initializeInfoBox(map) {

    /// <summary>
    /// Initializes the info box.
    /// </summary>
    /// <param name="map">The map.</param>

    // Create the infobox for the pushpins
    infobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0),
    {
        visible: false,
        offset: new Microsoft.Maps.Point(0, 0)
    });

    // Add the box to the map
    map.entities.push(infobox);
}