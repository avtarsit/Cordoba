app.controller('ContactUsController', function (StoreSessionDetail,UserDetail, $timeout, $state, $http,$location, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
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

    debugger;

    $scope.SendContactUsDetail = function (form) {
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/ContactUsAPI/SendContactUsDetails?firstname=" + $scope.contactUsObj.firstname + "&lastname=" + $scope.contactUsObj.lastname + "&email=" + $scope.contactUsObj.email + "&phone=" + $scope.contactUsObj.phone + "&description=" + $scope.contactUsObj.description, $scope.StoreDetailInSession)
              .then(function (response) {
                  toastr.success("Email sent successfully.");
                  $scope.contactUsObj = {};
                  angular.copy({}, form);
              })
          .catch(function (response) {

          })
          .finally(function () {

          });
        }
    }

})