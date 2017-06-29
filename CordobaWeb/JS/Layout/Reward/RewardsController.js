﻿app.controller('RewardController', function ($timeout,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    if (!(UserDetail.customer_id>0))
    {
             window.location.href = 'home/accessdenied';
    }
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "My Rewards";

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);
    $scope.LoginUserId = UserDetail.customer_id;

    $scope.MyRewardsList = function () {
        $http.get(configurationService.basePath + "api/RewardApi/MyRewards?id=" + $scope.LoginUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.MyRewardList = response.data;
              }
          })
        .catch(function (response) {

         })
         .finally(function () {

         });
    }

    $scope.GetAllRunningRewards = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetAllRunningRewards")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.AllRewards = response.data;
              }
          })
        .catch(function (response) {

        })
         .finally(function () {

         });
    }

    $scope.MyRewardsList();

    $scope.GetAllRunningRewards();

});