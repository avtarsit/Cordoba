

app.controller('OrderHistoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);



    $scope.GetOrderHistory = function () {
       
        var CustomerId = 1;
        $http.get(configurationService.basePath + "API/OrderApi/GetOrderHistory?customer_id=" + CustomerId)
          .then(function (response) {
              debugger;
              $scope.OrderHisotry = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetOrderHistory();


});