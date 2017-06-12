app.controller('ShowSupplierController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.SupplierList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true);

    $scope.dtColumnDefs = [
         DTColumnDefBuilder.newColumnDef(1).notSortable()
    ];

    $scope.PageTitle = "Show Suppliers";


    $scope.GetSupplierList = function () {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?SupplierID=0")
          .then(function (response) {
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