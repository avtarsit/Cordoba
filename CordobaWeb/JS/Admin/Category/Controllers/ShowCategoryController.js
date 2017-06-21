app.controller('ShowCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

    $scope.CategoryList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Categories";

    $scope.GetCategoryList = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryList?CategoryId=0" + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {          
              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetCategoryList();

});