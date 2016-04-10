var restaurantApp = angular.module('restaurantApp', ['infinite-scroll']);

restaurantApp.controller('infiniteScrollRestaurants', function ($scope, $http) {
    var page = 0;
    var url = "/api/restaurants/pages/" + page;
    $http.get(url).success(function (data) {
        $scope.restaurants = data;
    }).error(function () {
        alert('Failed to get api');
    });

    $scope.loadMore = function () {
        var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
        if (scrollTop != 0) {
            page++;
            var url = "/api/restaurants/pages/" + page;
            $http.get(url).success(function (data) {
                var data = data;
                for (var i = 0; i < data.length; i++) {
                    $scope.restaurants.push(data[i]);
                }
            }).error(function () {
                alert('Failed to get api');
            });
        }
    }
});

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