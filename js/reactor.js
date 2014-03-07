/// <reference path="esri-leaflet.min.js" />
/// <reference path="leaflet.js" />
/// <reference path="jquery-1.9.1.js" />
/// <reference path="jquery-ui-1.10.3.custom.min.js" />
/// <reference path="jquery.greybox2.js" />
/// <reference path="jquery.ui.touch-punch.js" />

// If JavaScript appears not to run, a line of code may not work properly


var map = L.map('mapSection').setView([39.861, -88.951], 13);

//ADD LEAFLET.USERMARKER PLUGIN
var marker = null;

map.on("locationfound", function (location) {
    if (!marker)
        marker = L.userMarker(location.latlng, { pulsing: true }).addTo(map);


    marker.setLatLng(location.latlng);
    marker.setAccuracy(location.accuracy);
});

map.locate({
    watch: false, //This makes the locator move as the user changes location. Set to false to disable.
    locate: true, // Set to true to locate the current location of the user
    setView: true,
    maxZoom: 20,
    minZoom: 18,
    enableHighAccuracy: true,
    inertia: true
});
//END LEAFLET.USERMARKER PLUGIN

// Esri Image Layer 
var imageLayer = L.esri.basemapLayer("Imagery");
//// Address Points, Streets, and Corporate Boundary
var lBaseLayer = L.esri.dynamicMapLayer("http://64.107.106.57/arcgis/rest/services/Base/MapServer");
// Parcel Layer identified in the application
var parcels = L.esri.dynamicMapLayer("http://64.107.106.57/arcgis/rest/services/Parcels/MapServer");
// Parcels with no PINs
var missingPins = L.esri.dynamicMapLayer("http://64.107.106.57/arcgis/rest/services/MissingAddressCodes/MapServer");


// Add featureLayer for drawing
var parcelsF = L.esri.featureLayer("http://64.107.106.57/arcgis/rest/services/Parcels/MapServer/0", {
    where: '1=0',
    style: function (feature) {
        return { color: "#70ca49", weight: 2 };
    },
    onEachFeature: function (feature, layer) {
        //layer.bindPopup(L.Util.template(template, feature.properties));
    }
}).addTo(map);


map.addLayer(imageLayer);
// I want to highlight the select parcel when I click on this layer
map.addLayer(parcels);
map.addLayer(lBaseLayer);
map.addLayer(missingPins);





// This is Esri JSAPI code; the first line determines how to get the name of the layer and the second is acquire the quantity of layers
//alert(dynamicMapServiceLayer.layerInfos[5].name);
//alert((dynamicMapServiceLayer.visibleLayers.length) + 1);
// Identify Dynamic Map Features
map.on("click", function (e) {
    parcels.identify(e.latlng, function (data) {
        //alert(data.results[0].layerName); //This will identify a layer name in leaflet
        document.forms["frmMap"].elements["txtPin"].value = data.results[0].attributes.PIN;
        document.forms["frmMap"].elements["txtAddress"].value = data.results[0].attributes.SITEADDRESS;
        document.forms["frmMap"].elements["txtPrimaryName"].value = data.results[0].attributes.PRIMARYNAME;
        document.forms["frmMap"].elements["txtPrimaryAddress"].value = data.results[0].attributes.PRIMARYADDRESS;
        document.forms["frmMap"].elements["txtLegalDescription"].value = data.results[0].attributes.LEGALDESCRIPTION;
        document.getElementById("btnStartCase").disabled = false;
        document.getElementById("lblCaseCreated").innerHTML = '';
        document.getElementById("btnCreateCase").disabled = true;
        // returns an array of results, just grab the first one
        var result = data.results[0];
        console.debug(data, result);
        if (result) {
            var parcelnum = result.attributes.PARCELNUMBER;
            highlightLayer(parcelnum);
        }
    });
});


/* Enables Sidebar to be draggable */
jQuery(function () {
    jQuery("#sideBar").draggable();
});


/* The accordion allows sections to expand/collapse which show or hide each section */
jQuery(function () {
    jQuery("#sideBarTitle").accordion({
        collapsible: true,
        heightStyle: "content"
    });
});




jQuery(function () {
    jQuery("#sideBarContent").accordion({
        heightStyle: "content"
    });
});



// Expands/collapses the Ask for Assistance   Note: Setting active to false collapses section. Setting heightStyle to Content autosizes the height
jQuery(function () {
    jQuery("#footerTitle").accordion({
        collapsible: true,
        heightStyle: "content",
        active: false
    });
});


// Opens the greybox for the help page
jQuery(document).ready(function () {
    var gbOptions = {
        gbWidth: 600,
        gbHeight: 500,
        captionHeight: 22,
        ffMacFlash: true
    };
    jQuery('#lnkHelp').greybox(gbOptions);
});



// Set active section of sidebar
// http://api.jqueryui.com/accordion/ - how to activate a certain panel in the accordion
function showAddCase() {
    jQuery("#sideBarContent").accordion("option", "active", 1);
}

// Set layer to be highlighted
function highlightLayer(value) {
    console.debug('highlight', value);
    parcelsF.setWhere("PARCELNUMBER = '" + value + "'");
}