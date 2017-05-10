app.controller('ShowCatalogue', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Product Catalogues";


    $scope.GetCatalogueList = function () {
        $http.get(configurationService.basePath + "api/CatalogueApi/GetCatalogueList")
          .then(function (response) {
              if (response.data.length > 0) {
                  debugger;
                  $scope.CatalogueList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });


    }
    $scope.GetCatalogueList();



});