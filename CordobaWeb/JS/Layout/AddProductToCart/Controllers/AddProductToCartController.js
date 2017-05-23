app.controller('AddProductToCartController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion  
    $scope.AddProductToCart = function (ProductObj) {
       
        var CustomerId = 1;
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + CustomerId + "&product_id=" + ProductObj.product_id + "&qty=1&cartgroup_id=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    



});