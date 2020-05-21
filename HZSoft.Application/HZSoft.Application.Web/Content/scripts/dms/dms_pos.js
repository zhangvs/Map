var echarts;
var emp_names = [];
var emp_counts = [];

var sum_counts = 0;

function clearData() {
    emp_names = [];
    emp_counts = [];

    sum_counts = 0;
}


//创建ECharts图表方法  
function DrawEChartEmp() {
    $("#mainEmp").css("height", 500);
    myChartEmp = echarts.init(document.getElementById('mainEmp'), 'chalk');

    optionEmp = {
        title: {
            text: '拓展排名'
        },
        legend: {
            top: 20,
            data: ['发展数量'],
            selected: {
                '发展数量': true
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
                        if (hh < 500) {
                            hh = 500;
                        }
                        $("#mainEmp").css("height", hh);
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

                        table += '<tr><td>合计</td><td>' + sum_counts + '</td></tr>';
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
                //margin: 2,
                //textStyle: {
                //    color: "#99ff99"
                //}
            },
            data: []
        },
        visualMap: {
            top: 20,
            right: 40,
            pieces: [
            { min: 100 }, // 不指定 max，表示 max 为无限大（Infinity）。
            { min: 20, max: 50 },
            { min: 10, max: 20 },
            { min: 5, max: 10, },
            //{value: 123, label: '123（自定义特殊颜色）', color: 'grey'}, // 表示 value 等于 123 的情况。
            { max: 5 }     // 不指定 min，表示 min 为无限大（-Infinity）。
            ],
            textStyle: {
                color: "#ffffff"
            },
            color: ['#d94e5d', '#eac736', '#50a3ba']
        },
        series: [
            {
                type: 'bar',
                name: '拓展数量',
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
    myChartEmp.setOption(optionEmp);
}

        

//window.setInterval("doserch()", "60000");
//window.setInterval("getDateOrder_status()", "60000");
