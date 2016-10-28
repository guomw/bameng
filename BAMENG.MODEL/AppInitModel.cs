/*
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
    public class AppInitModel
    {
        private AppInitBaseSettting _baseData = new AppInitBaseSettting();
        /// <summary>
        /// 基本数据
        /// </summary>
        public AppInitBaseSettting baseData
        {
            get { return _baseData; }
            set { _baseData = value; }
        }
        /// <summary>
        /// 用户数据
        /// </summary>
        public UserModel userData { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        public AppVersionModel versionData { get; set; }

    }

    public class AppInitBaseSettting
    {
        /// <summary>
        /// 用户状态 1激活  0冻结（该用户不可用）
        /// </summary>
        public int userStatus { get; set; }

        public string about { get; set; }

        public string agreement { get; set; }
    }

}
