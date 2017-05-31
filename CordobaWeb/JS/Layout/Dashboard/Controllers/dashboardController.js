app.controller('DashboardController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();   
    //#endregion      
    $scope.StoreDetailInSession = StoreSessionDetail;  
    $scope.WelcomeMsg = $scope.StoreDetailInSession.description.split('##ReadMore##');
    
    $scope.OpenLoginPopUp=function()
    {
        angular.element("#DivLoginModel").modal('show');     
    }

    $scope.Login=function(form)
    {
      
        if (form.$valid) {
       
            $scope.CustomerObj.cartgroup_id = UserDetail.cartgroup_id;
            $scope.CustomerObj.store_id = $scope.StoreDetailInSession.store_id;
            
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/CustomerLogin", $scope.CustomerObj)
                  .then(function (response) {                      
                      switch (response.data.ErrorTypeId)
                      {
                          case 0:

                              UserDetail.customer_id = response.data.customer_id;
                              UserDetail.firstname = response.data.firstname;
                              UserDetail.lastname = response.data.lastname;
                              UserDetail.points = response.data.points;
                              UserDetail.address_id = response.data.address_id;
                              UserDetail.cartgroup_id = response.data.cartgroup_id;
                              UserDetail.TotalItemAdded = response.data.TotalItemAdded;

                              localStorageService.set("loggedInUser", response.data);
                              $rootScope.CustomerDetail = response.data;

                              angular.element("#DivLoginModel").modal('hide');

                              break;
                          case 1:
                              toastr.error('Please enter correct email!');
                              $scope.CustomerObj.email = null;
                              break;
                          case 2:
                              toastr.error('Please enter correct password!');
                              $scope.CustomerObj.password = null;
                              break;
                          case 3:
                              toastr.error('Please enter correct email and password!');
                              $scope.CustomerObj = null;
                              break;
                      }                                         
                  })
              .catch(function (response) {

              })
              .finally(function () {

              });  
        }
    }


    $scope.Logout=function()
    {
        UserDetail.customer_id = 0;
        UserDetail.firstname = "";
        UserDetail.lastname = "";
        UserDetail.points = "";
        UserDetail.address_id = 0;
        UserDetail.cartgroup_id = 0;
        UserDetail.TotalItemAdded = 0;
        localStorageService.set("loggedInUser", UserDetail);
        $rootScope.CustomerDetail = UserDetail;

        $state.go('Home');
    }

    //$scope.GetStoreDetailForDashboard = function () {
      
    //    $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreDetailByStoreId?StoreID=0")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.StoreDetail = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}

  
    //$scope.GetStoreDetailForDashboard();
             
});