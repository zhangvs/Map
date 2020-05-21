var echarts;
var emp_see_names = [];
var emp_obtain_names = [];
var emp_record_names = [];

var sum_see_counts = 0;
var sum_obtain_counts = 0;
var sum_record_counts = 0;

function clearData() {
    emp_see_names = [];
    emp_obtain_names = [];
    emp_record_names = [];

    sum_obtain_counts = 0;
    sum_record_counts = 0;
    sum_see_counts = 0;
}

//创建ECharts图表方法  
function DrawEChartObtain() {
    $("#mainObtain").css("height", 500);
    myChartObtain = echarts.init(document.getElementById('mainObtain'), 'chalk');

    optionObtain = {
        title: {
            text: '员工客户数'
        },
        legend: {
            top: 20,
            data: ['客户数'],
            selected: {
                '客户数': true
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
                        if (hh < 500) {
                            hh =500;
                        }
                        $("#mainObtain").css("height", hh);
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

                        table += '<tr><td>合计</td><td>' + sum_obtain_counts + '</td></tr>';
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
                name: '客户数',
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
    myChartObtain.setOption(optionObtain);

    myChartObtain.on('click', function (params) {
        //关闭公司库搜索
        top.$.removeTab('9ebff981-fa67-4aab-9570-6f10579526a8');
        //打开公司库搜索
        top.tablist.newTab({
            id: '9ebff981-fa67-4aab-9570-6f10579526a8', title: '获取客户详情', closed: true, icon: "fa fa-bank",
            url: "/CustomerManage/Ku_Company/MyKu_CompanyIndex?ObtainUserName=" + escape(params.name) +
                "&ObtainStartTime=" + $("#StartTime").val() + "&ObtainEndTime=" + $("#EndTime").val()
        });

    });
}

        
//创建ECharts图表方法  
function DrawEChartSee() {
    $("#mainSee").css("height", 500);
    myChartSee = echarts.init(document.getElementById('mainSee'), 'chalk');

    optionSee = {
        title: {
            text: '员工查看数'
        },
        legend: {
            top: 20,
            data: ['查看数'],
            selected: {
                '查看数': true
            }
        },
        grid: {
            bottom: 200
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
                        $("#mainSee").css("height", hh);
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

                        table += '<tr><td>合计</td><td>' + sum_see_counts + '</td></tr>';
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
                name: '查看数',
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
    myChartSee.setOption(optionSee);

    myChartSee.on('click', function (params) {
        //关闭公司库搜索
        top.$.removeTab('163dd3db-5a61-473d-9527-e3150159cf3c');
        //打开公司库搜索
        top.tablist.newTab({
            id: '163dd3db-5a61-473d-9527-e3150159cf3c', title: '查看客户详情', closed: true, icon: "fa fa-bank",
            url: "/CustomerManage/Ku_CompanySee/Ku_CompanySeeIndex?SeeUserName=" + escape(params.name) +
                "&StartTime=" + $("#StartTime").val() + "&EndTime=" + $("#EndTime").val()
        });
    });

}

//创建ECharts图表方法  
function DrawEChartRecord() {
    $("#mainRecord").css("height", 500);
    myChartRecord = echarts.init(document.getElementById('mainRecord'), 'chalk');

    optionRecord = {
        title: {
            text: '员工跟进数'
        },
        legend: {
            top: 20,
            data: ['跟进数'],
            selected: {
                '跟进数': true
            }
        },
        grid: {
            bottom: 200
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
                        $("#mainRecord").css("height", hh);
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

                        table += '<tr><td>合计</td><td>' + sum_record_counts + '</td></tr>';
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
                name: '跟进数',
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
    myChartRecord.setOption(optionRecord);

    myChartRecord.on('click', function (params) {
        //关闭公司库搜索
        top.$.removeTab('9ebff981-fa67-4aab-9570-6f10579526a8');
        //打开公司库搜索
        top.tablist.newTab({
            id: '9ebff981-fa67-4aab-9570-6f10579526a8', title: '跟进客户详情', closed: true, icon: "fa fa-bank",
            url: "/CustomerManage/Ku_Company/MyKu_CompanyIndex?ObtainUserName=" + escape(params.name) +
                "&ModifyStartTime=" + $("#StartTime").val() + "&ModifyEndTime=" + $("#EndTime").val()
        });

    });
}