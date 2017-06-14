﻿app.controller('ProfileController', function ($timeout,StoreSessionDetail,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.GetCustomerDetails = function () {
        
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/CustomerDetailLayout?CustomerId=" + UserDetail.customer_id + "&StoreId=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) { 
              $scope.GetCustomerDetailObj = response.data;            
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }




    $scope.SaveCustomerBasicDetails = function (form)
    { 
        $scope.ProfileForm.$submitted = true;
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveCustomerBasicDetails_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.GetCustomerDetailObj)
         .then(function (response) {
             if (response.data>0)
             {
                 $(".update-profile").hide();
                 $("#cancelProfile").hide();
                 $("#editProfile").show();
                 $(".edit-profile i").toggleClass("fa-pencil fa-close");
                 toastr.success("Profile Detail successfully updated.");
             }
             else
             {
                 toastr.error("Something wrong! Please try again later.");
             }            
            })
           .catch(function (response) {

           })
           .finally(function () {
 
           });          
        }
    }

    $scope.SaveChangedPassword=function(form)
    {
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveChangedPassword_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.GetCustomerDetailObj)
                   .then(function (response) {
                       if (response.data > 0) {
                           toastr.success("Password successfully Changed.");
                       }
                       else {
                           toastr.error("Something wrong! Please try again later.");
                       }
                   })
                     .catch(function (response) {

                     })
                     .finally(function () {

                     });
        }
       
    }

    $scope.GetCustomerDetails();


});