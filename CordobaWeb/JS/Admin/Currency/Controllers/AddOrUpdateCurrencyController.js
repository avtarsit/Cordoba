﻿app.controller('AddOrUpdateCurrencyController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {



    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.LoggedInUserId = -1;
    $scope.currency_id = 0;
    $scope.IsEditMode = false;
    if ($stateParams.CurrencyId != undefined && $stateParams.CurrencyId != null) {

        $scope.PageTitle = "Update Currency";
        $scope.IsEditMode = true;
        $scope.currency_id = $stateParams.CurrencyId;
    }
    else {
        $scope.PageTitle = "Add Currency";
    }
    //#endregion

    debugger;
    $scope.GetCurrencyDetail = function () {


        $http.get(configurationService.basePath + "api/CurrencyApi/GetCurrencyDetail?CurrencyId=" + $scope.currency_id)
          .then(function (response) {
              debugger;
              $scope.CurrencyObj = response.data;
              if (!($scope.user_id > 0)) //New User
              {
                  //$scope.CurrencyObj.status = 1;  // Temporary
                  //$scope.CurrencyObj.user_group_id = 1;  // Temporary
              }

          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.SaveCurrency = function (form) {


        if (form.$valid) {
            $scope.CurrencyObj.status = 1;
            $scope.CurrencyObj.currency_id = $scope.currency_id;
            $http.post(configurationService.basePath + "api/CurrencyApi/CreateOrUpdateCurrency", $scope.CurrencyObj)
         .then(function (response) {
             if (response.data > 0) {
                 toastr.success('Successfully Added.');
                 $state.go('Currency');
             }

         })
      .catch(function (response) {

      })
     .finally(function () {

     });

        }
    }




    $scope.DeleteCurrency = function () {
        bootbox.dialog({
            message: "Do you want remove Currency?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CurrencyApi/DeleteCurrency?CurrencyId=" + $scope.currency_id)
                                        .then(function (response) {
                                            if (response.data > 0) {
                                                toastr.success('Successfully Deleted.');
                                                $state.go('Currency');
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
    };
    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('Currency');
                }

            });
        }
        else {
            $state.go('Currency');
        }

    }



    $scope.GetCurrencyDetail();



});