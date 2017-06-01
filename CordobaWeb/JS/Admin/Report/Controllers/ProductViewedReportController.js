app.controller('ProductViewedReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    createDatePicker();
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Products Viewed Report";


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


    $scope.GetProductViewedList = function () {
        if ($.fn.DataTable.isDataTable("#tblProductViewed")) {
            $('#tblProductViewed').DataTable().destroy();
        }
        var table = $('#tblProductViewed').DataTable({
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
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ProductPurchasedReportApi/GetProductViewedList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProductViewed').DataTable().page.info().page) + 1;
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
                { "mData": "name", "bSortable": true },
                { "mData": "model", "bSortable": true },
                {
                    "mData": "viewed", "bSortable": true
                },
                  {
                      "mData": "percent", "bSortable": true
                        , "render": function (data, type, row) {
                            if (data != null) {
                                return data + '%';
                            }
                        }
                  },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProductViewed").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    function init() {
        $scope.GetProductViewedList();
    }



    init();


});