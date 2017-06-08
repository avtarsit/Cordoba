

app.controller('AuditPointsController', function ($timeout,StoreSessionDetail,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);



    $scope.AuditPoints = function () {
        
        $http.get(configurationService.basePath + "API/PointsAuditApi/GetPointsAuditList?customer_id=" + UserDetail.customer_id)
          .then(function (response) {              
              $scope.AuditPointsList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.AuditPoints();


});