app.controller('ManageCustomerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CustomerObj = new Object();
    $scope.StoreObj = new Object();
    $scope.IsEditMode = false;
    $scope.PageTitle = "Manage Customer";
    $scope.customer_id = 0;
    if ($stateParams.CustomerId != undefined && $stateParams.CustomerId != null) {
        $scope.customer_id = $stateParams.CustomerId;
        $scope.IsEditMode = true;
    }  
    //#endregion  
    
    $scope.AddressList = [];
    var AddressObj = new Object();    
    AddressObj.Title = "Address 1";
    $scope.AddressList.push(AddressObj);

    $scope.AddAddress = function ()
    {        
        var AddressObj = new Object();
        AddressObj.Title = "Address " + ($scope.AddressList.length + 1);            
        $scope.AddressList.push(AddressObj);
        $scope.CustomerObj.Address = new Object();
        
    }
    $scope.RemoveAddress = function (item)
    {
        if ($scope.AddressList.length>1)
        {
            var index = $scope.AddressList.indexOf(item);
            $scope.AddressList.splice(index, 1);
        }
   
    }
    $scope.GotoAddress = function (item)
    {  
        var index = $scope.AddressList.indexOf(item);
        $scope.CustomerObj.Address = $scope.AddressList[index];
    }

    $scope.DeleteStore = function () {
        bootbox.dialog({
            message: "Do you want remove store?",
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

    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('Customer');
                }

            });
        }
        else {
            $state.go('Customer');
        }

    }

    function GetCustomerGroupList() {
        $http.get(configurationService.basePath + "api/CustomerGroupApi/GetCustomerGroupList")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CustomerGroupList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }



    $scope.GetCustomerById = function () {
        $http.get(configurationService.basePath + "api/CustomerApi/GetCustomerById?customer_id=" + $scope.customer_id)
          .then(function (response) {
              $scope.CustomerObj = response.data;
              debugger;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }



    function Init() {
        GetCustomerGroupList();
        $scope.GetCustomerById();
    }


    Init();
  
});