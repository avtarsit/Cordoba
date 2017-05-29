﻿app.controller('ShowRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.GetRewardList = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardList?reward_id=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.rewardList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetRewardList();
});