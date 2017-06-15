app.controller('OrderDetailController', function ($timeout, $state,StoreSessionDetail,UserDetail,$http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();

    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.order_id =parseInt($stateParams.OrderId);
    $scope.GetOrderDetail_Layout = function () {
        $http.get(configurationService.basePath + "api/OrderApi/GetOrderDetail_Layout?order_id=" + $scope.order_id + "&store_id=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {
                  $scope.OrderdetailObj = response.data;              
          })
        .catch(function (response) {

        })
         .finally(function () {

         });
    }

    $scope.GetOrderDetail_Layout();

});