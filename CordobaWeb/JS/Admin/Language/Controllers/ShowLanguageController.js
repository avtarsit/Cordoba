app.controller('ShowLanguageController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval,DTColumnDefBuilder,DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.languageId = 0;
    $scope.LanguageList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.dtColumnDefs = [
       DTColumnDefBuilder.newColumnDef(3).notSortable()
    ];

    $scope.PageTitle = "Show Languages";

    $scope.GetLanguageList = function () {
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=" + $scope.languageId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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