app.controller('AddRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.RewardName = $stateParams.rewardname;


});