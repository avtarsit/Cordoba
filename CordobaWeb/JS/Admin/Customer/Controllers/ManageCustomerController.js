﻿app.controller('ManageCustomerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreObj = new Object();
    $scope.IsEditMode = false;
    $scope.PageTitle = "Manage Customer";
    if ($stateParams.CustomerId != undefined && $stateParams.CustomerId != null) {
       
        $scope.IsEditMode = true;
    }  
    //#endregion    


    $scope.DeleteStore = function () {
        bootbox.dialog({
            message: "Do you want remove store?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {

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
                    $state.go('CustomerOrderReport');
                }

            });
        }
        else {
            $state.go('CustomerOrderReport');
        }

    }

});