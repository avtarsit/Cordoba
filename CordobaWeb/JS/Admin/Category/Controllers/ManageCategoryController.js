app.controller('ManageCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    //$scope.Category_Id = 0;

    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.PageTitle = "Update Category";
        $scope.Category_Id = $stateParams.CategoryId;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Category";
    }
    //#endregion
    GetLanguageList();

    $scope.SaveCategory = function (form) {
        if (form.$valid) {
          
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

    //Get language list

    function GetLanguageList() {

        $http({
            method: 'GET',
            url: configurationService.basePath + 'api/CategoryApi/GetLanguageList',
            headers: { 'Content-Type': 'application/json' }
        })
         .success(function (data) {

             $scope.languageList = data;
             $scope.language_id = $scope.languageList[0].language_id
             $scope.GetCategoryById($scope.language_id);
         }).error(function (err) {
             alert("false");
         });

    }

    $scope.GetCategoryById = function () {
        debugger;
        $http({
            method: 'GET',
            url: configurationService.basePath + 'api/CategoryApi/GetCategoryById?Category_Id=' + $scope.Category_Id + '&language_id=' + $scope.language_id,
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
            debugger;
            $scope.CategoryObj = {};
            $scope.CategoryObj.Category_Id = response.Category_Id;
            $scope.CategoryObj.CategoryName = response.CategoryName;
            $scope.CategoryObj.Description = response.description;
            //$scope.CategoryObj.Category_Id = response.parent;
            //$scope.CategoryObj.Category_Id = response.Category_Id;
            //$scope.CategoryObj.Category_Id = response.Category_Id;
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
                    $state.go('ShowCategory');
                }
            });
        }
        else {
            $state.go('ShowCategory');
        }
    }

    //$scope.GetCategoryById();



});

