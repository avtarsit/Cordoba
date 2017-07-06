app.controller('Dashboard_SpecialOffersController', function ($timeout,StoreSessionDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion     

    $scope.SpecialOfferList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;

    $scope.GetSpecialOfferListByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetSpecialOfferListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {          
              if (response.data.length > 0) {                  
                  $scope.SpecialOfferList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetSpecialOfferListByStoreId();

   




});