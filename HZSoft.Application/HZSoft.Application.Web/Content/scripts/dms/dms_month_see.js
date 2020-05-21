var echarts;
var username = '';
var daycount = 0;
var axisDatas = []; //x轴

var see_counts = [];//查看个数
var obtain_counts = [];//获取个数
var record_counts = [];//跟进个数
var followstates = [];//状态个数
var sum_see_counts = 0;
var sum_obtain_counts = 0;
var sum_record_counts = 0;
var sum_followstates = 0;

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
        see_counts[i] = 0;
        obtain_counts[i] = 0;
    }
}

function clearData() {
    see_counts = [];//查看个数
    obtain_counts = [];//获取个数
    record_counts = [];//跟进个数
    followstates = [];//状态个数
    sum_see_counts = 0;
    sum_obtain_counts = 0;
    sum_record_counts = 0;
    sum_followstates = 0;
    getDateCount();
}


//赋值查看次数
function getMonthSeeData(result) {
    for (var j = 0; j < result.length; j++) {
        var orderDate = parseInt(result[j].orderdate) - 1; //从1日开始，-1到角标0
        var seecount = parseFloat(result[j].seecount.toFixed());
        see_counts[orderDate] = seecount;
        sum_see_counts += seecount;
    }
}

//赋值获取次数
function getMonthObtainData(result) {
    for (var j = 0; j < result.length; j++) {
        var orderDate = parseInt(result[j].orderdate) - 1; //从1日开始，-1到角标0
        var obtaincount = parseFloat(result[j].obtaincount.toFixed());
        obtain_counts[orderDate] = obtaincount;
        sum_obtain_counts += obtaincount;
    }
}

//赋值跟进次数
function getMonthRecordData(result) {
    for (var j = 0; j < result.length; j++) {
        var orderDate = parseInt(result[j].orderdate) - 1; //从1日开始，-1到角标0
        var recordcount = parseFloat(result[j].recordcount.toFixed());
        record_counts[orderDate] = recordcount;
        sum_record_counts += recordcount;
    }
}

//赋值跟进状态
function getMonthFollowStateData(result) {
    for (var j = 0; j < result.length; j++) {
        var followstatename = result[j].followstatename;
        var followstate = result[j].followstate;
        var count = result[j].count;

        var val = {
            value: count,
            state: followstate,
            name: followstatename + "：" + count
        };
        followstates.push(val);
    }
}

//创建ECharts图表方法  
function DrawEChartMonth() {
    $("#main").css("height", 700);
    option = {
        title: {
            text: username + '月报指标',
            subtext: '获取总数：' + sum_obtain_counts + '\n查看总数：' + sum_see_counts + '\n跟进总数：' + sum_record_counts
        },
        legend: {
            top: 20,
            data: ['获取个数', '查看次数', '跟进条数'],
            selected: {
                '获取个数': true,
                '查看次数': false,
                '跟进条数': false
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

                        table += '<tr><td>合计</td><td>' + sum_see_counts + '</td><td>' + sum_obtain_counts + '</td><td>' + sum_record_counts + '</td></tr>';
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
                name: '获取个数',
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
                data: obtain_counts
            },
            {
                type: 'bar',
                name: '查看次数',
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
                data: see_counts
            },
            {
                type: 'bar',
                name: '跟进条数',
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
                data: record_counts
            },
            {
                name: '合作状态',
                type: 'pie',
                center: ['70%', '20%'],
                radius: '20%',
                z: 100,
                data: followstates,
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };
    var myChart = echarts.init(document.getElementById('main'), 'macarons');
    myChart.setOption(option);
    window.onresize = myChart.resize;//随屏幕的大小改变自适应

    myChart.on('click', function (params) {
        if (params.seriesIndex == 0) {
            //关闭公司库搜索
            top.$.removeTab('9ebff981-fa67-4aab-9570-6f10579526a8');
            //打开公司库搜索
            top.tablist.newTab({
                id: '9ebff981-fa67-4aab-9570-6f10579526a8', title: '获取客户详情', closed: true, icon: "fa fa-bank",
                url: "/CustomerManage/Ku_Company/MyKu_CompanyIndex?ObtainDate=" + year + "-" + month + "-" + params.name
            });                
        }
        else if (params.seriesIndex == 1) {
            //关闭公司库搜索
            top.$.removeTab('163dd3db-5a61-473d-9527-e3150159cf3c');
            //打开公司库搜索
            top.tablist.newTab({
                id: '163dd3db-5a61-473d-9527-e3150159cf3c', title: '查看客户详情', closed: true, icon: "fa fa-bank",
                url: "/CustomerManage/Ku_CompanySee/Ku_CompanySeeIndex?SeeDate=" + year + "-" + month + "-" + params.name
            });
        }
        else if (params.seriesIndex == 2) {
            //关闭公司库搜索
            top.$.removeTab('9ebff981-fa67-4aab-9570-6f10579526a8');
            //打开公司库搜索
            top.tablist.newTab({
                id: '9ebff981-fa67-4aab-9570-6f10579526a8', title: '跟进客户详情', closed: true, icon: "fa fa-bank",
                url: "/CustomerManage/Ku_Company/MyKu_CompanyIndex?ModifyDate=" + year + "-" + month + "-" + params.name
            });
        }
        else if (params.seriesIndex == 3) {
            //关闭公司库搜索
            top.$.removeTab('9ebff981-fa67-4aab-9570-6f10579526a8');
            //打开公司库搜索
            top.tablist.newTab({
                id: '9ebff981-fa67-4aab-9570-6f10579526a8', title: '合作状态详情', closed: true, icon: "fa fa-bank",
                url: "/CustomerManage/Ku_Company/MyKu_CompanyIndex?syear=" + year + "&smonth=" + month + "&FollowState=" + params.data.state
            });
        }


    });
    
}