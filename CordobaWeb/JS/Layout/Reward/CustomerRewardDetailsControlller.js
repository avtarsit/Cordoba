app.controller('CustomerRewardDetailsController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.RewardName = $stateParams.rewardname;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.RewradDetails = function () {
        if ($stateParams.reward_id != null) {
            $http.get(configurationService.basePath + "api/RewardApi/RewardCustomerDetails?reward_id=" + $stateParams.reward_id)
              .then(function (response) {
                  if (response.data.length > 0) {
                      $scope.RewardDetails = response.data;
                  }
              })
            .catch(function (response) {

            })
             .finally(function () {

             });
        }
    }

    $scope.RewradDetails();

});