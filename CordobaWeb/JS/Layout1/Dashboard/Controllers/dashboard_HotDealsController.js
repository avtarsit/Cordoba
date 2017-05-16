app.controller('Dashboard_HotDealsController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);
   
    $scope.HotDealsList = [
       { HotDealsImgSrc: "/Content/layout1/images/deals.jpg", HotDealsName: "Longchamp 2.0 Crossbody Bag Black", HotDealsCode: "FPB0024", Points: "29 Points" }
     //, { HotDealsImgSrc: "/Content/layout1/images/deals.jpg", HotDealsName: "Longchamp 2.0 Crossbody Bag Black", HotDealsCode: "FPB0024", Points: "29 Points" }
     //, { HotDealsImgSrc: "/Content/layout1/images/deals.jpg", HotDealsName: "Longchamp 2.0 Crossbody Bag Black", HotDealsCode: "FPB0024", Points: "29 Points" }        
    ];


});