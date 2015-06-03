/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/AngularJS/PlayerModule.js" />


app.service('playerService', function ($http) {


    //Create new record
    this.post = function (player) {
        var request = $http({
            method: "post",
            url: "/api/PlayersAPI",
            data: player
        });
        return request;
    }

    //Get Single Records
    this.get = function (userId) {
        return $http.get("/api/PlayersAPI/" + userId);
    }

    //Get All Employees
    this.getPlayers = function () {
        return $http.get("/api/PlayersAPI");
    }


    //Update the Record
    this.put = function (userId, player) {
        var request = $http({
            method: "put",
            url: "/api/PlayersAPI/" + userId,
            data: player
        });
        return request;
    }
    //Delete the Record
    this.delete = function (userId) {
        var request = $http({
            method: "delete",
            url: "/api/PlayersAPI/" + userId
        });
        return request;
    }
});