app.controller('Dashboard_PopularCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);


    $scope.PopularCategoryList = [
        { category_id: 1, ImgSrc: '/Content/layout1/images/electronics.png', category_name: 'Electronics', AltTitle: 'Electronics' }
       , { category_id: 2, ImgSrc: '/Content/layout1/images/fashion.png', category_name: 'Fashion', AltTitle: 'Fashion' }
       , { category_id: 3, ImgSrc: '/Content/layout1/images/personalCare.png', category_name: 'Personal Care', AltTitle: 'Personal Care' }
      , { category_id: 4, ImgSrc: '/Content/layout1/images/homeApp.png', category_name: 'Home Appliances', AltTitle: 'Home Appliances' }
    ];

});