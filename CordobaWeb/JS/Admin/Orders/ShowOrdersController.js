﻿app.controller('ShowOrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;
    $scope.PageTitle = "Order List";

    $scope.OrderStatus = [
       { id: 1, name: 'Processing' },
       { id: 2, name: 'shipped' },
       { id: 3, name: 'PartiallyShipped' },
       { id: 4, name: 'Returned' },
       { id: 5, name: 'Cancelled' }
    ];


    $scope.filter = {
        orderID: '',
        selectedOrderStatus: 1,
        dateAdded: '',
        Customer: '',
        Total: '',
        dateModified: ''
    };

    //$scope.GetOrderList = function () {
    //    $http.get(configurationService.basePath + "api/OrderApi/GetOrderList")
    //         .then(function (response) {
    //             debugger;
    //             if (response.data.length > 0) {
    //                 $scope.orderList = response.data;
    //             }
    //         })
    //     .catch(function (response) {

    //     })
    //     .finally(function () {

    //     });
    //}
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

    $scope.GetOrderList = function () {
        debugger;
        if ($.fn.DataTable.isDataTable("#tblOrders")) {
            $('#tblOrders').DataTable().destroy();
            //$('#tblOrders').html('<table class="table grid table-condensed table-hover" id="tblOrders" width="100%"></table>');
        }

        //var table;
        var table = $('#tblOrders').DataTable({
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
            "sAjaxSource": configurationService.basePath + 'api/OrderApi/GetOrderList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                //aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblOrders').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId + "&PageIndex=" + PageIndex + "&orderId=" + $scope.filter.orderID + "&order_status_id=" + $scope.filter.selectedOrderStatus + "&CustomerName=" + $scope.filter.Customer + "&total="+$scope.filter.Total+"&DateAdded="+$scope.filter.dateAdded+"&DateModified="+$scope.filter.dateModified,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "order_id", "bSortable": true, "sClass": " text-center"
                    //"render": function (data, type, row) {
                    //    return '<a data-Id=' + row.JobId + ' class="cursor-pointer" ng-click="EditJobCode($event)">' + data + '</a>'
                    //}
                },
                { "mData": "customer", "bSortable": true },
                 { "mData": "OrderStatusName", "bSortable": true },
                 { "mData": "total", "bSortable": true, "sClass": "text-right" },
                   {
                       "mData": "date_added", "bSortable": true,
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
                         "mData": "date_modified", "bSortable": true,
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
                    "mData": null, "bSortable": false,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ui-sref="Orders({OrderId:' + row.order_id + '})"><i class="glyphicon glyphicon-eye-close cursor-pointer" title="view"></i></a> &nbsp;  <a ui-sref="ManageOrder({orderId:' + row.order_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>  &nbsp;  <i class="glyphicon glyphicon-trash  cursor-pointer" title="delete"></i>'
                    }
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

    $scope.GetOrderList();
});