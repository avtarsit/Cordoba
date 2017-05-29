app.controller('CartDetailController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.cartgroup_id = 0;
    if ($stateParams.cartgroup_id != undefined && $stateParams.cartgroup_id != null)
    {
        $scope.cartgroup_id = $stateParams.cartgroup_id;
    }

    //#endregion  
    debugger;
    $scope.GetCartDetailsByCartGroupId = function () {
        
        $http.get(configurationService.basePath + "API/CartApi/GetCartDetailsByCartGroupId?StoreID=" + $scope.StoreDetailInSession.store_id + "&cartgroup_id=" + $scope.cartgroup_id)
          .then(function (response) {
              debugger;
              $scope.CartItemList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCartDetailsByCartGroupId();



});