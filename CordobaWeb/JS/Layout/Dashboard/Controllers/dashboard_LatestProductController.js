app.controller('Dashboard_LatestProductController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.LatestProductList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;


    $scope.GetLatestProductByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetLatestProductByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {
              debugger;
              if (response.data.length > 0) {
                  $scope.LatestProductList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetLatestProductByStoreId();

});