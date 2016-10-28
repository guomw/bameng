﻿/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


using BAMENG.CONFIG;
using BAMENG.IDAL;
using BAMENG.MODEL;
using HotCoreUtils.DB;
using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopDAL : AbstractDAL, IShopDAL
    {
        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddShopInfo(ShopModel model)
        {
            string strSql = @"insert into BM_ShopManage(ShopName,ShopType,ShopBelongId,ShopProv,ShopCity,ShopArea,ShopAddress,Contacts,ContactWay,LoginName,LoginPassword,IsActive)
                                values (@ShopName,@ShopType,@ShopBelongId,@ShopProv,@ShopCity,@ShopArea,@ShopAddress,@Contacts,@ContactWay,@LoginName,@LoginPassword,@IsActive)";

            var param = new[] {
                        new SqlParameter("@ShopName", model.ShopName),
                        new SqlParameter("@ShopType", model.ShopType),
                        new SqlParameter("@ShopBelongId", model.ShopBelongId),
                        new SqlParameter("@ShopProv", model.ShopProv),
                        new SqlParameter("@ShopCity", model.ShopCity),
                        new SqlParameter("@ShopArea", model.ShopArea),
                        new SqlParameter("@ShopAddress", model.ShopAddress),
                        new SqlParameter("@Contacts", model.Contacts),
                        new SqlParameter("@ContactWay", model.ContactWay),
                        new SqlParameter("@LoginName", model.LoginName),
                        new SqlParameter("@LoginPassword",  EncryptHelper.MD5_8(model.LoginPassword)),
                        new SqlParameter("@IsActive",model.IsActive)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
        }
        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleltShopInfo(int shopId)
        {
            string strSql = "update BM_ShopManage set IsDel=1 where ShopID=@ShopID";
            var param = new[] {
                        new SqlParameter("@ShopID",shopId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

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
            string strSql = @"select ShopID,ShopName,ShopType,ShopBelongId,ShopProv,ShopCity,ShopArea,ShopAddress,Contacts,ContactWay,LoginName,LoginPassword,IsActive,CreateTime from BM_ShopManage where 1=1 and IsDel<>1 ";

            strSql += " and ShopType=@ShopType";
            if (ShopType == 0)
            {
                strSql += " and ShopBelongId=@ShopBelongId";
            }

            if (!string.IsNullOrEmpty(model.key))
            {
                strSql += string.Format(" and ShopName like '%{0}%' ", model.key);
            }

            if (!string.IsNullOrEmpty(model.province))
            {
                strSql += " and ShopProv=@ShopProv";
            }
            if (!string.IsNullOrEmpty(model.city))
            {
                strSql += " and ShopCity=@ShopCity";
            }


            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)>=@startTime ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)<=@endTime ";


            var param = new[] {
                        new SqlParameter("@ShopType", ShopType),
                        new SqlParameter("@ShopBelongId", ShopBelongId),
                        new SqlParameter("@ShopProv", model.province),
                        new SqlParameter("@ShopCity", model.city),
                        new SqlParameter("@startTime", model.startTime),
                        new SqlParameter("@endTime", model.endTime),

            };

            //生成sql语句
            return getPageData<ShopModel>(model.PageSize, model.PageIndex, strSql, "CreateTime", false, param);

        }
        /// <summary>
        /// 冻结/解冻门店账户
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateShopActive(int shopId, int active)
        {
            string strSql = "update BM_ShopManage set IsActive=@IsActive where ShopID=@ShopID";
            var param = new[] {
                        new SqlParameter("@IsActive",active),
                        new SqlParameter("@ShopID",shopId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateShopInfo(ShopModel model)
        {
            string strSql = @"update BM_ShopManage set ShopName=@ShopName,ShopType=@ShopType,ShopBelongId=@ShopBelongId,ShopProv=@ShopProv,ShopCity=@ShopCity,ShopArea=@ShopArea,ShopAddress=@ShopAddress,Contacts=@Contacts,ContactWay=@ContactWay,IsActive=@IsActive";

            if (!string.IsNullOrEmpty(model.LoginPassword))
                strSql += ",LoginPassword=@LoginPassword";

            strSql += " where ShopID=@ShopID";
            var param = new[] {
                        new SqlParameter("@ShopName", model.ShopName),
                        new SqlParameter("@ShopType", model.ShopType),
                        new SqlParameter("@ShopBelongId", model.ShopBelongId),
                        new SqlParameter("@ShopProv", model.ShopProv),
                        new SqlParameter("@ShopCity", model.ShopCity),
                        new SqlParameter("@ShopArea", model.ShopArea),
                        new SqlParameter("@ShopAddress", model.ShopAddress),
                        new SqlParameter("@Contacts", model.Contacts),
                        new SqlParameter("@ContactWay", model.ContactWay),
                        new SqlParameter("@LoginPassword", EncryptHelper.MD5_8(model.LoginPassword)),
                        new SqlParameter("@IsActive",model.IsActive),
                        new SqlParameter("@ShopID",model.ShopID)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

    }
}
