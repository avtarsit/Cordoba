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
              $scope.SelectedProductId = $scope.ProductObj.product_id;
              $scope.GetRelatedProductList($scope.ProductObj.product_id);
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetRelatedProductList = function (ProductId) {
        $http.get(configurationService.basePath + "API/ProductApi/GetRelatedProductList?StoreID=" + $scope.StoreDetailInSession.store_id + "&SelectedProductId=" + $scope.SelectedProductId + "&RelatedProductId=" + ProductId)
          .then(function (response) {

              $scope.RelatedProductList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.DecreaseQuantity=function(Product)
    {
        if (Product.CartQuantity>1)
        {
            Product.CartQuantity = Product.CartQuantity - 1;
        }
   
    }
   
   $scope.IncreaseQuantity = function (Product) {
        Product.CartQuantity = Product.CartQuantity + 1;
    }

    $scope.GetProductDetail();


});