require('angular');
require('ng-infinite-scroll');

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
    $scope.searchChanged = function () {
        var page = 0;
        value = document.getElementById('searchBar').value;
        if (value == "") {
            var url = "/api/restaurants/pages/" + page + "/all";
            $http.get(url).success(function (data) {
                $scope.restaurants = data;
            }).error(function () {
            });
        }
        else {
            var url = "/api/restaurants/pages/" + page + "/" + value;
            $http.get(url).success(function (data) {
                $scope.restaurants = data;
            }).error(function () {
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
        });
    });
}


restaurantApp.controller('oneRestaurantController', function ($scope, $http) {
    var url = "/api/restaurants/" + document.getElementById('restaurantId').value;
    $http.get(url).success(function (data) {
        $scope.uniqueTables = [];
        $scope.allTables = [];
        $scope.exactTableCount;
        for (var i = 0; i < data.RestaurantTables.length; i++) {
            var seats = data.RestaurantTables[i].RestaurantTableSeats;
            $scope.allTables.push(seats);
            if (!checkIfAlreadyIn($scope.uniqueTables, seats)) {
                $scope.uniqueTables.push(seats);
            }
        }

        $scope.restaurant = data;
        $scope.changed = function () {
            seats = parseInt($scope.data.selectedTable);
            var tableArray = $scope.allTables;
            count = 0;
            for (var i = 0; i < tableArray.length; i++) {
                if (tableArray[i] == seats) {
                    count++;
                }
            }
            $scope.exactTableCount = count;
            var full = getHowManyFull(seats, data.RestaurantTables);
            $scope.emptyTableCount = count - full;
            if($scope.emptyTableCount==0)
            {
                document.getElementById('submitButton').disabled = true;
            }
            else
            {
                document.getElementById('submitButton').disabled = false;
            }
        };
    }).error(function () {
    });
});

restaurantApp.controller('reservationController', function ($scope, $http) {
    var url = "/api/restaurants/" + document.getElementById('restaurantId').value;
    $http.get(url).success(function (data) {
        $scope.reservations = [];
        $scope.tableCounts = [];
        for (var i = 0; i < data.RestaurantTables.length; i++) {
            if (data.RestaurantTables[i].Reservations.length != 0) {
                for (var j = 0; j < data.RestaurantTables[i].Reservations.length; j++)
                {
                    var tables = data.RestaurantTables[i];
                    var currentdate = Date.parse(new Date());
                    if (currentdate < Date.parse(data.RestaurantTables[i].Reservations[j].ReservationStartDateTime))
                    {
                        $scope.reservations.push(data.RestaurantTables[i].Reservations[j]);
                        $scope.tableCounts.push(data.RestaurantTables[i].RestaurantTableSeats);
                    }
                }
            }
        }

    }).error(function () {
    });
});


function checkIfAlreadyIn(array, value) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] == value) {
            return true;
        }
    }
    return false;
}

function getHowManyInArray(array, value) {
    var value = 0;
    for (var i = 0; i < array.length; i++) {
        console.log(array[i] + ' ' + value)
        if (array[i] == value) {
            value++;
        }
    }
    return value;
}

function getHowManyFull(seat, seatsArray) {
    var full = 0;
    for (var i = 0; i < seatsArray.length; i++) {
        if (seat == seatsArray[i].RestaurantTableSeats) {
            var date = document.getElementById('datepicker').value;
            var time = document.getElementById('timepicker').value;
            for (var j = 0; j < seatsArray[i].Reservations.length; j++) {
                var startTime = Date.parse(seatsArray[i].Reservations[j].ReservationStartDateTime);
                var endTime = Date.parse(seatsArray[i].Reservations[j].ReservationEndDateTime);
                var selectedTime = Date.parse(date + "T" + time);
                if (startTime < selectedTime && endTime > selectedTime) {
                    full++;
                }
            }
        }
    }
    return full;
}