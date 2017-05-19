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
         { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points:  "40 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "40 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "40 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "40 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "40 Points" }
        , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "40 Points" }
    
    ];


});