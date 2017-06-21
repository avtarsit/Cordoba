app.controller('Dashboard_StoreImageController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;

    //$scope.Store_Id = StoreSessionDetail.store_id;
    //GetStoreImageList();
    //#endregion    
    $scope.GetStoreImageList = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreImageList?Store_Id=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {
              if (response.data.length > 0) {
                  var FirstImage = $filter('filter')(response.data, { 'ImageKey': 1 });
                  if (FirstImage != undefined && FirstImage != null) {
                      $scope.FirstStoreImage = FirstImage[0].Image;
                  }

                  var SecondImage = $filter('filter')(response.data, { 'ImageKey': 2 });
                  if (SecondImage != undefined && SecondImage != null) {
                      $scope.SecondStoreImage = SecondImage[0].Image;
                  }

                  var ThirdImage = $filter('filter')(response.data, { 'ImageKey': 3 });
                  if (ThirdImage != undefined && ThirdImage != null) {
                      $scope.ThirdStoreImage = ThirdImage[0].Image;
                  }

                  //$scope.FirstStoreImage = response.data[0].Image;
                  //$filter('filter', response.data, { 'ImageKey': '1' });
                  //var PopularCategoryObj = $filter('filter')($scope.PopularCategoryList, { 'category_Id': Item.category_Id });
              }
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    $scope.GetStoreImageList();

});