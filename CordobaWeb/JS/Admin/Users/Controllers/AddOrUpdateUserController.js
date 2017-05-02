app.controller('AddOrUpdateUserController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.UserID = 0;
    $scope.IsEditMode = false;
    if ($stateParams.UserID != undefined && $stateParams.UserID != null) {
        $scope.PageTitle = "Update User";
        $scope.IsEditMode = true;
        $scope.UserID = $stateParams.UserID;
    }
    else {
        $scope.PageTitle = "Add User";
    }
    //#endregion


    //Get Status List -- START
    $scope.UserGroupList = [
        { 'UserGroupId': 1, 'UserGroupName': 'Administrator' }
      , { 'UserGroupId': 2, 'UserGroupName': 'ClientAdmin' }
      , { 'UserGroupId': 3, 'UserGroupName': 'Demonstration' }
    ];
    //END

    //Get Status List -- START
    $scope.UserStatus = [
        { 'StatusCd': 'A', 'StatusName': 'Active' }
      , { 'StatusCd': 'IN', 'StatusName': 'Inactive' }
    ];
    //END
    $scope.GetUserDetail = function () {
        $http.get(configurationService.basePath + "api/UserApi/GetUserDetail?UserID=" + $scope.UserID)
          .then(function (response) {
              $scope.UserObj = response.data;
              $scope.UserObj.StatusCd = 'A';  // Temporary
              $scope.UserObj.UserGroupId = 1;  // Temporary
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.SaveUser = function (form) {
        if (form.$valid) {

        }
    }

    $scope.DeleteUser = function () {
        bootbox.dialog({
            message: "Do you want remove User?",
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
                    $state.go('Users');
                }

            });
        }
        else {
            $state.go('Users');
        }

    }
  


    $scope.GetUserDetail();

});