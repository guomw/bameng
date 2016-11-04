﻿/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.LOGIC
{
    public class ArticleLogic
    {
        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <param name="AuthorIdentity">作者身份类型，0集团，1总店，2分店  3盟主 4盟友</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetArticleList(int AuthorId, int AuthorIdentity, SearchModel model)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.GetArticleList(AuthorId, AuthorIdentity, model);
            }
        }

        /// <summary>
        /// 编辑资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditArticle(ArticleModel model)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                if (model.ArticleId > 0)
                    return dal.UpdateArticle(model);
                else
                    return dal.AddArticle(model) > 0;
            }
        }

        /// <summary>
        /// 设置资讯置顶状态
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static bool SetArticleEnableTop(int articleId, bool enable)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.SetArticleEnableTop(articleId, enable);
            }
        }
        /// <summary>
        /// 设置资讯发布状态
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static bool SetArticleEnablePublish(int articleId, bool enable)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.SetArticleEnablePublish(articleId, enable);
            }
        }
        /// <summary>
        /// 删除资讯
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool DeleteArticle(int articleId)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.DeleteArticle(articleId);
            }
        }
        /// <summary>
        /// 获取资讯信息
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static ArticleModel GetModel(int articleId)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.GetModel(articleId);
            }
        }

        /// <summary>
        /// 设置资讯审核状态
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="status">-1 审核失败 1审核成功</param>
        /// <param name="remark">The remark.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SetArticleStatus(int articleId, int status,string remark)
        {
            using (var dal = FactoryDispatcher.ArticleFactory())
            {
                return dal.SetArticleStatus(articleId, status, remark);
            }
        }



    }
}
