app.controller('ManageProductCatalogueController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    $scope.CatalogueObj = new Object();
    $scope.CatalogueId = 0;
    if ($stateParams.CatalogueId != undefined && $stateParams.CatalogueId != null) {
        $scope.PageTitle = "Update Product Catalogue";
        $scope.CatalogueId = $stateParams.CatalogueId;
        $scope.IsEditMode = true;
        GetCatalogueById();
    }
    else {
        $scope.PageTitle = "Add Product Catalogue";
    }
    //#endregion


    $scope.InsertUpdateCatalogue = function (form) {
        if (form.$valid) {
            var catalogueEntity = JSON.stringify($scope.CatalogueObj);
            $http.post(configurationService.basePath + "api/CatalogueApi/InsertUpdateCatalogue", catalogueEntity)
                .then(function (response) {
                    if (response.data == 1) {
                        notificationFactory.customSuccess("Product Catalogue Saved Successfully.");
                        $state.go('ShowProductCatalogue');
                    }
                    else if (response.data == -1) {
                        notificationFactory.customError("Product Catalogue name is already Exists!");
                    }
                })
                .catch(function (response) {
                })
                .finally(function () {
                    
                });
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


    function GetCatalogueById() {
        $http.get(configurationService.basePath + "api/CatalogueApi//GetCatalogueById?CatalogueId=" + $scope.CatalogueId)
          .then(function (response) {

              $scope.CatalogueObj = response.data;
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }


    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowProductCatalogue');
                }
            });
        }
        else {
            $state.go('ShowProductCatalogue');
        }
    }

   

});