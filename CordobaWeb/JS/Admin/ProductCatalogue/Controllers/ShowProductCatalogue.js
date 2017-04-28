app.controller('ShowProductCatalogue', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.ProductCatalogueList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Product Catalogues";


    $scope.GetProductCatalogueList = function () {
        $http.get(configurationService.basePath + "api/ProductCatalogueApi/GetProductCatalogueList")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ProductCatalogueList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });


    }
    $scope.GetProductCatalogueList();



});