app.controller('ManageCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    $scope.Category_Id = 0;
    $scope.CategoryStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];

    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.PageTitle = "Update Category";
        $scope.Category_Id = $stateParams.CategoryId;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Category";
    }
    //#endregion
   
    //$scope.SaveCategory = function (form) {
    //    if (form.$valid) {
          
    //    }
    //}

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

    //Get language list

    function GetLanguageList() {

        $http({
            method: 'GET',
            url: configurationService.basePath + 'api/CategoryApi/GetLanguageList',
            headers: { 'Content-Type': 'application/json' }
        })
         .success(function (data) {

             $scope.LanguageList = data;
             $scope.language_id = $scope.LanguageList[0].language_id
            
           
         }).error(function (err) {
             alert("false");
         });

    }

    $scope.GetCategoryById = function () {
        $scope.Category_Id = ($scope.Category_Id > 0 ? $scope.Category_Id : 0);
        $http({
            method: 'GET',
            url: configurationService.basePath + 'api/CategoryApi/GetCategoryById?Category_Id=' + $scope.Category_Id ,
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
            $scope.CategoryObj = response;
            debugger;
            CreateDescriptionObject();
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    function CreateDescriptionObject() {
       
        var TempDescObject = [];
        angular.copy($scope.CategoryObj.CategoryDescriptionList, TempDescObject);
        $scope.CategoryObj.CategoryDescriptionList = [];
        angular.forEach($scope.LanguageList, function (col, i) {
            var CategoryDescObj = $filter('filter')(TempDescObject, { language_id: col.language_id }, true);
            if (CategoryDescObj == undefined || CategoryDescObj == null || CategoryDescObj.length == 0) {
                var DescObj = new Object();
                DescObj.category_id = $scope.Category_Id;
                DescObj.language_id = col.language_id;
                DescObj.description = "";
                DescObj.name = "";
                DescObj.CategoryDescription = "";
                $scope.CategoryObj.CategoryDescriptionList.push(DescObj);
            }
            else {
                $scope.CategoryObj.CategoryDescriptionList.push(CategoryDescObj[0]);
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
                    $state.go('ShowCategory');
                }
            });
        }
        else {
            $state.go('ShowCategory');
        }
    }

 

    function GetParentCategoryList() {
      
        $http.get(configurationService.basePath + "api/CategoryApi/GetParentCategoryList")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ParentCategoryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    //Insert Update Category
    $scope.InsertOrUpdateCategory = function (form) {
    
        if (form.$valid) {
            $scope.CategoryObj.StoreIdCSV = "";
            $scope.CategoryObj.StoreIdCSV = GetSelectedStoreListCSV($scope.CategoryObj.StoreList);
           // var categoryEntity = JSON.stringify($scope.CategoryObj);
            debugger;
            return;
            $http.post(configurationService.basePath + "api/CategoryApi/InsertOrUpdateCategory", categoryEntity)
              .then(function (response) {
                  if (response.data > 0) {
                      notificationFactory.customSuccess("Category Saved Successfully.");
                      $state.go('Category');
                  }
                  else if (response.data == -1) {
                      notificationFactory.customError("Category name is already Exists!");
                  }
              })
          .catch(function (response) {
              notificationFactory.error("Error occur during save record.");
          })
          .finally(function () {

          });

        }
    }

    function GetSelectedStoreListCSV(StoreObj) {
        var StoreIdCSV = "";
        var SelectedStoreList = $filter('filter')(StoreObj, { IsSelected: true }, true);
        StoreIdCSV = GetCSVFromJsonArray(SelectedStoreList, "store_id");
        return StoreIdCSV;
    }


    GetLanguageList();
    GetParentCategoryList();
    $scope.GetCategoryById(0);


});

