app.controller('Dashboard_BannerController', function ($timeout,StoreSessionDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion    
    //$scope.BannerList = [
    //      { Image: "/Content/layout1/images/sliderBanner1.jpg" }
    //    , { Image: "/Content/layout1/images/sliderBanner2.jpg" }
    //    , { Image: "/Content/layout1/images/sliderBanner1.jpg" }
    //    , { Image: "/Content/layout1/images/sliderBanner2.jpg" }
    //];

    $scope.GetBanner_Layout = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetBanner_Layout?StoreID=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {
              debugger;
              if (response.data.length > 0) {
                  $scope.BannerList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetBanner_Layout();

});