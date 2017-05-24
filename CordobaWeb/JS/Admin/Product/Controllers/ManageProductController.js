app.controller('ManageProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion

    function Init() {
        $scope.ProductObjSubtract = [{ ID: 1, Name: 'Yes' }, { ID: 0, Name: 'No' }];
        $scope.ProductStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
        $scope.ProductObjStock_Status = [{ ID: 6, Name: '2-3 Days' }, { ID: 7, Name: 'In Stock' }, { ID: 5, Name: 'Out Of Stock' }, { ID: 8, Name: 'Pre-Order' }];

        $scope.IsEditMode = false;
        $scope.product_id = 0;
        $scope.ProductObj = new Object();

        createDatePicker();
        $scope.EnumLanguageList = [
            { 'LangId': 1, 'LangName': 'English' }
          , { 'LangId': 2, 'LangName': 'Deutsch' }
           , { 'LangId': 3, 'LangName': 'Français' }
            , { 'LangId': 4, 'LangName': 'Italiano' }
             , { 'LangId': 5, 'LangName': 'Español' }
              , { 'LangId': 6, 'LangName': 'Português' }
               , { 'LangId': 7, 'LangName': 'Nederlands' }
        ];

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
        GetSupplierList();
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
              debugger;
              $scope.ProductObj = response.data;
              CreateDescriptionObject();
              if($scope.ProductObj.product_id==0)
              {
                 
                  $scope.ProductObj.manufacturer_id = 0;
                  $scope.ProductObj.supplier_id = 0;
                  $scope.ProductObj.country_id = 222   // country_id  -United Kingdom
              }
          })
      .catch(function (response) {
          
      })
      .finally(function () {

      });
    }

    function CreateDescriptionObject() {
        var TempDescObject = [];
        angular.copy($scope.ProductObj.ProductDescriptionList, TempDescObject);
        $scope.ProductObj.ProductDescriptionList = [];
        debugger;
        angular.forEach($scope.LanguageList, function (col, i) {
            var ProductDescObj = $filter('filter')(TempDescObject, { language_id: col.language_id }, true);
            if (ProductDescObj == undefined || ProductDescObj == null || ProductDescObj.length==0)
            {
                var DescObj = new Object();
                DescObj.language_id = col.language_id;
                DescObj.name = "";
                DescObj.description = "";
                DescObj.tag = "";
                $scope.ProductObj.ProductDescriptionList.push(DescObj);
            }
            else {
                $scope.ProductObj.ProductDescriptionList.push(ProductDescObj[0]);
            }
        });
        debugger;
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
                  debugger;
                  $scope.ManufacturersList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.manufacturer_id=0;
                  DefaultOption.name = " --- None --- ";
                  $scope.ManufacturersList.push(DefaultOption);
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

    function GetSupplierList() {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?SupplierID=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.SupplierList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.supplier_id = 0;
                  DefaultOption.name = " --- None --- ";
                  $scope.SupplierList.push(DefaultOption);
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetSelectedCatalogueListCSV(CatalogueObj) {
        var CatalogueIdCSV = "";
        var SelectedCatalogueList = $filter('filter')(CatalogueObj, { IsSelected: true }, true);
        CatalogueIdCSV = GetCSVFromJsonArray(SelectedCatalogueList, "catalogue_Id");
        return CatalogueIdCSV;
    }

    $scope.InsertUpdateProduct = function (form) {
        debugger;
        if (form.$valid) {
            $scope.ProductObj.CatalogueIdCSV = "";
            $scope.ProductObj.CatalogueIdCSV = GetSelectedCatalogueListCSV($scope.ProductObj.CatalogueList);
            var productEntity = JSON.stringify($scope.ProductObj);
            $http.post(configurationService.basePath + "api/ProductApi/InsertUpdateProduct", productEntity)
              .then(function (response) {
                  if (response.data > 0) {
                      notificationFactory.customSuccess("Product Saved Successfully.");
                      $state.go('Product');
                  }
                  else if (response.data == -1) {
                      notificationFactory.customError("Product name is already Exists!");
                  }
              })
          .catch(function (response) {
              notificationFactory.error("Error occur during save record.");
          })
          .finally(function () {

          });

        }
    }


    //#region init
    Init();
    //#endregion Image Tab


});