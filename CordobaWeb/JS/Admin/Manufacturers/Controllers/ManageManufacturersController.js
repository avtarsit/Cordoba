app.controller('ManageManufacturerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    if ($stateParams.ManufacturerID != undefined && $stateParams.ManufacturerID != null) {
        $scope.PageTitle = "Update Manufacturer";
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Manufacturer";
    }
    //#endregion



    $scope.SaveManufacturer = function (form) {
        if (form.$valid) {
            debugger;
        }
    }

    $scope.DeleteManufacturer = function () {
        bootbox.dialog({
            message: "Do you want remove Manufacturer?",
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
                        label: "NO",
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
                    $state.go('ShowManufacturer');
                }

            });
        }
        else {
            $state.go('ShowManufacturer');
        }

    }

});