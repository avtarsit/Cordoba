app.controller('AddOrUpdateSupplierController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

    $scope.SupplierID = 0;
    $scope.SupplierObj = {};
    $scope.IsEditMode = false;
    if ($stateParams.SupplierID != undefined && $stateParams.SupplierID != null) {
        $scope.PageTitle = "Update Supplier";
        $scope.IsEditMode = true;
        $scope.SupplierID = $stateParams.SupplierID;
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?SupplierID=" + $scope.SupplierID)
          .then(function (response) {
              $scope.SupplierObj = response.data[0];
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    else {
        $scope.PageTitle = "Add Supplier";
    }
    //#endregion


    $scope.SaveSupplier = function (form) {
     
        if (form.$valid) {
            $http.post(configurationService.basePath + "api/SupplierApi/InsertOrUpdateSupplier", $scope.SupplierObj)
           .then(function (response) {
      
               if (response.data == 0) {
                   //alert('already exists');
                   notificationFactory.customError("Supplier Name is already Exists!!");
               }
               if (response.data == 1) {
                   notificationFactory.customSuccess("Supplier Saved Successfully.");
                   $state.go('ShowSupplier');
               }
           })
            .catch(function (response) {

            })
     .finally(function () {

     });
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
                                $http.get(configurationService.basePath + "api/SupplierApi/DeleteSupplier?SupplierID=" + $scope.SupplierID)
                                      .then(function (response) {
                                          $state.go('ShowSupplier');
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
                    $state.go('ShowSupplier');
                }

            });
        }
        else {
            $state.go('ShowSupplier');
        }

    }



});