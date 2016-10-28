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

namespace BAMENG.IDAL
{
    public interface IUserDAL : IDisposable
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="ShopId">所属门店</param>
        /// <param name="UserIdentity">用户身份，0盟友  1盟主，值为盟主时，shopid无效</param>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultPageModel GetUserList(int ShopId, int UserIdentity, SearchModel model);
        /// <summary>
        /// 获取他的盟友列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultPageModel GetAllyList(SearchModel model);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddUserInfo(UserRegisterModel model);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateUserInfo(UserRegisterModel model);


        /// <summary>
        /// 获取用户实体信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserModel GetUserModel(int UserId);

        /// <summary>
        /// 冻结/解冻账户
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateUserActive(int userId, int active);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        bool DeleltUserInfo(int userId);


    }
}
