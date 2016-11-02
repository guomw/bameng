﻿/// <reference path="../plugins/switchery/switchery.js" />
/// <reference path="../plugins/summernote/summernote.min.js" />
/// <reference path="../plugins/sweetalert/sweetalert.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />
/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
**/

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




var articleHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    picDir: "article/img",
    dataId: hotUtil.getQuery("articleid"),
    loadData: function () {
        var self = this;
        var postData = {
            action: "GetArticleInfo",
            articleid: this.dataId
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200 && ret.data) {
                    $("#articleTitle").val(ret.data.ArticleTitle);
                    $("#articleIntro").val(ret.data.ArticleIntro);
                    $("#articleTop").setChecked(ret.data.EnableTop == 1);
                    $("#articlePublish").setChecked(ret.data.EnablePublish == 1);
                    $("#txtcover").val(ret.data.ArticleCover);
                    articleHelper.setEditContent(ret.data.ArticleBody);
                }
            }
            self.initCheck();
            hotUtil.loading.close();
        });
    },
    edit: function () {
        var TargetId = 0;
        $("input[name='radioInline']").each(function (i, v) {
            if ($(this).attr("data-check") == "true")
                TargetId = $(this).val();
        });
        var self = this;

        this.upload(function () {
            var postData = {
                action: "EditArticle",
                articleid: articleHelper.dataId,
                top: $("#articleTop").attr("checked") ? 1 : 0,
                publish: $("#articlePublish").attr("checked") ? 1 : 0,
                title: hotUtil.encode($("#articleTitle").val()),
                intro: hotUtil.encode($("#articleIntro").val()),
                content: hotUtil.encode(articleHelper.getEditContent()),
                targetid: TargetId,
                cover: $("#txtcover").val()
            }
            hotUtil.loading.show();
            hotUtil.ajaxCall(articleHelper.ajaxUrl, postData, function (ret, err) {
                if (ret) {
                    if (ret.status == 200) {
                        swal("提交成功", "", "success");
                        $("#signupForm")[0].reset();
                    }
                }
                hotUtil.loading.close();
            });
        });
    },
    getEditContent: function () {
        return $(".summernote").code();
    },
    setEditContent: function (content) {
        $(".summernote").code(content)
    },
    initCheck: function () {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elems.forEach(function (html) {
            var switchery = new Switchery(html);
        });
    },
    upload: function (callback) {
        if (!hotUtil.isNullOrEmpty($("#uploadfile").val())) {
            hotUtil.loading.show();
            hotUtil.uploadImg("uploadfile", this.picDir, function (url) {
                hotUtil.loading.close();
                if (url) {
                    $("#txtcover").val(url);
                    callback();

                }
                else
                    swal("图片上传失败", "请检查图片格式是否正确", "warning");
            });
        }
        else
            callback();
    }
};



$(document).ready(function () {
    $("#articleTop,#articlePublish").change(function () {
        if ($(this).attr("checked"))
            $(this).setChecked(false);
        else
            $(this).setChecked(true);
    });

    $("input[name='radioInline']").change(function () {
        $("input[name='radioInline']").removeAttr("data-check");
        if (!$(this).attr("data-check")) {
            $(this).attr("data-check", "true");
        }
    });

    $('input[type="file"]').prettyFile();
    $(".summernote").summernote({ lang: "zh-CN" });

    articleHelper.loadData();

    var e = "<i class='fa fa-times-circle'></i> ";
    $("#signupForm").validate({
        rules: {
            articleTitle: {
                required: !0,
                minlength: 2
            },
            articleIntro: "required"
        },
        messages: {
            articleTitle: {
                required: e + "请输入名称",
                minlength: e + "联系人必须两个字符以上"
            },
            articleIntro: e + "请输入您的手机号码"
        },
        submitHandler: function (form) {
            articleHelper.edit();
        }
    });
});

