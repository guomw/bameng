/// <reference path="demoLeftData.js" />
/// <reference path="jquery.min.js" />
/// <reference path="plugins/hot/Jquery.util.js" />
/// <reference path="plugins/metisMenu/jquery.metisMenu.js" />
/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

var menusulTemplate = '<ul class="nav {levelClass}">{li}</ul>'
var menusliTemplate = '<li><a class="{menuItemClass}" href="{linkUrl}"><i class="fa {icons}"></i><span class="nav-label">{menuName}</span><span class="fa {arrow}"></span></a>{childMenus}</li>';

$(function () {

    var authority = "";
    var firstList;
    var menuListProvider = {
        menuList: [],
        getChildMenu: function (parentid) {
            var resultList = [];
            $.each(this.menuList, function (o, item) {
                if (authority == "" || authority.indexOf("|" + item.ItemCode + "|") >= 0) {
                    if (item.ItemParentCode == parentid && item.ItemShow == 1) {
                        resultList.push(item);
                    }
                }
            });
            return resultList;
        }
    };


    //默认菜单，如果没有设置菜单的话，则显示默认菜单index_v1.html
    function defaultMenus() {
        menuListProvider.menuList = getDemoData();
    }


    function LoadMenu() {
        if (menuListProvider.menuList == null || menuListProvider.menuList.length == 0) {
            //请求头
            var header = {
                action: "getleftmenu"
            }
            hotUtil.ajaxCall(hotUtil.ajaxUrl, null, function (ret, err) {
                if (ret != null && ret.code == 200) {
                    menuListProvider.menuList = ret.result;
                }

                outputFirst();
            }, header);
        }
        else {
            outputFirst();
        }
    }

    //输出第一级菜单
    function outputFirst() {
        firstList = menuListProvider.getChildMenu("0");
        var appendHtml = "";
        $.each(firstList, function (o, item) {
            var tempHtml = menusliTemplate;
            tempHtml = tempHtml.replace("{menuName}", item.ItemNavLabel);
            tempHtml = tempHtml.replace("{linkUrl}", item.ItemUrl);
            tempHtml = tempHtml.replace("{icons}", item.ItemIcons);
           
            var second = outputChild(item.ItemCode, 1);
            if (hotUtil.isNullOrEmpty(second)) {
                tempHtml = tempHtml.replace("{menuItemClass}", "J_menuItem");
                tempHtml = tempHtml.replace("{arrow}", "");
            }
            else {
                tempHtml = tempHtml.replace("{menuItemClass}", "");
                tempHtml = tempHtml.replace("{arrow}", "arrow");
            }
            tempHtml = tempHtml.replace("{childMenus}", outputChild(item.ItemCode, 1));
            appendHtml += tempHtml;
        });
        $("#side-menu").append(appendHtml);
        $("#side-menu").metisMenu();
        refreshmenu();//刷新菜单事件，只有在动态加载菜单时，才需执行此函数，否则，加载的菜单，点击之后，无法再右侧打开
    }
    //递归输出子菜单，最多两级
    function outputChild(parentid, level) {
        if (level > 2)
            return "";
        var levelClass = level == 1 ? "nav-second-level" : "nav-third-level";
        level = level + 1;
        var childHtml = "";
        var childList = menuListProvider.getChildMenu(parentid);
        if (childList == null || childList.length == 0)
            return childHtml;
        $.each(childList, function (o, item) {
            var tempHtml = menusulTemplate;
            tempHtml = tempHtml.replace("{li}", menusliTemplate);
            tempHtml = tempHtml.replace("{levelClass}", levelClass);
            tempHtml = tempHtml.replace("{icons}", item.ItemIcons);
            tempHtml = tempHtml.replace("{menuName}", item.ItemNavLabel);
            tempHtml = tempHtml.replace("{linkUrl}", item.ItemUrl);

            var child = outputChild(item.ItemCode, level);
            if (hotUtil.isNullOrEmpty(child)) {
                tempHtml = tempHtml.replace("{menuItemClass}", "J_menuItem");
                tempHtml = tempHtml.replace("{arrow}", "");
            }
            else {
                tempHtml = tempHtml.replace("{menuItemClass}", "");
                tempHtml = tempHtml.replace("{arrow}", "arrow");
            }

            tempHtml = tempHtml.replace("{childMenus}", outputChild(item.ItemCode, level));
            childHtml += tempHtml;
        });
        return childHtml;
    }


    defaultMenus();
    LoadMenu();

});