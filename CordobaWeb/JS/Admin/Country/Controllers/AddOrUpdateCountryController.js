app.controller('AddOrUpdateCountryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;
    if ($stateParams.CountryCd != undefined && $stateParams.CountryCd != null)
    {
        $scope.PageTitle = "Update Country";
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Country";
    }
    //#endregion



    $scope.SaveCountry=function(form)
    {
        if (form.$valid) {
          
        }
    }

    $scope.DeleteCountry = function () {
        bootbox.dialog({
            message:"Do you want remove country?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                
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

    $scope.Cancel=function()
    {        
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData)
        {   
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if(result)
                {
                    $state.go('ShowCountry');
                }
                    
                });        
        }
        else
        {
            $state.go('ShowCountry');
        }
      
    }

});