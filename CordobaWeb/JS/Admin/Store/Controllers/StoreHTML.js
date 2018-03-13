app.controller('StoreHTMLController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.StoreHTMLSummary = [];

    function LoadCharts() {
        // Set paths
        // ------------------------------

        require.config({
            paths: {
                echarts: 'Scripts/admin/js/plugins/visualization/echarts'
            }
        });

        // Configuration
        // ------------------------------

        require(
            [
                'echarts',
                'echarts/theme/limitless',
                'echarts/chart/pie',
                 'echarts/chart/bar',
                'echarts/chart/funnel',
                'echarts/chart/line'
            ],
             function (ec, limitless) {
                 // Initialize charts

                 var StoreSummary = ec.init(document.getElementById('StoreHTMLStoreSummary'), limitless);

                 // Charts setup
                 // ------------------------------                    
                 StoreSummary_options = {

                     //// Add title
                     //title: {
                     //    text: 'Current year Order details',
                     //    subtext: 'Senior front end developer',
                     //    x: 'center'
                     //},

                     // Add tooltip
                     tooltip: {
                         trigger: 'item',
                         formatter: "{a} <br/>{b}: {c} ({d}%)"
                     },

                     // Add legend
                     legend: {
                         x: 'left',
                         y: 'top',
                         orient: 'vertical',
                         data: $scope.StoreHTMLSummary.StoreHTMLStoreSummary
                     },

                     // Display toolbox
                     toolbox: {
                         show: true,
                         orient: 'vertical',
                         feature: {
                             //mark: {
                             //    show: true,
                             //    title: {
                             //        mark: 'Markline switch',
                             //        markUndo: 'Undo markline',
                             //        markClear: 'Clear markline'
                             //    }
                             //},
                             //dataView: {
                             //    show: true,
                             //    readOnly: false,
                             //    title: 'View data',
                             //    lang: ['View chart data', 'Close', 'Update']
                             //},
                             //magicType: {
                             //    show: true,
                             //    title: {
                             //        pie: 'Switch to pies',
                             //        funnel: 'Switch to funnel',
                             //    },
                             //    type: ['pie', 'funnel']
                             //},
                             //restore: {
                             //    show: true,
                             //    title: 'Restore'
                             //},
                             //saveAsImage: {
                             //    show: true,
                             //    title: 'Save as image',
                             //    lang: ['Save']
                             //}
                         }
                     },

                     // Enable drag recalculate
                     calculable: false,

                     // Add series
                     series: [
                         {
                             ////name: 'Increase (brutto)',
                             //type: 'pie',
                             //radius: ['50%', '70%'],
                             //center: ['50%', '59%'],
                             //roseType: 'radius',

                             //// Funnel
                             //width: '40%',
                             //height: '78%',
                             //x: '30%',
                             //y: '17.5%',
                             //max: 400,

                             //itemStyle: {
                             //    normal: {
                             //        label: {
                             //            show: false
                             //        },
                             //        labelLine: {
                             //            show: false
                             //        }
                             //    },
                             //    emphasis: {
                             //        label: {
                             //            show: true
                             //        },
                             //        labelLine: {
                             //            show: true
                             //        }
                             //    }
                             //},
                             //data: $scope.StoreHTMLSummary.StoreHTMLStoreSummary


                             name: '',
                             type: 'pie',
                             radius: ['50%', '70%'],
                             avoidLabelOverlap: false,
                             label: {
                                 normal: {
                                     show: false,
                                     position: 'center'
                                 },
                                 emphasis: {
                                     show: true,
                                     textStyle: {
                                         fontSize: '30',
                                         fontWeight: 'bold'
                                     }
                                 }
                             },
                             labelLine: {
                                 normal: {
                                     show: false
                                 }
                             },
                             data: $scope.StoreHTMLSummary.StoreHTMLStoreSummary
                             
                         }
                     ]
                 };
                 StoreSummary.setOption(StoreSummary_options);

                 setTimeout(function () {

                     StoreSummary.resize();
                 }, 100);


                 // Resize charts
                 // ------------------------------

                 window.onresize = function () {
                     setTimeout(function () {

                         StoreSummary.resize();
                     }, 200);
                 }
             }
        );
    }

    $scope.GetActiveInAciveCustomersByStore = function () {
        debugger;
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreHTMLCharts?StoreID=" + $scope.StoreId)
        .then(function (response) {
            debugger;
            if (response.data != null) {
                $scope.StoreHTMLSummary.StoreHTMLStoreSummary = [];
                //for(var i=0; i<response.data.)
                //$scope.StoreHTMLSummary.StoreHTMLStoreSummary = response.data.storeSummary;
                for (var i = 0; i < response.data.storeSummary.length; i++) {
                    $scope.StoreHTMLSummary.StoreHTMLStoreSummary.push({ value: response.data.storeSummary[i].Count, name: response.data.storeSummary[i].Status })
                }
                
                
            }
            // $scope.activeInactiveCustomer = response.data;
            LoadCharts();
        })
        .catch(function (response) {

        })
        .finally(function (response) {

        });
    }

    $scope.GetActiveInAciveCustomersByStore();

    $scope.ExportStoreHTMLPDF = function () {
        debugger;
        
        

        html2canvas($("#pdf"), {
            onrendered: function (canvas) {
                debugger;
                theCanvas = canvas;
                theCanvas.setAttribute("id", "Div1");
                document.body.appendChild(canvas);

                // Convert and download as image 
                //Canvas2Image.saveAsPNG(canvas);
                $("#img-out").html(canvas);
                debugger;
                
                var base64 = $('#Div1')[0].toDataURL();
                $("#imgCapture").attr("src", base64);
                $("#imgCapture").show();

                $("#img-out").hide();
                $("#img-capture").hide();
                // Clean up 
                //document.body.removeChild(canvas);
            }
        });
        //$scope.ExportStoreHTMLPDF1();
        
        $timeout(function () { $("#Generatepdf").trigger("click"); }, 3000);

        
    }
    $("#Generatepdf").click(function () {
    //function ExportStoreHTMLPDF1() {
        debugger;
        var template = $('#img-capture').html();
        debugger;
        var storeentity = { template: template };
        $http({
            url: configurationService.basePath + 'api/StoreApi/ExportStoreHTMLPDF',
            method: "POST",
            data: storeentity,
            async: false,
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            debugger;
            var type = headers('Content-Type');
            var disposition = headers('Content-Disposition');
            if (disposition) {
                var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
                if (match[1])
                    defaultFileName = match[1];
            }
            defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');
            var blob = new Blob([data], { type: type });
            if (navigator.appVersion.toString().indexOf('.NET') > 0) // For IE 
                window.navigator.msSaveBlob(blob, defaultFileName);
            else {
                var objectUrl = URL.createObjectURL(blob);
                var downloadLink = document.createElement("a");
                downloadLink.href = objectUrl;
                downloadLink.download = defaultFileName;
                document.body.appendChild(downloadLink);
                downloadLink.click();
                document.body.removeChild(downloadLink);
            }
        }).error(function (data, status, headers, config) {
            toastr.error("Some error has occured, please contact to admin");
        });
    });

    //$scope.ExportStoreHTMLPDF1 = function () {
    //    debugger;
    //    var template = $('#img-capture').html();
    //    debugger;
    //    var storeentity = { template: template };
    //    $http({
    //        url: configurationService.basePath + 'api/StoreApi/ExportStoreHTMLPDF',
    //        method: "POST",
    //        data: storeentity,
    //        async: false,
    //        responseType: 'arraybuffer'
    //    }).success(function (data, status, headers, config) {
    //        debugger;
    //        var type = headers('Content-Type');
    //        var disposition = headers('Content-Disposition');
    //        if (disposition) {
    //            var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
    //            if (match[1])
    //                defaultFileName = match[1];
    //        }
    //        defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');
    //        var blob = new Blob([data], { type: type });
    //        if (navigator.appVersion.toString().indexOf('.NET') > 0) // For IE 
    //            window.navigator.msSaveBlob(blob, defaultFileName);
    //        else {
    //            var objectUrl = URL.createObjectURL(blob);
    //            var downloadLink = document.createElement("a");
    //            downloadLink.href = objectUrl;
    //            downloadLink.download = defaultFileName;
    //            document.body.appendChild(downloadLink);
    //            downloadLink.click();
    //            document.body.removeChild(downloadLink);
    //        }
    //    }).error(function (data, status, headers, config) {
    //        toastr.error("Some error has occured, please contact to admin");
    //    });
    //}

});