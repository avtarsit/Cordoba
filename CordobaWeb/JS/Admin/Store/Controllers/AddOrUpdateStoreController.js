app.controller('AddOrUpdateStoreController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreObj = new Object();

    $scope.IsEditMode = false;
    $scope.store_id = 0;
    if ($stateParams.StoreID != undefined && $stateParams.StoreID != null) {
        $scope.PageTitle = "Update Store";
        $scope.IsEditMode = true;
        $scope.store_id = $stateParams.StoreID;
    }
    else {
        $scope.PageTitle = "Add Store";
    }
    //#endregion    
    

    $scope.TemplateList = [
                        { 'TemplateId': 1, 'TemplateName':'Default Theme' }
                      , { 'TemplateId': 2, 'TemplateName':'Default'}
                      , { 'TemplateId': 3, 'TemplateName':'Default-2'}
                    ];
  
    $scope.LayoutList = [
                    { 'LayoutId': 1, 'LayoutName': 'Account' }
                  , { 'LayoutId': 2, 'LayoutName': 'Affiliate' }
                  , { 'LayoutId': 3, 'LayoutName': 'Categories' }
                  , { 'LayoutId': 4, 'LayoutName': 'Checkout' }
                  , { 'LayoutId': 5, 'LayoutName': 'Compare' }
                  , { 'LayoutId': 6, 'LayoutName': 'Contact' }
                  , { 'LayoutId': 7, 'LayoutName': 'Default' }
                  , { 'LayoutId': 8, 'LayoutName': 'default2 - Layout Block Demo' }
                  , { 'LayoutId': 9, 'LayoutName': 'Home' }
                  , { 'LayoutId': 10, 'LayoutName': 'Information' }
                  
    ];
   
    function GetCountryList() {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?countryId=0")
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
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=0")
        .then(function (response) {
            $scope.LanguageList = response.data;
        })
       .catch(function (response) {

       })
       .finally(function () {

       });
    }
    function GetCurrencyList() {
        $http.get(configurationService.basePath + "api/CurrencyApi/GetCurrencyList")
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
   
    $scope.GetStoreById = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreById?store_id=" + $scope.store_id)
          .then(function (response) {
              $scope.StoreObj = response.data;
              debugger;
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
                    $state.go('ShowStore');
                }

            });
        }
        else {
            $state.go('ShowStore');
        }

    }

    function Init() {
        GetCountryList();
        GetLanguageList();
        GetCurrencyList();

        $scope.GetStoreById();
    }



    Init();
});