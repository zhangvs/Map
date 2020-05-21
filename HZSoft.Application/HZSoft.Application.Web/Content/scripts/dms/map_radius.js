
var mapStyle11 = {
    styleJson: []
};
var mapStyle22 = {
    styleJson: [{
        "featureType": "all",
        "elementType": "all",
        "stylers": {
            "lightness": 47,
            "saturation": -100
        }
    }, {
        "featureType": "highway",
        "elementType": "geometry.fill",
        "stylers": {
            "color": "#ffffff"
        }
    }, {
        "featureType": "poi",
        "elementType": "labels.icon",
        "stylers": {
            "visibility": "off"
        }
    }, {
        "featureType": "road",
        "elementType": "labels",
        "stylers": {
            "visibility": "off"
        }
    }]
};
var mapStyle33 = {
    'styleJson': [{
        "featureType": "water",
        "elementType": "all",
        "stylers": {
            "color": "#021019"
        }
    }, {
        "featureType": "highway",
        "elementType": "geometry.fill",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "highway",
        "elementType": "geometry.stroke",
        "stylers": {
            "color": "#147a92"
        }
    }, {
        "featureType": "arterial",
        "elementType": "geometry.fill",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "arterial",
        "elementType": "geometry.stroke",
        "stylers": {
            "color": "#0b3d51"
        }
    }, {
        "featureType": "local",
        "elementType": "geometry",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "land",
        "elementType": "all",
        "stylers": {
            "color": "#08304b"
        }
    }, {
        "featureType": "railway",
        "elementType": "geometry.fill",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "railway",
        "elementType": "geometry.stroke",
        "stylers": {
            "color": "#08304b"
        }
    }, {
        "featureType": "subway",
        "elementType": "geometry",
        "stylers": {
            "lightness": -70
        }
    }, {
        "featureType": "building",
        "elementType": "geometry.fill",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "all",
        "elementType": "labels.text.fill",
        "stylers": {
            "color": "#857f7f"
        }
    }, {
        "featureType": "all",
        "elementType": "labels.text.stroke",
        "stylers": {
            "color": "#000000"
        }
    }, {
        "featureType": "building",
        "elementType": "geometry",
        "stylers": {
            "color": "#022338"
        }
    }, {
        "featureType": "green",
        "elementType": "geometry",
        "stylers": {
            "color": "#062032"
        }
    }, {
        "featureType": "boundary",
        "elementType": "all",
        "stylers": {
            "color": "#1e1c1c"
        }
    }, {
        "featureType": "manmade",
        "elementType": "all",
        "stylers": {
            "color": "#022338"
        }
    }]
};


function getMap() {
    return myChart.getModel().getComponent('bmap').getBMap();
    //bmap.addControl(new BMap.MapTypeControl());
}


//画圈
function renderBrushed(params) {
    var bmap = getMap();
    //bmap.disableDragging();     //禁止拖拽

    setTimeout(getBrushed(params), 2000)
    //$('.areaCountTR').on('click',function(){

    //});
    //var areas = params.batch[0].areas;
    //if (areas.length == 0) {
    //    setTimeout(function () {
    //        bmap.enableDragging();   //两秒后开启拖拽
    //    }, 5000);
    //}
}
function getBrushed(params) {
    var selectedItems = [];
    var counts = 0;
    var selecteds = params.batch[0].selected;
    for (var i = 0; i < selecteds.length; i++) {
        var mainSeries = selecteds[i];
        for (var j = 0; j < mainSeries.dataIndex.length; j++) {
            var rawIndex = mainSeries.dataIndex[j];
            var dataItem = seriesData[i][rawIndex];
            counts += dataItem[4];//counts
            selectedItems.push(dataItem[2]);//ids
        }
    }
    if (counts > 0) {
        //询问框
        layer.msg('选择区域内一共有' + counts + '家公司，是否继续查看详情？', {
            time: 0 //不自动关闭
            , btn: ['确定', '关闭']
            , yes: function (index) {
                layer.close(index);
                //打开新页签
                top.tablist.newTab({
                    id: counts, title: '选取详情', closed: true, icon: "fa fa-bank",
                    url: "/CustomerManage/Ku_Company/Ku_CompanyIndex?LocationIds=" + selectedItems
                });
            }
        });
    }
    
}

function toggleFullScreen() {
    if (!document.fullscreenElement && // alternative standard method
        !document.mozFullScreenElement && !document.webkitFullscreenElement) {// current working methods
        if (document.documentElement.requestFullscreen) {
            document.documentElement.requestFullscreen();
        } else if (document.documentElement.mozRequestFullScreen) {
            document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullscreen) {
            document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
        $(".titlePanel").hide();
        document.getElementById("container").style.height = window.screen.height + "px";
    } else {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        }

    }
}
//监听退出全屏事件
window.onresize = function () {
    if (!checkFull()) {
        //要执行的动作
        document.getElementById("container").style.height = $(window).height() - 60 + "px";
        $(".titlePanel").show();
    }
}
function checkFull() {
    var isFull = document.fullscreenEnabled || window.fullScreen || document.webkitIsFullScreen || document.msFullscreenEnabled;
    //to fix : false || undefined == undefined
    if (isFull === undefined) { isFull = false; }
    return isFull;
}