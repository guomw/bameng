/*
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

namespace BAMENG.DAL
{
    public class ArticleDAL : AbstractDAL, IArticleDAL
    {
        public int AddArticle(ArticleModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArticle(int articleId)
        {
            throw new NotImplementedException();
        }

        public ResultPageModel GetArticleList(int AuthorId, int AuthorIdentity, SearchModel model)
        {
            throw new NotImplementedException();
        }

        public bool SetArticleEnablePublish(int articleId, bool enable)
        {
            throw new NotImplementedException();
        }

        public bool SetArticleEnableTop(int articleId, bool enable)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArticle(ArticleModel model)
        {
            throw new NotImplementedException();
        }
    }
}
