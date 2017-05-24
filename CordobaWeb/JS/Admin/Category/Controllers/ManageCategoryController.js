app.controller('ManageCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    $scope.Category_Id = 0;
    if ($stateParams.Category_Id != undefined && $stateParams.language_id != undefined && $stateParams.Category_Id != null && $stateParams.language_id != null) {
        $scope.PageTitle = "Update Category";
        $scope.Category_Id = $stateParams.Category_Id;
        $scope.language_id = $stateParams.language_id;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Category";
    }
    //#endregion


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


    $scope.GetCategoryById = function () {
        $http.get(configurationService.basePath + "api/CategoryApi//GetCategoryById?Category_Id=" + $scope.Category_Id + "language_id=" + $scope.language_id)
          .then(function (response) {
          
              $scope.CategoryObj = response.data;
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

    $scope.GetCategoryById();



    //Get language list

    function GetLanguageList() {

        $http({
            method: 'GET',
            url: '/GetLanguageList?OrderId=' + $scope.language_id,
            headers: { 'Content-Type': 'application/json' }
        })
         .success(function (data) {


             $scope.orderProductDetail = data.Data;

         }).error(function (err) {
             showNotification(false, err);
         });

    }



});

