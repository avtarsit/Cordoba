app.controller('ProductPurchasedReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    createDatePicker();
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
    $scope.PageTitle = "Products Purchased List";

    $scope.ProductFilter = new Object();
    $scope.ProductFilter.order_status_id = 0;
    $scope.ProductFilter.store_id = 0;
    $scope.ProductFilter.DateStart = '';
    $scope.ProductFilter.DateEnd = '';



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


    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.store_id = 0;
                  DefaultOption.name = "All Stores";
                  $scope.StoreList.push(DefaultOption);
              }
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    function GetOrderStatus() {
        $http.get(configurationService.basePath + "api/ProductPurchasedReportApi/GetOrderStatus?language_id=1")
       .then(function (response) {
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;
               var DefaultOption = new Object()
               DefaultOption.order_status_id = 0;
               DefaultOption.name = "All Statuses";
               $scope.OrderStatusList.push(DefaultOption);
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }


    $scope.GetProductPurchasedList = function () {
        if ($.fn.DataTable.isDataTable("#tblProductPurchased")) {
            $('#tblProductPurchased').DataTable().destroy();
        }
        var table = $('#tblProductPurchased').DataTable({
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
            "aaSorting": [[1, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ProductPurchasedReportApi/GetProductPurchasedList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProductPurchased').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex + "&order_status_id=" + $scope.ProductFilter.order_status_id + "&store_id=" + $scope.ProductFilter.store_id + "&DateStart=" + $scope.ProductFilter.DateStart + "&DateEnd=" + $scope.ProductFilter.DateEnd,
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
                { "mData": "categoryname", "bSortable": true },
                 { "mData": "storename", "bSortable": true },
                {
                    "mData": "quantity", "bSortable": true
                },
                 {
                     "mData": "purchaseddate", "bSortable": true
                  , "render": function (data, type, row) {
                      return $filter('date')(data, $rootScope.GlobalDateFormat);
                  }
                 },
                  { "mData": "total", "bSortable": true },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProductPurchased").contents())($scope);
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
        GetOrderStatus();
        GetStoreList();

        $scope.GetProductPurchasedList();
    }

    init();

});