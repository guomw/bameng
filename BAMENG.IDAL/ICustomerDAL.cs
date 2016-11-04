/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-$today.year. All rights reserved.
**/

using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.IDAL
{
    public interface ICustomerDAL : IDisposable
    {
        /// <summary>
        /// 根据用户ID，获取客户列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isvalid">是否是有效客户</param>
        /// <returns></returns>
        ResultPageModel GetCustomerList(SearchModel model, bool isvalid = true);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCustomerInfo(CustomerModel model);

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertCustomerInfo(CustomerModel model);

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool DeleteCustomerInfo(int customerId);


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="status"></param>
        /// <param name="userId">操作人ID(此方法只有盟主操作)</param>
        /// <returns></returns>
        bool UpdateStatus(int customerId, int status, int userId);


        /// <summary>
        /// 判断客户是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="addr">地址</param>
        /// <returns></returns>
        bool IsExist(string mobile, string addr);




    }
}
