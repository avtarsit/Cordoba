app.controller('ShowCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CategoryList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Categories";


    $scope.GetCategoryList = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryList?CategoryId=0")
          .then(function (response) {
          
              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    $scope.GetCategoryList();



});