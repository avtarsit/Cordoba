app.controller('OrderReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Sales Reports - Orders";


    //$scope.GetManufacturersList = function () {
    //    $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.ManufacturersList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}
    //$scope.GetManufacturersList();




});