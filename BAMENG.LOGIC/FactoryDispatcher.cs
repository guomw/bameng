/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


using BAMENG.DAL;
using BAMENG.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.LOGIC
{
    /// <summary>
    /// 工厂管理
    /// </summary>
    public class FactoryDispatcher
    {
        /// <summary>
        /// 门店
        /// </summary>
        /// <returns></returns>
        public static IShopDAL ShopFactory()
        {
            return new ShopDAL();
        }

    }
}
