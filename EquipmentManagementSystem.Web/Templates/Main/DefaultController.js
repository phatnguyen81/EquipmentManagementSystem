"use strict";

define(['application-configuration', function (app) {

    app.register.controller('defaultController', ['$scope', '$rootScope', 'mainService', 'alertsService', function ($scope, $rootScope, mainService, alertsService) {


        $scope.initializeController = function () {
            mainService.initializeApplication($scope.initializeApplicationComplete, $scope.initializeApplicationError);
        }

        $scope.initializeApplicationComplete = function (response) {

        }

        $scope.initializeApplicationError = function (response) {

        }



    }]);
}]);

