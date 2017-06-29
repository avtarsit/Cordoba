﻿app.controller('ManageBannerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.EnumStatus = [
               { 'StatusId': 1, 'StatusName': 'Enabled' }
             , { 'StatusId': 2, 'StatusName': 'Disabled' }
    ];
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.IsEditMode = false;
    $scope.BannerId = $stateParams.BannerId;

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
        NewBannerImage.banner_id = 0;
        NewBannerImage.banner_image_id = 0;
        NewBannerImage.link = null;
        NewBannerImage.image = null;
        NewBannerImage.sort_order = null;
        $scope.BannerImageObj.push(NewBannerImage);
        if ($scope.BannerImageObj == null) {
            $scope.BannerImageObj = [];
        }
       
        //console.log($scope.BannerImageObj);
    }

    

    $scope.RemoveBannerImage = function (index) {
        if ($scope.BannerImageObj[index]["banner_image_id"] > 0) {
            $http.get(configurationService.basePath + "api/BannerApi/DeleteBannerImage?banner_image_id=" + $scope.BannerImageObj[index]["banner_image_id"])
             .then(function (response) {

                 $scope.BannerImageObj = response.data;
                 $scope.GetBannerImageById();
                 notificationFactory.customSuccess("Banner Image deleted Successfully.");
             })
         .catch(function (response) {
         })
         .finally(function () {

         });
        }
        else {
            $scope.BannerImageObj.splice(index, index);
        }
        

    }

    $scope.removeBanner = function ()
    {
        $http.get(configurationService.basePath + "api/BannerApi/DeleteBanner?bannerId=" + $scope.BannerId)
          .then(function (response) {
              notificationFactory.customSuccess("Banner deleted Successfully.");
              
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }


    //#endregion

    $scope.SaveBanner = function (form) {
        if (form.$valid) {
            $http.post(configurationService.basePath + "api/BannerApi/InsertUpdateBanner?banner_id=" + $scope.BannerId + "&name=" + $scope.BannerObj.name + "&status=" + $scope.BannerObj.status)
          .then(function (response) {

              $scope.BannerObj = response.data;
              $state.go('Banner')
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
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
        //debugger;
        $http.get(configurationService.basePath + "api/BannerApi/GetBannerById?bannerId=" + $scope.BannerId)
          .then(function (response) {

              $scope.BannerObj = response.data;
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    $scope.GetBannerImageById = function () {

        $http.get(configurationService.basePath + "api/BannerApi/GetBannerImageList?bannerId=" + $scope.BannerId)
        .then(function (response) {
            debugger;
            $scope.BannerImageObj = response.data;
            //console.log($scope.BannerImageObj)
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

    $scope.UploadBannerImage = function (index) {
        debugger;
        var data = new FormData();
        debugger;
        var files = $("#Image" + index).get(0).files;
        if (files.length == 0) {
            notificationFactory.customError("Please select atleast one file.");
            return notificationFactory;
        }

        var filename = files[0].name;

        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
            //console.log(data);
        }


        var ajaxRequest = $.ajax({
            type: "POST",
            url: configurationService.basePath + 'api/BannerApi/UploadBannerImage?banner_id=' + $scope.BannerObj.banner_id + '&banner_image_id=' + $scope.BannerImageObj[index]["banner_image_id"] + '&link=' + $scope.BannerImageObj[index]["link"] + '&sort_order=' + $scope.BannerImageObj[index]["sort_order"],
            contentType: false,
            processData: false,
            data: data,
            //data: {
            //    data: data,
            //    banner: $scope.BannerImageObj[index]
            //},
            success: function (response) {
                notificationFactory.customSuccess("Store Image Upload Successfully.");
                $('#ImageUpload').val('');
                $scope.GetBannerImageById();
            },
            error: function (response) {
                debugger;
                notificationFactory.error("Error occur during image upload.");
            }
        });
    }

    $scope.GetBannerById();

        $scope.GetBannerImageById();
    

});