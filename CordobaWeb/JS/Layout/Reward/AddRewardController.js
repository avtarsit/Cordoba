app.controller('AddRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.reward_type_id = $stateParams.type;

    $scope.GetRewardGroupCustomers = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardGroupCustomers?loginUserId=29")
           .then(function (response) {
               if (response.data.length > 0) {
                   $scope.Customers = response.data;
               }
           })
          .catch(function (response) {

          })
          .finally(function () {

          });
    }

    $scope.GetRewardGroupCustomers();

    $scope.MakeWriteCmtFadeIn = function (id) {
        $('#write-cmt' + id).fadeIn();
    }
    $scope.MakeWriteCmtFadeOut = function (id) {
        $('#write-cmt' + id).fadeOut();
    }

    $scope.isReadonly = false; // default test value
    $scope.changeOnHover = false; // default test value
    $scope.maxValue = 5; // default test value

    $scope.ratingValue = 0;

    $scope.AddReward = function (ratingValue, item, index) {
        debugger;
        alert(ratingValue);

     //   $scope.AddRewardObj =
     //       {
     //           customer_id: reward_gientocustomer_id,
     //           reward_id: $stateParams.rewardId,
     //           IsWinner: false,

     //       };
     //   $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward", $scope.AddRewardObj)
     //      .then(function (response) {
     //          debugger;
     //          if (response.data > 0) {
     //              notificationFactory.customSuccess("Reward Saved Successfully.");
     //          }
     //      })
     //       .catch(function (response) {

     //       })
     //.finally(function () {

     //});

    }

    //$scope.InitStars = function (index) {
    //    debugger;
    //    $('#example' + index).barrating({
    //        theme: 'fontawesome-stars'
    //    });
    //}
});