﻿app.controller('ContactUsController', function (StoreSessionDetail,UserDetail, $timeout, $state, $http,$location, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.contactUsObj = {
        firstname: '',
        lastname: '',
        email: '',
        phone: '',
        description: ''
    }

    $scope.SendContactUsDetail = function (form) {    
        //$scope.captchaValid = false;
        //if (grecaptcha.getResponse() == undefined || grecaptcha.getResponse() == '' || grecaptcha.getResponse() == null) {  
        //    return false;
            
        //}
        //else {
        //    $scope.captchaValid = true;
        //}
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/ContactUsAPI/SendContactUsDetails?firstname=" + $scope.contactUsObj.firstname + "&lastname=" + $scope.contactUsObj.lastname + "&email=" + $scope.contactUsObj.email + "&phone=" + $scope.contactUsObj.phone + "&description=" + $scope.contactUsObj.description, $scope.StoreDetailInSession)
              .then(function (response) {
                  
                  $scope.contactUsObj = new Object();
                  window.location.reload(true);
                  toastr.success("Successfully Submitted.");
              })
          .catch(function (response) {

          })
          .finally(function () {

          });
        }
    }




})