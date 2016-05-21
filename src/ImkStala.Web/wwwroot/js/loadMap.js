var address;
var data;
var url = "/api/restaurants/" + document.getElementById('restaurantId').value;

$.ajax({
    url: url,
    type: 'get',
    dataType: 'json',
    async: false,
    success: function (data) {
        address = data.Address;
        title = data.RestaurantName;
    }
});

var apiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address;
var LatLng;

$.ajax({
    url: apiUrl,
    type: 'get',
    dataType: 'json',
    async: false,
    success: function (data) {
        LatLng = data.results[0].geometry.location;
    }
});

function initialize() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();
    var mapProp = {
        center: LatLng,
        zoom: 17,
        scrollwheel: false,
        mapTypeControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    directionsDisplay.setMap(map);
    restaurantMarker = new google.maps.Marker({
        position: LatLng,
        map: map,
        title: title
    });
}

google.maps.event.addDomListener(window, 'load', initialize);
