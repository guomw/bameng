/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.LOGIC
{
    public class ShopLogic
    {
        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="ShopType">门店类型1 总店 0分店</param>
        /// <param name="ShopBelongId">门店所属总店ID，门店类型为总店时，此值0</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetShopList(int ShopType, int ShopBelongId, SearchModel model)
        {
            using (var dal = FactoryDispatcher.ShopFactory())
            {
                return dal.GetShopList(ShopType, ShopBelongId, model);
            }
        }


        /// <summary>
        /// 编辑门店信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditShopInfo(ShopModel model)
        {
            using (var dal = FactoryDispatcher.ShopFactory())
            {
                if (model.ShopID > 0)
                    return dal.UpdateShopInfo(model);
                else
                    return dal.AddShopInfo(model) > 0;
            }
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static bool DeleteShop(int shopId)
        {
            using (var dal = FactoryDispatcher.ShopFactory())
            {
                return dal.DeleltShopInfo(shopId);
            }
        }

        /// <summary>
        /// 冻结或解冻门店
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public static bool UpdateShopActive(int shopId, int active)
        {
            using (var dal = FactoryDispatcher.ShopFactory())
            {
                return dal.UpdateShopActive(shopId, active);
            }
        }

    }
}
