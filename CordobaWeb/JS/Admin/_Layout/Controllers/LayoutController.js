app.controller('LayoutController', function(AdminUserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTColumnDefBuilder, DTOptionsBuilder, $http, $log, $q) {
    
    $scope.AdminUserDetail = AdminUserDetail;  
    $rootScope.storeId = $scope.AdminUserDetail.store_id;
    $rootScope.loggedInUserId = $scope.AdminUserDetail.user_id;
    $rootScope.userGroupId = $scope.AdminUserDetail.user_group_id
    
});