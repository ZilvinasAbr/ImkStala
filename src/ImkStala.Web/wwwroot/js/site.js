function goToUrl(url) {
    window.location.href = url;
}

$(function () {
    $("#datepicker").datepicker();
});

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