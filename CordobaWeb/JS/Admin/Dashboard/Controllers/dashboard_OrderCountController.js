app.controller('dashboard_OrderCountController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService, AdminUserDetail) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.AdminUserDetail = AdminUserDetail;

    $scope.GetOrderCountData = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetOrderDetailCount?Store_Id=" + $scope.AdminUserDetail.store_id)
        .then(function (response) {
            if (response.data.length > 0) {
                $scope.ProcessingOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Processing' });
                $scope.DeliveredOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Delivered' });
                $scope.ReturnedOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Returned' });
                $scope.ProcessingOrderCount = $scope.ProcessingOrder.length > 0 ? $scope.ProcessingOrder[0].OrderCount : 0;
                $scope.DeliveredOrderCount = $scope.DeliveredOrder.length > 0 ? $scope.DeliveredOrder[0].OrderCount : 0;
                $scope.ReturnedOrderCount = $scope.ReturnedOrder.length > 0 ? $scope.ReturnedOrder[0].OrderCount : 0;
            }
        })
    .catch(function (response) {

    })
    .finally(function () {

    });
    }

    $scope.GetOrderCountData();
});