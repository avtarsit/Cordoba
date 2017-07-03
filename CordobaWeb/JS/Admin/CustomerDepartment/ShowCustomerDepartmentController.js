app.controller('ShowCustomerDepartmentController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, DTColumnDefBuilder, $http, $log, $q) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId; 
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.IsStoreDropDownDisabled = false;

    $scope.CustomerDepartmentList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Customer Department";

    $scope.GetCustomerDepartmentList = function () {
        $http.get(configurationService.basePath + "api/CustomerDepartmentApi/GetCustomerDepartmentList?StoreId=" + $scope.StoreId)
          .then(function (response) {
              if (response.data != undefined && response.data != null) {
                  $scope.CustomerDepartmentList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetStoreList = function () {

        $http.get(configurationService.basePath + "api/CategoryApi/GetStoreNameList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                 .then(function (response) {
                     if (response.data.length > 0) {
                         $scope.StoreList = response.data;

                     }
                 })
             .catch(function (response) {

             })
             .finally(function () {

             });

    }

    $scope.CheckIsStoreDropDownDisabled=function()
    {        
        if ($scope.StoreId!=0)
        {
              $scope.IsStoreDropDownDisabled = true;
        }
    }

    $scope.GetStoreList();
    $scope.GetCustomerDepartmentList();

});