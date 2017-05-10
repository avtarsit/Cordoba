app.controller('ShowLanguageController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.LanguageList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);



    $scope.PageTitle = "Show Languages";

    //$scope.LanguageList = [
    // {LanguageName:'Deutsch',LanguageCd:'de',SortOrder:1}
    //,{LanguageName:'English (Default)',LanguageCd:'en',SortOrder:2}
    //,{LanguageName:'Español',LanguageCd:'en',SortOrder:3}
    //,{LanguageName:'Français',LanguageCd:'es',SortOrder:4}
    //,{LanguageName:'Italiano',LanguageCd:'fr',SortOrder:5}
    //];



    $scope.GetLanguageList = function () {
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

    $scope.GetLanguageList();
});