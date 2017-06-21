app.controller('OrderReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.OrderReportObj = new Object();
    $scope.OrderReportObj.DateStart = null;
    $scope.OrderReportObj.DateEnd = null;
    $scope.OrderReportObj.GroupById = null;
    $scope.OrderReportObj.StatusId = null;
    //#endregion  
    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //                 .withOption('bDestroy', true)

    $scope.PageTitle = "Reports - Orders Summary";


    $scope.GroupBy = [
       { id: 0, name: 'Years' },
       { id: 1, name: 'Months' },
       { id: 2, name: 'Weeks' },
       { id: 3, name: 'Days' }
    ];


    $scope.OrderStatus = [
       { id: 0, name: 'All Statuses' },
       { id: 1, name: 'Processing' },
       { id: 2, name: 'Shipped' },
       { id: 3, name: 'Partially Shipped' },
       { id: 4, name: 'Returned' },
       { id: 5, name: 'Cancelled' }
    ];


    $scope.filter = {
        DateStart: '',
        DateEnd: '',
        NoReturns: '',
        NoProducts: '',
        Total: '',
        Tax:''
    };


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

    $scope.GetOrderReportList = function () {
 
        if ($.fn.DataTable.isDataTable("#tblOrderReport")) {
            $('#tblOrderReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblOrderReport').DataTable({
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
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetOrderReportList?StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId,
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblOrderReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.OrderReportObj.DateStart + '&DateEnd=' + $scope.OrderReportObj.DateEnd + '&GroupById=' + $scope.OrderReportObj.GroupById + '&StatusId=' + $scope.OrderReportObj.StatusId + '&StoreId=' + $scope.StoreId,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [

                {
                    "mData": "DateStart", "bSortable": true,
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    }
                },
                   {
                       "mData": "DateEnd", "bSortable": true,
                       "render": function (data, type, row) {
                           if (data != null) {
                               return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                           }
                           else {
                               return "";
                           }
                       }
                   },
                {
                    "mData": "No_Of_Orders",
                    "bSortable": true
                },
                {
                    "mData": "No_Of_Products",
                    "bSortable": true
                },
                {
                    "mData": "Total",
                    "bSortable": true
                },
                {
                    "mData": "Tax",
                    "bSortable": true
                },


                //{
                //    "mData": null, "bSortable": false,
                //    "sClass": "action text-right",
                //    "render": function (data, type, row) {
                //        return '<a ui-sref="Orders({OrderId:' + row.order_id + '})"><i class="glyphicon glyphicon-eye-close cursor-pointer" title="view"></i></a> &nbsp;  <a ui-sref="ManageOrder({orderId:' + row.order_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>  &nbsp;  <i class="glyphicon glyphicon-erase cursor-pointer" title="delete"></i>'
                //    }
                //},
            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    $scope.GetOrderReportList();
});