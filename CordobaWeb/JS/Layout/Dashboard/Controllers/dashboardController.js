app.controller('DashboardController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion      
    $scope.StoreDetailInSession = StoreSessionDetail;
    $rootScope.storeName = $scope.StoreDetailInSession.name;
    $scope.WelcomeMsg = $scope.StoreDetailInSession.description.split('##ReadMore##');
    $scope.TermsConditionMsg = "";

    $scope.OpenLoginPopUp = function () {
        angular.element("#DivLoginModel").modal('show');
        $scope.IsVisibleloginForm = false;
        $scope.IsVisibleChangePassswordForm = true;
        $scope.IsVisibleforgotPasswordForm = true;
        $scope.IsVisibleOTPForm = true;
    }

    $scope.Login = function (form) {

        if (form.$valid) {

            $scope.CustomerObj.cartgroup_id = UserDetail.cartgroup_id;
            $scope.CustomerObj.store_id = $scope.StoreDetailInSession.store_id;

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/CustomerLogin", $scope.CustomerObj)
                  .then(function (response) {
                      switch (response.data.ErrorTypeId) {
                          case 0:

                              UserDetail.customer_id = response.data.customer_id;
                              UserDetail.firstname = response.data.firstname;
                              UserDetail.lastname = response.data.lastname;
                              UserDetail.points = response.data.points;
                              UserDetail.address_id = response.data.address_id;
                              UserDetail.cartgroup_id = response.data.cartgroup_id;
                              UserDetail.TotalItemAdded = response.data.TotalItemAdded;

                              localStorageService.set("loggedInUser", response.data);
                              $rootScope.CustomerDetail = response.data;

                              angular.element("#DivLoginModel").modal('hide');

                              break;
                          case 1:
                              toastr.error('Please enter correct email!');
                              $scope.CustomerObj.email = null;
                              break;
                          case 2:
                              toastr.error('Please enter correct password!');
                              $scope.CustomerObj.password = null;
                              break;
                          case 3:
                              toastr.error('Please enter correct email and password!');
                              $scope.CustomerObj = null;
                              break;
                      }
                  })
              .catch(function (response) {

              })
              .finally(function () {

              });
        }
    }


    $scope.Logout = function () {
        UserDetail.customer_id = 0;
        UserDetail.firstname = "";
        UserDetail.lastname = "";
        UserDetail.points = "";
        UserDetail.address_id = 0;
        UserDetail.cartgroup_id = 0;
        UserDetail.TotalItemAdded = 0;
        localStorageService.set("loggedInUser", UserDetail);
        $rootScope.CustomerDetail = UserDetail;
        $state.go('Home');
    }
  
    $scope.ForgotPassword = function (form) {
            $scope.IsVisibleloginForm = true;
            $scope.IsVisibleforgotPasswordForm = false;

            if (form.$valid) {

                $scope.otpObj.store_id = $scope.StoreDetailInSession.store_id;
                $scope.otpObj.store_name = $scope.StoreDetailInSession.name;
                $scope.otpObj.logo = $scope.StoreDetailInSession.logo;
                $http.post(configurationService.basePath + "API/LayoutDashboardAPI/ForgotPassword", $scope.otpObj)
                      .then(function (response) {              
                          if (response.data.errorcode > 0) {
                              $scope.IsVisibleloginForm = true;
                              $scope.IsVisibleforgotPasswordForm = true;
                              $scope.IsVisibleOTPForm = false;
                              $scope.IsVisibleChangePassswordForm = true;
                              $scope.otpObj = response.data;
                             

                          }
                          else {
                              //notificationFactory.customError("Email does not exist");
                              toastr.error("Email does not exist");
                          }
                      })
                  .catch(function (response) {

                  })
                  .finally(function () {

                  });
            }
        }

     $scope.VerifyOTP = function (form) {
            $scope.IsVisibleloginForm = true;
            $scope.IsVisibleforgotPasswordForm = true;
            
           
            if (form.$valid) {
                $http.post(configurationService.basePath + "API/LayoutDashboardAPI/VerifyOTP", $scope.otpObj)
                      .then(function (response) {
                          if (response.data.errorcode > 0) {
                              //$scope.IsVisibleloginForm = true;
                              //$scope.IsVisibleforgotPasswordForm = true;
                              $scope.IsVisibleOTPForm = true;
                              $scope.IsVisibleChangePassswordForm = false;
                              $scope.otpObj.password = '';
                          }
                          else {
                              toastr.error("Please Enter valid OTP");
                          }

                      })
                  .catch(function (response) {

                  })
                  .finally(function () {

                  });
            }
        }

        $scope.GotoMyWishlist = function() {          
            if (UserDetail.customer_id > 0) {
                $state.go('LayoutCategoryORProductList', { 'CategoryId': -2 });
            }
            else {
                $scope.OpenLoginPopUp();
            }
        }

        $scope.GotoProductList = function (Whatyouarelookingfor) {
            $state.go('LayoutCategoryORProductList', { 'CategoryId': -3, 'Search': Whatyouarelookingfor });
        }

        $scope.OpenTermsCondition = function () {       
            $scope.GetTermsCondition();
            angular.element("#DivTermsConditionModel").modal('show');
        }

        $scope.GetTermsCondition = function () {   
            $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreTermsDetail?Store_Id=" + $scope.StoreDetailInSession.store_id)
              .then(function (response) {          
                  if (response.data.length > 0) {
                      $scope.TermsConditionMsg = $('<div />').html(response.data[0].Terms).text();                   

                  }
              })
          .catch(function (response) {

          })
          .finally(function () {

          });
        }

        $scope.ChangePassword = function(form)
        {
            if (form.$valid) {
               
                $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveChangedPassword_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.otpObj)
                          .then(function (response) {                   
                              if (response.data > 0) {
                                  notificationFactory.customSuccess("Password changed Successfully.");
                                  
                                  $scope.IsVisibleforgotPasswordForm = true;
                                  $scope.IsVisibleOTPForm = true;
                                  $scope.IsVisibleChangePassswordForm = true;
                                  $scope.IsVisibleloginForm = false;
                                  $scope.otpObj.otp = '';
                                  $scope.otpObj.password = '';
                                  $scope.otpObj.confirmPassword = '';
                                  $scope.otpObj.email = '';
                                  form.$valid = true;
                                  $scope.forgotPasswordForm.$valid = true;
                              }
                              else {
                                  //notificationFactory.customError("Something went wrong");
                                  toastr.error("Something went wrong");
                              }


                          })
                      .catch(function (response) {

                      })
                      .finally(function () {

                      });
            }
        }

        //function decodeHtml(html) {
        //    var txt = document.createElement("textarea");
        //    txt.innerHTML = html;
        //    return txt.value;
        //}

        //$scope.GetStoreDetailForDashboard = function () {

        //    $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreDetailByStoreId?StoreID=0")
        //      .then(function (response) {
        //          if (response.data.length > 0) {
        //              $scope.StoreDetail = response.data;
        //          }
        //      })
        //  .catch(function (response) {

        //  })
        //  .finally(function () {

        //  });
        //}


        //$scope.GetStoreDetailForDashboard();

});


