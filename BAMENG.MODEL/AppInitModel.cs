﻿/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2016 All rights reserved.
**/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.MODEL
{

    /// <summary>
    /// 初始化接口实体对象
    /// </summary>
    public class AppInitModel
    {

        private AppInitBaseSettting _baseData = new AppInitBaseSettting();
        /// <summary>
        /// 基本数据
        /// </summary>
        /// <value>The base data.</value>
        public AppInitBaseSettting baseData
        {
            get { return _baseData; }
            set { _baseData = value; }
        }
        /// <summary>
        /// 用户数据
        /// </summary>
        /// <value>The user data.</value>
        public UserModel userData { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        /// <value>The version data.</value>
        public AppVersionModel versionData { get; set; }

    }

    /// <summary>
    /// 基本数据对象
    /// </summary>
    public class AppInitBaseSettting
    {
        /// <summary>
        /// 用户状态 1激活  0冻结（该用户不可用） -1 未登录
        /// </summary>
        public int userStatus { get; set; }

        /// <summary>
        /// 关于我们（URL）
        /// </summary>
        /// <value>The about.</value>
        public string aboutUrl { get; set; }

        /// <summary>
        /// 协议(URL)
        /// </summary>
        /// <value>The agreement.</value>
        public string agreementUrl { get; set; }
    }

}
