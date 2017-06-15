app.controller('ManageBannerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
  
    $scope.EnumStatus = [
               { 'StatusId': 1, 'StatusName': 'Enabled' }
             , { 'StatusId': 2, 'StatusName': 'Disabled' }
    ];
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.IsEditMode = false;
    $scope.BannerId = 0;

    if ($stateParams.BannerId != undefined && $stateParams.BannerId != null) {
        $scope.PageTitle = "Update Banner";
        $scope.BannerId = $stateParams.BannerId;
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Banner";
    }
    //#endregion


    //#region Banner Image Attribute
    $scope.AddBannerImage = function () {
        var NewBannerImage = new Object();
        NewBannerImage.Id = 0;
        NewBannerImage.Title = null;
        NewBannerImage.Link = null;
        NewBannerImage.Image = null;
        NewBannerImage.SortOrder = null;
        if ($scope.BannerObj.BannerAttributeList == null) {
            $scope.BannerObj.BannerAttributeList = [];
        }
        $scope.BannerObj.BannerAttributeList.push(NewBannerImage);
    }

    $scope.RemoveBannerImage = function (event, item) {
        if (item.Id != undefined && item.Id != null && item.Id != 0) {
            RemoveBannerImage(item);
        }
        else {
            $scope.BannerObj.BannerAttributeList.pop(item);
        }
    }

    function RemoveBannerImage() {

    }

    //#endregion

    $scope.SaveBanner = function (form) {
        if (form.$valid) {
       
        }
    }

    $scope.DeleteBanner = function () {
        bootbox.dialog({
            message: "Do you want remove Banner?",
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


    $scope.GetBannerById = function () {
        $http.get(configurationService.basePath + "api/BannerApi/GetBannerById?BannerId=" + $scope.BannerId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
          
              $scope.BannerObj = response.data;
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
                    $state.go('Banner');
                }
            });
        }
        else {
            $state.go('Banner');
        }
    }

    $scope.GetBannerById();

});