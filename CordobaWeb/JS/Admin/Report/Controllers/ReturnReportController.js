app.controller('ReturnReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.ReturnReportObj = new Object();
    $scope.ReturnReportObj.DateStart = null;
    $scope.ReturnReportObj.DateEnd = null;
    $scope.ReturnReportObj.GroupById = null;
    $scope.ReturnReportObj.StatusId = null;
    //#endregion  
    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //                 .withOption('bDestroy', true)
    debugger;
    $scope.PageTitle = "Sales Reports -Returns";


    $scope.GroupBy = [
       { id: 0, name: 'Years' },
       { id: 1, name: 'Months' },
       { id: 2, name: 'Weeks' },
       { id: 3, name: 'Days' }
    ];


    $scope.ReturnStatus = [
       { id: 0, name: 'All Statuses' },
       { id: 1, name: 'Pending' },
       { id: 2, name: 'Awaiting Products' },
       { id: 3, name: 'Complete' }
    ];


    $scope.filter = {
        DateStart: '',
        DateEnd: '',
        NoReturns: ''
    };

    //$scope.GetManufacturersList = function () {
    //    $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.ManufacturersList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}
    //$scope.GetManufacturersList();
    function BindSearchCriteria(aoData) {

        aoData.push({ 'name': 'searchKey', 'value': '' });
        aoData.push({ 'name': 'Operation', 'value': '' });
        aoData.push({ 'name': 'searchValue', 'value': '' });

        return aoData;
    }


    function BindSorting(aoData, oSettings) {
        debugger;
        angular.forEach(oSettings.aaSorting, function (row, i) {
            var sortObj = new Object();
            sortObj.Column = oSettings.aoColumns[row[0]].mData;
            sortObj.Desc = row[1] == 'desc';
            aoData.push({ 'name': 'SortColumns', 'value': JSON.stringify(sortObj) });
            return;
        });
        return aoData;
    }

    $scope.GetReturnList = function () {
        debugger;
        if ($.fn.DataTable.isDataTable("#tblReturnReport")) {
            $('#tblReturnReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblReturnReport').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "searching": true,
            "dom": '<"table-responsive"rt><"bottom"lip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetReturnList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            
                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblReturnReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex='+PageIndex+'&DateStart=' + $scope.ReturnReportObj.DateStart + '&DateEnd=' + $scope.ReturnReportObj.DateEnd + '&GroupById=' + $scope.ReturnReportObj.GroupById + '&StatusId=' + $scope.ReturnReportObj.StatusId,
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
   

});