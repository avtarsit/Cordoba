﻿app.controller('CartDetailController', function (StoreSessionDetail, UserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.cartgroup_id = 0;
    if ($stateParams.cartgroup_id != undefined && $stateParams.cartgroup_id != null)
    {
        $scope.cartgroup_id =parseInt($stateParams.cartgroup_id);
    }
    
    $scope.PlaceOrderObj = new Object();
    //#endregion      
    $scope.GetCartDetailsByCartGroupId = function () {     
        $http.get(configurationService.basePath + "API/CartApi/GetCartDetailsByCartGroupId?StoreID=" + $scope.StoreDetailInSession.store_id + "&cartgroup_id=" + $scope.cartgroup_id)
          .then(function (response) {              
              if (response.data.length > 0) {                 
                  $scope.CartItemList = response.data;
                  $scope.TotalItems = $scope.CartItemList.length;
                  $scope.AllItemSubtotal = $scope.CartItemList[0].AllItemSubtotal;
                  $scope.AllItemTotal = $scope.CartItemList[0].AllItemTotal;                                
                  UserDetail.cartgroup_id = response.data[0].cartgroup_id;
                  UserDetail.TotalItemAdded = response.data[0].TotalItemAdded;
                  $rootScope.CustomerDetail = UserDetail;
                  localStorageService.set("loggedInUser", UserDetail);
              }
              else {
                  $scope.CartItemList = response.data;
                  $scope.TotalItems = $scope.CartItemList.length;
                  $scope.AllItemSubtotal = 0;
                  $scope.AllItemTotal = 0;                          
                  UserDetail.cartgroup_id = 0;
                  UserDetail.TotalItemAdded = 0;
                  $rootScope.CustomerDetail = UserDetail;
                  localStorageService.set("loggedInUser", UserDetail);
              }

              if (UserDetail.customer_id>0)
              {
                  $scope.GetCustmoreAddressList();
              }
              
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCustmoreAddressList=function()
    {
        $http.get(configurationService.basePath + "API/CartApi/GetCustmoreAddressList?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id)
        .then(function (response) {
            if (response.data.length>0)
            {
                $scope.CustomerAddressList = response.data;
                $scope.SelectedCustomerAddress = $scope.CustomerAddressList[0];
                $scope.SelectedCustomerAddress.SelectedIndex = 0;
            }
            else {

            }
       
        })
    .catch(function (response) {

    })
    .finally(function () {

    });
    }


    $scope.AddOrRemoveItemFromCart=function(productObj,Quantity)
    {       
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id + "&product_id=" + productObj.product_id + "&qty=" + Quantity + "&cartgroup_id=" + productObj.cartgroup_id)
        .then(function (response) {              
            productObj.quantity = productObj.quantity + Quantity;
            $scope.GetCartDetailsByCartGroupId();
            toastr.success("Shopping bag updated successfully.");
        })
    .catch(function (response) {

    })
    .finally(function () {

    });

    }

    $scope.DecreaseQuantity = function (Product) {
        if (Product.quantity >= 2) {
            $scope.AddOrRemoveItemFromCart(Product,-1);        
        }

    }

    $scope.IncreaseQuantity = function (Product) {
        $scope.AddOrRemoveItemFromCart(Product, 1);      
    }

    $scope.RemoveProductFromCart=function(Product)
    {
        $http.get(configurationService.basePath + "API/CartApi/RemoveProductFromCart?CartId=" + Product.cart_id)
        .then(function (response) {    
            $scope.GetCartDetailsByCartGroupId();
            toastr.success("Product successfully removed from cart.");            
        })
          .catch(function (response) {

             })
         .finally(function () {

         });

    }

    $scope.Checkout=function()
    {        
        if (UserDetail.customer_id>0)
        {
            if (($rootScope.CustomerDetail.points - $scope.AllItemTotal) >= 0) {
                $state.go('Checkout', { 'cartgroup_id': UserDetail.cartgroup_id });
            }
            else {
                toastr.warning('You have insufficient points to purchase items.');
            }
        }
        else {
            $scope.OpenLoginPopUp();       
        }                         
    }
    
    $scope.PlaceOrder=function()
    {
     
        $scope.PlaceOrderObj.store_id=$scope.StoreDetailInSession.store_id;
        $scope.PlaceOrderObj.customer_id = UserDetail.customer_id; 
        $scope.PlaceOrderObj.shipping_addressId = $scope.SelectedCustomerAddress.address_id;
        $scope.PlaceOrderObj.IpAddress=$scope.IpAddress;
        $scope.PlaceOrderObj.CartGroupId = UserDetail.cartgroup_id;


        $http.post(configurationService.basePath + "API/CartApi/PlaceOrder", $scope.PlaceOrderObj)
        .then(function (response) {
         
            if (response.data > 0)
            {
                $scope.PlaceOrderObj = new Object();
                notificationFactory.customSuccess("Order successfully placed.");
                $scope.GetCartDetailsByCartGroupId();
                UserDetail.points = ($rootScope.CustomerDetail.points - $scope.AllItemTotal);
                localStorageService.set("loggedInUser", UserDetail);
                $state.go('OrderSuccessful', { 'OrderId': response.data });
                
            }
            
        })
          .catch(function (response) {

          })
         .finally(function () {

         });
    }

    $scope.GetIpAddress=function()
    {
        $.getJSON("http://jsonip.com/?callback=?", function (data) {
            $scope.IpAddress = data.ip;            
        });
    }

    $scope.NavigateAddressSlide=function(index ,IsNext)
    {        
        var totalAddress = $scope.CustomerAddressList.length;
        var CurrIndex = 0;
        if(index>=0)
        {
            if (IsNext==1)
            {
                CurrIndex = index + 1;
                if (CurrIndex>=totalAddress)
                {
                    CurrIndex = 0;
                }                
            }
            else {
                if (index>0)
                {
                    CurrIndex = index - 1;
                }
                else {
                    CurrIndex = totalAddress - 1;
                }
            }
            if ($scope.CustomerAddressList.length >= CurrIndex) {
                $scope.SelectedCustomerAddress = $scope.CustomerAddressList[CurrIndex];
                $scope.SelectedCustomerAddress.SelectedIndex = CurrIndex;
            }
        }
       
      
       
       
    }

        

    $scope.GetCartDetailsByCartGroupId();

    $scope.GetIpAddress();

});