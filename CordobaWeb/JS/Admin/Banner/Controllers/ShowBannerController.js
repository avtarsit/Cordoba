app.controller('ShowBannerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Banners";


    $scope.GetBannerList = function () {
        $http.get(configurationService.basePath + "api/BannerApi/GetBannerList/StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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
    $scope.GetBannerList();

});