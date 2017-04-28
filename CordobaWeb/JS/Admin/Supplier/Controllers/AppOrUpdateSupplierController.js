app.controller('AddOrUpdateSupplierController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.SupplierID = 0;
    $scope.IsEditMode = false;
    if ($stateParams.SupplierID != undefined && $stateParams.SupplierID != null) {
        $scope.PageTitle = "Update Supplier";
        $scope.IsEditMode = true;
        $scope.SupplierID = $stateParams.SupplierID;
    }
    else {
        $scope.PageTitle = "Add Supplier";
    }
    //#endregion


    $scope.GetSupplierDetail = function () {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierDetail?SupplierID=" + $scope.SupplierID)
          .then(function (response) {
              $scope.SupplierObj = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.SaveSupplier = function (form) {
        if (form.$valid) {

        }
    }

    $scope.DeleteSupplier = function () {
        bootbox.dialog({
            message: "Do you want remove Supplier?",
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
                    $state.go('ShowSupplier');
                }

            });
        }
        else {
            $state.go('ShowSupplier');
        }

    }


    $scope.GetSupplierDetail();

});