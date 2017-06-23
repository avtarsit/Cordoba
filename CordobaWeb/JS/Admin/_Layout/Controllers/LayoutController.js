app.controller('LayoutController', function DemoController(AdminUserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTColumnDefBuilder, DTOptionsBuilder, $http, $log, $q) {
    
    this.AdminUserDetail = AdminUserDetail;
    console.log(this.AdminUserDetail);
    debugger;
    $rootScope.storeId = this.AdminUserDetail.store_id;
    $rootScope.loggedInUserId = this.AdminUserDetail.user_id;
    $rootScope.userGroupId = this.AdminUserDetail.user_group_id
    
});