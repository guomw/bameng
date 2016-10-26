﻿/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-$today.year. All rights reserved.
**/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.MODEL
{
    public class AppVersionModel
    {
        /// <summary>
        /// 服务器版本号
        /// </summary>
        public string serverVersion { get; set; }
        /// <summary>
        /// 更新类型 0无更新  1整包更新  2强制更新
        /// </summary>
        public int updateType { get; set; }

        public string updateTip { get; set; }


        public string updateUrl { get; set; }
    }
}
