app.controller('ManageCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.PageTitle = "Update Category";
        $scope.CategoryId = $stateParams.CategoryId;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Category";
    }
    //#endregion


    $scope.SaveCategory = function (form) {
        if (form.$valid) {
            debugger;
        }
    }

    $scope.DeleteCategory = function () {
        bootbox.dialog({
            message: "Do you want remove category?",
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
                    $state.go('ShowCategory');
                }
            });
        }
        else {
            $state.go('ShowCategory');
        }
    }

});