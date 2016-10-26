/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />
/// <reference path="../plugins/layui/layui.js" />

/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


var pageIndex = hotUtil.getQuery("page");
var totalPage = 20;
var pageinate;



$(function () {

    loadShopList();


    function loadShopList() {
        var postData = {
            action: "getshoplist",
            pageIndex: pageIndex,
            pageSize: 20
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall("/handler/HQ.ashx", postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    window.console.log(ret);
                    if (ret.data) {
                        var listhtml = "";
                        $.each(ret.data.Rows, function (i, item) {
                            var tempHtml = $("#templist").html();
                            tempHtml = tempHtml.replace("{ShopID}", item.ShopID);
                            tempHtml = tempHtml.replace("{ShopName}", item.ShopName);
                            tempHtml = tempHtml.replace("{Contacts}", item.Contacts);
                            tempHtml = tempHtml.replace("{ShopProv}", item.ShopProv + item.ShopCity + item.ShopArea);
                            tempHtml = tempHtml.replace("{ContactWay}", item.ContactWay);
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);
                        alert(listhtml);

                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageCount, 7);
                        pageinate.init((p) => {
                            hotUtil.refreshPage(p);
                        });
                    }
                }
            }
            hotUtil.loading.close();

        });
    }
});
