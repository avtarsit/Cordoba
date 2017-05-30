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
    $scope.GetCartDetailsByCartGroupId = function () {
        
        $http.get(configurationService.basePath + "API/CartApi/GetCartDetailsByCartGroupId?StoreID=" + $scope.StoreDetailInSession.store_id + "&cartgroup_id=" + $scope.cartgroup_id)
          .then(function (response) {
              if (response.data.length>0)
              {                  
                  $scope.CartItemList = response.data;
                  $scope.TotalItems = $scope.CartItemList.length;
                  $scope.AllItemSubtotal = $scope.CartItemList[0].AllItemSubtotal;
                  $scope.AllItemTotal = $scope.CartItemList[0].AllItemTotal;
                  $scope.CustomerAvailablePoints = $scope.CartItemList[0].CustomerAvailablePoints;
                  $rootScope.ShoppingCart.cartgroup_id = response.data[0].cartgroup_id;
                  $rootScope.ShoppingCart.TotalItemAdded = response.data[0].TotalItemAdded;
              }
              
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.AddOrRemoveItemFromCart=function(productObj,Quantity)
    {       
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + productObj.customer_id + "&product_id=" + productObj.product_id + "&qty=" + Quantity + "&cartgroup_id=" + productObj.cartgroup_id)
        .then(function (response) {         
            $rootScope.ShoppingCart = response.data;
            productObj.quantity = productObj.quantity + Quantity;
            $scope.GetCartDetailsByCartGroupId();

            notificationFactory.customSuccess("Shopping bag updated successfully.");
        })
    .catch(function (response) {

    })
    .finally(function () {

    });

    }

    $scope.DecreaseQuantity = function (Product) {
        if (Product.quantity >= 2) {
            $scope.AddOrRemoveItemFromCart(Product,-1);        
        }

    }

    $scope.IncreaseQuantity = function (Product) {
        $scope.AddOrRemoveItemFromCart(Product, 1);      
    }

    $scope.RemoveProductFromCart=function(Product)
    {
        $http.get(configurationService.basePath + "API/CartApi/RemoveProductFromCart?CartId=" + Product.cart_id)
        .then(function (response) {    
            $scope.GetCartDetailsByCartGroupId();
            notificationFactory.customSuccess("Product successfully removed from cart.");
        })
          .catch(function (response) {

             })
         .finally(function () {

         });

    }

    $scope.PlaceOrder=function()
    {

        $http.get(configurationService.basePath + "API/CartApi/PlaceOrder?CartId=" + Product.cart_id)
        .then(function (response) {
            $scope.GetCartDetailsByCartGroupId();
            notificationFactory.customSuccess("Product successfully removed from cart.");
        })
          .catch(function (response) {

          })
         .finally(function () {

         });
    }

    $scope.GetIpAddress=function()
    {
        $.getJSON("http://jsonip.com/?callback=?", function (data) {
            $scope.IpAddress = data.ip;            
        });
    }

    $scope.GetCartDetailsByCartGroupId();

    $scope.GetIpAddress();

});