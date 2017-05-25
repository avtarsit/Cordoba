app.controller('CategoryListController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.SelectedCategoryId = 0;
    $scope.SelectedSubCategory = 0;
    $scope.StoreDetailInSession = StoreSessionDetail;
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
        }
    }

    $scope.GetSubCategory=function(SubCategoryId)
    {
        $scope.SelectedSubCategory = SubCategoryId;
        var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedSubCategory });
        if (CategoryObj != undefined && CategoryObj != null) {
            $scope.SelectedCategory = CategoryObj[0];
        }
        $scope.GetProductListByCategoryAndStoreId();
    }


    $scope.GetProductListByCategoryAndStoreId = function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductListByCategoryAndStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&CategoryId=" + $scope.SelectedSubCategory)
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


});