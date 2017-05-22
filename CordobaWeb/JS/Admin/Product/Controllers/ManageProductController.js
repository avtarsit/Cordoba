app.controller('ManageProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion

    function Init() {
        createDatePicker();
        $scope.EnumStatus = [
             { 'StatusId': 1, 'StatusName': 'Enabled' }
           , { 'StatusId': 2, 'StatusName': 'Disabled' }
        ];
        $scope.EnumLanguageList = [
            { 'LangId': 1, 'LangName': 'English' }
          , { 'LangId': 2, 'LangName': 'Deutsch' }
           , { 'LangId': 3, 'LangName': 'Français' }
            , { 'LangId': 4, 'LangName': 'Italiano' }
             , { 'LangId': 5, 'LangName': 'Español' }
              , { 'LangId': 6, 'LangName': 'Português' }
               , { 'LangId': 7, 'LangName': 'Nederlands' }
        ];


        $scope.IsEditMode = false;
        $scope.product_id = 0;
        $scope.ProductObj = new Object();
        if ($stateParams.ProductId != undefined && $stateParams.ProductId != null) {
            $scope.PageTitle = "Update Product";
            $scope.product_id = $stateParams.ProductId;
            $scope.IsEditMode = true;
        }
        else {
            $scope.PageTitle = "Add Product";
        }


        GetLanguageList();
        GetManufacturersList();
        GetCategoryList();

        $scope.GetProductById();
    }


    //#region Image Tab
    $scope.AddImage = function () {
        var NewImage = new Object();
        NewImage.Id = 0;
        NewImage.Image = null;
        NewImage.SortOrder = null;
        if ($scope.ProductObj.ImageList == undefined || $scope.ProductObj.ImageList == null) {
            $scope.ProductObj.ImageList = [];
        }
        $scope.ProductObj.ImageList.push(NewImage);
    }
    $scope.RemoveImage = function (event, item) {
        if (item.Id != undefined && item.Id != null && item.Id != 0) {
            RemoveBannerImage(item);
        }
        else {
            $scope.ProductObj.ImageList.pop(item);
        }
    }
    //#endregion

    $scope.SaveProduct = function (form) {
        debugger;
        $scope.ProductObj;
        if (form.$valid) {
        }
    }

    $scope.DeleteProduct = function () {
        bootbox.dialog({
            message: "Do you want remove Product?",
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

    $scope.GetProductById = function () {
        $http.get(configurationService.basePath + "api/ProductApi/GetProductById?product_id=" + $scope.product_id)
          .then(function (response) {
              $scope.ProductObj = response.data;
              debugger;
          })
      .catch(function (response) {
          debugger;
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
                    $state.go('Product');
                }
            });
        }
        else {
            $state.go('Product');
        }
    }

    function GetLanguageList() {
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=0")
        .then(function (response) {
            if (response.data.length > 0) {
                $scope.LanguageList = response.data;
            }
        })
    .catch(function (response) {

    })
    .finally(function () {

    });

    }

    function GetManufacturersList() {
        $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ManufacturersList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetCategoryList() {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryList?CategoryId=0")
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    Init();
});