app.controller('ShowSupplierController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Show Suppliers";


    $scope.GetSupplierList = function () {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?SupplierID=0")
          .then(function (response) {
              debugger;
              if (response.data.length > 0) {                  
                  $scope.SupplierList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetSupplierList();




});