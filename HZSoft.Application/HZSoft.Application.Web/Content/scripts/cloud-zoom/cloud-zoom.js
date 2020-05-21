/*
	imgbox ��ǰͼƬ����
	hoverbox �����������
	l ��ǰͼƬ�����
	t ��ǰͼƬ�Ͼ���
	x ������X��
	y ������Y��
	h_w �������ͼƬ����
	h_h �������ͼƬ��߶�
	showbox չʾ��ͼ����
*/
function Zoom(imgbox, hoverbox, l, t, x, y, h_w, h_h, showbox) {
    var moveX = x - l - (h_w / 2);
    //����������
    var moveY = y - t - (h_h / 2);
    //����������
    if (moveX < 0) { moveX = 0 }
    if (moveY < 0) { moveY = 0 }
    if (moveX > imgbox.width() - h_w) { moveX = imgbox.width() - h_w }
    if (moveY > imgbox.height() - h_h) { moveY = imgbox.height() - h_h }
    //�ж����ʹ�䲻�ܳ�ͼƬ��
    var zoomX = showbox.width() / imgbox.width()
    //��ͼƬ����
    var zoomY = showbox.height() / imgbox.height()

    showbox.css({ left: -(moveX * zoomX), top: -(moveY * zoomY) })
    hoverbox.css({ left: moveX, top: moveY })
    //ȷ��λ��

}
function Zoomhover(imgbox, hoverbox, showbox) {
    var l = imgbox.offset().left;
    var t = imgbox.offset().top;
    var w = hoverbox.width();
    var h = hoverbox.height();
    var time;
    $(".probox img,.hoverbox").mouseover(function (e) {
        var x = e.pageX;
        var y = e.pageY;
        $(".hoverbox,.showbox").show();
        hoverbox.css("opacity", "0.3")
        time = setTimeout(function () { Zoom(imgbox, hoverbox, l, t, x, y, w, h, showbox) }, 1)
    }).mousemove(function (e) {
        var x = e.pageX;
        var y = e.pageY;
        time = setTimeout(function () { Zoom(imgbox, hoverbox, l, t, x, y, w, h, showbox) }, 1)
    }).mouseout(function () {
        showbox.parent().hide()
        hoverbox.hide();
    })
}