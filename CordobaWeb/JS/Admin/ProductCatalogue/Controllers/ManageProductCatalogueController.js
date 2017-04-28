app.controller('ManageProductCatalogueController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    $scope.ProductCatalogueId = 0;
    if ($stateParams.ProductCatalogueId != undefined && $stateParams.ProductCatalogueId != null) {
        $scope.PageTitle = "Update Product Catalogue";
        $scope.ProductCatalogueId = $stateParams.ProductCatalogueId;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Product Catalogue";
    }
    //#endregion


    $scope.SaveProductCatalogue = function (form) {
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


    $scope.GetProductCatalogueById = function () {
        $http.get(configurationService.basePath + "api/ProductCatalogueApi//GetProductCatalogueById?ProductCatalogueId=" + $scope.ProductCatalogueId)
          .then(function (response) {
              debugger;
              $scope.ProductCatalogueObj = response.data;
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

    $scope.GetProductCatalogueById();

});