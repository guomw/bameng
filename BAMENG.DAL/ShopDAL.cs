/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


using BAMENG.IDAL;
using BAMENG.MODEL;
using HotCoreUtils.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopDAL : AbstractDAL,IShopDAL
    {
        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="ShopType">门店类型1 总店 0分店</param>
        /// <param name="ShopBelongId">门店所属总店ID，门店类型为总店时，此值0</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultPageModel GetShopList(int ShopType, int ShopBelongId, SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select ShopID,ShopName,ShopType,ShopBelongId,ShopProv,ShopCity,ShopArea,ShopAddress,Contacts,ContactWay,LoginName,LoginPassword,IsActive,CreateTime from BM_ShopManage where 1=1 ";

            strSql += " and ShopType=@ShopType";
            if (ShopType == 0)
            {
                strSql += " and ShopBelongId=@ShopBelongId";
            }

            if (!string.IsNullOrEmpty(model.key))
            {
                strSql += string.Format(" and ShopName like '%{0}%' ", model.key);
            }

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)>=@startTime ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)<=@endTime ";


            var param = new[] {
                        new SqlParameter("@ShopType", ShopType),
                        new SqlParameter("@ShopBelongId", ShopBelongId),
                        new SqlParameter("@startTime", model.startTime),
                        new SqlParameter("@endTime", model.endTime)
            };

            //生成sql语句
            return getPageData<ShopModel>(model.PageSize, model.PageIndex, strSql, "CreateTime", param);

        }
    }
}
