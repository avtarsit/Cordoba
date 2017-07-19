app.controller('TransactionReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();

    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.store_id = $rootScope.storeId;

    $scope.TransactionReportObj = new Object();
    $scope.TransactionReportObj.DateStart = null;
    $scope.TransactionReportObj.DateEnd = null;
    $scope.TransactionReportObj.store_id = $scope.store_id;
   

    $scope.PageTitle = "Reports - Transaction Report";

    

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    $scope.GetTransactionReportList = function () {   
        if ($.fn.DataTable.isDataTable("#tblTransactionReport")) {
            $('#tblTransactionReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblTransactionReport').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "autoWidth": false,
            "searching": true,
            "dom": '<"table-responsive"><"top"lrt><"bottom"ip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetTransactionReportList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblTransactionReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.TransactionReportObj.DateStart + '&DateEnd=' + $scope.TransactionReportObj.DateEnd + '&StoreId=' + $scope.TransactionReportObj.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [

                {
                    "mData": "Date", "bSortable": true,
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    }
                },
                   //{
                   //    "mData": "DateEnd", "bSortable": true,
                   //    "render": function (data, type, row) {
                   //        if (data != null) {
                   //            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                   //        }
                   //        else {
                   //            return "";
                   //        }
                   //    }
                   //},
                {
                    "mData": "firstname",
                    "bSortable": true
                },
                {
                    "mData": "lastname",
                    "bSortable": true
                },
                {
                    "mData": "email",
                    "bSortable": true
                },
                {
                    "mData": "store",
                    "bSortable": true
                },
                {
                    "mData": "adjustment",
                    "bSortable": true
                },
                {
                    "mData": "type_of_points",
                    "bSortable": true
                },
                {
                    "mData": "comments",
                    "bSortable": true
                },


            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    function init() {
        $scope.GetTransactionReportList();
    }

    init();
});