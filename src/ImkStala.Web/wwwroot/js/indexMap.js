var url = "/api/restaurants"
var adresses = [];
var restInfo = [];
var markers = [];
var contentString;
var infowindow = [];
var bounds = new google.maps.LatLngBounds();

$.ajax({
    url: url,
    type: 'get',
    dataType: 'json',
    async: false,
    success: function (data) {
        for (var i = 0; i < data.length; i++) {
            var apiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + data[i].Address;
            $.ajax({
                url: apiUrl,
                type: 'get',
                dataType: 'json',
                async: false,
                success: function (data) {
                    adresses.push(data.results[0].geometry.location);
                }
            });
            restInfo.push(data[i]);
        }
    }
});

function initialize() {
    var mapProp = {
        center: new google.maps.LatLng(51.508742, -0.120850),
        zoom: 5,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    for (var i = 0; i < adresses.length; i++) {
        markers[i] = new google.maps.Marker({
            position: new google.maps.LatLng(adresses[i]),
            map: map,
            title: 'samplemarker'
        });
        contentString = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h1 id="firstHeading" class="firstHeading">' + restInfo[i].RestaurantName + '</h1>' +
        '<div id="bodyContent">' +
           restInfo[i].Description +
        '<br> <a href="/Home/Restaurant/' + restInfo[i].Id + '"> Daugiau informacijos ' +
        '</a>' +
        '</div>' +
        '</div>';

        markers[i].info = new google.maps.InfoWindow({
            content: contentString
        });

        markers[i].addListener('click', function () {
            var marker_map = this.getMap();
            this.info.open(marker_map, this);
        });

    }
    for (var i = 0; i < markers.length; i++) {
        bounds.extend(markers[i].getPosition());
    }

    map.fitBounds(bounds);
}
google.maps.event.addDomListener(window, 'load', initialize);