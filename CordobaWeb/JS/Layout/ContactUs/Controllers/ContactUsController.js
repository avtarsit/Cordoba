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

    $scope.SendContactUsDetail = function () {
        $http.post(configurationService.basePath + "API/ContactUsAPI/SendContactUsDetails", $scope.StoreDetailInSession, $scope.contactUsObj)
          .then(function (response) {
              
                  toastr.success("Saved successfully.");
              
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

})