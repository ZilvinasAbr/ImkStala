function goToUrl(url) {
    window.location.href = url;
}

function addToType(id) {
    var obj = document.getElementById(id);
    var val = obj.value;
    val++;
    obj.value = val;

}
function downToType(id) {
    var obj = document.getElementById(id);
    var val = obj.value;
    if (val > 0) {
        val--;
        obj.value = val;
    }
}
function keyValidation(evt) {
    var key = window.evt ? evt.keyCode : evt.which;
    if (evt.keyCode == 8 || evt.keyCode == 46
         || evt.keyCode == 37 || evt.keyCode == 39) {
        return true;
    }
    else if (key < 48 || key > 57) {
        return false;
    }
    else return true;
}

function loadMapByAddress(address)
{
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
}
var x = document.getElementById("demo");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    x.innerHTML = "Latitude: " + position.coords.latitude +
    "<br>Longitude: " + position.coords.longitude;
    var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    var userMarker = new google.maps.Marker({
        position: latlng,
        title: 'Jusu pozicija',
        draggable: false,
        map: map
    });
    map.setCenter(userMarker.getPosition())
    var start = userMarker.getPosition();
    var end = restaurantMarker.getPosition();
    var bounds = new google.maps.LatLngBounds();
    bounds.extend(start);
    bounds.extend(end);
    map.fitBounds(bounds);
    var request = {
        origin: start,
        destination: end,
        travelMode: google.maps.TravelMode.DRIVING
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            directionsDisplay.setMap(map);
            var distance = google.maps.geometry.spherical.computeDistanceBetween(start, end);
            if (distance > 1000) {
                x.innerHTML = 'Atstumas iki restorano: ' + Math.round(distance / 1000) + ' km';
            }
            else {
                x.innerHTML = 'Atstumas iki restorano: ' + distance + ' m';
            }
        } else {
            alert("Directions Request from " + start.toUrlValue(6) + " to " + end.toUrlValue(6) + " failed: " + status);
        }
    });
}

$('input[type=radio]').on('change', function () {
    $(this).closest("form").submit();
});