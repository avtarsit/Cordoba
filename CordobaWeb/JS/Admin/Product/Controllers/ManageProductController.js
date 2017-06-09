app.controller('ManageProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion

    function Init() {
        $scope.ProductObjSubtract = [{ ID: 1, Name: 'Yes' }, { ID: 0, Name: 'No' }];
        $scope.ProductStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
        $scope.HotSpecialProductStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
        $scope.ProductObjStock_Status = [{ ID: 6, Name: '2-3 Days' }, { ID: 7, Name: 'In Stock' }, { ID: 5, Name: 'Out Of Stock' }, { ID: 8, Name: 'Pre-Order' }];
        $scope.IsEditMode = false;
        $scope.product_id = 0;
        $scope.ProductObj = new Object();
        $scope.CurrentDate = new Date();

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
        $scope.GetHotOrSpecialProductById();
        $scope.NeedToShowAddHotBtn = 0;
        $scope.NeedToShowSpecialAddBtn = 0;
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
                                $http.post(configurationService.basePath + "api/ProductApi/DeleteProduct?product_id=" + $scope.product_id)
                               .then(function (response) {
                                   if (response.data > 0)
                                       notificationFactory.successDelete();
                                   else {
                                       notificationFactory.FKReferenceDelete();
                                   }
                                   $state.go('Product');
                               })
                               .catch(function (response) {
                                   notificationFactory.errorDelete(response.data.ExceptionMessage);
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

    $scope.GetProductById = function () {
        $http.get(configurationService.basePath + "api/ProductApi/GetProductById?product_id=" + $scope.product_id)
          .then(function (response) {
              $scope.ProductObj = response.data;
              CreateDescriptionObject();
              if ($scope.ProductObj.product_id == 0) {
                  // Default Values
                  $scope.ProductObj.manufacturer_id = 0;
                  $scope.ProductObj.supplier_id = 0;
                  $scope.ProductObj.country_id = 222   // country_id  -United Kingdom
                  $scope.ProductObj.Quantity = 1;
                  $scope.ProductObj.minimum = 1;
                  $scope.ProductObj.minimum = 1;
                  $scope.ProductObj.subtract = 1;
                  $scope.ProductObj.stock_status_id = 6;
                  $scope.ProductObj.shipping = 1;
                  $scope.ProductObj.date_available = $filter('date')($scope.CurrentDate, $rootScope.GlobalDateFormat);
                  //$('#startdate').bootstrapDP('update', $scope.RewardObj.start_date);
                  $scope.ProductObj.shipping = 1;
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
        angular.forEach($scope.LanguageList, function (col, i) {
            var ProductDescObj = $filter('filter')(TempDescObject, { language_id: col.language_id }, true);
            if (ProductDescObj == undefined || ProductDescObj == null || ProductDescObj.length == 0) {
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
                  var DefaultOption = new Object()
                  DefaultOption.manufacturer_id = 0;
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


    $scope.InsertAsHotOrSpecialProduct = function (form) {

        if (form.$valid) {
            $scope.HotOrSpecialProductObj.CatalogueIdCSV = "";
            $scope.HotOrSpecialProductObj.CatalogueIdCSV = GetSelectedCatalogueListCSV($scope.ProductObj.CatalogueList);
            $scope.HotOrSpecialProductObj.store_id = 0;  // this is temporary
            $scope.HotOrSpecialProductObj.product_id = $scope.product_id;
            $scope.HotOrSpecialProductObj.created_by = -1;// this is temporary
            var hotSpecialProductEntity = JSON.stringify($scope.HotOrSpecialProductObj);
            if($scope.HotOrSpecialProductObj.IsHotProduct==true)
            {
            
                $http.post(configurationService.basePath + "api/ProductApi/InsertAsHotProduct", hotSpecialProductEntity)
                  .then(function (response) {
                      if (response.data > 0) {
                          notificationFactory.customSuccess("Product Saved Successfully.");
                          $scope.GetHotOrSpecialProductById();                          
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
            else {
                $http.post(configurationService.basePath + "api/ProductApi/InsertAsSpecialProduct", hotSpecialProductEntity)
                            .then(function (response) {
                                if (response.data > 0) {
                                    notificationFactory.customSuccess("Product Saved Successfully.");
                                    $scope.GetHotOrSpecialProductById();
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
    }


    //$scope.GetHotOrSpecialProductById=function()
    //{
    //    debugger;

    //    $http.get(configurationService.basePath + "API/ProductApi/GetHotOrSpecialProductById?language_id="
    //                        + $scope.StoreDetailInSession.language_id +
    //                        "&store_id=" + $scope.StoreDetailInSession.store_id +
    //                        "&product_id=" + $scope.product_id  
    //                        )
    //      .then(function (response) {

    //          if (response.data.length > 0) {
    //              $scope.HotOrSpecialProductList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });

    //}

    $scope.bindHotOrSpecial = function (data) {
      
        var dataList = data;
        if ($.fn.DataTable.isDataTable("#dataTableHotOrSpecial")) {
            $('#dataTableHotOrSpecial').DataTable().clear();
            $('#dataTableHotOrSpecial').DataTable().destroy();
        }

        $('#dataTableHotOrSpecial').DataTable({
            searching: false,
            dom: '<"table-responsive"rt><"bottom"lip<"clear">>',
            bProcessing: true,            
            retrieve: true,          
            stateSave: false,
            paging: false,
            bInfo : false,
            data: dataList,
            "order": [[0, "asc"]],
            columnDefs: [
            
                {
                    orderable: true,
                    mData: 'productName',
                    title: '<B><h5>Product Name</h5></B>',
                    targets: [0]
                },
                {
                    orderable: true,                    
                    title: 'Type',
                    targets: [1],
                    "render": function (data, type, row) {
                        if (row.IsHotProduct ==true) {
                            return '<label>' + 'Hot Product' + '</label>';
                        }
                        else {
                            return '<label>' +'Special Product' + '</label>';;
                        }
                    }
                },
                {
                    orderable: true,
                    mData: 'startDate',
                    title: '<B><h5>Start Date</h5></B>',
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    },
                    targets: [2]
                },
                {
                    orderable: true,
                    mData: 'endDate',
                    title: '<B><h5>End Date</h5></B>',
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    },
                    targets: [3]
                },
                {
                    orderable: true,
                    title: '<B><h5>Status</h5></B>',
                    targets: [4],
                    render: function (data, type, row) {
                        if (row.status == 1) {
                            return "Enabled";
                        }
                        else {
                            return "Disabled";
                        }

                    }
                },
               {
                   orderable: true,
                   mData: 'PrimaryKeyId',
                   title: '<B><h5>Id</h5></B>',
                   targets: [5],
                   visible:false
               },
                {
                    orderable: false,
                    width: '95px',
                    render: function (data, type, row) {
                        debugger;
                        //return '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a title="Edit" href="" ng-click="EditHotOrSpecialProduct($event)"><span class="fa fa-pencil-square-o"></span></a>';             
                        if (row.status==1)
                        {                           
                            return '<button class="btn-danger" ng-click="EditHotOrSpecialProduct($event)">Disable</button>';
                        }
                        else {                        
                            return '<button class="btn-success" ng-click="EditHotOrSpecialProduct($event)">Enable</button>';
                        }
                      
                    },
                    title: 'Action',
                    targets: [6]
                }

            ],

            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            }
        });
    }

    $scope.EditHotOrSpecialProduct = function ($event)
    {
        debugger;
        var table = $('#dataTableHotOrSpecial').DataTable();
        var HorOrSpecialProductObj = table.row($($event.target).parents('tr')).data();

        if(HorOrSpecialProductObj.PrimaryKeyId>0)
        {
            $scope.HotOrSpecialProductObj = HorOrSpecialProductObj;
            if(HorOrSpecialProductObj.IsHotProduct==true)
            {
                $scope.HotOrSpecialProductObj.hot_productid = HorOrSpecialProductObj.PrimaryKeyId;
                $scope.HotOrSpecialProductObj.special_productid = 0;
            }
            else {
                $scope.HotOrSpecialProductObj.special_productid = HorOrSpecialProductObj.PrimaryKeyId;
                $scope.HotOrSpecialProductObj.hot_productid = 0;
            }           
            if ($scope.HotOrSpecialProductObj.status==1) {
                $scope.HotOrSpecialProductObj.status = 0;
            }
        else
        {
                $scope.HotOrSpecialProductObj.status = 1;
        }
            

            $scope.InsertAsHotOrSpecialProduct($scope.form);
           
        }      
    }
    $scope.GetHotOrSpecialProductById=function()
    {
        $scope.store_id = 0;
        $scope.language_id = 1;
        $http.get(configurationService.basePath + "API/ProductApi/GetHotOrSpecialProductById?language_id="
                           + $scope.language_id +
                           "&store_id=" + $scope.store_id +
                           "&product_id=" + $scope.product_id
                           )
        .success(function (data) {       
            //var Hot = $filter('filter')(data, { IsHotProduct: true ,status:1}, true)[0];
            //var Special = $filter('filter')(data, { IsHotProduct: false, status: 1 }, true)[0];
            //if (Hot != undefined && Hot!=null)
            //{
            //    $scope.NeedToShowAddHotBtn = 0;
            //}
            //else {
            //    $scope.NeedToShowAddHotBtn = 1;
            //}
            //if (Special != undefined && Special != null) {
            //    $scope.NeedToShowSpecialAddBtn = 0;
            //}
            //else {
            //    $scope.NeedToShowSpecialAddBtn = 1;
            //}        
            $scope.bindHotOrSpecial(data);

        }).error(function (err) {
            showNotification(false, "error : " + err);
        });
    };

    $scope.HotOrSpecialCancel = function () {
        $scope.NeedtoShowHot_SpeacialContainer = 0;
    }

    //$scope.GetHotOrSpecialProductDetailById = function (isHotProduct, product_id) {
    //    debugger;
    //    $http.get(configurationService.basePath + "API/ProductApi/GetHotOrSpecialProductDetailById?IsHotProduct="
    //                       + isHotProduct +
    //                       "&product_id=" + product_id
    //                       )
    //    .success(function (data) {   
    //        debugger; 
    //        $scope.HotOrSpecialProductObj =
    //            {
    //                startDate: data[0].startDate,
    //                endDate: data[0].endDate,
    //                priority: data[0].priority,
    //                status : data[0].status
    //            };
           
    //        $scope.NeedtoShowHot_SpeacialContainer = 1;

    //        $scope.bindHotOrSpecial(data);
    //    }).error(function (err) {
    //        alert(false, "error : " + err);
    //    });
    //};


    $scope.NeedtoShowHot_SpeacialContainerDiv=function(IsHotProduct)
    {
        $scope.HotOrSpecialProductObj = new Object();
        $scope.HotOrSpecialProductObj.IsHotProduct = IsHotProduct;
        $scope.HotOrSpecialProductObj.status = 1;
        $scope.NeedtoShowHot_SpeacialContainer = 1;
        if(IsHotProduct==1)
        {
            $scope.NeedtoShowHot_SpeacialContainerTitle = "Add as Hot Product";
        }
        else {
            $scope.NeedtoShowHot_SpeacialContainerTitle = "Add as Special Product";
        }
    }

    //#region init
    Init();
   
    //#endregion Image Tab
});