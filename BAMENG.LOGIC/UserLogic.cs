/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/


using BAMENG.CONFIG;
using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        /// <summary>
        /// 获取等级列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ResultPageModel GetLevelList(int type)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.GetLevelList(ConstConfig.storeId, type);
            }
        }

        public static bool DeleteLevel(int levelId)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                return dal.DeleteLevel(levelId, ConstConfig.storeId);
            }
        }

        /// <summary>
        /// 编辑等级
        /// </summary>
        /// <param name="levelId"></param>
        /// <param name="levelType"></param>
        /// <param name="levelname"></param>
        /// <param name="upgradeCount"></param>
        /// <returns></returns>
        public static bool EditLevel(int levelId, int levelType, string levelname, int upgradeCount)
        {
            using (var dal = FactoryDispatcher.UserFactory())
            {
                MallUserLevelModel model = new MallUserLevelModel()
                {
                    IntegralPreID = 0,
                    PricePreID = 0,
                    UL_BelongOne_Content = "",
                    UL_BelongTwo_Content = "",
                    UL_CustomerID = ConstConfig.storeId,
                    UL_DefaultLevel = 0,
                    UL_Description = "",
                    UL_DirectTeamNum = 0,
                    UL_Gold = 0,
                    UL_GuidetLevel = -1,
                    UL_ID = levelId,
                    UL_IndirectTeamNum = 0,
                    UL_Integral = 0,
                    UL_Level = 1,
                    UL_LevelName = levelname,
                    UL_MemberNum = upgradeCount,
                    UL_Money = 0,
                    UL_OpenLevel_One = false,
                    UL_OpenLevel_Two = false,
                    UL_Type = levelType,

                };
                if (levelId > 0)
                {
                    model.UL_Level = dal.GetLevelCount(ConstConfig.storeId, levelType) + 1;
                    return dal.UpdateLevel(model);
                }
                else
                {
                    model.UL_Level = dal.GetMaxLevel(ConstConfig.storeId, levelType) + 1;
                    return dal.InsertLevel(model) > 0;
                }
            }
        }

    }
}
