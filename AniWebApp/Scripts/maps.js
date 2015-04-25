
var infobox = null;

function addPin(map, lat, lng, severityId, severityName, title, description) {

    // Build the location of the pin
    var location = new Microsoft.Maps.Location(lat, lng);

    // Create a pin object
    var pin = new Microsoft.Maps.Pushpin(location, { text: '!', draggable: false });

    // TODO: Support for severity-based icons would be nice.

    pin.Title = title;
    pin.Description = description;
    pin.SeverityName = severityName;

    // Add handler for the pushpin click event.
    Microsoft.Maps.Events.addHandler(pin, 'click', displayInfobox);

    // Add the items to the map
    map.entities.push(pin);
}

function initializeMap(mapKey, lat, long, zoom) {

    // Set up mapping options
    var mapOptions = {
        credentials: mapKey,
        center: new Microsoft.Maps.Location(lat, long),
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
    if (e.targetType == 'pushpin') {
        infobox.setLocation(e.target.getLocation());
        infobox.setOptions({ visible: true, title: e.target.SeverityName + ": " + e.target.Title, description: e.target.Description });
    } else {
        hideInfobox(e);
    }
}

function hideInfobox(e) {
    infobox.setOptions({ visible: false });
}

function initializeInfoBox(map) {
    // Create the infobox for the pushpins
    infobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0),
    {
        visible: false,
        offset: new Microsoft.Maps.Point(0, 0)
    });
    map.entities.push(infobox);
}