app.controller('ShowCustomerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions

    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

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
        //$scope.CustomerFilter.customerName = "";
        //$scope.CustomerFilter.email = "";
        //$scope.CustomerFilter.customer_group_id = "";
        //$scope.CustomerFilter.status = "";
        //$scope.CustomerFilter.approved = "";
        //$scope.CustomerFilter.ip = "";
        //$scope.CustomerFilter.date_added = "";
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
            "sAjaxSource": configurationService.basePath + 'api/CustomerApi/GetCustomerList?&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId,
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblCustomer').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex+ "&customerName=" + $scope.CustomerFilter.customerName + "&email=" + $scope.CustomerFilter.email + "&customer_group_id=" + $scope.CustomerFilter.customer_group_id + "&status=" + $scope.CustomerFilter.status + "&approved=" + $scope.CustomerFilter.approved + "&ip=" + $scope.CustomerFilter.ip + "&date_added=" + $scope.CustomerFilter.date_added,
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
                { "mData": "customer_group_name", "bSortable": true },
                { "mData": "StatusName", "bSortable": true },
                { "mData": "ip", "bSortable": true },
                {
                    "mData": "date_added", "bSortable": true
                  , "render": function (data, type, row) {
                      return $filter('date')(data, $rootScope.GlobalDateFormat);
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



    function Init() {
      
        GetCustomerGroupList();

        $scope.GetCustomerList();
    }



    Init();

});