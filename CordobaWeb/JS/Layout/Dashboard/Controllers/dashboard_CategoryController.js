app.controller('Dashboard_CategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion     
    $scope.GetCategoryListForDashboard = function () { 
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID=0&NeedToGetAllSubcategory=false")
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

    $scope.GetCategoryListForDashboard();

});