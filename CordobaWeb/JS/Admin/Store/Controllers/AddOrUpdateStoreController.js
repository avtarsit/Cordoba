app.controller('AddOrUpdateStoreController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab(); 
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.store_id = 0;
    $scope.StoreObj = new Object();
    $scope.StoreObj.template = '0';
    $scope.IsEditMode = false;
    if ($stateParams.StoreID != undefined && $stateParams.StoreID != null && $stateParams.StoreID != 0) {
        $scope.PageTitle = "Update Store";
        $scope.IsEditMode = true;
        $scope.store_id = parseInt($stateParams.StoreID);
    }
    else {
        $scope.PageTitle = "Add Store";
    }
    //#endregion    
    $scope.TemplateList = [
                        { 'TemplateId':'0', 'TemplateName': 'Theme1' }
                      , { 'TemplateId':'1', 'TemplateName': 'Theme2' }
    ];

    function GetCountryList() {        
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?storeId="+ $scope.store_id+"&LoggedInUserId=" + $scope.LoggedInUserId + "&countryId=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CountryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    function GetLanguageList() {
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=0&StoreID="+$scope.store_id+"&LoggedInUserId=" + $scope.LoggedInUserId)
        .then(function (response) {
            $scope.LanguageList = response.data;
        })
       .catch(function (response) {

       })
       .finally(function () {

       });
    }
    function GetCurrencyList() {
        $http.get(configurationService.basePath + "api/CurrencyApi/GetCurrencyList?StoreID="+$scope.store_id+"&LoggedInUserId=" + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CurrencyList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetZoneListByCountry = function (countryId) {          
        countryId = countryId == null ? 0 : countryId;
        $http.get(configurationService.basePath + "api/OrderApi/GetZoneListByCountry?countryId=" + countryId + "&StoreID="+$scope.store_id+"&LoggedInUserId=" + $scope.LoggedInUserId)
        .then(function (response) {
            $scope.RegionStateList = [];
            if (response.data.length > 0) {
                $scope.RegionStateList = response.data;
            }
            else {
                $scope.RegionStateList = [];
            }
        })
       .catch(function (response) {
           $scope.RegionStateList = [];
       })
       .finally(function () {

       });
    }

    $scope.GetStoreById = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreById?store_id=" + $scope.store_id + "&LoggedInUserId=" + $scope.LoggedInUserId)
          .then(function (response) {
              $scope.StoreObj = response.data;
              if ($scope.StoreObj.store_id == 0) {
                  $scope.StoreObj.country_id = 222;
                  $scope.StoreObj.language = 'en';
                  $scope.StoreObj.currency = 'P82';
                  $scope.StoreObj.template = '0';
              }
              $scope.GetZoneListByCountry($scope.StoreObj.country_id);

          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.DeleteStore = function () {
        bootbox.dialog({
            message: "Do you want remove store?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {

                                $http.get(configurationService.basePath + "api/StoreApi/DeleteStoreById_Admin?store_id=" + $scope.store_id + "&LoggedInUserId=" + $scope.LoggedInUserId)
                                             .then(function (response) {                                  
                                                 if(response.data>0)
                                                 {
                                                     $http.get(configurationService.basePath + "api/StoreApi/DeleteStoreById_Admin?store_id=" + $scope.store_id)
                                                                  .then(function (response) {
                                                                      if (response.data > 0) {
                                                                          notificationFactory.customSuccess("Store Deleted Successfully.");
                                                                          $state.go('ShowStore');
                                                                      }
                                                                  })
                                                          .catch(function (response) {

                                                          })
                                                          .finally(function () {

                                                          });
                                                 }
                                             })
                            }
                
                        }
                    }
                
                    }
            })
        }


    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowStore');
                }
            });
        }
        else {
            $state.go('ShowStore');
        }

    }

    $scope.InsertUpdateStore = function (form) {
        if (form.$valid) {
            var StoreEntity = JSON.stringify($scope.StoreObj);
            $http.post(configurationService.basePath + "api/StoreApi/InsertUpdateStore?LoggedInUserId=" + $scope.LoggedInUserId, StoreEntity)
              .then(function (response) {           
                  if (response.data > 0) {
                      notificationFactory.customSuccess("Store Saved Successfully.");
                      
                      if ($scope.store_id > 0) {
                           $state.go('ShowStore');
                      }
                      else {
                          $scope.store_id = parseInt(response.data);
                          $state.go('.', { StoreID: Encodestring(response.data) }, { notify: false, reload: false, location: 'replace', inherit: false });
                          window.location.reload(true);
                      }
                  }
              })
          .catch(function (response) {
              notificationFactory.customError("Error occur during save record.");
          })
          .finally(function () {
          });
        }
    }


    $scope.UploadImage = function (store_id, imageKey, uploadId, TemplateId) {
        var data = new FormData();
        var files = $('#'+uploadId).get(0).files;
        if (files.length == 0) {
            notificationFactory.customError("Please select atleast one file.");
            return notificationFactory;
        }
        var filename = files[0].name;
        //var extention = filename.substr(filename.lastIndexOf(".") + 1).toLowerCase();
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
        }    
        var ajaxRequest = $.ajax({
            type: "POST",
            url: configurationService.basePath + 'api/StoreApi/UploadStoreImage?Store_Id=' + store_id + "&ImageKey=" + imageKey + "&layout=" + TemplateId,
            contentType: false,
            processData: false,
            data: data,
            success: function (response) {                
                notificationFactory.customSuccess("Store Image Upload Successfully.");
                $('#' + uploadId).val('');
                $scope.GetAdvertisementImageList();
            },
            error: function (response) {
                notificationFactory.error("Error occur during image upload.");
            }
        });

    }

    $scope.uploadStoreLogo = function () {
        var data = new FormData();
        var files = $("#Image").get(0).files;
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
            url: configurationService.basePath + 'api/StoreApi/UploadStoreLogo?store_id=' + $scope.store_id + '&store_name=' + $scope.StoreObj.name,
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
                $scope.GetStoreById();
            },
            error: function (response) {
                notificationFactory.error("Error occur during image upload.");
            }
        });
    }

    $scope.GetBannerList = function () {
        $http.get(configurationService.basePath + "api/BannerApi/GetBannerList")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.BannerList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetAdvertisementImageList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetAdvertisementImageList?store_id=" + $scope.store_id )
          .then(function (response) {         
              if (response.data.length > 0) {
                  $scope.StoreAdvertisementImageList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function init() {
        GetCountryList();
        GetLanguageList();
        GetCurrencyList();
        $scope.GetStoreById();
        $scope.GetBannerList();
        $scope.GetAdvertisementImageList();
    }


    init();
});