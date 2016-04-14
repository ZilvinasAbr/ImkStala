var restaurantApp = angular.module('restaurantApp', ['infinite-scroll']);

restaurantApp.controller('infiniteScrollRestaurants', function ($scope, $http) {
    var page = 0;
    var value = "all";
    var url = "/api/restaurants/pages/" + page + "/" + value;
    $http.get(url).success(function (data) {
        $scope.restaurants = data;
    }).error(function () {
    });

    $scope.loadMore = function () {
        var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
        if (scrollTop != 0) {
            page++;
            var url = "/api/restaurants/pages/" + page + "/" + value;
            $http.get(url).success(function (data) {
                var data = data;
                for (var i = 0; i < data.length; i++) {
                    $scope.restaurants.push(data[i]);
                }
            }).error(function () {
            });
        }
    }
    $scope.searchChanged = function()
    {
        var page = 0;
        value = document.getElementById('searchBar').value;
        var url = "/api/restaurants/pages/" + page + "/" + value;
        $http.get(url).success(function (data) {
            $scope.restaurants = data;
        }).error(function () {
        });
    }
});

function getAllRestaurantData() {
    var url = "/api/restaurants";
    restaurantApp.controller('restaurantController', function ($scope, $http) {
        $http.get(url).success(function (data) {
            $scope.restaurants = data;
        }).error(function () {
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
        });
    });
}

function getAdressById(id)
{
    var returnAdress;
    var url = "/api/restaurants/" + id;
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        async: false,
        success: function (data) {
            returnAdress = data.Adress;
        }
    });
    return returnAdress;
}
function loadScope(scope,http,url)
{
    http.get(url).success(function (data) {
        scope.restaurant = data;
    }).error(function () { });
}