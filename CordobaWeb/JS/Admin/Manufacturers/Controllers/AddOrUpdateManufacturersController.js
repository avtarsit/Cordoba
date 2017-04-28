app.controller('AddOrUpdateManufacturersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.ManufacturerID = 0; 
    $scope.IsEditMode = false;    
    if ($stateParams.ManufacturerID != undefined && $stateParams.ManufacturerID != null) {
        $scope.PageTitle = "Update Manufacturer";
        $scope.IsEditMode = true;
        $scope.ManufacturerID = $stateParams.ManufacturerID;
    }
    else {
        $scope.PageTitle = "Add Manufacturer";
    }
    //#endregion



    $scope.GetManufaturerDetail = function () {
        $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufaturerDetail?ManufacturersID=" + $scope.ManufacturerID)
          .then(function (response) {
              $scope.ManufacturerObj = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.SaveManufacturer = function (form) {
        if (form.$valid) {
            
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
                    $state.go('ShowManufacturer');
                }

            });
        }
        else {
            $state.go('ShowManufacturer');
        }

    }


    $scope.GetManufaturerDetail();

});