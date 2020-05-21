var echarts;
var customer_names = [];
var SumTotalAmounts = [];
var SumTotalCounts = [];
var SumYuCounts = [];
var SumSaleCounts = [];

var SumTotalAmountss = 0;
var SumTotalCountss = 0;
var SumYuCountss = 0;
var SumSaleCountss = 0;

function clearData() {
    customer_names = [];
    SumTotalAmounts = [];
    SumTotalCounts = [];
    SumYuCounts = [];
    SumSaleCounts = [];

    SumTotalAmountss = 0;
    SumTotalCountss = 0;
    SumYuCountss = 0;
    SumSaleCountss = 0;
}


//创建ECharts图表方法  
function DrawEChartCustomer() {
    $("#mainCustomer").css("height", 700);
    myChartCustomer = echarts.init(document.getElementById('mainCustomer'), 'chalk');

    optionCustomer = {
        title: {
            text: '拓展排名'
        },
        legend: {
            top: 20,
            data: ['总金额', '总数量', '剩余数量', '销售数量'],
            selected: {
                '总金额': true,
                '总数量': false,
                '剩余数量': false,
                '销售数量': false
            }
        },
        grid:{
            bottom:200
        },
        tooltip: {
        },
        toolbox: {
            show: true,
            orient: 'vertical',
            feature: {
                mark: {
                    show: true
                },
                dataView: {
                    show: true,
                    readOnly: false,
                    optionToContent: function (opt) {
                        var hh = (opt.xAxis[0].data.length + 1) * 22;
                        if (hh < 700) {
                            hh =700;
                        }
                        $("#mainCustomer").css("height", hh);
                        var series = opt.series;
                        var axisData = opt.xAxis[0].data;
                        var table = '<table class="form" ><tbody><tr><td>员工</td>';
                        for (var i = 0, l = series.length; i < l; i++) {
                            table += '<td>' + series[i].name + '</td>'
                        };

                        table += '</tr>';
                        for (var m = 0, g = axisData.length; m < g; m++) {
                            table += '<tr><td>' + axisData[m] + '</td>';
                            for (var n = 0, l = series.length; n < l; n++) {
                                var val = series[n].data[m];

                                table += '<td>' + val + '</td>'
                            }
                            table += '</tr>';
                        };

                        table += '<tr><td>合计</td><td>' + SumTotalAmountss + '</td><td>' + SumTotalCountss + '</td><td>' + SumYuCountss + '</td><td>' + SumSaleCountss + '</td></tr>';
                        table += '</tbody></table>';
                        return table;
                    }
                },
                magicType: {
                    show: true,
                    type: ['line', 'bar']
                },
                restore: {
                    show: true
                },
                saveAsImage: {
                    show: true
                }
            }
        },
        yAxis: {
            type: 'value',
            scale: true,
            axisLabel: {
                formatter: function (v) {
                    return v
                }
            },
            splitArea: {
                show: true
            }

        },
        xAxis: {
            type: 'category',
            axisLabel: {
                interval: 0,
                rotate: 45,//倾斜度 -90 至 90 默认为0  
            },
            data: []
        },
        series: [
            {
                type: 'bar',
                name: '总金额',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            position: 'top'
                        }
                    }
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                },
                data: []
            },
            {
                type: 'bar',
                name: '总数量',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            position: 'top'
                        }
                    }
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                },
                data: []
            },
            {
                type: 'bar',
                name: '剩余数量',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            position: 'top'
                        }
                    }
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                },
                data: []
            },
            {
                type: 'bar',
                name: '销售数量',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            position: 'top'
                        }
                    }
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                },
                data: []
            }
        ]
    };
    myChartCustomer.setOption(optionCustomer);
}

        

//window.setInterval("doserch()", "60000");
//window.setInterval("getDateOrder_status()", "60000");
