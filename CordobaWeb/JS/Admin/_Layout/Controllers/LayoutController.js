app.controller('LayoutController', function (AdminUserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTColumnDefBuilder, DTOptionsBuilder, $http, $log, $q) {
    $scope.AdminUserDetail = AdminUserDetail;
    $rootScope.IsStoreAdmin = AdminUserDetail.IsStoreAdmin;
    $rootScope.storeId = AdminUserDetail.store_id;
    $rootScope.loggedInUserId = $scope.AdminUserDetail.user_id;
    $rootScope.userGroupId = $scope.AdminUserDetail.user_group_id;
   
    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $rootScope.storeId + "&LoggedInUserId=" + $rootScope.loggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

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

    $scope.GetStoreList();
});