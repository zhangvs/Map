var echarts;
var username = '';
var axisDatas = []; //x轴
var emp_amounts = [];//金额
var emp_counts = [];//个数
var sum_amounts = 0;
var sum_counts = 0;
var daycount = 0;

//日期天数
function getDateCount() {
    //年月
    syear = $("#syear").val();
    smonth = $("#smonth").val();
    if (smonth.length == 1) {
        smonth = '0' + smonth;
    }
    //天数
    var day = new Date(syear, smonth, 0);//构造一个日期对象
    daycount = day.getDate();//获取天数
    for (var i = 0; i < daycount; i++) {
        axisDatas[i] = i + 1;
        emp_amounts[i] = 0;
        emp_counts[i] = 0;
    }
}

function clearData() {
    getDateCount();
    sum_amounts = 0;
    sum_counts = 0;
}


//赋值
function getMonthEmpData(result) {
    for (var j = 0; j < result.length; j++) {
        var orderDate = parseInt(result[j].orderdate) - 1; //从1日开始，-1到角标0
        var salemoney=parseFloat(result[j].salemoney.toFixed());
        emp_amounts[orderDate] = salemoney;
        sum_amounts += salemoney;
        var salecount = parseFloat(result[j].salecount.toFixed());
        emp_counts[orderDate] = salecount;
        sum_counts += salecount;
    }
}


//创建ECharts图表方法  
function DrawEChartMonthEmp() {
    $("#main").css("height", 700);
    option = {
        title: {
            text: username + '月报',
            subtext:'总金额：' + sum_amounts + '\n总数量：' + sum_counts
        },
        legend: {
            top: 20,
            data: ['销售金额','销售数量'],
            selected: {
                '销售金额': true,
                '销售数量': false
            }
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
                            hh = 700;
                        }
                        $("#main").css("height", hh);
                        var series = opt.series;
                        var axisData = opt.xAxis[0].data;
                        var table = '<table class="form" ><tbody><tr><td>日期</td>';
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

                        table += '<tr><td>合计</td><td>' + sum_amounts + '</td><td>' + sum_counts + '</td></tr>';
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
                formatter: function (v) {
                    return v + '日'
                }
            },
            data: axisDatas
        },
        series: [
            {
                type: 'bar',
                name: '销售金额',
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
                data: emp_amounts
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
                data: emp_counts
            }
        ]
    };
    var myChart = echarts.init(document.getElementById('main'), 'macarons');
    myChart.setOption(option);
    window.onresize = myChart.resize;//随屏幕的大小改变自适应


    
}