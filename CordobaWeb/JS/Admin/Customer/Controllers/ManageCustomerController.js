app.controller('ManageCustomerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, DTOptionsBuilder, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                   .withOption('bDestroy', true)
                   .withOption("deferRender", true)
  .withOption('bFilter', false);
    $scope.CustomerObj = new Object();
    $scope.StoreObj = new Object();
    $scope.IsEditMode = false;
    $scope.PageTitle = "Manage Customer";
    $scope.Status = [{ ID: 1, Name: 'Yes' }, { ID: 0, Name: 'No' }];
    $scope.customer_id = 0;
    if ($stateParams.CustomerId != undefined && $stateParams.CustomerId != null) {
        $scope.customer_id = $stateParams.CustomerId;
        $scope.IsEditMode = true;
    }
    //#endregion  

    //$scope.AddressList = [];
    //var AddressObj = new Object();    
    //AddressObj.Title = "Address 1";
    //$scope.AddressList.push(AddressObj);


    $scope.AddAddress = function () {
        var AddressObj = new Object();
        //  AddressObj.Title = "Address " + ($scope.AddressList.length + 1);            
        $scope.CustomerObj.AddressList.push(AddressObj);
        $scope.CustomerObj.Address = new Object();
    }
    $scope.RemoveAddress = function (item) {
        if ($scope.CustomerObj.AddressList.length > 1) {
            var index = $scope.CustomerObj.AddressList.indexOf(item);
            $scope.CustomerObj.AddressList.splice(index, 1);
        }
    }
    $scope.GotoAddress = function (item) {
        var index = $scope.CustomerObj.AddressList.indexOf(item);
        $scope.CustomerObj.Address = $scope.CustomerObj.AddressList[index];
        if ($scope.CustomerObj.Address.country_id > 0) {
            $scope.GetZoneListByCountry($scope.CustomerObj.Address.country_id);
        }
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

    function GetCountryList() {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?countryId=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CountryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetZoneListByCountry = function (countryId) {
        debugger;
        countryId = countryId == null ? 0 : countryId;
        $http.get(configurationService.basePath + "api/OrderApi/GetZoneListByCountry?countryId=" + countryId)
        .then(function (response) {
            $scope.RegionStateList = [];
            if (response.data.length > 0) {
                $scope.RegionStateList = response.data;
            }
            else {
                $scope.RegionStateList = [];
            }
        })
       .catch(function (response) {

       })
       .finally(function () {

       });
    }
    function ValidateAddress() {
        var IsValidAddress = true;
        angular.forEach($scope.CustomerObj.AddressList, function (col, i) {
            if (col.firstname == undefined || col.firstname == '' || col.lastname == undefined || col.lastname == '' || col.address_1 == undefined || col.address_1 == ''
                || col.city == undefined || col.city == '' || col.country_id == undefined || col.country_id == '')
            {
                IsValidAddress = false;
            }
        });
        return IsValidAddress && $scope.CustomerObj.AddressList.length>0;
    }
    function CalculateTotalRewardBalance() {
        $scope.TotalBalance = 0;
        var TotalWithdrawal = 0, TotalDeposit = 0;
        if ($scope.CustomerObj != undefined && $scope.CustomerObj != null && $scope.CustomerObj.PointsAuditList != undefined && $scope.CustomerObj.PointsAuditList.length > 0) {
            for (var i = 0; i < $scope.CustomerObj.PointsAuditList.length; i++) {
                if ($scope.CustomerObj.PointsAuditList[i].Withdrawal != '-') {
                    TotalWithdrawal = TotalWithdrawal + parseInt($scope.CustomerObj.PointsAuditList[i].Withdrawal);
                }
                else if ($scope.CustomerObj.PointsAuditList[i].Deposit != '-') {
                    TotalDeposit = TotalDeposit + parseInt($scope.CustomerObj.PointsAuditList[i].Deposit);
                }
            }
            $scope.TotalBalance = TotalWithdrawal + TotalDeposit;
        }
    }


    $scope.AddRewardPointObj = function (item) {
        var RewardPoint = new Object();
        RewardPoint.points_audit_id = 0;
        RewardPoint.customer_id = $scope.customer_id;
        var CurrentDate = new Date();
        if (item != undefined && item.Point != '' && item.Description != '') {
            RewardPoint.adjustment = item.Point;
            if (item.Point > 0) {
                RewardPoint.Deposit = item.Point;
                RewardPoint.Withdrawal = '-';
            }
            else {
                RewardPoint.Withdrawal = item.Point;
                RewardPoint.Deposit = '-';
            }
            RewardPoint.comment = item.Description;
            RewardPoint.timestamp = $filter('date')(CurrentDate, $rootScope.GlobalDateFormat);
            $scope.CustomerObj.PointsAuditList.push(RewardPoint);
            $scope.RewardPointObj = new Object();
            CalculateTotalRewardBalance();
        }
        else {
            notificationFactory.customError("Description and Point are required.");
        }
    }


        

    $scope.GetCustomerById = function () {
        $http.get(configurationService.basePath + "api/CustomerApi/GetCustomerById?customer_id=" + $scope.customer_id)
          .then(function (response) {
              $scope.CustomerObj = response.data;
              if ($scope.CustomerObj.AddressList == undefined || $scope.CustomerObj.AddressList.length == 0) {
                  var AddressObj = new Object();
                  $scope.CustomerObj.AddressList.push(AddressObj);
              }
              CalculateTotalRewardBalance();
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=0")
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
      
    $scope.DeleteCustomer = function () {
        bootbox.dialog({
            message: "Do you want to remove Customer?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CustomerApi/DeleteCustomer?customer_id=" + $scope.customer_id)
                                   .then(function (response) {
                                       if (response.data > 0)
                                           notificationFactory.successDelete();
                                            $state.go('Customer');
                                   })
                               .catch(function (response) {
                                   notificationFactory.errorDelete();
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


    $scope.InsertUpdateCustomer = function (form) {
        debugger;
        if (form.$valid) {

            if (!ValidateAddress())
            {
                notificationFactory.customError("Not a valid address/add at least 1 address.");
                return;
            }
            var CustomerEntity = JSON.stringify($scope.CustomerObj);
            $http.post(configurationService.basePath + "api/CustomerApi/InsertUpdateCustomer", CustomerEntity)
              .then(function (response) {
                  if (response.data > 0) {
                      notificationFactory.customSuccess("Customer Saved Successfully.");
                      $state.go('Customer');
                  }
              })
          .catch(function (response) {
              notificationFactory.customError("Error occur during save record.");
          })
          .finally(function () {

          });
        }
    }
    function Init() {
        GetCountryList();
        GetStoreList();
        GetCustomerGroupList();
        $scope.GetCustomerById();
    }


    Init();

});