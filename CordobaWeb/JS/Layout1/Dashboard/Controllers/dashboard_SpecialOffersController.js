app.controller('Dashboard_SpecialOffersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion     

    $scope.SpecialOfferList = [
        { ProductId: 1, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      , { ProductId: 2, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      , { ProductId: 3, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      //, { ProductId: 4, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      //, { ProductId: 5, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      //, { ProductId: 6, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
      //, { ProductId: 7, ImgSrc: '/Content/layout1/images/specialOffer.jpg', Title: 'Longchamp 2.0 Crossbody Bag Black', Code: 'FPB0024', Points: '29 Points' }
    ];



    //$scope.$on('SpecialOfferBindindComplete', function (ngRepeatFinishedEvent) {

    //    alert(1);
    //    $('#specialOffer').bxSlider({
    //        pager: false,
    //        nextText: '',
    //        prevText: '',
    //        minSlides: 2,
    //        maxSlides: 1,
    //        mode: 'vertical'
    //    });
    //});


});