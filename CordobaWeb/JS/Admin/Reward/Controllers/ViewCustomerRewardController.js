app.controller('ViewCustomerRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "View Reward";
    $scope.RewardName = $stateParams.name;
    $scope.ViewRewards = [];

    if ($stateParams.rewardId != undefined && $stateParams.rewardId != null) {
        $http.get(configurationService.basePath + "api/RewardApi/ViewCustomerRewards?reward_id=" + $stateParams.rewardId)
        .then(function (response) {
            debugger;
            $scope.ViewRewards = response.data;
        })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }

   
});