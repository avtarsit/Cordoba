app.controller('Dashboard_LatestProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  

    $scope.LatestProductList = [
        { product_id: 1, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 2, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 3, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 4, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 5, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 6, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 7, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 8, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 9, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 10, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 11, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
      , { product_id: 12, ImgSrc: '/Content/layout1/images/product1.jpg', product_name: 'Longchamp 2.0 Crossbody Bag Black', product_code: 'LONG - 00024', Points: '539 points' }
    ];


});