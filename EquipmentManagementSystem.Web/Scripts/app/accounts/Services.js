app.service("SPACRUDService", function ($http) {

    //Read all Students  
    this.getAccounts = function () {

        return $http.get("/AccountApi/GetAccounts");
    };

   
});