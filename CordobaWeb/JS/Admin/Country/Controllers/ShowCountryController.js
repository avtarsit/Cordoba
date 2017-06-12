app.controller('ShowCountryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http,$log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);
                                         
    $scope.dtColumnDefs = [
    DTColumnDefBuilder.newColumnDef(2).notSortable() 
    ];

    $scope.PageTitle = "Show Countries";
    
    $scope.GetCountryList=function()
    {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?countryId=0")
          .then(function (response) {
          if (response.data.length > 0)
                  {                     
                      $scope.CountryList = response.data;
                  }          
        })
      .catch(function (response) {
         
      })
      .finally(function () {
          
      });
    }
    $scope.GetCountryList();




});