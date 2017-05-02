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

});