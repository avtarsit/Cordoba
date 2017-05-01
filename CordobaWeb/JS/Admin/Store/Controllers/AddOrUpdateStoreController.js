app.controller('AddOrUpdateStoreController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreObj = new Object();
    $scope.IsEditMode = false;
    if ($stateParams.StoreID != undefined && $stateParams.StoreID != null) {
        $scope.PageTitle = "Update Store";
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Store";
    }
    //#endregion    
    /// Get Template List-- START---

    $scope.TemplateList = [
                        { 'TemplateId': 1, 'TemplateName':'Default Theme' }
                      , { 'TemplateId': 2, 'TemplateName':'Default'}
                      , { 'TemplateId': 3, 'TemplateName':'Default-2'}
                    ];

    /////-----END
    $scope.StoreObj.TemplateId = 1; // Temporary for Selected Default Theme


    //--- Get Layout List -- START
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

    /////-----END
    $scope.StoreObj.LayoutId = 1; // Temporary for Selected Default Theme


    //-----END


    ///Get Country List --- START
    $scope.GetCountryList = function () {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?CountryCd=''")
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

    $scope.GetCountryList();
    $scope.StoreObj.CountryCd = ' IN';
    ///END
 
    ///Get Country List --- START
    //$scope.GetStateList = function () {
    //    $http.get(configurationService.basePath + "api/StateApi/GetStateList?CountryCd=''")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.StateList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}

    $scope.StateList = [
                { 'StateId': 1, 'StateName': 'Andaman and Nicobar Islands' }
              , { 'StateId': 2, 'StateName': 'Andhra Pradesh' }
              , { 'StateId': 3, 'StateName': 'Arunachal Pradesh' }
              , { 'StateId': 4, 'StateName': 'Assam' }
              , { 'StateId': 5, 'StateName': 'Bihar' }
              , { 'StateId': 6, 'StateName': 'Chandigarh' }
              , { 'StateId': 7, 'StateName': 'Dadra and Nagar Haveli' }
              , { 'StateId': 8, 'StateName': 'Daman and Diu' }
              , { 'StateId': 9, 'StateName': 'Delhi' }
              , { 'StateId': 10, 'StateName': 'Goa' }

    ];    
    ///END


    ///-- Get Language List ----START

    $scope.LanguageList = [
        { 'LanguageCd': 'en', 'LanguageName': 'English' }
      , { 'LanguageCd': 'de', 'LanguageName': 'Deutsch' }
      , { 'LanguageCd': 'fr', 'LanguageName': 'Français' }
      , { 'LanguageCd': 'it', 'LanguageName': 'Italiano' }
      , { 'LanguageCd': 'es', 'LanguageName': 'Español' }
      , { 'LanguageCd': 'pt', 'LanguageName': 'Português' }
      , { 'LanguageCd': 'nl', 'LanguageName': 'Nederlands' }
    ];

    $scope.StoreObj.LanguageCd='en';
    ///END


    ///-- Get Currency List ----START

    $scope.CurrencyList = [
        { 'CurrencyCd': 'P41', 'CurrencyName': 'Points (1=41p)' }
      , { 'CurrencyCd': 'P50', 'CurrencyName': 'Points (1=50p)' }
      , { 'CurrencyCd': 'GBP', 'CurrencyName': 'Pound Sterling' }
    ];

    $scope.StoreObj.CurrencyCd = 'P50';
    ///END

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

});