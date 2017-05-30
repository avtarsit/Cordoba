﻿app.controller('CategoryListController', function (StoreSessionDetail,UserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.SelectedCategoryId = 0;
    $scope.SelectedSubCategory = 0;
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.TitleHeader = '';
    $scope.WhatAreYouLookingFor = '';
    //#endregion   
    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId!=null)
    {
        $scope.SelectedCategoryId = parseInt($stateParams.CategoryId);
        
    }
 
    $scope.GetCategoryListForDashboard = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID="+$scope.StoreDetailInSession.store_id+"&NeedToGetAllSubcategory=true")
          .then(function (response) {
              if (response.data.length > 0) {              
                  $scope.CategoryList = response.data;
                  var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId });
                  if (CategoryObj != undefined && CategoryObj != null) {
                      $scope.SelectedCategory = CategoryObj[0];
                      
                  }
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCategory=function(ParentCategoryId)
    {
        $scope.SelectedCategoryId = ParentCategoryId;
        $scope.SelectedSubCategory = 0;
        var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId });
        if (CategoryObj != undefined && CategoryObj != null) {
            $scope.SelectedCategory = CategoryObj[0];
            $scope.TitleHeader = $scope.SelectedCategory.name;
        }
    }

    $scope.GetSubCategory=function(SubCategoryId)
    {
        $scope.SelectedSubCategory = SubCategoryId;
        var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedSubCategory });
        if (CategoryObj != undefined && CategoryObj != null) {
            $scope.SelectedCategory = CategoryObj[0];
            $scope.TitleHeader = $scope.SelectedCategory.name;
        }
        $scope.GetProductListByCategoryAndStoreId();
    }


    $scope.GetProductListByCategoryAndStoreId = function () {
        
        $http.get(configurationService.basePath + "API/ProductApi/GetProductListByCategoryAndStoreId?StoreID="
                            + $scope.StoreDetailInSession.store_id +
                            "&CategoryId=" + $scope.SelectedSubCategory + 
                            "&Customer_Id=" + UserDetail.address_id +
                            "&WhatAreYouLookingFor=" + $scope.WhatAreYouLookingFor
                            )
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


    $scope.GetOurProductListByByStoreId = function () {
        $scope.GetProductListByCategoryAndStoreId();
    }


    $scope.GetCategoryListForDashboard();


    if ($scope.SelectedCategoryId == -1) {
        $scope.SelectedSubCategory = -1;
        $scope.TitleHeader =  'Our Products';
        $scope.GetOurProductListByByStoreId();
    }
    else if ($scope.SelectedCategoryId == -2) {
        $scope.SelectedSubCategory = -2;
        $scope.TitleHeader =  'My WishList';
        $scope.GetOurProductListByByStoreId();
    }
    else if ($scope.SelectedCategoryId == -3) {
        $scope.SelectedSubCategory = -3;
        $scope.TitleHeader = 'Search Result';
        $scope.WhatAreYouLookingFor = $("#txtSearchFor").val();
        $scope.GetOurProductListByByStoreId();
    }

});