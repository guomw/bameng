/*
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
    /// <summary>
    /// 
    /// </summary>
    public class CustomerLogic
    {
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetCustomerList(SearchModel model, bool isvalid = true)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.GetCustomerList(model, isvalid);
            }
        }


        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertCustomerInfo(CustomerModel model)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.InsertCustomerInfo(model)>0;
            }
        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool UpdateCustomerInfo(CustomerModel model)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.UpdateCustomerInfo(model);
            }
        }



        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool DeleteCustomerInfo(int customerId)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.DeleteCustomerInfo(customerId);
            }
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="status"></param>
        /// <param name="userId">操作人ID(此方法只有盟主操作)</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool UpdateStatus(int customerId, int status, int userId)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.UpdateStatus(customerId, status, userId);
            }
        }

        /// <summary>
        /// 判断客户是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="addr">地址</param>
        /// <returns></returns>
        public static bool IsExist(string mobile, string addr)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.IsExist(mobile, addr);
            }
        }

    }
}
