$(function () {
    var operate = $('#page-operate').val();
    if (operate) {
        var item = operate.split(",");
        for (var i = 0; i < item.length; i++) {
            if (item[i].indexOf("#") == -1)
                $('.' + item[i]).removeClass("page-role");
        }
    }
});
