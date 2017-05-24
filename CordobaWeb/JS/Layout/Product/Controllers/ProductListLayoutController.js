﻿app.controller('ProductListLayoutController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion   
    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.SelectedCategoryId = parseInt($stateParams.CategoryId);
    }
    debugger;
    $scope.GetCategoryListForDashboard = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID="+$scope.StoreDetailInSession.store_id+"&NeedToGetAllSubcategory=true")
          .then(function (response) {
              if (response.data.length > 0) {
                  debugger;
                  $scope.CategoryList = response.data;
                 $scope.ParentCategoryId = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId })[0].ParentId;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetProductListByCategoryAndStoreId= function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductListByCategoryAndStoreId?StoreID="+$scope.StoreDetailInSession.store_id+"&CategoryId="+ $scope.SelectedCategoryId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ProductList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }



    $scope.GetCategoryListForDashboard();
    $scope.GetProductListByCategoryAndStoreId();


});