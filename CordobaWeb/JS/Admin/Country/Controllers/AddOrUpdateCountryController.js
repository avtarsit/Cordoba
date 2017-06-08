app.controller('AddOrUpdateCountryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    $scope.StatusForActive = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CountryId = 0;
    $scope.CountryObj = {};
    $scope.IsEditMode = false;
    if ($stateParams.CountryId != undefined && $stateParams.CountryId != null) {
        $scope.PageTitle = "Update Country";
        $scope.IsEditMode = true;
        $scope.CountryId = $stateParams.CountryId;
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?countryId=" + $stateParams.CountryId)
          .then(function (response) {              
              if (response.data.length > 0) {
                  $scope.CountryObj = response.data[0];
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    else {
        $scope.PageTitle = "Add Country";
    }
    //#endregion



    $scope.SaveCountry = function (form) {

        if (form.$valid) {
            $http.post(configurationService.basePath + "api/CountryApi/InsertOrUpdateCountry", $scope.CountryObj)
            .then(function (response) {                
                if (response.data == 0) {                    
                    notificationFactory.customError("Country Code is already Exists!!");
                }
                if (response.data > 0) {
                    notificationFactory.customSuccess("Country Saved Successfully.");
                    $state.go('ShowCountry');
                }
            })
             .catch(function (response) {

             })
      .finally(function () {

      });

        }
    }

    $scope.DeleteCountry = function () {
        bootbox.dialog({
            message: "Do you want remove country?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CountryApi/DeleteCountry?countryId=" + $scope.CountryId)
                                   .then(function (response) {
                                       $state.go('ShowCountry');
                                   })
                               .catch(function (response) {
                               })
                               .finally(function () {
                               });
                            }
                        }
                    },
                danger:
                    {
                        label: "No",
                        className: "btn btn-default",
                        callback: function () {
                            return true;
                        }
                    }
            }
        });
    };

    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowCountry');
                }

            });
        }
        else {
            $state.go('ShowCountry');
        }

    }

});