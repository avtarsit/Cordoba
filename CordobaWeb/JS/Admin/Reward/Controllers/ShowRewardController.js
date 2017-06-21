app.controller('ShowRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.GetRewardList = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardList?reward_id=0" + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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