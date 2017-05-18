app.controller('ShowCustomerGroupController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions

    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.UserList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Show Customer Group";


    $scope.GetUserList = function () {
        $http.get(configurationService.basePath + "api/CustomerGroupApi/GetCustomerGroupList")
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.CustomerGroupList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetUserList();




});