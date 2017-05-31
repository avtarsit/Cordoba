app.controller('ProductPurchasedReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    createDatePicker();
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
    $scope.PageTitle = "Products Purchased List";
    $scope.ProductFilter = new Object();
    $scope.ProductFilter.order_status_id = 0;
    $scope.ProductFilter.store_id = 0;

    function GetStoreList() {

        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.store_id = 0;
                  DefaultOption.name = "All Stores";
                  $scope.StoreList.push(DefaultOption);
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    function GetOrderStatus() {
        $http.get(configurationService.basePath + "api/ProductPurchasedReportApi/GetOrderStatus?language_id=1")
       .then(function (response) {
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;
               var DefaultOption = new Object()
               DefaultOption.order_status_id = 0;
               DefaultOption.name = "All Statuses";
               $scope.OrderStatusList.push(DefaultOption);
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }

    function init() {
        GetOrderStatus();
        GetStoreList();
    }

    init();

});