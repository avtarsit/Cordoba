

app.controller('AuditPointsController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);



    $scope.AuditPoints = function () {

        var CustomerId = 1;
        $http.get(configurationService.basePath + "API/PointsAuditApi/GetPointsAuditList?customer_id=" + CustomerId)
          .then(function (response) {
              debugger;
              $scope.AuditPointsList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.AuditPoints();


});