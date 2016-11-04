/// <reference path="../plugins/sweetalert/sweetalert.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />

/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

var focusHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    loaclData: [],
    type: hotUtil.getQuery("type"),
    pageIndex: 1,
    reset: null,
    loadList: function (page) {
        var self = this;
        self.loaclData = [];
        this.pageIndex = page;
        var postData = {
            action: "GetFocusPicList",
            pageIndex: page,
            pageSize: 20,
            key: $("#keyword").val(),            
            type: this.type
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    if (ret.data) {
                        var listhtml = "";
                        self.loaclData = ret.data.Rows;
                        $.each(ret.data.Rows, function (i, item) {
                            var tempHtml = $("#templist").html();
                            tempHtml = tempHtml.replace("{Title}", item.Title);
                            tempHtml = tempHtml.replace(/{ID}/gm, item.ID);
                            tempHtml = tempHtml.replace("{Description}", item.Description);
                            tempHtml = tempHtml.replace(/{LinkUrl}/g, item.LinkUrl);
                            tempHtml = tempHtml.replace("{Sort}", item.Sort);
                            tempHtml = tempHtml.replace("{CreateTime}", item.CreateTime);                            
                            if (!hotUtil.isNullOrEmpty(item.PicUrl))
                                tempHtml = tempHtml.replace("{PicUrl}", item.PicUrl);
                            else
                                tempHtml = tempHtml.replace("{PicUrl}", "/static/img/bg.png");
                            
                            tempHtml = tempHtml.replace("{IsEnable}", item.IsEnable == 1 ? "<span style='color:red;'>启用</span>" : "禁用")
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);

                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init((p) => {
                            goTo(p, function (page) {
                                focusHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    search: function () {
        focusHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword").val("");
        focusHelper.loadList(1);
    },
    getModel: function (dataId) {
        var model = null;
        if (!hotUtil.isNullOrEmpty(dataId) && this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.ID == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    edit: function () {
        var param = hotUtil.serializeForm("#signupForm .form-control");
        param.action = "EditFocusPic";
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("提交成功！", "", "success");
                    focusHelper.loadList(focusHelper.pageIndex);
                    $(".close").click();
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
    },
    del: function (dataId) {
        swal({
            title: "您确定要删除这条信息吗",
            text: "删除后将无法恢复，请谨慎操作！",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "删除",
            closeOnConfirm: false,
        }, function () {
            var param = {
                action: "DeleteUser",
                userid: dataId
            }
            hotUtil.loading.show();
            hotUtil.ajaxCall(focusHelper.ajaxUrl, param, function (ret, err) {
                if (ret) {
                    if (ret.status == 200) {
                        swal("删除成功！", "您已经永久删除了这条信息。", "success");
                        focusHelper.loadList(focusHelper.pageIndex);
                    }
                    else {
                        swal(ret.statusText, "", "warning");
                    }
                }
                hotUtil.loading.close();
            });
        });
    },
    updateActive: function (dataId, active) {
        var param = {
            action: "UpdateUserActive",
            userid: dataId,
            active: parseInt(active) == 1 ? 0 : 1
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("提交成功！", "", "success");
                    focusHelper.loadList(focusHelper.pageIndex);
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
    },
    dialog: function (dataId) {
        if (this.reset)
            this.reset.resetForm();
        var data = this.getModel(dataId);
        if (data != null) {
            $("#modal-title").text("编辑轮播图");
            $("#userid").val(dataId);
            $("#username").val(data.RealName);
            $("#usernickname").val(data.NickName);
            $("#usermobile").val(data.UserMobile);
        }
        else {
            $("#modal-title").text("添加轮播图");
            $("#signupForm input").val("");
        }
    },
    goTab: function (dataId) {
        var data = this.getModel(dataId);
        hotUtil.newTab('admin/userdetails.html?userid=' + data.UserId + '&type=' + this.isAlly + '', (this.isAlly == 1 ? "盟友" : "盟主") + '详情-【' + data.RealName + '】');
    },
    pageInit: function () {
        focusHelper.loadList(focusHelper.pageIndex);
        focusHelper.validate();

        if (this.isAlly == 1) {
            $("#btnUser").hide();
            $(".allyText").text("客户信息提交量");
            $("#allyLable").text("盟友名称")
        }
    },
    validate: function () {
        var e = "<i class='fa fa-times-circle'></i> ";
        this.reset = $("#signupForm").validate({
            rules: {
                username: {
                    required: !0,
                    minlength: 2
                },
                usermobile: "required",
                userloginname: {
                    required: !0,
                    minlength: 5
                },
                usernickname: "required",
                password: {
                    minlength: 6
                },
                confirm_password: {
                    minlength: 6,
                    equalTo: "#password"
                }
            },
            messages: {
                username: {
                    required: e + "请输入" + (focusHelper.isAlly == 1 ? "盟友" : "盟主") + "名称",
                    minlength: e + "联系人必须两个字符以上"
                },
                usermobile: e + "请输入您的手机号码",
                userloginname: {
                    required: e + "请输入您的登录名",
                    minlength: e + "登录名必须5个字符以上"
                },
                usernickname: e + "请输入昵称",
                password: {
                    minlength: e + "密码必须6个字符以上"
                },
                confirm_password: {
                    minlength: e + "密码必须6个字符以上",
                    equalTo: e + "两次输入的密码不一致"
                }
            },
            submitHandler: function (form) {
                focusHelper.edit();
            }
        })
    }
};

$.validator.setDefaults({
    highlight: function (e) {
        $(e).closest(".form-group").removeClass("has-success").addClass("has-error")
    },
    success: function (e) {
        e.closest(".form-group").removeClass("has-error").addClass("has-success")
    },
    errorElement: "span",
    errorPlacement: function (e, r) {
        e.appendTo(r.is(":radio") || r.is(":checkbox") ? r.parent().parent().parent() : r.parent())
    },
    errorClass: "help-block m-b-none",
    validClass: "help-block m-b-none"
});

$(function () {
    focusHelper.pageInit();
});


