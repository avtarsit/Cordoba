﻿app.controller('OrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.PageTitle = "Orders";

    $scope.OrderDetails =
       {
           OrderId: '#41309',
           StoreName: 'Grenke Rewards',
           StoreUrl: 'http://www.grenkerewards.co.uk/',
           Customer: 'ANDREW DOVER',
           Email: 'ANDREW.DOVER@VIRGIN.NET',
           Telephone: '07967803738',
           Fax: '07967803738',
           Total: '3,321points',
           OrderStatus: 'Processing',
           Comment: 'largest denomination amazon possible please',
           IPAddress: '82.19.10.36',
           UserAgent: 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36',
           AcceptLanguage: 'en-US,en;q=0.8',
           DateAdded: '12/05/2017',
           DateModified: '12/05/2017'
       };

    $scope.PaymentDetail =
      {
          FirstName: 'andrew',
          LastName: 'Dover',
          Address1: '40 wilstrode avenue',
          Address2: 'binfield',
          City: 'bracknell',
          Postcode: 'rg42 4uw',
          RegionState:'',
          Country: 'United Kingdom',
          PaymentMethod: 'Point Based Deduction'
          
      }
    ;


    $scope.Shippingdetail =
      {
          FirstName: 'andrew',
          LastName: 'Dover',
          Company:'holly digital c/o maine installa',
          Address1: 'unit 2 the furze',
          Address2: 'dunt avenue',
          City: 'hurst',
          Postcode: 'rg10 0sy',
          RegionState: 'Berkshire',
          RegionStateCode: 'BERKS',
          Country: 'United Kingdom',
          ShippingMethod: 'Free Shipping'

      }
    ;

    $scope.Products = [{
        Product: 'Sonos Playbase White',
        Model: 'SONOS-00004',
        Quantity: 1,
        UnitPrice: '1,321points',
        Total: '1,321points'
    },
    {
        Product: 'Amazon £50 Gift Code',
        Model: 'AMDD0050',
        Quantity: 20,
        UnitPrice: '100points',
        Total: '2,000points'
    },
     {
         Product: '',
         Model: '',
         Quantity: '',
         UnitPrice: 'Sub Total:',
         Total: '3,321points'
     },
      {
          Product: '',
          Model: '',
          Quantity: '',
          UnitPrice: 'Total:',
          Total: '3,321points'
      }
    ];

    $scope.OrderStatus = [
        { Id: 1, Name: 'Cancelled' },
        { Id: 2, Name: 'PartiallyShipped' },
        { Id: 3, Name: 'Processing' },
        { Id: 4, Name: 'Returned' },
        { Id: 5, Name: 'Shipped' }
    ];

    $scope.OrderHistory =
        {
            SelectedOrderStatus: 1,
            NotifyCustomer: true,
            Comment: ''
        };
});