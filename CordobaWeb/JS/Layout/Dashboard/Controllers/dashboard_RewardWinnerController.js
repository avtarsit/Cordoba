app.controller('Dashboard_RewardWinnerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  

    //$scope.RewardWinners = [
    //  { CustomerImage: "/Content/images/error-img.png", CustomerName: "Jeff perry", Rating: "4", Points: "29 Points" }
    //, { CustomerImage: "/Content/images/error-img.png", CustomerName: "Jeff perry", Rating: "4", Points: "29 Points" }
    //, { CustomerImage: "/Content/images/error-img.png", CustomerName: "Jeff perry", Rating: "4", Points: "29 Points" }
    //];


    $scope.Dashboard_RewardWinner = function () {
        $http.get(configurationService.basePath + "api/RewardApi/Dashboard_RewardWinner")
        .then(function (response) {
          debugger;
          if (response.data.length > 0) {
              $rootScope.Make_rewardWinnerVisible = true;
          }
          else {
              $rootScope.Make_rewardWinnerVisible = false;
          }
          $scope.RewardWinners = response.data;
      })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.Dashboard_RewardWinner();

});