app.controller('DashboardController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion      
    $scope.StoreDetailInSession = StoreSessionDetail;
    $rootScope.no_image_path = StoreSessionDetail.no_image_path;
    $scope.WelcomeMsg = $scope.StoreDetailInSession.description.split('##ReadMore##');
    $scope.TermsConditionMsg = "";

    function OpenLoginForm() {
        $rootScope.OpenLoginPopUpUsingRootScope();
    }

    $rootScope.OpenLoginPopUpUsingRootScope = function () {
        $scope.Logout();
        $scope.OpenLoginPopUp();
    }
    $scope.OpenLoginPopUp = function () {
        angular.element("#DivLoginModel").modal('show');
        $scope.IsVisibleloginForm = false;
        $scope.IsVisibleChangePassswordForm = true;
        $scope.IsVisibleforgotPasswordForm = true;
        $scope.IsVisibleResetPassswordForm = true;
        $scope.IsVisibleFirstTimeVisiteForm = true;
        $scope.IsVisibleOTPForm = true;
        $scope.CustomerObj = new Object();
        $scope.loginForm.$submitted = false;

    }

    $scope.Login = function (form) {
        if (form.$valid) {

            $scope.CustomerObj.cartgroup_id = UserDetail.cartgroup_id;
            $scope.CustomerObj.store_id = $scope.StoreDetailInSession.store_id;
            $scope.CustomerObj.IsFromAdmin = $("#IsFromAdmin").val();

            debugger;
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/CustomerLogin", $scope.CustomerObj)
                  .then(function (response) {
                      debugger;
                      if (response.data != null) {
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
                      } else {
                          toastr.error('Please enter correct email and password!');
                          //$scope.CustomerObj = null;
                      }
                      
                  })
              .catch(function (response) {

              })
              .finally(function () {

              });
        }
    }

    //for first time login 
    $scope.validateEmailaddress = function () {
        if ($scope.CustomerObj.email != null) {
            
            //$scope.validateObj = new Object();
            //$scope.validateObj.email = $scope.CustomerObj.email;
            //$scope.validateObj.store_id = $scope.StoreDetailInSession.store_id;
           
            //$http.post(configurationService.basePath + "API/LayoutDashboardAPI/VisitedCustomerInfo", $scope.validateObj)
            //   .then(function (response) {
                  
                  
            //       if (response.data === 0) {
            //           $scope.IsVisibleloginForm = true;
            //           $scope.IsVisibleFirstTimeVisiteForm = false;
            //       }
            //    })
            //    .catch(function (response) {

            //    })
            //    .finally(function () {

            //    });
        }
    }

    $scope.OpenResetPasswordForm = function() {
        if ($scope.CustomerObj.email != null) {
           
            $scope.resetObj = new Object();
            $scope.resetObj.store_id = $scope.StoreDetailInSession.store_id;
            $scope.resetObj.store_name = $scope.StoreDetailInSession.name;
            $scope.resetObj.logo = $scope.StoreDetailInSession.logo;
            $scope.resetObj.email = $scope.CustomerObj.email;

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SendResetPassEmail",  $scope.resetObj)
                .then(function (response) {
                   
                    
                    if (response.data.errorcode > 0) {
                        
                        $scope.IsVisibleloginForm = true;
                        $scope.IsVisibleFirstTimeVisiteForm = true;
                        $scope.IsVisibleResetPassswordForm = false;
                    }
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.ResetPassword = function(form) {
        if (form.$valid) {
            $scope.resetObj.email = $scope.CustomerObj.email;
            $scope.resetObj.store_id = $scope.StoreDetailInSession.store_id;

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/ResetPasswordAndOtpVerify", $scope.resetObj)
                .then(function (response) {
                 
                    
                    if (response.data.errorcode > 0) {

                        notificationFactory.customSuccess("Password Reseted Successfully.");

                        $scope.IsVisibleforgotPasswordForm = true;
                        $scope.IsVisibleOTPForm = true;
                        $scope.IsVisibleChangePassswordForm = true;
                        $scope.IsVisibleResetPassswordForm = true;
                        $scope.IsVisibleFirstTimeVisiteForm = true;
                        $scope.IsVisibleloginForm = false;
                        $scope.otpObj.otp = '';
                        $scope.otpObj.password = '';
                        $scope.otpObj.confirmPassword = '';
                        $scope.otpObj.email = '';
                        form.$valid = true;
                        $scope.forgotPasswordForm.$valid = true;
                    } else {
                        toastr.error("OTP verification failed.Please Check");
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

    $scope.GotoMyWishlist = function () {

        if (UserDetail.customer_id > 0) {
            $state.go('LayoutCategoryORProductList', { 'CategoryId': -2 });
        }
        else {
            $scope.OpenLoginPopUp();
        }
    }

    $scope.GotoProductList = function (Whatyouarelookingfor) {
        if (Whatyouarelookingfor != undefined && Whatyouarelookingfor != null && Whatyouarelookingfor != "") {        
            $state.go('LayoutCategoryORProductList', { 'CategoryId': -3, 'Search': Whatyouarelookingfor });
        }     
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

    $scope.ChangePassword = function (form) {
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

    $scope.SetNoImageSrc = function (Image) {
        Image.src = $rootScope.no_image_path;
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

    function inIframe() {
        try {
            return window.self !== window.top;
        } catch (e) {
            return true;
        }
    }

    angular.element(document).ready(function () {
        function param(name) {
            return (location.href.split(name + '=')[1] || '').split('&')[0];
        }

        function DecodeString(element) {
            if (element.indexOf(specialCharacter) > -1) {
                element = decodeURIComponent(element);
                var decodedstring = element.substring(keylength + 1, element.length - (keylength + 1));
                if (decodedstring.length >= 3) {

                    decodedstring = (String.fromCharCode(parseInt(decodedstring.substring(0, 3))) + decodedstring.substring(3, decodedstring.length)).toString();
                }
                return decodedstring;
            }
        }

        if (window.location.href.indexOf("IsFromAdmin") > 0 && window.location.href.indexOf("Email") > 0) {
            var IsFromAdmin = DecodeString(param('IsFromAdmin'));
            var Email = DecodeString(param('Email'));
            if (IsFromAdmin == "true") {
                window.document.getElementById("IsFromAdmin").value = IsFromAdmin;
                window.document.getElementById("email").value = Email;
                var scope = angular.element('#loginForm').scope();
                scope.CustomerObj = new Object();
                scope.CustomerObj.email = Email;

                if (inIframe()) {
                    OpenLoginForm();
                }
            }
        }
    });

    $interval(function () { CheckVersionNumber(); }, 3000);
    function CheckVersionNumber() {
        $.ajax({
            method: "GET",
            url: "Home/CheckVersionNumber",
            async: false,
            success: function (data) {
                if (data != ApplicationVersion) {
                    angular.element("#Reloadlookup").modal('show');
                }
            }

        })
    };
});


