var restaurantApp = angular.module('restaurantApp', []);

function getAllRestaurantData() {
    var url = "/api/restaurants";
    restaurantApp.controller('restaurantController', function ($scope, $http) {
        $http.get(url).success(function (data) {
            $scope.restaurants = data;
        }).error(function () {
            alert('Failed to get api');
        });
    });
}

function getOneRestaurantData(id)
{
    var url = "/api/restaurants/" + id;
    console.log(url);
    restaurantApp.controller('oneRestaurantController', function ($scope, $http) {
        $http.get(url).success(function (data) {
            $scope.restaurant = data;
        }).error(function () {
            alert('Failed to get api');
        });
    });
}