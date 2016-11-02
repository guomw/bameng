﻿/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using BAMENG.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAMENG.MODEL;
using System.Data.SqlClient;
using BAMENG.CONFIG;
using HotCoreUtils.DB;
using System.Data;

namespace BAMENG.DAL
{
    public class ArticleDAL : AbstractDAL, IArticleDAL
    {

        private const string APP_SELECT = @"select A.ArticleId,A.AuthorId,a.AuthorName,a.AuthorIdentity,a.SendTargetId,a.SendType,a.ArticleSort,a.ArticleType,a.ArticleClassify
                                            ,a.ArticleTitle,a.ArticleIntro,a.ArticleCover,a.ArticleBody,a.EnableTop,a.EnablePublish,a.BrowseAmount,a.ArticleStatus,a.IsDel,a.IsRead,a.TopTime,a.UpdateTime,a.PublishTime,a.CreateTime
                                             from BM_ArticleList A with(nolock) where a.IsDel=0";


        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddArticle(ArticleModel model)
        {
            string strSql = @"insert into BM_ArticleList(AuthorId,AuthorName,AuthorIdentity,SendTargetId,SendType,ArticleSort,ArticleType,ArticleClassify
                                ,ArticleTitle,ArticleIntro,ArticleCover,ArticleBody,EnableTop,EnablePublish,ArticleStatus,TopTime,PublishTime)
                                values(@AuthorId,@AuthorName,@AuthorIdentity,@SendTargetId,@SendType,@ArticleSort,@ArticleType,@ArticleClassify
                                ,@ArticleTitle,@ArticleIntro,@ArticleCover,@ArticleBody,@EnableTop,@EnablePublish,@ArticleStatus,@TopTime,@PublishTime)";
            var param = new[] {
                        new SqlParameter("@AuthorId", model.AuthorId),
                        new SqlParameter("@AuthorName", model.AuthorName),
                        new SqlParameter("@AuthorIdentity", model.AuthorIdentity),
                        new SqlParameter("@SendTargetId", model.SendTargetId),
                        new SqlParameter("@SendType", model.SendType),
                        new SqlParameter("@ArticleSort", model.ArticleSort),
                        new SqlParameter("@ArticleType", model.ArticleType),
                        new SqlParameter("@ArticleClassify", model.ArticleClassify),
                        new SqlParameter("@ArticleTitle", model.ArticleTitle),
                        new SqlParameter("@ArticleIntro", model.ArticleIntro),
                        new SqlParameter("@ArticleCover", model.ArticleCover),
                        new SqlParameter("@ArticleBody",model.ArticleBody),
                        new SqlParameter("@EnableTop",model.EnableTop),
                        new SqlParameter("@EnablePublish",model.EnablePublish),
                        new SqlParameter("@ArticleStatus",model.ArticleStatus),
                        new SqlParameter("@TopTime",model.TopTime),
                        new SqlParameter("@PublishTime",model.PublishTime)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
        }
        /// <summary>
        /// 删除资讯
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public bool DeleteArticle(int articleId)
        {
            string strSql = "update BM_ArticleList set IsDel=1  where ArticleId=@ArticleId";
            var param = new[] {
                new SqlParameter("@ArticleId",articleId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <param name="AuthorIdentity">作者身份类型，0集团，1总店，2分店  3盟主 4盟友</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultPageModel GetArticleList(int AuthorId, int AuthorIdentity, SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = APP_SELECT;

            if (!string.IsNullOrEmpty(model.key))
            {
                switch (model.searchType)
                {
                    case (int)SearchType.标题:
                        strSql += string.Format(" and A.ArticleTitle like '%{0}%' ", model.key);
                        break;
                    default:
                        break;
                }
            }
            strSql += " and a.AuthorIdentity=@AuthorIdentity ";
            if (AuthorId > 0)
                strSql += " and A.AuthorId=@AuthorId ";

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),A.CreateTime,121)>=@startTime ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),A.CreateTime,121)<=@endTime ";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@AuthorIdentity", AuthorIdentity),
                new SqlParameter("@AuthorId",AuthorId)
            };
            //生成sql语句
            return getPageData<ArticleModel>(model.PageSize, model.PageIndex, strSql, "A.CreateTime", false, param);
        }
        /// <summary>
        /// 获取资讯信息
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public ArticleModel GetModel(int articleId)
        {
            string strSql = APP_SELECT + "  and ArticleId=@ArticleId";
            var param = new[] {
                new SqlParameter("@ArticleId",articleId)
            };

            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntity<ArticleModel>(dr);
            }
        }

        /// <summary>
        /// 设置资讯发布状态
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public bool SetArticleEnablePublish(int articleId, bool enable)
        {
            string strSql = "update BM_ArticleList set EnablePublish=@EnablePublish";
            if (enable)
                strSql += ",PublishTime=@PublishTime ";

            strSql += " where ArticleId=@ArticleId";

            var param = new[] {
                new SqlParameter("@EnablePublish", enable ? 1 : 0),
                new SqlParameter("@PublishTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@ArticleId",articleId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }
        /// <summary>
        /// 设置资讯置顶状态
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public bool SetArticleEnableTop(int articleId, bool enable)
        {
            string strSql = "update BM_ArticleList set EnableTop=@EnableTop";
            if (enable)
                strSql += ",TopTime=@TopTime ";

            strSql += " where ArticleId=@ArticleId";

            var param = new[] {
                new SqlParameter("@EnableTop", enable ? 1 : 0),
                new SqlParameter("@TopTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@ArticleId",articleId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }
        /// <summary>
        /// 修改资讯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateArticle(ArticleModel model)
        {
            string strSql = @"update BM_ArticleList set SendTargetId=@SendTargetId,SendType=@SendType,ArticleSort=@ArticleSort,ArticleType=@ArticleType,ArticleClassify=@ArticleClassify
                            ,ArticleTitle=@ArticleTitle,ArticleIntro=@ArticleIntro,ArticleCover=@ArticleCover,ArticleBody=@ArticleBody,EnableTop=@EnableTop,EnablePublish=@EnablePublish,ArticleStatus=@ArticleStatus,UpdateTime=@UpdateTime";

            if (model.EnablePublish == 1)
                strSql += ",PublishTime=@PublishTime";

            if (model.EnableTop == 1)
                strSql += ",TopTime=@TopTime";

            strSql += " where ArticleId=@ArticleId";
            var param = new[] {
                        new SqlParameter("@SendTargetId", model.SendTargetId),
                        new SqlParameter("@SendType", model.SendType),
                        new SqlParameter("@ArticleSort", model.ArticleSort),
                        new SqlParameter("@ArticleType", model.ArticleType),
                        new SqlParameter("@ArticleClassify", model.ArticleClassify),
                        new SqlParameter("@ArticleTitle", model.ArticleTitle),
                        new SqlParameter("@ArticleIntro", model.ArticleIntro),
                        new SqlParameter("@ArticleCover", model.ArticleCover),
                        new SqlParameter("@ArticleBody",model.ArticleBody),
                        new SqlParameter("@EnableTop",model.EnableTop),
                        new SqlParameter("@EnablePublish",model.EnablePublish),
                        new SqlParameter("@ArticleStatus",model.ArticleStatus),
                        new SqlParameter("@TopTime",model.TopTime),
                        new SqlParameter("@PublishTime",model.PublishTime),
                        new SqlParameter("@UpdateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        new SqlParameter("@ArticleId",model.ArticleId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }
    }
}