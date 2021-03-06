﻿app.controller('ShowCountryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http,$log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);
                      
                     

    $scope.PageTitle = "Show Countries";
    


 
    $scope.GetCountryList=function()
    {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?CountryCd=''")
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


        //debugger;
        //$.ajax({
        //    url: configurationService.basePath + "api/CountryApi/GetCountryList?CountryCd=''",
        //    dataType: 'json',
        //    type: 'GET',
        //    async: false,          
        //    success: function (data) {
        //        if(data.length>0)
        //        {
        //            debugger;
        //            $scope.CountryList = data;
        //        }
              
        //    }
        //});
    }
    $scope.GetCountryList();




});