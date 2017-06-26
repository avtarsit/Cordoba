app.controller('Dashboard_BestSellerController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);


    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.BestSellerList = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetBestSellerListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id )
          .then(function (response) {
              if (response.data.length > 0) {               
                  $scope.BestSellerList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.BestSellerList();


    //$scope.BestSellerList = [
    //     { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points:  "41 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "42 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "43 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "44 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "45 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "46 Points" }
    
    //];


});