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

$('input[type=radio]').on('change', function () {
    $(this).closest("form").submit();
});