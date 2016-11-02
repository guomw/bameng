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


        /// <summary>
        /// 获取商户的等级数量
        /// 作者：郭孟稳
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetLevelCount(int storeId, int type);

        /// <summary>
        /// 获取当前最大等级级别
        /// 作者：郭孟稳
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetMaxLevel(int storeId, int type);

        /// <summary>
        /// 获取等级信息
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="storeId">商户ID</param>
        /// <returns></returns>
        MallUserLevelModel GetLevelModel(int levelId, int storeId);

        /// <summary>
        /// 删除等级
        /// 作者：郭孟稳
        /// </summary>
        /// <param name="levelId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        bool DeleteLevel(int levelId, int storeId);


        /// <summary>
        /// 修改等级
        /// 作者：郭孟稳
        /// 时间：2016.07.13
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateLevel(MallUserLevelModel model);

        /// <summary>
        /// 添加等级
        /// 作者：郭孟稳
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertLevel(MallUserLevelModel model);

        /// <summary>
        /// 获取等级列表
        /// 作者：郭孟稳        
        /// </summary>
        ///<param name="storeId"></param>
        ///<param name="type">0盟友，1盟主</param>
        /// <returns></returns>
        ResultPageModel GetLevelList(int storeId, int type);


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <param name="IsShop">是否是门店登录</param>
        /// <returns></returns>
        AdminLoginModel Login(string loginName, string loginPassword, bool IsShop);
    }
}
