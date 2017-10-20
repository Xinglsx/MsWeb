//淘宝客一件粘贴功能实现
function analysisShareText() {
    var content = $('#inputShareText').val();
    if (content.length > 0) {
        var startIndex = 0;
        var endIndex = content.indexOf("【在售价】") - 1;
        if(endIndex >0 && endIndex > startIndex){
            $('#inputGoodsDescription').val(content.substring(startIndex, endIndex));
        }

        startIndex = content.indexOf("【在售价】") + 5;
        endIndex = content.indexOf("【券后价】") - 2;
        if(startIndex  >= 0 && endIndex > startIndex) {
            $('#inputPrice').val("￥" + content.substring(startIndex, endIndex));
        }

        startIndex = content.indexOf("【券后价】") + 5;
        endIndex = content.indexOf("【下单链接】") - 2;
        if(startIndex  >= 0 && endIndex > startIndex) {
            $('#inputCouponPrice').val("￥" + content.substring(startIndex, endIndex));
        }

        startIndex = content.indexOf("【下单链接】") + 6;
        endIndex = content.indexOf("-----") - 1;
        if(startIndex  >= 0 && endIndex >0 && endIndex > startIndex) {
            $('#inputCouponLink').val(content.substring(startIndex, endIndex));
        }

        startIndex = content.indexOf("复制这条信息") + 7;
        endIndex = content.indexOf("打开【手机淘宝】") - 2;
        if(startIndex  >= 0 && endIndex >0 && endIndex > startIndex) {
            $('#inputZkl').val(content.substring(startIndex, endIndex));
        }
    }
    return false;
}
//保存草稿功能实现
function savrRecommandGoods() {
    var imageBase64 = $("#showImage")[0].src.toString();
    imageBase64 = imageBase64.substring(imageBase64.indexOf("base64,")+7, imageBase64.length);
    var data = {
        description: $("#inputGoodsDescription").val(),
        link: $("#inputCouponLink").val(),
        command: $("#inputZkl").val(),
        oldprice: $("#inputPrice").val(),
        price: $("#inputCouponPrice").val(),
        expirydate: $("#inputExpiryDate").val(),
        reason: $("#inputRecommendReason").val(),
        state: $(this).id == "btnSaveDraft" ? 0 : 1,
        image: imageBase64,
    }

    $.ajax({
        url: "/api/RecommandGoods/SaveGoodsInfo",
        type: "post",
        data: {goodsInfo:data},
        success: function (rst) {
            if (rst.code > 0) {
                //写cookie、localStorage
                location.href = "/Home";
            } else {
                alert(rst.message);
            }
        }
    });

    return false;
}

function imgChange(e) {
    console.info(e.target.files[0]);//图片文件
    var dom = $("input[id^='inputUploadImage']")[0];
    console.info(dom.value);//这个是文件的路径 C:\fakepath\icon (5).png
    console.log(e.target.value);//这个也是文件的路径和上面的dom.value是一样的
    var reader = new FileReader();
    reader.onload = (function (file) {
        return function (e) {
            console.info(this.result); //这个就是base64的数据了
            var sss = $("#showImage");
            $("#showImage")[0].src = this.result;
        };
    })(e.target.files[0]);
    reader.readAsDataURL(e.target.files[0]);

}
