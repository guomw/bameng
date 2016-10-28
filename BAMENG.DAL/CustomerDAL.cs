/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved. 
**/


using BAMENG.IDAL;
using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.DAL
{
    public class CustomerDAL : AbstractDAL, ICustomerDAL
    {
        private const string APP_SELECT = @"select C.ID,C.BelongOne,C.BelongTwo,C.Status,C.Name,C.Mobile,C.Addr,C.Remark,C.ShopId,C.CreateTime,U.UB_UserRealName as BelongOneName,UB.UB_UserRealName as BelongTwoName from BM_CustomerManage C 
                                                left join Hot_UserBaseInfo U with(nolock) on U.UB_UserID=C.BelongOne
                                                left join Hot_UserBaseInfo UB with(nolock) on UB.UB_UserID=C.BelongTwo
                                                where 1=1 ";
        /// <summary>
        /// 获取他的客户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultPageModel GetCustomerListByUserId(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            string strSql = APP_SELECT + " and C.Status= 1 and C.BelongTwo= @BelongOne";
            var param = new[] {
                new SqlParameter("@BelongOne",model.UserId),
            };
            //生成sql语句
            return getPageData<CustomerModel>(model.PageSize, model.PageIndex, strSql, "C.CreateTime", param);
        }
    }
}
