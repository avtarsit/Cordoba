app.controller('StoreHTMLController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.StoreHTMLSummary = [];

    var currentTime = new Date();
    $scope.Years = [{ id: currentTime.getFullYear(), name: currentTime.getFullYear() }, { id: currentTime.getFullYear() - 1, name: currentTime.getFullYear() - 1 }];

    $scope.selectedyear = $scope.Years[0].id;
    //const MONTH_NAMES = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
    const MONTH_NAMES = [{ id: 1, name: "January" }, { id: 2, name: "February" }, { id: 3, name: "March" },
                         { id: 4, name: "April" }, { id: 5, name: "May" }, { id: 6, name: "June" },
                         { id: 7, name: "July" }, { id: 8, name: "August" }, { id: 9, name: "September" },
                         { id: 10, name: "October" }, { id: 11, name: "November" }, { id: 12, name: "December" }];

    $scope.selectedmonth = 0;
    $scope.monthname =''
    $scope.GetMonthData = function (year) {
        debugger;
        $scope.Months = [];
        if (year == currentTime.getFullYear()) {
            for (var i = 0 ; i < currentTime.getMonth() + 1 ; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        else {
            for (var i = 0 ; i < 12 ; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        $scope.selectedmonth = $scope.Months[0].id;
        $scope.monthname = $scope.Months[0].name;
        $scope.GetActiveInAciveCustomersByStore();

    }

    $scope.GetChartData = function (year, month) {
        debugger;
        $scope.selectedyear = year;
        $scope.selectedmonth = month;
        $scope.monthname = MONTH_NAMES[month-1].name;
        //$state.go('StoreHTML');
        $scope.GetActiveInAciveCustomersByStore();
        
    }

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
                 debugger;
                 var StoreSummary = ec.init(document.getElementById('StoreHTMLStoreSummary'), limitless);
                 var PointsRemaining = ec.init(document.getElementById('PointsRemaining'), limitless);
                 var ParticipantsLoadedByMonth = ec.init(document.getElementById('ParticipantsLoadedByMonth'), limitless);
                 var PointsLoadedByMonth = ec.init(document.getElementById('PointsLoadedByMonth'), limitless);
                 var PointsRedeemedByMonth = ec.init(document.getElementById('PointsRedeemedByMonth'), limitless);
                 var OrdersPlacedByType = ec.init(document.getElementById('OrdersPlacedByType'), limitless);
                 //window.onresize = OrdersPlacedByType.resize;

                 //global._preResize && $(window).off('resize', global._preResize);
                 //global._preResize = OrdersPlacedByType.resize;
                 //$(window).on('resize', OrdersPlacedByType.resize);
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

                 //PointsRemaining

                 PointsRemaining_options = {

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
                         data: $scope.StoreHTMLSummary.PointsRemaining
                     },

                     // Display toolbox
                     toolbox: {
                         show: true,
                         orient: 'vertical',
                         feature: {

                             //}
                         }
                     },

                     // Enable drag recalculate
                     calculable: false,

                     // Add series
                     series: [
                         {
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
                             data: $scope.StoreHTMLSummary.PointsRemaining

                         }
                     ]
                 };

                 //ParticipantsLoadedByMonth
                 ParticipantsLoadedByMonth_options = {

                     tooltip: {
                         trigger: 'axis'
                     },
                     //legend: {
                     //    data: ['sdfds']
                     //},
                     toolbox: {
                         show: true,
                         orient: 'vertical',
                         feature: {
                             //mark: { show: true },
                             //dataView: { show: true, readOnly: false },
                             //magicType: { show: true, type: ['line', 'bar'], title: 'Switch to line', },
                             //magicType: {
                             //    show: true,
                             //    title: {
                             //        bar: 'Switch to pies',
                             //        line: 'Switch to line',
                             //    },
                             //    type: ['bar', 'line']
                             //},
                             //restore: { show: true, title: 'Restore' },
                             //saveAsImage: { show: true, title: 'Save as image' }
                         }
                     },
                     calculable: false,
                     xAxis: [
                         {
                             type: 'category',
                             data: $scope.ParticipantsLoadedByMonthname
                              , axisLabel: {
                                  show: true,
                                  interval: 0,    // {number}
                                  rotate: 45,
                                  margin: -20,
                                  formatter: $scope.ParticipantsLoadedByMonthname,
                                  textStyle: {
                                      color: 'blue',
                                      fontFamily: 'sans-serif',
                                      fontSize: 10,
                                      fontStyle: 'italic',
                                      fontWeight: 'bold'
                                  }

                              }
                         }
                     ],
                     yAxis: [
                         {
                             type: 'Month'
                         }
                     ],
                     series: [
                         {
                             name: 'Points',
                             type: 'bar',
                             data: $scope.ParticipantsLoadedByMonthvalue,
                             itemStyle: {
                                 normal: {
                                     color: function (param) {
                                         var colorList = ['#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B'];
                                         return colorList[param.dataIndex]
                                     }
                                 }
                             }
                         }
                     ]
                 };



                 //PointsLoadedByMonth
                 PointsLoadedByMonth_options = {
                     tooltip: {
                         trigger: 'axis',
                         axisPointer: {
                             type: 'shadow'
                         }
                     },
                     legend: {
                         data: ['Month', 'Points']
                     },
                     grid: {
                         left: '3%',
                         right: '4%',
                         bottom: '3%',
                         containLabel: true
                     },
                     xAxis: {
                         type: 'value',
                         boundaryGap: [0, 0.01]
                     },
                     yAxis: {
                         type: 'category',
                         data: $scope.StoreHTMLSummary.PointsLoadedByMonth
                     },
                     series: [

                         {
                             name: 'Points',
                             type: 'bar',
                             data: $scope.StoreHTMLSummary.PointsLoadedByMonthPoints
                         }
                     ]
                 };

                 //PointsRedeemedByMonth
                 PointsRedeemedByMonth_options = {
                     tooltip: {
                         trigger: 'axis',
                         axisPointer: {
                             type: 'shadow'
                         }
                     },
                     legend: {
                         data: ['Month', 'Points']
                     },
                     grid: {
                         left: '3%',
                         right: '4%',
                         bottom: '3%',
                         containLabel: true
                     },
                     xAxis: {
                         type: 'value',
                         boundaryGap: [0, 0.01]
                     },
                     yAxis: {
                         type: 'category',
                         data: $scope.StoreHTMLSummary.PointsRedeemedByMonth
                     },
                     series: [

                         {
                             name: 'Points',
                             type: 'bar',
                             data: $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints
                         }
                     ]
                 };

                 //Orders placed by Type
                 console.log($scope.StoreHTMLSummary.OrdersPlacedByTypeName);
                 console.log($scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount);

                 OrdersPlacedByType_options = {

                     //color: ['#3398DB'],
                     //tooltip: {
                     //    trigger: 'axis',
                     //    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                     //        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                     //    }
                     //},
                     //grid: {
                     //    left: '3%',
                     //    right: '4%',
                     //    bottom: '3%',
                     //    containLabel: true
                     //},
                     //xAxis: [
                     //    {
                     //        type: 'category',
                     //        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
                     //        axisTick: {
                     //            alignWithLabel: true
                     //        }
                     //    }
                     //],
                     //yAxis: [
                     //    {
                     //        type: 'value'
                     //    }
                     //],
                     //series: [
                     //    {
                     //        name: '直接访问',
                     //        type: 'bar',
                     //        barWidth: '60%',
                     //        data: [10, 52, 200, 334, 390, 330, 220]
                     //    }
                     //]

                     tooltip: {
                         trigger: 'axis'
                     },
                     //legend: {
                     //    data: ['sdfds']
                     //},
                     toolbox: {
                         show: true,
                         orient: 'vertical',
                         feature: {
                             //mark: { show: true },
                             //dataView: { show: true, readOnly: false },
                             //magicType: { show: true, type: ['line', 'bar'], title: 'Switch to line', },
                             //magicType: {
                             //    show: true,
                             //    title: {
                             //        bar: 'Switch to pies',
                             //        line: 'Switch to line',
                             //    },
                             //    type: ['bar', 'line']
                             //},
                             //restore: { show: true, title: 'Restore' },
                             //saveAsImage: { show: true, title: 'Save as image' }
                         }
                     },
                     calculable: false,
                     xAxis: [
                         {
                             type: 'category',
                             data: $scope.StoreHTMLSummary.OrdersPlacedByTypeName
                              , axisLabel: {
                                  show: true,
                                  interval: 0,    // {number}
                                  rotate: 45,
                                  margin: -20,
                                  formatter: $scope.StoreHTMLSummary.OrdersPlacedByTypeName,
                                  textStyle: {
                                      color: 'blue',
                                      fontFamily: 'sans-serif',
                                      fontSize: 10,
                                      fontStyle: 'italic',
                                      fontWeight: 'bold'
                                  }

                              }
                         }
                     ],
                     yAxis: [
                         {
                             type: 'OrderCount'
                         }
                     ],
                     series: [
                         {
                             name: 'OrderCount',
                             type: 'bar',
                             data: $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount,
                             itemStyle: {
                                 normal: {
                                     color: function (param) {
                                         var colorList = ['#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B'];
                                         return colorList[param.dataIndex]
                                     }
                                 }
                             }
                         }
                     ]
                 };

                 StoreSummary.setOption(StoreSummary_options);
                 PointsRemaining.setOption(PointsRemaining_options);
                 ParticipantsLoadedByMonth.setOption(ParticipantsLoadedByMonth_options);
                 PointsLoadedByMonth.setOption(PointsLoadedByMonth_options);
                 PointsRedeemedByMonth.setOption(PointsRedeemedByMonth_options);
                 OrdersPlacedByType.setOption(OrdersPlacedByType_options);


                 $timeout(function () {

                     StoreSummary.resize();
                     PointsRemaining.resize();
                     ParticipantsLoadedByMonth.resize();
                     PointsLoadedByMonth.resize();
                     PointsRedeemedByMonth.resize();
                     OrdersPlacedByType.resize();
                 }, 0);


                 // Resize charts
                 // ------------------------------

                 window.onresize = function () {
                     $timeout(function () {

                         StoreSummary.resize();
                         PointsRemaining.resize();
                         ParticipantsLoadedByMonth.resize();
                         PointsLoadedByMonth.resize();
                         PointsRedeemedByMonth.resize();
                         OrdersPlacedByType.resize();
                     }, 0);
                 }
             }
        );
    }

    

    $scope.GetActiveInAciveCustomersByStore = function () {
        debugger;
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreHTMLCharts?StoreID=" + $scope.StoreId + "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear)
        .then(function (response) {
            debugger;
            if (response.data != null) {
                $scope.StoreHTMLSummary.StoreHTMLStoreSummary = [];

                for (var i = 0; i < response.data.storeSummary.length; i++) {
                    $scope.StoreHTMLSummary.StoreHTMLStoreSummary.push({ value: response.data.storeSummary[i].Count, name: response.data.storeSummary[i].Status })
                }
                $scope.StoreHTMLSummary.PointsRemaining = [];
                $scope.StoreHTMLSummary.PointsRemaining.push({ value: response.data.pointsRemaining[0].Count, name: response.data.pointsRemaining[0].Status })

                $scope.ParticipantsLoadedByMonthvalue = [];
                $scope.ParticipantsLoadedByMonthname = [];
                $scope.ParticipantsLoadedByMonth = [];
                for (var i = 0; i < response.data.participantsLoadedByMonth.length; i++) {
                    $scope.ParticipantsLoadedByMonthvalue.push(response.data.participantsLoadedByMonth[i].CustomerCount);
                    $scope.ParticipantsLoadedByMonthname.push(response.data.participantsLoadedByMonth[i].Month)
                    $scope.ParticipantsLoadedByMonth.push({ CustomerCount: response.data.participantsLoadedByMonth[i].CustomerCount, Month: response.data.participantsLoadedByMonth[i].Month });
                }
                debugger;
                $scope.StoreHTMLSummary.PointsLoadedByMonth = [];
                $scope.StoreHTMLSummary.PointsLoadedByMonthPoints = [];
                for (var i = 0; i < response.data.pointsLoadedByMonth.length; i++) {
                    $scope.StoreHTMLSummary.PointsLoadedByMonth.push(response.data.pointsLoadedByMonth[i].Month);
                    $scope.StoreHTMLSummary.PointsLoadedByMonthPoints.push(response.data.pointsLoadedByMonth[i].Points)
                }

                $scope.StoreHTMLSummary.PointsRedeemedByMonth = [];
                $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints = [];
                for (var i = 0; i < response.data.pointsRedeemedByMonth.length; i++) {
                    $scope.StoreHTMLSummary.PointsRedeemedByMonth.push(response.data.pointsRedeemedByMonth[i].Month);
                    $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints.push(response.data.pointsRedeemedByMonth[i].Points)
                }

                $scope.StoreHTMLSummary.TopPointsHolders = response.data.topPointsHolders;
                debugger;
                
                    $scope.StoreHTMLSummary.OrdersPlacedByTypeName = [];
                    $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount = [];
                    debugger;

                    for (var i = 0; i < response.data.orderPlacedByType.length; i++) {
                        $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount.push(response.data.orderPlacedByType[i].OrderCount);
                        $scope.StoreHTMLSummary.OrdersPlacedByTypeName.push(response.data.orderPlacedByType[i].Name)
                    }
                    debugger;
                    $scope.storelogo = response.data.logo;
                    $scope.myObj = {
                        "width": "456px",
                        "height": "90px",
                        "float": "right",
                        "background-image": "url(" + $scope.storelogo + ")"
                    }
                //$timeout(function () {
                //    $scope.$apply(function () {
                //        $scope.StoreHTMLSummary.OrdersPlacedByTypeName = [];
                //        $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount = [];
                //        for (var i = 0; i < response.data.orderPlacedByType.length; i++) {
                //            $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount.push(response.data.orderPlacedByType[i].OrderCount);
                //            $scope.StoreHTMLSummary.OrdersPlacedByTypeName.push(response.data.orderPlacedByType[i].Name)
                //        }
                //    });
                //}, 3000);

            }
            // $scope.activeInactiveCustomer = response.data;
            LoadCharts();
        })
        .catch(function (response) {

        })
        .finally(function (response) {

        });
    }


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