﻿app.controller('StoreDashboardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);


    $scope.CurrentYearOrderData = [
                                    { value: 300, name: 'January' },
                                    { value: 200, name: 'February' },
                                    { value: 350, name: 'March' },
                                    { value: 250, name: 'April' },
                                    { value: 210, name: 'May' },
                                    { value: 350, name: 'June' },
                                    { value: 300, name: 'July' },
                                    { value: 430, name: 'August' },
                                    { value: 400, name: 'September' },
                                    { value: 450, name: 'October' },
                                    { value: 430, name: 'November' },
                                    { value: 200, name: 'December' }
    ];



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


            // Charts setup
            function (ec, limitless) {


                // Initialize charts

                var OrderSummary = ec.init(document.getElementById('OrderSummary'), limitless);
                var SalesAnalytics = ec.init(document.getElementById('SalesAnalytics'), limitless);
                //var Top5Sales_Chart = ec.init(document.getElementById('Top5Sales_Chart'), limitless);
                var Top5Customer_Chart = ec.init(document.getElementById('Top5Customer_Chart'), limitless);

                var Top5Product_Chart = ec.init(document.getElementById('Top5Product_Chart'), limitless);


                // Charts setup
                // ------------------------------                    
                OrderSummary_options = {

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
                        data: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
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
                            magicType: {
                                show: true,
                                title: {
                                    pie: 'Switch to pies',
                                    funnel: 'Switch to funnel',
                                },
                                type: ['pie', 'funnel']
                            },
                            restore: {
                                show: true,
                                title: 'Restore'
                            },
                            saveAsImage: {
                                show: true,
                                title: 'Same as image',
                                lang: ['Save']
                            }
                        }
                    },

                    // Enable drag recalculate
                    calculable: false,

                    // Add series
                    series: [
                        {
                            //name: 'Increase (brutto)',
                            type: 'pie',
                            radius: ['15%', '73%'],
                            center: ['50%', '59%'],
                            roseType: 'radius',

                            // Funnel
                            width: '40%',
                            height: '78%',
                            x: '30%',
                            y: '17.5%',
                            max: 400,

                            itemStyle: {
                                normal: {
                                    label: {
                                        show: false
                                    },
                                    labelLine: {
                                        show: false
                                    }
                                },
                                emphasis: {
                                    label: {
                                        show: true
                                    },
                                    labelLine: {
                                        show: true
                                    }
                                }
                            },
                            data: $scope.CurrentYearOrderData
                        }
                    ]
                };



                //
                // Basic bars options
                //

                SalesAnalytics_options = {

                    // Setup grid
                    grid: {
                        x: 75,
                        x2: 35,
                        y: 35,
                        y2: 25
                    },

                    // Add tooltip
                    tooltip: {
                        trigger: 'axis',
                        formatter: "{a} <br/>{b}: {c} points",
                        axisPointer: {
                            type: 'shadow'
                        }
                    },

                    // Add legend
                    //legend: {
                    //    data: ['Year 2013']
                    //},
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
                            magicType: {
                                show: true,
                                title: {
                                    bar: 'Switch to bar',
                                    line: 'Switch to line',
                                },
                                type: ['line', 'bar']
                            },
                            restore: {
                                show: true,
                                title: 'Restore'
                            },
                            saveAsImage: {
                                show: true,
                                title: 'Same as image',
                                lang: ['Save']
                            }
                        }
                    },
                    // Enable drag recalculate
                    calculable: false,

                    // Horizontal axis
                    xAxis: [{
                        type: 'value',
                        boundaryGap: [0, 0.01]
                    }],

                    // Vertical axis
                    yAxis: [{
                        type: 'category',
                        data:
                        ['December', 'November', 'October', 'September', 'August', 'July', 'June', 'May', 'April', 'March', 'February', 'January']
                    }],

                    // Add series
                    series: [
                        {
                            //name: 'Year 2013',
                            type: 'bar',
                            itemStyle: {
                                normal: {
                                    color: '#EF5350'
                                }
                            },
                            data: [318, 713, 112, 201, 331, 318, 713, 112, 201, 331, 120, 110]
                        }
                    ]
                };

                


                // Top 5 Customer   =  Top5Customer_Chart
                Top5Customer_Chart_option = {
                    //title: {
                    //    text: 'Store'
                    //    //subtext: 'dsfg'
                    //},
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
                            magicType: {
                                show: true,
                                title: {
                                    bar: 'Switch to pies',
                                    line: 'Switch to line',
                                },
                                type: ['bar', 'line']
                            },
                            restore: { show: true, title: 'Restore' },
                            saveAsImage: { show: true, title: 'Same as image' }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            data: ['Paul', 'Clubb ', 'AJ', 'Adil', 'Adrian']
                            , rotated: true
                            , boundaryGap: ['25%', '24%']
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value'
                        }
                    ],
                    series: [
                        {
                            name: 'purchase',
                            type: 'bar',
                            data: [2.0, 2.6, 20.0, 6.4, 3.3],
                        }
                    ]
                };



                // END - Top 5 Customer   =  Top5Customer_Chart

                // Top 5 Customer   =  Top5Customer_Chart
                Top5Product_Chart_option = {
                    //title: {
                    //    text: 'Store'
                    //    //subtext: 'dsfg'
                    //},
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
                            magicType: {
                                show: true,
                                title: {
                                    bar: 'Switch to pies',
                                    line: 'Switch to line',
                                },
                                type: ['bar', 'line']
                            },
                            restore: { show: true, title: 'Restore' },
                            saveAsImage: { show: true, title: 'Same as image' }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            data: ['Marks','Apple', 'Azz', 'as', 'Capital']
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value'
                        }
                    ],
                    series: [
                        {
                            name: 'Product',
                            type: 'bar',
                            data: [20, 55, 20, 64, 33],
                        }
                    ]
                };



                // END - Top 5 Customer   =  Top5Customer_Chart



                // Apply options
                // ------------------------------

                OrderSummary.setOption(OrderSummary_options);
                SalesAnalytics.setOption(SalesAnalytics_options);
              
                Top5Customer_Chart.setOption(Top5Customer_Chart_option);
                Top5Product_Chart.setOption(Top5Product_Chart_option);


                // Resize charts
                // ------------------------------

                window.onresize = function () {
                    setTimeout(function () {

                        OrderSummary.resize();
                        SalesAnalytics.resize();
                        Top5Customer_Chart.resize();
                        Top5Product_Chart.resize();
                    }, 200);
                }
            }
        );
    }


    LoadCharts();

});