app.controller('AddRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.reward_type_id = $stateParams.type;
    $scope.loginUserid = 29;
    $scope.reward_id = $stateParams.rewardId;

    $scope.GetRewardGroupCustomers = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardGroupCustomers?loginUserId=" + $scope.loginUserid + "&reward_id=" + $scope.reward_id)
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

    //$scope.MarkAlreadyStargiven = function () {
    //    debugger;
    //    $scope.isreadonlyAleardyAssigned = false;
    //    for (var i = 0; i < $scope.Customers.length; i++) {
    //        debugger;
    //        if ($scope.Customers[i].IsRewarded == true) {
                
    //            $("#ratingValue" + i).find('i').removeClass('fa-star-o').addClass('fa-star')
    //            $("#ratingValue" + i + " *").attr("disabled", "disabled").off('click');
    //        }
    //    }
    //}



    //////region star ratting Directive deafult setting
    $scope.isReadonly = false; // default test value
    $scope.changeOnHover = false; // default test value
    $scope.maxValue = 5; // default test value
    //////region star ratting Directive deafult setting


    $scope.ratingValue = 0;

    $scope.AddReward = function (item, index) {
        if (parseInt($scope.reward_type_id) == 2) {
            debugger;
            var ratingValue = $("#medalDiv" + index).find('input[type=radio]:checked').val();
            if ($("#medalDiv" + index).find('input[type=radio]:checked').length > 0) {

                $scope.AddRewardObj = item;
                $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                $scope.AddRewardObj.reward_name = ratingValue;
                $scope.AddRewardObj.IsWinner = false;
                $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);

                bootbox.dialog({
                    message: "Do you want Give Reward to selected User? Once you will give reward you can't modify it. Please make sure once again.",
                    title: "Confirmation",
                    className: "model",
                    buttons: {
                        success:
                            {
                                label: "Yes",
                                className: "btn btn-primary theme-btn",
                                callback: function (result) {
                                    if (result) {
                                        $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward", $scope.AddRewardObj)
                                          .then(function (response) {
                                              debugger;
                                              if (response.data > 0) {
                                                  notificationFactory.customSuccess("Reward Saved Successfully.");
                                                  $scope.GetRewardGroupCustomers();
                                              }
                                          })
                                           .catch(function (response) {

                                           })
                                           .finally(function () {

                                           });
                                    }
                                }
                            },
                        danger:
                            {
                                label: "No",
                                className: "btn btn-default",
                                callback: function () {
                                    return true;
                                }
                            }
                    }
                });
            }
            else {
                notificationFactory.customError("Please select Medal.");
            }
        }
        if (parseInt($scope.reward_type_id) == 1) {
            alert($("#ratingValue" + index + " .fa-star").find('i').prevObject.size());
            debugger;
            var ratingValue = $("#ratingValue" + index + " .fa-star").find('i').prevObject.size();
            if (ratingValue > 0) {
                $scope.AddRewardObj = item;
                $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                $scope.AddRewardObj.NoOfStars = parseInt(ratingValue);
                $scope.AddRewardObj.IsWinner = false;
                $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);

                bootbox.dialog({
                    message: "Do you want Give Reward to selected User? Once you will give reward you can't modify it. Please make sure once again.",
                    title: "Confirmation",
                    className: "model",
                    buttons: {
                        success:
                            {
                                label: "Yes",
                                className: "btn btn-primary theme-btn",
                                callback: function (result) {
                                    if (result) {
                                        $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward", $scope.AddRewardObj)
                                            .then(function (response) {
                                                debugger;
                                                if (response.data > 0) {
                                                    notificationFactory.customSuccess("Reward Saved Successfully.");
                                                    $scope.GetRewardGroupCustomers();
                                                }
                                            })
                                             .catch(function (response) {

                                             })
                                              .finally(function () {

                                              });
                                    }
                                }
                            },
                        danger:
                            {
                                label: "No",
                                className: "btn btn-default",
                                callback: function () {
                                    return true;
                                }
                            }
                    }
                });
            }
            else {
                notificationFactory.customError("Please select ratting.");
            }
        }
    }
});