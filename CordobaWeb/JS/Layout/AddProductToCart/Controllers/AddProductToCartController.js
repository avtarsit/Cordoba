app.controller('AddProductToCartController', function (StoreSessionDetail,$timeout,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion  
    $scope.AddProductToCart = function (ProductObj) {

        if ($rootScope.ShoppingCart.cartgroup_id == undefined || $rootScope.ShoppingCart.cartgroup_id == null)
        {
             cartgroup_id = 0;
        }
        else
        {
            cartgroup_id = $rootScope.ShoppingCart.cartgroup_id;
        }
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id + "&product_id=" + ProductObj.product_id + "&qty=1&cartgroup_id=" + cartgroup_id)
          .then(function (response) {
              toastr.success("Item successfully added in cart.");
              $rootScope.ShoppingCart = response.data;

          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    



});