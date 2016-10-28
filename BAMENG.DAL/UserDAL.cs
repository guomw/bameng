/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-$today.year. All rights reserved.
**/


using BAMENG.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAMENG.MODEL;
using HotCoreUtils.DB;
using System.Data.SqlClient;
using System.Transactions;
using BAMENG.CONFIG;
using System.Data;
using HotCoreUtils.Helper;

namespace BAMENG.DAL
{
    public class UserDAL : AbstractDAL, IUserDAL
    {
        /// <summary>
        /// 获取用户基本信息SQL 语句
        /// </summary>
        private const string APP_USER_SELECT = @"select ue.UserId,ue.UserIdentity,ue.MerchantID,ue.ShopId,ue.IsActive,ue.Score,ue.ScoreLocked,ue.MengBeans,ue.MengBeansLocked,ue.CreateTime
                            ,U.UB_UserLoginName as LoginName,U.UB_UserRealName as RealName,U.UB_UserNickName as NickName,U.UB_UserMobile as UserMobile,U.UB_WxHeadImg as UserHeadImg
                            ,S.ShopName,S.ShopProv,S.ShopCity
                             from BM_User_extend ue
                            inner join Hot_UserBaseInfo U with(nolock) on U.UB_UserID =ue.UserId
                            left join BM_ShopManage S with(nolock) on S.ShopID=ue.ShopId
                            left join Mall_UserLevel L on L.UL_ID=U.UB_LevelID 
                            where 1=1 and  U.UB_IsDelete=0 ";

        /// <summary>
        /// 添加用户（盟主或盟友）
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="loginname"></param>
        /// <param name="username"></param>
        /// <param name="belongOne"></param>
        /// <returns></returns>
        public int AddUserInfo(UserRegisterModel model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int userId = AddUserBaseInfoModel(model.storeId, model.mobile, model.loginName, model.loginPassword, model.username, model.nickname, model.belongOne);
                if (userId <= 0)
                    return userId;
                string strSql = "insert into BM_User_extend(UserId,UserIdentity,MerchantID,ShopId) values(@UserId,@UserIdentity,@MerchantID,@ShopId);select @@IDENTITY";

                var param = new[] {
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@UserIdentity",model.UserIdentity),
                        new SqlParameter("@MerchantID",model.storeId),
                        new SqlParameter("@ShopId", model.ShopId)
                        };
                object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
                int flag = 0;
                if (obj == null)
                    flag = 0;
                else
                    flag = Convert.ToInt32(obj);
                if (flag > 0)
                {
                    scope.Complete();
                    return userId;
                }
                else
                    return 0;
            }
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="ShopId"></param>
        /// <param name="UserIdentity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultPageModel GetUserList(int ShopId, int UserIdentity, SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = APP_USER_SELECT;

