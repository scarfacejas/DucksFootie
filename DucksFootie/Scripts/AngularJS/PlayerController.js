/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/AngularJS/PlayerModule.js" />

//The controller is having 'playerService' dependency.
//This controller makes call to methods from the service 
app.controller('playerController', function ($scope, playerService) {

    $scope.IsNewRecord = 1; //The flag for the new record

    loadRecords();

    //Function to load all Player records
    function loadRecords() {
        var promiseGet = playerService.getPlayers(); //The Method Call from service

        promiseGet.then(function (pl) { $scope.players = pl.data; },
              function (errorPl) {
                  $log.error('Failure loading Players', errorPl);
              });
    }
    
    //The Save scope method use to define the Player object.
    //In this method if IsNewRecord is not zero then Update Player else 
    //Create the Player information to the server
    $scope.save = function () {
        var player = {
            UserId: $scope.UserId,
            Name: $scope.Name,
            Email: $scope.Email,
            Active: true
        };
        //If the flag is 1 the it si new record
        if ($scope.IsNewRecord == 1) {
            //player.UserId = -1;
            var promisePost = playerService.post(player);
            promisePost.then(function (pl) {
                $scope.UserId = pl.data.UserId;
                $scope.Active = true;
                loadRecords();
            }, function (err) {
                console.log("Err" + err);
            });
        } else { //Else Edit the record
            var promisePut = playerService.put($scope.UserId, player);
            promisePut.then(function (pl) {
                $scope.Message = "Updated Successfully";
                loadRecords();
            }, function (err) {
                console.log("Err" + err);
            });
        }
    };

    //Method to Delete
    $scope.delete = function () {
        var promiseDelete = playerService.delete($scope.UserId);
        promiseDelete.then(function (pl) {
            $scope.Message = "Deleted Successfuly";
            $scope.Name = "";
            $scope.Email = "";
            $scope.Active = false;
            loadRecords();
        }, function (err) {
            console.log("Err" + err);
        });
    }

    //Method to Get Single Player based on EmpNo
    $scope.get = function (player) {
        var promiseGetSingle = playerService.get(player.UserId);

        promiseGetSingle.then(function (pl) {
            var res = pl.data;
            $scope.UserId = res.UserId;
            $scope.Name = res.Name;
            $scope.Email = res.Email;
            $scope.Active = res.Active;

            $scope.IsNewRecord = 0;
        },
        function (errorPl) {
            console.log('failure loading Player', errorPl);
        });
    }
    //Clear the Scopr models
    $scope.clear = function () {
        $scope.IsNewRecord = 1;
        $scope.UserId = 0;
        $scope.Name = "";
        $scope.Email = "";
        $scope.Active = true;
    }
});