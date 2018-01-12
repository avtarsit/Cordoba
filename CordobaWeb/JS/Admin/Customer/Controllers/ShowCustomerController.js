app.controller('ShowCustomerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, $sce, localStorageService) {
    //#region CallGlobalFunctions
    
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.IsStoreDropDownEnabled = false;
    createDatePicker();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                 .withOption('bDestroy', true)
    $scope.PageTitle = "Show Customer";

    $scope.CustomerFilter = new Object();
    $scope.CustomerFilter.customerName = "";
    $scope.CustomerFilter.email = "";
    $scope.CustomerFilter.customer_group_id = "";
    $scope.CustomerFilter.status = "";
    $scope.CustomerFilter.approved = "";
    $scope.CustomerFilter.ip = "";
    $scope.CustomerFilter.date_added = "";
    $scope.CustomerFilter.storeId = $scope.StoreId;


    if ($stateParams.CustomerApproved != undefined && $stateParams.CustomerApproved != null) {
        $scope.CustomerFilter.approved = $stateParams.CustomerApproved;
    }

    //#endregion  


    function BindSearchCriteria(aoData) {

        aoData.push({ 'name': 'searchKey', 'value': '' });
        aoData.push({ 'name': 'Operation', 'value': '' });
        aoData.push({ 'name': 'searchValue', 'value': '' });

        return aoData;
    }

    function BindSorting(aoData, oSettings) {
        angular.forEach(oSettings.aaSorting, function (row, i) {
            var sortObj = new Object();
            sortObj.Column = oSettings.aoColumns[row[0]].mData;
            sortObj.Desc = row[1] == 'desc';
            aoData.push({ 'name': 'SortColumns', 'value': JSON.stringify(sortObj) });
            return;
        });
        return aoData;
    }


    $scope.GetCustomerList = function () {
        //$scope.CustomerFilter = new Object();

        if ($.fn.DataTable.isDataTable("#tblCustomer")) {
            $('#tblCustomer').DataTable().destroy();
        }
        var table = $('#tblCustomer').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "autoWidth": false,
            "searching": false,
            "dom": '<"table-responsive"rt><"bottom"lip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/CustomerApi/GetCustomerList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblCustomer').DataTable().page.info().page) + 1;
                $scope.GridParams = aoData;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex + "&customerName=" + $scope.CustomerFilter.customerName + "&email=" + $scope.CustomerFilter.email + "&customer_group_id=" + $scope.CustomerFilter.customer_group_id + "&status=" + $scope.CustomerFilter.status + "&approved=" + $scope.CustomerFilter.approved + "&ip=" + $scope.CustomerFilter.ip + "&date_added=" + $scope.CustomerFilter.date_added + "&storeId=" + ($scope.CustomerFilter.storeId == null ? 0 : $scope.CustomerFilter.storeId),
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        // exceptionService.ShowException(data.responseJSON, data.status);
                        alert("error")
                    }
                });
            },

            "aoColumns": [
                { "mData": "customerName", "bSortable": true },
                { "mData": "email", "bSortable": true },
                { "mData": "StatusName", "bSortable": true },
                { "mData": "ip", "bSortable": true },
                { "mData": "store_name", "bSortable": true },
                //{ "mData": "customer_group_name", "bSortable": true },

                {
                    "mData": "date_added", "bSortable": true
                  , "render": function (data, type, row) {
                      return $filter('date')(data, $rootScope.GlobalDateFormat);
                  }
                },
                {
                    "mData": null, "bSortable": true,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ng-click="OpenCreatOrderPopup($event)"><i class="glyphicon glyphicon-plus-sign position-left cursor-pointer" title="Create Order"></i></a>'
                    }
                },
                {
                    "mData": null, "bSortable": false,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ui-sref="ManageCustomer({CustomerId:' + row.customer_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>'
                    }
                },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblCustomer").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    //$scope.GetCustomerList = function () {
    //    $http.get(configurationService.basePath + "api/CustomerApi/GetCustomerList")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.CustomerList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}



    //export Employee List to Excel
    $scope.CustomerExportToExcel = function () {
        var column = "";
        if ($scope.GridParams.length != undefined) {
            column = $filter('filter')($scope.GridParams, { name: "SortColumns" }, true);
            //  alert(JSON.stringify(column));
        }

        $http({
            url: configurationService.basePath + 'api/Customerapi/CustomerExportToExcel?PageIndex=' + 1 + "&customerName=" + $scope.CustomerFilter.customerName + "&email=" + $scope.CustomerFilter.email + "&customer_group_id=" + $scope.CustomerFilter.customer_group_id + "&status=" + $scope.CustomerFilter.status + "&approved=" + $scope.CustomerFilter.approved + "&ip=" + $scope.CustomerFilter.ip + "&date_added=" + $scope.CustomerFilter.date_added + "&storeId=" + ($scope.CustomerFilter.storeId == null ? 0 : $scope.CustomerFilter.storeId),
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            data: column != "" ? column[0].value : "",
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {

            var type = headers('Content-Type');
            var disposition = headers('Content-Disposition');
            if (disposition) {
                var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
                if (match[1])
                    defaultFileName = match[1];
            }
            defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');

            var blob = new Blob([data], { type: type });

            if (navigator.appVersion.toString().indexOf('.NET') > 0) // For IE 
                window.navigator.msSaveBlob(blob, defaultFileName);
            else {
                var objectUrl = URL.createObjectURL(blob);
                var downloadLink = document.createElement("a");
                downloadLink.href = objectUrl;
                downloadLink.download = defaultFileName;
                document.body.appendChild(downloadLink);
                downloadLink.click();
                document.body.removeChild(downloadLink);
                //window.open(objectUrl);
            }
        }).error(function (data, status, headers, config) {
        });

    }



    function GetCustomerGroupList() {
        $http.get(configurationService.basePath + "api/CustomerGroupApi/GetCustomerGroupList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
                  $scope.CustomerFilter.storeId = $scope.StoreId;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.CheckStoreDropDownEnabled = function () {
        if (!($scope.StoreId > 0)) {
            $scope.IsStoreDropDownEnabled = true;
        }

    }

    $scope.OpenCreatOrderPopup = function ($event) {
        var table = $('#tblCustomer').DataTable();
        var row = table.row($($event.target).parents('tr')).data();
        angular.element("#DivCreateOrderModel").modal('show');
        $scope.CreatedOrderUrl = $sce.trustAsResourceUrl(row.localhosturl) + "/#/Home" + "?IsFromAdmin=" + Encodestring(true) + "&Email=" + Encodestring(row.email);
        debugger;
        $scope.Email = row.email;
        $scope.IsFromAdmin = true;
        $scope.StoreName = row.store_name;
    }

    function Init() {

        GetCustomerGroupList();
        GetStoreList();
        $scope.GetCustomerList();
        $scope.CustomerFilter.storeId = $scope.StoreId;
    }

    Init();

});