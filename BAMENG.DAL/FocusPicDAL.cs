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
using BAMENG.CONFIG;
using System.Data.SqlClient;
using HotCoreUtils.DB;
using System.Data;

namespace BAMENG.DAL
{
    public class FocusPicDAL : AbstractDAL, IFocusPicDAL
    {


        private const string APP_SELECT = "select ID,Type,Title,PicUrl,Description,IsEnable,Sort,LinkUrl,CreateTime from BM_BannerManage where 1=1 ";

        /// <summary>
        /// 添加焦点广告图
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int AddFocusPic(FocusPicModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除焦点广告图
        /// </summary>
        /// <param name="fid">The fid.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DeleteFocusPic(int fid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取焦点广告图列表
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ResultPageModel GetList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            string strSql = APP_SELECT;
            //0 资讯轮播图 1首页轮播图
            if (model.type == 0)
                strSql += " and Type=0 ";
            else
                strSql += " and Type=2 ";

            if (!string.IsNullOrEmpty(model.key))
            {
                strSql += string.Format(" and Title like '%{0}%' ", model.key);
            }
            return getPageData<FocusPicModel>(model.PageSize, model.PageIndex, strSql, "Sort");
        }

        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="fid">The fid.</param>        
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetEnable(int fid)
        {
            string strSql = "update BM_BannerManage set IsEnable=ABS(IsEnable-1) where ID=@ID";
            var param = new[] {
                new SqlParameter("@ID",fid)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 更新焦点广告图
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdateFocusPic(FocusPicModel model)
        {
            throw new NotImplementedException();
        }
    }
}
