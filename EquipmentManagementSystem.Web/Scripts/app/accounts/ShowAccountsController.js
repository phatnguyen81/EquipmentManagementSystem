app.controller('ShowAccountsController', function ($scope, $location, SPACRUDService, ShareData) {
    loadAllStudentsRecords();

    function loadAllStudentsRecords() {
        var promiseGetStudent = SPACRUDService.getAccounts();

        promiseGetStudent.then(function (pl) { $scope.Students = pl.data },
              function (errorPl) {
                  $scope.error = errorPl;
              });
    }

});