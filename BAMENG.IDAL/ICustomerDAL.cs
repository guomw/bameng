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
        /// <returns></returns>
        ResultPageModel GetCustomerListByUserId(SearchModel model);
    }
}
