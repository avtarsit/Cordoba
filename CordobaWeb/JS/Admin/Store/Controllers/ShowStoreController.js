app.controller('ShowStoreController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval,DTColumnDefBuilder,DTOptionsBuilder, $http, $log, $q) {
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

    $scope.PageTitle = "Show Stores";

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=0")
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

    $scope.GetStoreList();




});