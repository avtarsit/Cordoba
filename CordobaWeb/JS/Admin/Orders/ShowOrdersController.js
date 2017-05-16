app.controller('ShowOrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.PageTitle = "Order List";



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

        if ($.fn.DataTable.isDataTable("#tblOrders")) {
            $('#tblOrders').DataTable().destroy();
        }

        //var table;
        var table = $('#tblOrders').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "searching": false,
            "dom": '<"table-responsive"rt><"bottom"lip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'asc']],
            "sAjaxSource": configurationService.basePath + 'api/OrderApi/GetOrderList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblOrders').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "order_id", "bSortable": true,
                    //"render": function (data, type, row) {
                    //    return '<a data-Id=' + row.JobId + ' class="cursor-pointer" ng-click="EditJobCode($event)">' + data + '</a>'
                    //}
                },
                { "mData": "customer", "bSortable": true },
                 { "mData": "OrderStatusName", "bSortable": true },
                 { "mData": "total", "bSortable": true },

                { "mData": "date_added", "bSortable": true, },
                { "mData": "date_modified", "bSortable": true }
                //{
                //    "mData": null, "bSortable": false,
                //    "sClass": "action",
                //    "render": function (data, type, row) {
                //        return '<a><i class="glyphicon glyphicon-trash cursor-pointer"  data-Id=' + row.JobId + ' data-jobCode=' + row.JobCode + '  ng-click="DeleteJobCode($event)" data-original-title="Delete" data-toggle="tooltip"></i></a>'
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



    $scope.GetOrderList();
});