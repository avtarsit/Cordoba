app.controller('ShowCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CategoryList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Show Categories";


    $scope.GetCategoryList = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryList?CategoryId=0")
          .then(function (response) {
              debugger;
              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });


    }
    $scope.GetCategoryList();

    //function BindCurrencytList() {
    //    if ($.fn.DataTable.isDataTable("#tblCategory")) {
    //        $('#tblCategory').DataTable().destroy();
    //    }
    //    $('#tblCategory').DataTable({
    //        data: $scope.CurrencyData,
    //        "bDestroy": true,
    //        "dom": '<"top"f><"table-responsive"rt><"bottom"lip<"clear">>',
    //        "oLanguage": {
    //            sProcessing: "",
    //            "sZeroRecords": "<span class='pull-left'>No records found</span>"
    //        },
    //        "columns": [
    //            { "title": "Category Name", "data": "CategoryName", },
    //               { "title": "Sort Order", "data": "SortOrder", },
    //             {
    //                 "title": "IsActive",
    //                 "data": "IsActive",
    //                 "render": function (data, type, row) {
    //                     return data ? "Yes" : "No";
    //                 }
    //             },

    //            {
    //                "title": "Action",
    //                "data": null,
    //                "bSortable": false,
    //                "sClass": "action",
    //                "render": function (data, type, row) {
    //                    return '<a><i class="glyphicon glyphicon-edit curso cursor-pointer"   ng-click="ManageCategory($event)" data-original-title="Edit" data-toggle="tooltip"></i></div>'
    //                }
    //            }
    //        ],
    //        "fnCreatedRow": function (nRow, aData, iDataIndex) {
    //            $compile(angular.element(nRow).contents())($scope);
    //        },
    //        "initComplete": function () {
    //            var dataTable = $('#tblCategory').DataTable();
    //            BindCustomerSearchBar($scope, $compile, dataTable);
    //        }
    //    });
    //}


});