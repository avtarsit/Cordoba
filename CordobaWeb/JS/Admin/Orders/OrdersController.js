﻿app.controller('OrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.PageTitle = "Orders";
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.OrderStatusNotify = 0;
    $scope.OrderStatusComment = "";
    //$scope.OrderDetails =
    //   {
    //       OrderId: '#41309',
    //       StoreName: 'Grenke Rewards',
    //       StoreUrl: 'http://www.grenkerewards.co.uk/',
    //       Customer: 'ANDREW DOVER',
    //       Email: 'ANDREW.DOVER@VIRGIN.NET',
    //       Telephone: '07967803738',
    //       Fax: '07967803738',
    //       Total: '3,321points',
    //       OrderStatus: 'Processing',
    //       Comment: 'largest denomination amazon possible please',
    //       IPAddress: '82.19.10.36',
    //       UserAgent: 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36',
    //       AcceptLanguage: 'en-US,en;q=0.8',
    //       DateAdded: '12/05/2017',
    //       DateModified: '12/05/2017'
    //   };

    //$scope.PaymentDetail =
    //  {
    //      FirstName: 'andrew',
    //      LastName: 'Dover',
    //      Address1: '40 wilstrode avenue',
    //      Address2: 'binfield',
    //      City: 'bracknell',
    //      Postcode: 'rg42 4uw',
    //      RegionState:'',
    //      Country: 'United Kingdom',
    //      PaymentMethod: 'Point Based Deduction'

    //  }
    //;


    //$scope.Shippingdetail =
    //  {
    //      FirstName: 'andrew',
    //      LastName: 'Dover',
    //      Company:'holly digital c/o maine installa',
    //      Address1: 'unit 2 the furze',
    //      Address2: 'dunt avenue',
    //      City: 'hurst',
    //      Postcode: 'rg10 0sy',
    //      RegionState: 'Berkshire',
    //      RegionStateCode: 'BERKS',
    //      Country: 'United Kingdom',
    //      ShippingMethod: 'Free Shipping'

    //  }
    //;

    //$scope.Products = [{
    //    Product: 'Sonos Playbase White',
    //    Model: 'SONOS-00004',
    //    Quantity: 1,
    //    UnitPrice: '1,321points',
    //    Total: '1,321points'
    //},
    //{
    //    Product: 'Amazon £50 Gift Code',
    //    Model: 'AMDD0050',
    //    Quantity: 20,
    //    UnitPrice: '100points',
    //    Total: '2,000points'
    //},
    // {
    //     Product: '',
    //     Model: '',
    //     Quantity: '',
    //     UnitPrice: 'Sub Total:',
    //     Total: '3,321points'
    // },
    //  {
    //      Product: '',
    //      Model: '',
    //      Quantity: '',
    //      UnitPrice: 'Total:',
    //      Total: '3,321points'
    //  }
    //];

    $scope.OrderStatus = [
        { Id: 1, Name: 'Processing' },
        { Id: 2, Name: 'Shipped' },
        { Id: 3, Name: 'PartiallyShipped' },
        { Id: 4, Name: 'Returned' },
        { Id: 5, Name: 'Cancelled' }
    ];

    $scope.OrderHistory = {
        order_id: 0,
        order_status_id: 0,
        notify: 0,
        comment: ''
    }
    $scope.OrderHistory.order_status_id = 1;

    //$scope.OrderHistory =
    //    {
    //        SelectedOrderStatus: 1,
    //        NotifyCustomer: true,
    //        Comment: ''
    //    };

    $scope.GetOrderDetails = function () {

        $http.get(configurationService.basePath + "api/OrderApi/GetOrderDetails?orderId=" + $stateParams.OrderId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.OrderDetails = response.data[0];
                  $scope.OrderHistoryList = $scope.OrderDetails.orderHistoryEntity;
                  $scope.Products = $scope.OrderDetails.orderProductEntity;
                  //$scope.MainTotal = $scope.Products[0].title;
                  $scope.total_title = $scope.Products[0].total_title;
                  $scope.total_value = $scope.Products[0].total_value;
                  $scope.subtotal_title = $scope.Products[0].subtotal_title;
                  $scope.subtotal_value = $scope.Products[0].subtotal_value;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetOrderDetails();



    $scope.InsertOrderHistory = function (OrderHistory) {
        var objHistoryEntity = {
            order_id: $scope.OrderDetails.order_id,
            order_status_id: OrderHistory.order_status_id,
            notify: (OrderHistory.notify == true ? 1 : 0),
            comment: OrderHistory.comment
        }
        //return false;
        $http.post(configurationService.basePath + "api/OrderApi/InsertOrderHistory?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, objHistoryEntity)
           .then(function (response) {

               if (response.data > 0) {
                   //alert('already exists');
                   $scope.GetOrderDetails();
               }
           })
            .catch(function (response) {

            })
             .finally(function () {

             });
    }

    $scope.UpdateOrderStatus = function () {
        $http.post(configurationService.basePath + "api/OrderApi/UpdateOrderStatus?OrderId=" + $scope.OrderDetails.order_id + '&OrderStatusId=' + $scope.OrderDetails.order_status_id + "&Comment=" + $scope.OrderStatusComment)
       .then(function (response) {

           if (response.data > 0) {
               $scope.GetOrderDetails();
               $scope.OrderStatusComment = "";
           }
       })
        .catch(function (response) {

        })
         .finally(function () {

         });
    }

    function GetOrderStatus() {
        $http.get(configurationService.basePath + 'api/ProductPurchasedReportApi/GetOrderStatus?store_id=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId + '&language_id=1')
       .then(function (response) {
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;
               var DefaultOption = new Object();
               DefaultOption.order_status_id = 0;
               DefaultOption.name = "All Status";
               $scope.OrderStatusList.push(DefaultOption);
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }

    GetOrderStatus();


});