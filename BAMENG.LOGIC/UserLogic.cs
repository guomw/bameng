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
    public class UserLogic
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="ShopId"></param>
        /// <param name="UserIdentity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetUserList(int ShopId, int UserIdentity, SearchModel model)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.GetUserList(ShopId, UserIdentity, model);
            }
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditUserInfo(UserRegisterModel model)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                if (model.UserId > 0)
                    return dal.UpdateUserInfo(model);
                else
                    return dal.AddUserInfo(model) > 0;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteUser(int userId)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.DeleltUserInfo(userId);
            }
        }

        /// <summary>
        /// 冻结或解冻账户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public static bool UpdateUserActive(int userId, int active)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.UpdateUserActive(userId, active);
            }
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserModel GetModel(int userId)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.GetUserModel(userId);
            }
        }

        /// <summary>
        /// 获取他的盟友列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultPageModel GetAllyList(SearchModel model)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.GetAllyList(model);
            }
        }
    }
}
