app.controller('ShowManufaturersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)                

    $scope.dtColumnDefs = [
       DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Show Manufacturers";


    $scope.GetManufacturersList = function () {
        $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0")
          .then(function (response) {
              if (response.data.length > 0) {                  
                  $scope.ManufacturersList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetManufacturersList();




});