app.controller('Dashboard_BannerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion    
    $scope.BannerList = [
          { ImgSrc: "/Content/layout1/images/sliderBanner1.jpg" }
        , { ImgSrc: "/Content/layout1/images/sliderBanner2.jpg" }
        , { ImgSrc: "/Content/layout1/images/sliderBanner1.jpg" }
        , { ImgSrc: "/Content/layout1/images/sliderBanner2.jpg" }    
    ];


});