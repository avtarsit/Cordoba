app.controller('OrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.PageTitle = "Orders";
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.OrderStatusNotify = 0;
    $scope.OrderStatusComment = "";  

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
                  $scope.OrderHistory.order_status_id = $scope.OrderDetails.order_status_id;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
  

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
                   toastr.success("Order status updated successfully.");
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

    $scope.GetOrderDetails();
    GetOrderStatus();


});