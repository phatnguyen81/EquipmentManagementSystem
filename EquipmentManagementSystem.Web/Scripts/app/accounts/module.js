var app = angular.module("AccountModule", ["ngRoute"]);

app.factory("ShareData", function () {
    return { value: 0 }
});

//Showing Routing  
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    debugger;
    $routeProvider.when('/showaccounts',
                        {
                            templateUrl: 'Account/Index',
                            controller: 'ShowAccountsController'
                        });
    $routeProvider.when('/createaccount',
                        {
                            templateUrl: 'Account/Create',
                            controller: 'CreateAccountController'
                        });
    $routeProvider.when("/editStudent",
                        {
                            templateUrl: 'ManageStudentInfo/EditStudent',
                            controller: 'EditStudentController'
                        });
    $routeProvider.when('/deleteStudent',
                        {
                            templateUrl: 'ManageStudentInfo/DeleteStudent',
                            controller: 'DeleteStudentController'
                        });
    $routeProvider.otherwise(
                        {
                            redirectTo: '/'
                        });

    $locationProvider.html5Mode(true).hashPrefix('!')
}]);