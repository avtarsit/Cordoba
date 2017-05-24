app.controller('ShowProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true)
    .withOption('bFilter', false);

    $scope.PageTitle = "Products";

    $scope.ProductFilter = new Object();
    $scope.ProductFilter.name = "";
    $scope.ProductFilter.Model = "";
    $scope.ProductFilter.Price = "";
    $scope.ProductFilter.Quantity = "";
    $scope.ProductFilter.status = "";

    //$scope.GetProductList = function () {
    //    $http.get(configurationService.basePath + "api/ProductApi/GetProductList")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.ProductList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
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

    $scope.GetProductList = function () {
        var filter = $.param({
            name: $scope.ProductFilter.name,
            Price: $scope.ProductFilter.Price,
            status: $scope.ProductFilter.status,
            Model: $scope.ProductFilter.Model,
            Quantity: $scope.ProductFilter.Quantity,
        });
        if ($.fn.DataTable.isDataTable("#tblProduct")) {
            $('#tblProduct').DataTable().destroy();
        }
        var table = $('#tblProduct').DataTable({
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
            "sAjaxSource": configurationService.basePath + 'api/ProductApi/GetProductList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProduct').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex + "&name=" + $scope.ProductFilter.name + "&Price=" + $scope.ProductFilter.Price + "&status=" + $scope.ProductFilter.status + "&Model=" + $scope.ProductFilter.Model + "&Quantity=" + $scope.ProductFilter.Quantity,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "ImagePath", "bSortable": false
                    , "render": function (data, type, row) {
                        return '<img src=' + row.ImagePath + ' class="img-thumbnail" />'
                    }
                },
                { "mData": "name", "bSortable": true },
                { "mData": "Model", "bSortable": true },
                { "mData": "Price", "bSortable": true },
                {
                    "mData": "Quantity", "bSortable": true
                    ,"sClass": "action text-center"
                      , "render": function (data, type, row) {
                          return ' <span class="label label-success">' + row.Quantity + '</span>'
                      }
                },
                { "mData": "StatusName", "bSortable": true },
                {
                    "mData": null, "bSortable": false,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ui-sref="ManageProduct({ProductId:' + row.product_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>'
                    }
                },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProduct").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }


    $scope.GetProductList();
});