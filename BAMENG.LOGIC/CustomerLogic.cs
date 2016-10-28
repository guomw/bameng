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
        /// 根据用户ID获取该用户下的客户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetCustomerListByUserId(SearchModel model)
        {
            using (var dal = FactoryDispatcher.CustomerFactory())
            {
                return dal.GetCustomerListByUserId(model);
            }
        }
    }
}
