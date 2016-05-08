require('jquery');

var address;
var url = "/api/restaurants/" + document.getElementById('restaurantId').value;

$.ajax({
    url: url,
    type: 'get',
    dataType: 'json',
    async: false,
    success: function (data) {
        address = data.Address;
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
    var mapProp = {
        center: LatLng,
        zoom: 17,
        mapTypeControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    var marker = new google.maps.Marker({
        position: LatLng,
        map: map
    });
}
google.maps.event.addDomListener(window, 'load', initialize);