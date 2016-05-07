require('angular');

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
        if (value == "")
        {
            var url = "/api/restaurants/pages/" + page + "/all";
            $http.get(url).success(function (data) {
                $scope.restaurants = data;
            }).error(function () {
            });
        }
        else
        {
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

function getOneRestaurantData(id)
{
    var url = "/api/restaurants/" + id;
    var seats;
    var count;
    console.log(url);
    restaurantApp.controller('oneRestaurantController', function ($scope, $http) {
        $http.get(url).success(function (data) {
            $scope.uniqueTables = [];
            $scope.allTables = [];
            $scope.exactTableCount;
            for (var i = 0; i < data.RestaurantTables.length; i++)
            {
                var seats = data.RestaurantTables[i].RestaurantTableSeats;
                $scope.allTables.push(seats);
                if (!checkIfAlreadyIn($scope.uniqueTables, seats))
                {
                    $scope.uniqueTables.push(seats);
                }
            }
            $scope.restaurant = data;
            $scope.changed = function () {
                seats = parseInt($scope.data.selectedTable);
                var tableArray = $scope.allTables;
                count = 0;
                for (var i = 0; i < tableArray.length; i++)
                {
                    if(tableArray[i]==seats)
                    {
                        count++;
                    }
                }
                $scope.exactTableCount = count;
                var full = getHowManyFull(seats, data.RestaurantTables);
                $scope.emptyTableCount = count - full;
            };

            $('#timepicker, #datepicker').change(function () {
                var full = getHowManyFull(seats, data.RestaurantTables);
                $scope.emptyTableCount = count - full;
            });
            
        }).error(function () {
        });
    });
}

function getReservedTables(id)
{
    var url = "/api/restaurants/" + id;
    console.log(url);
    restaurantApp.controller('oneRestaurantController', function ($scope, $http) {
        $http.get(url).success(function (data) {
            $scope.reservations = [];
            for (var i = 0; i < data.RestaurantTables.length; i++) {
                if (data.RestaurantTables[i].Reservations.length != 0)
                {
                    var tables = data.RestaurantTables[i];
                    console.log(data.RestaurantTables[i]);
                    $scope.reservations.push(data.RestaurantTables[i]);
                }
            }

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

function checkIfAlreadyIn(array,value)
{
    for(var i=0; i<array.length; i++)
    {
        if(array[i]==value)
        {
            return true;
        }
    }
    return false;
}

function getHowManyInArray(array,value)
{
    var value=0;
    for(var i=0; i<array.length; i++)
    {
        console.log(array[i] + ' ' + value)
        if(array[i]==value)
        {
            value++;
        }
    }
    return value;
}

function getHowManyFull(seat,seatsArray)
{
    var full = 0;
    for(var i=0; i<seatsArray.length; i++)
    {
        if (seat == seatsArray[i].RestaurantTableSeats)
        {
            var date = document.getElementById('datepicker').value;
            var time = document.getElementById('timepicker').value;
            for(var j=0; j<seatsArray[i].Reservations.length; j++)
            {
                var startTime = Date.parse(seatsArray[i].Reservations[j].ReservationStartDateTime);
                var endTime = Date.parse(seatsArray[i].Reservations[j].ReservationEndDateTime);
                var selectedTime = Date.parse(date + "T" + time);
                if (startTime < selectedTime && endTime > selectedTime)
                {
                    full++;
                }
            }
        }
    }
    return full;
}