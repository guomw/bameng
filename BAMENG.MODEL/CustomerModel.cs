﻿/*
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
    /// 客户信息
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 所属盟友
        /// </summary>
        public int BelongOne { get; set; }
        /// <summary>
        /// 所属盟主
        /// </summary>
        public int BelongTwo { get; set; }
        /// <summary>
        /// 0 审核中，1已同意  2已拒绝
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 客户手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 所属门店ID
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 盟友姓名
        /// </summary>
        public string BelongOneName { get; set; }
        /// <summary>
        /// 盟主姓名
        /// </summary>
        public string BelongTwoName { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
