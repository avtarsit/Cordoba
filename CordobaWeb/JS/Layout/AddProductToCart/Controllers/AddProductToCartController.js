app.controller('AddProductToCartController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    //#endregion  
    $scope.AddProductToCart = function () {
        debugger;
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=0&customer_id=1&product_id=1&qty=1&cartgroup_id=0")
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