app.controller('ProductDetailLayoutController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion   
    if ($stateParams.ProductId != undefined && $stateParams.ProductId != null) {
        $scope.SelectedProductId = parseInt($stateParams.ProductId);
    }
  
    $scope.GetProductDetail = function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductDetailForLayout?StoreID=" + $scope.StoreDetailInSession.store_id + "&ProductId=" + $scope.SelectedProductId)
          .then(function (response) {

                  $scope.ProductObj = response.data;               
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }




    $scope.GetProductDetail();


});