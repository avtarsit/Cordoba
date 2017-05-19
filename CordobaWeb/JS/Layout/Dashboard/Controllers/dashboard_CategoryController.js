app.controller('Dashboard_CategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
  
    //$scope.CategoryList = [
    //      { category_id: 1, category_name: "Apple Products" }
    //    , { category_id: 2, category_name: "Baby" }
    //    , { category_id: 3, category_name: "Beauty & Fragrance" }
    //    , { category_id: 4, category_name: "Books" }
    //    , { category_id: 5, category_name: "Computing" }
    //    , { category_id: 6, category_name: "Cooking & Dining" }
    //    , { category_id: 7, category_name: "Experiences & Vouchers" }
    //    , { category_id: 8, category_name: "Gifts Food & Alcohol" }
    //    , { category_id: 9, category_name: "Home & Garden" }
    //    , { category_id: 10, category_name: "Home Appliances" }
    //    , { category_id: 11, category_name: "Pets" }
    //    , { category_id: 12, category_name: "Seasonal Events" }
    //    , { category_id: 13, category_name: "Smart Tech & Phones" }
    //    , { category_id: 14, category_name: "Apple Products" }
    //    , { category_id: 15, category_name: "Baby" }
    //    , { category_id: 16, category_name: "Beauty & Fragrance" }
    //    , { category_id: 17, category_name: "Books" }
    //    , { category_id: 18, category_name: "Cooking & Dining" }
    //    , { category_id: 19, category_name: "Experiences & Vouchers" }
    //];


    $scope.GetCategoryListForDashboard = function () {
        debugger;
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