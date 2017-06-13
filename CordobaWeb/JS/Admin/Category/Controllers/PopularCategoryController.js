app.controller('PopularCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.PopularCategoryList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.dtColumnDefs = [
       DTColumnDefBuilder.newColumnDef(1).notSortable()
    ];


    $scope.PageTitle = "Popular Categories";

    $scope.PopularCategoryObj = new Object();
    $scope.PopularCategoryObj.store_id = 0;

    $scope.GetCategoryListByStoreIdPopular = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryListByStoreIdPopular?storeID=" + $scope.PopularCategoryObj.store_id)
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.PopularCategoryList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });


    }

    $scope.GetStoreList=function()
    {

                 $http.get(configurationService.basePath + "api/CategoryApi/GetStoreNameList")
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

    $scope.GetStoreList();
    $scope.GetCategoryListByStoreIdPopular();


    $scope.SwitchClick=function(Item)
    {    
        Item.createdby = -1;
        //    $scope.PopularObj.status = 1;
        //    $scope.PopularObj.category_popularId = $scope.category_popularId;
        $http.post(configurationService.basePath + "api/CategoryApi/InsertOrUpdateCategoryAsPopular", Item)
         .then(function (response) {
            
             if (response.data >= 0) {
                 Item.category_popularId = response.data;
                 //var PopularCategoryObj = $filter('filter')($scope.PopularCategoryList, { 'category_Id': Item.category_Id });
                 //if (PopularCategoryObj != undefined && PopularCategoryObj != null)
                 //{
                 //    //PopularCategoryObj[0].category_popularId = response.data;
                 //    Item.category_popularId = response.data;
                 //}
                
                 toastr.success('Successfully updated.');
                 
             }

         })
      .catch(function (response) {

      })
     .finally(function () {

     });

        }
        


});