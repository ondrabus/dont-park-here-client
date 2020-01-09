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

function mapSetParkingMachines(parkingMachine1, parkingMachine2) {
    alert('test');
    let parkingMachines = [parkingMachine1, parkingMachine2];

    //map.getControls().filter(c => "_alwaysShow" in c).forEach(c => map.removeControl(c));
    if (markerLayer) {
        markerLayer.disable();
    }
    markerLayer = null;

    markerLayer = new SMap.Layer.Marker();
    map.addLayer(markerLayer);
    markerLayer.enable();

    for (let i = 0; i < parkingMachines.length; i++) {
        let image = JAK.mel("img", { className: "meter", src: "./img/meter.png" });
        markerLayer.addMarker(new SMap.Marker(SMap.Coords.fromWGS84(parkingMachines[i].longitude, parkingMachines[i].latitude), "Parking machine", { url: image }));

        //let pointer = new SMap.Control.Pointer({ "showDistance": true });
        //map.addControl(pointer);
        //pointer.setCoords(SMap.Coords.fromWGS84(parkingMachines[i].longitude, parkingMachines[i].latitude));
    }
    console.log("Redrew marker layer with new parking machines");
}