            if (!string.IsNullOrEmpty(model.key))
            {
                switch (model.searchType)
                {
                    case (int)SearchType.姓名:
                        strSql += string.Format(" and U.UB_UserRealName like '%{0}%' ", model.key);
                        break;
                    case (int)SearchType.昵称:
                        strSql += string.Format(" and U.UB_UserNickName like '%{0}%' ", model.key);
                        break;
                    case (int)SearchType.手机:
                        strSql += " and U.UB_UserMobile=@UserMobile ";
                        break;
                    case (int)SearchType.门店:
                        strSql += string.Format(" and S.ShopName like '%{0}%' ", model.key);
                        break;
                    default:
                        break;
                }
            }
            strSql += " and ue.UserIdentity=@UserIdentity ";
            if (ShopId > 0)
                strSql += " and ue.ShopId=@ShopId ";
            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),ue.CreateTime,121)>=@startTime ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),ue.CreateTime,121)<=@endTime ";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@UserMobile", model.key),
                new SqlParameter("@UserIdentity", UserIdentity),
                new SqlParameter("@ShopId",ShopId)
            };
            //生成sql语句
            return getPageData<UserModel>(model.PageSize, model.PageIndex, strSql, "ue.CreateTime", param, (items) =>
            {
                //TODO:暂时写死
                items.ForEach((item) =>
                {
                    item.CustomerAmount = 100;
                    item.OrderSuccessAmount = 10;
                });
            });
        }


        /// <summary>
        /// 获取用户实体信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserModel GetUserModel(int UserId)
        {
            string strSql = APP_USER_SELECT + " and ue.UserId=@UserId";
            var param = new[] {
                        new SqlParameter("@UserId", UserId)
                        };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntity<UserModel>(dr);
            }

        }


        /// <summary>
        /// 添加商城用户
        /// 作者：郭孟稳
        /// 时间：2016.07.11
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="userName"></param>
        /// <param name="belongOne"></param>
        /// <returns></returns>
        private int AddUserBaseInfoModel(int storeId, string mobile, string loginName, string password, string userName, string nickName, int belongOne, int levelId = 0)
        {
            if (UserExist(mobile, storeId))
                return 0;
            UserBaseInfoModel model = new UserBaseInfoModel();
            model.UB_UserLoginName = loginName;
            model.UB_UserLoginPassword = password;
            model.UB_CustomerID = storeId;
            model.UB_UserMobile = mobile;
            model.UB_UserType = 0;
            model.UB_GroupId = 0;
            model.UB_RebateEnabled = 0;
            model.WxNickName = nickName;//QQ昵称对应微信昵称
            model.UB_UserRealName = userName;
            model.UB_UserNickName = nickName;
            model.UB_ShareCount = 0; //分享机会赠送
            model.UB_InviteCount = 0;
            model.UB_UserEmail = "";
            model.UB_UserCardID = "";
            model.UB_UserCity = "";
            model.UB_UserProvince = "";
            model.BelongName = "";
            model.LevelName = "";
            model.ParentName = "";
            model.PayPassword = "";
            model.UB_SourcePath = "";
            model.UB_StoreAddr = "";
            model.UB_UserAddress = "";
            model.UB_UserArea = "";
            model.UB_UserBirthday = "";
            model.UB_UserFace = "";
            model.UB_UserIncome = "";
            model.UserAllArea = "";
            model.WxHeadImg = "";
            model.UB_AccountSrc = 0;
            model.UB_MobileToBeBind = 0;
            model.UB_ShareTaskID = 0;
            model.UB_ShareTaskType = 0;

            UserRelationViewEntity sourceModel = null;
            if (belongOne > 0)
            {
                model.UB_SourceID = belongOne;
                model.UB_SourceDesc = "我引导注册";
                sourceModel = GetRelationInfoPlus(belongOne);
            }
            else
            {
                model.UB_SourceDesc = "管理员后台添加";
            }
            if (sourceModel != null)
            {
                model.UB_BelongOne = sourceModel.UserId;
                model.UB_BelongTwo = sourceModel.BelongOne;
                model.UB_BelongThree = sourceModel.BelongTwo;
                model.UB_SourceID = sourceModel.UserId;
                model.UB_ParentID = 0;
            }
            if (levelId == 0)
                model.UB_LevelID = GetMinLevelID(storeId, 0);
            else
                model.UB_LevelID = levelId;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Hot_UserBaseInfo(");
                strSql.Append("UB_UserMobile,UB_UserEmail,UB_UserCardID,UB_UserProvince,UB_UserCity,UB_UserArea,UB_UserAddress,UB_UserLoginPassword,UB_UserNickName,UB_UserFace,UB_UserRealName,UB_UserAge,UB_UserIncome,UB_IsDelete,UB_CustomerID,UB_UserBirthday,UB_HasCard,UB_UserLoginName,UB_LevelID,UB_UserType,UB_BelongTo,UB_SourceID,UB_SourceDesc,UB_ShareCount,UB_InviteCount,UB_ShareTaskID,UB_ShareTaskType,UB_StoreAddr,UB_IsStore,UB_ParentID,UB_SourcePath,UB_SourceDepth,UB_BelongOne,UB_BelongTwo,UB_BelongThree,UB_UserGroupId,UB_AccountSrc,UB_MobileToBeBind,UB_WxNickName,UB_WxHeadImg");
                strSql.Append(") values (");
                strSql.Append("@UB_UserMobile,@UB_UserEmail,@UB_UserCardID,@UB_UserProvince,@UB_UserCity,@UB_UserArea,@UB_UserAddress,@UB_UserLoginPassword,@UB_UserNickName,@UB_UserFace,@UB_UserRealName,@UB_UserAge,@UB_UserIncome,@UB_IsDelete,@UB_CustomerID,@UB_UserBirthday,@UB_HasCard,@UB_UserLoginName,@UB_LevelID,@UB_UserType,@UB_BelongTo,@UB_SourceID,@UB_SourceDesc,@UB_ShareCount,@UB_InviteCount,@UB_ShareTaskID,@UB_ShareTaskType,@UB_StoreAddr,@UB_IsStore,@UB_ParentID,@UB_SourcePath,@UB_SourceDepth,@UB_BelongOne,@UB_BelongTwo,@UB_BelongThree,@UB_UserGroupId,@UB_AccountSrc,@UB_MobileToBeBind,@UB_WxNickName,@UB_WxHeadImg");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                        new SqlParameter("@UB_UserMobile", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserEmail", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserCardID", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserProvince", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserCity", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserArea", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserAddress", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserLoginPassword", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserNickName", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserFace", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserRealName", SqlDbType.NVarChar) ,
                        new SqlParameter("@UB_UserAge", SqlDbType.Int) ,
                        new SqlParameter("@UB_UserIncome", SqlDbType.NVarChar),
                        new SqlParameter("@UB_IsDelete",SqlDbType.Int),
                        new SqlParameter("@UB_CustomerID",SqlDbType.Int),
                        new SqlParameter("@UB_UserBirthday",SqlDbType.NVarChar),
                        new SqlParameter("@UB_HasCard",SqlDbType.Int),
                        new SqlParameter("@UB_UserLoginName",SqlDbType.NVarChar),
                        new SqlParameter("@UB_LevelID",SqlDbType.Int),
                        new SqlParameter("@UB_UserType",SqlDbType.Int),
                        new SqlParameter("@UB_BelongTo",SqlDbType.Int),
                        new SqlParameter("@UB_SourceID",SqlDbType.Int),
                        new SqlParameter("@UB_SourceDesc",SqlDbType.NVarChar),
                        new SqlParameter("@UB_ShareCount",SqlDbType.Int),
                        new SqlParameter("@UB_InviteCount",SqlDbType.Int),
                        new SqlParameter("@UB_ShareTaskID",SqlDbType.Int),
                        new SqlParameter("@UB_ShareTaskType",SqlDbType.Int),
                        new SqlParameter("@UB_StoreAddr",SqlDbType.NVarChar),
                        new SqlParameter("@UB_IsStore",SqlDbType.Int),
                        new SqlParameter("@UB_ParentID",SqlDbType.Int),
                        new SqlParameter("@UB_SourcePath",SqlDbType.NVarChar),
                        new SqlParameter("@UB_SourceDepth",SqlDbType.Int),
                        new SqlParameter("@UB_BelongOne", SqlDbType.Int),
                        new SqlParameter("@UB_BelongTwo", SqlDbType.Int),
                        new SqlParameter("@UB_BelongThree", SqlDbType.Int),
                        new SqlParameter("@UB_UserGroupId", SqlDbType.Int),
                        new SqlParameter("@UB_AccountSrc", SqlDbType.Int),
                        new SqlParameter("@UB_MobileToBeBind", SqlDbType.Int),
                        new SqlParameter("@UB_WxNickName", SqlDbType.VarChar),
                        new SqlParameter("@UB_WxHeadImg", SqlDbType.VarChar)
                };
                parameters[0].Value = model.UB_UserMobile;
                parameters[1].Value = model.UB_UserEmail;
                parameters[2].Value = model.UB_UserCardID;
                parameters[3].Value = model.UB_UserProvince;
                parameters[4].Value = model.UB_UserCity;
                parameters[5].Value = model.UB_UserArea;
                parameters[6].Value = model.UB_UserAddress;
                parameters[7].Value = model.UB_UserLoginPassword;
                parameters[8].Value = model.UB_UserNickName;
                parameters[9].Value = model.UB_UserFace;
                parameters[10].Value = model.UB_UserRealName;
                parameters[11].Value = model.UB_UserAge;
                parameters[12].Value = model.UB_UserIncome;
                parameters[13].Value = model.UB_IsDelete;
                parameters[14].Value = model.UB_CustomerID;
                parameters[15].Value = model.UB_UserBirthday;
                parameters[16].Value = model.UB_HasCard;
                parameters[17].Value = model.UB_UserLoginName;
                parameters[18].Value = model.UB_LevelID;
                parameters[19].Value = model.UB_UserType;
                parameters[20].Value = model.UB_BelongTo;
                parameters[21].Value = model.UB_SourceID;
                parameters[22].Value = model.UB_SourceDesc;
                parameters[23].Value = model.UB_ShareCount;
                parameters[24].Value = model.UB_InviteCount;
                parameters[25].Value = model.UB_ShareTaskID;
                parameters[26].Value = model.UB_ShareTaskType;
                parameters[27].Value = model.UB_StoreAddr;
                parameters[28].Value = model.UB_IsStore;
                parameters[29].Value = model.UB_ParentID;
                parameters[30].Value = model.UB_SourcePath;
                parameters[31].Value = model.UB_SourceDepth;
                parameters[32].Value = model.UB_BelongOne;
                parameters[33].Value = model.UB_BelongTwo;
                parameters[34].Value = model.UB_BelongThree;
                parameters[35].Value = model.UB_UserGroupId;
                parameters[36].Value = model.UB_AccountSrc;
                parameters[37].Value = model.UB_MobileToBeBind;
                parameters[38].Value = model.WxNickName;
                parameters[39].Value = model.WxHeadImg;

                object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    int userId = Convert.ToInt32(obj);

                    MemberChangeLogModel changeLog = new MemberChangeLogModel()
                    {
                        Member_Id = userId,
                        Change_Type = 5,
                        Remark = "会员注册",
                        Add_Time = DateTime.Now,
                        Customer_Id = storeId,
                        BelongOne = belongOne,
                        BelongTwo = model.UB_BelongTwo,
                        BelongThree = model.UB_BelongThree,
                        ParentId = model.UB_ParentID,
                        GroupId = 0,
                        LevelId = model.UB_LevelID,
                        BeforeBelongOne = 0,
                        BeforeBelongTwo = 0,
                        BeforeBelongThree = 0,
                        BeforeParentId = 0,
                        BeforeGroupId = 0,
                        BeforeLevelId = 0,
                        Reason = "邀请注册"
                    };

                    if (userId > 0)
                        AddRegisterLog(changeLog);
                    return userId;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("AddUserBaseInfoModel:{0}", ex), LogHelperTag.ERROR, WebConfig.debugMode());
                return 0;
            }
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        private bool UserExist(string mobile, int storeId)
        {
            string strSql = "select COUNT(1) from Hot_UserBaseInfo with(nolock) where UB_UserLoginName=@LoginName and UB_CustomerID=@CustomerID";
            var param = new[] {
                new SqlParameter("@LoginName",mobile),
                new SqlParameter("@CustomerID",storeId)
            };
            return Convert.ToInt32(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param)) > 0;
        }
        /// <summary>
        /// 获取最小商城等级
        /// 作者：郭孟稳        
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetMinLevelID(int customerid, int type)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT TOP 1 UL_ID FROM Mall_UserLevel WHERE UL_CustomerID={0}  order by UL_Level", customerid);
            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, sql.ToString());
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 获取用户关联信息新版
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        public UserRelationViewEntity GetRelationInfoPlus(int userId)
        {
            string sql = @"select 
                           UB_UserLoginName,
                           ISNULL(UB_BelongOne,0) AS UB_BelongOne,
                           ISNULL(UB_BelongTwo,0) as UB_BelongTwo ,
                           ISNULL(UB_BelongThree,0) AS UB_BelongThree,         
                           UB_CustomerId
                           from Hot_UserBaseInfo  with(nolock) where UB_UserID=@UB_UserID";
            var parameters = new[] {
                    new SqlParameter("@UB_UserID", userId)};

            DataTable dt = DbHelperSQLP.GetDataTable(WebConfig.getConnectionString(), CommandType.Text, sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return new UserRelationViewEntity() { UserId = -1 };
            }
            UserRelationViewEntity entity = new UserRelationViewEntity();
            entity.UserId = userId;
            entity.LoginName = dt.Rows[0]["UB_UserLoginName"].ToString();
            entity.BelongOne = Convert.ToInt32(dt.Rows[0]["UB_BelongOne"].ToString());
            entity.BelongTwo = Convert.ToInt32(dt.Rows[0]["UB_BelongTwo"].ToString());
            entity.BelongThree = Convert.ToInt32(dt.Rows[0]["UB_BelongThree"].ToString());
            entity.CustomerId = Convert.ToInt32(dt.Rows[0]["UB_CustomerId"]);
            entity.UB_CustomerID = Convert.ToInt32(dt.Rows[0]["UB_CustomerID"]);
            return entity;
        }

        /// <summary>
        /// 增加一条注册日志数据
        /// 作者：郭孟稳        
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <param name="LevelId"></param>
        /// <param name="belongOne"></param>
        /// <returns></returns>
        private int AddRegisterLog(MemberChangeLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into Mall_Member_ChangeLog (Member_Id,Change_Type,Remark,Add_Time,Customer_Id
                       ,BelongOne,BelongTwo,BelongThree,BeforeBelongOne,BeforeBelongTwo,BeforeBelongThree,ParentId,BeforeParentId,Reason)
                        values (@Member_Id,@Change_Type,@Remark,@Add_Time,@Customer_Id
                        ,@BelongOne,@BelongTwo,@BelongThree,@BeforeBelongOne,@BeforeBelongTwo,@BeforeBelongThree,@ParentId,@BeforeParentId,@Reason) 
                        select @@IDENTITY");
            var parm = new[] {
                new SqlParameter("@Member_Id", model.Member_Id),
                new SqlParameter("@Change_Type", model.Change_Type),
                new SqlParameter("@Remark", model.Remark),
                new SqlParameter("@Add_Time", model.Add_Time),
                new SqlParameter("@Customer_Id", model.Customer_Id),
                new SqlParameter("@BelongOne", model.BelongOne),
                new SqlParameter("@BelongTwo", model.BelongTwo),
                new SqlParameter("@BelongThree", model.BelongThree),
                new SqlParameter("@BeforeBelongOne", model.BeforeBelongOne),
                new SqlParameter("@BeforeBelongTwo", model.BeforeBelongTwo),
                new SqlParameter("@BeforeBelongThree", model.BeforeBelongThree),
                new SqlParameter("@ParentId", model.ParentId),
                new SqlParameter("@BeforeParentId", model.BeforeParentId),
                new SqlParameter("@Reason", model.Reason)
            };

            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserRegisterModel model)
        {
            string strSql = "update Hot_UserBaseInfo set UB_UserRealName=@UB_UserRealName,UB_UserNickName=@UB_UserNickName,UB_UserMobile=@UB_UserMobile";
            if (!string.IsNullOrEmpty(model.loginPassword))
                strSql += ",UB_UserLoginPassword =@UB_UserLoginPassword";
            strSql += " where UB_UserID=@UserID";
            var parm = new[] {
                new SqlParameter("@UB_UserRealName", model.username),
                new SqlParameter("@UB_UserNickName", model.nickname),
                new SqlParameter("@UB_UserMobile", model.mobile),
                new SqlParameter("@UB_UserLoginPassword", model.loginPassword),
                new SqlParameter("@UserID", model.UserId),
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }


        /// <summary>
        /// 冻结/解冻账户
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateUserActive(int userId, int active)
        {
            string strSql = "update BM_User_extend set IsActive=@IsActive where UserId=@UserId";
            var param = new[] {
                        new SqlParameter("@IsActive",active),
                        new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleltUserInfo(int userId)
        {
            string strSql = "update Hot_UserBaseInfo set UB_IsDelete=1 where UB_UserID=@UB_UserID";
            var param = new[] {
                        new SqlParameter("@UB_UserID",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 获取他的盟友列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultPageModel GetAllyList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            string strSql = APP_USER_SELECT;

            strSql += " and ue.UserIdentity=0 and U.UB_BelongOne=@UB_BelongOne";

            var param = new[] {
                new SqlParameter("@UB_BelongOne",model.UserId),
            };
            //生成sql语句
            return getPageData<UserModel>(model.PageSize, model.PageIndex, strSql, "ue.CreateTime", param, (items) =>
            {
                items.ForEach((item) =>
                {
                    item.CustomerAmount = 100;
                    item.OrderSuccessAmount = 10;
                });
            });
        }
    }
}
