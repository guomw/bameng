/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.MODEL
{
    /// <summary>
    /// /系统菜单实体
    /// </summary>
    public class SystemMenuModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 0总后台菜单，1总门店菜单，2分店菜单
        /// </summary>
        public int ItemType { get; set; }


        public string ItemCode { get; set; }

        public string ItemNavLabel { get; set; }


        public string ItemParentCode { get; set; }

        public string ItemUrl { get; set; }

        public int ItemShow { get; set; }


        public string ItemIcons { get; set; }


        public DateTime CreateTime { get; set; }
    }

    public class SystemLeftModel
    {
        public List<SystemMenuModel> menuData { get; set; }

        public AdminLoginModel userData { get; set; }
        /// <summary>
        /// 用户菜单权限代码，用隔开,如 |10001|10000101|
        /// </summary>
        public string authority { get; set; }
    }

}
