
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

function init() {
    initMap();
}

function getMap() {
    return myChart.getModel().getComponent('bmap').getBMap();
    //bmap.addControl(new BMap.MapTypeControl());
}

function initMap() {
    var top_left_navigation = new BMap.NavigationControl({
        type: BMAP_NAVIGATION_CONTROL_SMALL
    });
    var map = getMap();
    map.addControl(top_left_navigation);
    map.disableDoubleClickZoom();
    map.addControl(new BMap.MapTypeControl({
        mapTypes: [
            BMAP_NORMAL_MAP,
            BMAP_HYBRID_MAP
        ],
        offset: new BMap.Size(150, 5)
    }));
    //map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放

    var myDis = new BMapLib.DistanceTool(map);
    map.addEventListener("load", function () {
        myDis.open();  //开启鼠标测距
        //myDis.close();  //关闭鼠标测距大
    });

    return map;
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

setTimeout(function () {
    madeBoundary();
}, 2000);

//区域图
function madeBoundary() {
    var datas = new Array("兰山区-#e19ee6", "罗庄区-#1ad3da", "临沂市河东区-#fff492", "沂南县-#1199cc",
        "郯城县-#b95817", "沂水县-#39d0a4", "兰陵县-#98d057", "费县-#fff492", "平邑县-#8bbb53", "莒南县-#98e800", "蒙阴县-#666ddd", "临沭县-#03a9f4");
    var bdary = new BMap.Boundary();
    for (var i = 0; i < datas.length; i++) {
        getBoundary(datas[i], bdary);

    }
    addlabel();
}
//设置区域图

function getBoundary(data, bdary) {
    var map = getMap();
    data = data.split("-");
    bdary.get(data[0], function (rs) {       //获取行政区域
        var count = rs.boundaries.length; //行政区域的点有多少个

        var pointArray = [];
        for (var i = 0; i < count; i++) {
            var ply = new BMap.Polygon(rs.boundaries[i], { strokeWeight: 2, strokeColor: "#ff0000", fillOpacity: 0.5, fillColor: data[1] }); //建立多边形覆盖物

            map.addOverlay(ply);  //添加覆盖物

        }
    });
}


function addlabel() {
    var map = getMap();
    var pointArray = [
      new BMap.Point(118.312243, 35.174845),  //兰山区
      new BMap.Point(118.297279, 34.964343),  //罗庄区
      new BMap.Point(118.517311, 35.127031),  //河东区
      new BMap.Point(118.417586, 35.536723),  //沂南县
      new BMap.Point(118.324431, 34.649855),  //郯城县
      new BMap.Point(118.609358, 35.914369),  //沂水县
      new BMap.Point(118.007509, 34.86262),  //兰陵县
      new BMap.Point(117.985838, 35.254971),  //费县
      new BMap.Point(117.682448, 35.43425),  //平邑县
      new BMap.Point(118.890079, 35.213123),   //莒南县
      new BMap.Point(118.036742, 35.74744),  //蒙阴县
      new BMap.Point(118.659445, 34.885484)   //临沭县
    ];
    var optsArray = [{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}];
    var labelArray = [];
    var contentArray = ["兰山区", "罗庄区", "河东区", "沂南县", "郯城县", "沂水县", "兰陵县", "费县", "平邑县", "莒南县", "蒙阴县", "临沭县"];
    for (var i = 0; i < pointArray.length; i++) {
        optsArray[i].position = pointArray[i];
        labelArray[i] = new BMap.Label(contentArray[i], optsArray[i]);
        labelArray[i].setStyle({
            color: "red",
            fontSize: "9px",
            backgroundColor: "0.05",
            border: "0",
            fontWeight: "bold"
        });
        map.addOverlay(labelArray[i]);
    }
}