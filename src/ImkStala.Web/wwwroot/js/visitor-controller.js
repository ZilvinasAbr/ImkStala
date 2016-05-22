require('angular')

var visitorApp = angular.module('visitorApp', []);

visitorApp.controller('history', function ($scope, $http) {
    var url = "/api/visitors/reservations/" + document.getElementById('visitorId').value;
    $http.get(url).success(function (data) {
        $scope.reservationHistory = data;
    }).error(function () {
    });
});

visitorApp.controller('favorites', function ($scope, $http) {
    var url = "/api/visitors/favorites/" + document.getElementById('visitorId').value;
    $http.get(url).success(function (data) {
        $scope.favoriteRestaurants = data;
    }).error(function () {
    });
});