app.controller('DashboardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.ProductCatalogueList = [];
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
    //#region Chart




        $(function () {

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
                    // ------------------------------

                    //var basic_pie = ec.init(document.getElementById('basic_pie'), limitless);
                    //var basic_donut = ec.init(document.getElementById('basic_donut'), limitless);
                    //var nested_pie = ec.init(document.getElementById('nested_pie'), limitless);
                    //var infographic_donut = ec.init(document.getElementById('infographic_donut'), limitless);
                    var rose_diagram_hidden = ec.init(document.getElementById('rose_diagram_hidden'), limitless);
                    //var rose_diagram_visible = ec.init(document.getElementById('rose_diagram_visible'), limitless);
                    //var lasagna_donut = ec.init(document.getElementById('lasagna_donut'), limitless);
                    //var pie_timeline = ec.init(document.getElementById('pie_timeline'), limitless);
                    //var multiple_donuts = ec.init(document.getElementById('multiple_donuts'), limitless);
                    var basic_bars = ec.init(document.getElementById('basic_bars'), limitless);

                    // Charts setup
                    // ------------------------------                    


                    //
                    // Nightingale roses with hidden labels options
                    //

                    rose_diagram_hidden_options = {

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

                    basic_bars_options = {

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
                            axisPointer: {
                                type: 'shadow'
                            }
                        },

                        // Add legend
                        //legend: {
                        //    data: ['Year 2013']
                        //},

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
                                data: [38, 73, 12, 20, 33, 38, 73, 12, 20, 33, 20, 10]
                            }
                        ]
                    };


                    // Apply options
                    // ------------------------------

                    rose_diagram_hidden.setOption(rose_diagram_hidden_options);
                    basic_bars.setOption(basic_bars_options);
                    // Resize charts
                    // ------------------------------
                   
                    window.onresize = function () {
                        setTimeout(function () {
                            //basic_pie.resize();
                            //basic_donut.resize();
                            //nested_pie.resize();
                            //infographic_donut.resize();
                            rose_diagram_hidden.resize();
                            basic_bars.resize();
                            //rose_diagram_visible.resize();
                            //lasagna_donut.resize();
                            //pie_timeline.resize();
                            //multiple_donuts.resize();
                        }, 200);
                    }
                }
            );
        });



 
    //#endregion  


    //#region Bar chart

    



    //#endregion 




});