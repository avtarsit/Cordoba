app.controller('DashboardController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();   
    //#endregion  
    debugger;
    var abc = StoreSessionDetail;
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