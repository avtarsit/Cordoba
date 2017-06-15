﻿app.controller('ShowUserController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval,DTColumnDefBuilder, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.UserList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.dtColumnDefs = [
      DTColumnDefBuilder.newColumnDef(3).notSortable()
    ];

    $scope.PageTitle = "Show Users";


    $scope.GetUserList = function () {
        $http.get(configurationService.basePath + "api/UserApi/GetUserList")
          .then(function (response) {
            
              if (response.data.length > 0) {
                  $scope.UserList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetUserList();




});