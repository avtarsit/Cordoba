

app.controller('OrderHistoryController', function ($timeout, StoreSessionDetail, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);



    $scope.GetOrderHistory = function () {
               
        $http.get(configurationService.basePath + "API/OrderApi/GetOrderHistory?customer_id=" + UserDetail.customer_id)
          .then(function (response) {              
              $scope.OrderHisotry = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetOrderHistory();


});