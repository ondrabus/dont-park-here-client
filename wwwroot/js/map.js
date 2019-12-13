var map = null;
var markerLayer = null;

function mapInit() {
    if (map !== null) {
        return;
    }

    let latitude = 49.199002;
    let longitude = 16.606710;

    var center = SMap.Coords.fromWGS84(longitude, latitude);
    map = new SMap(JAK.gel("m"), center, 15);

    map.addControl(new SMap.Control.Sync());
    map.addDefaultLayer(SMap.DEF_BASE).enable();

    var mouse = new SMap.Control.Mouse(SMap.MOUSE_PAN | SMap.MOUSE_WHEEL | SMap.MOUSE_ZOOM);
    map.addControl(mouse);

    markerLayer = new SMap.Layer.Marker();
    map.addLayer(markerLayer);
    markerLayer.enable();

    map.getSignals().addListener(window, "map-click", function (e, elm) {
        var coords = SMap.Coords.fromEvent(e.data.event, map);
        DotNet.invokeMethodAsync('DontParkHere', 'SetLocation', coords.y.toString(10), coords.x.toString(10));
        mapCenter(coords.y, coords.x);
    });
};

function mapCenter(latitude, longitude) {
    map.setCenter(SMap.Coords.fromWGS84(longitude, latitude), true);
}
