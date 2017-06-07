app.controller('Dashboard_BestSellerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.BestSellerList = [
         { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points:  "41 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "42 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "43 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "44 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "45 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "46 Points" }
    
    ];


});