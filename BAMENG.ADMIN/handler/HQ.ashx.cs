using BAMENG.CONFIG;
using BAMENG.LOGIC;
using BAMENG.MODEL;
using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BAMENG.ADMIN.handler
{
    /// <summary>
    /// HQ--总部
    /// </summary>
    public class HQ : PageBaseHelper, IHttpHandler
    {
        public new void ProcessRequest(HttpContext context)
        {

            string resultMsg = string.Format(@"{0} header:{1} Form:{2} UserAgent:{3} IP:{4};referrer:{5}"
                  , context.Request.Url.ToString()
                  , context.Request.Headers.ToString()
                  , context.Request.Form.ToString()
                  , StringHelper.ToString(context.Request.UserAgent)
                  , StringHelper.GetClientIP()
                  , context.Request.UrlReferrer != null ? StringHelper.ToString(context.Request.UrlReferrer.AbsoluteUri) : ""
                 );
            try
            {
                DoRequest(context);
                LogHelper.Log(resultMsg, LogHelperTag.INFO, WebConfig.debugMode());
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("{0} StackTrace:{1} Message:{2}", resultMsg, ex.StackTrace, ex.Message), LogHelperTag.ERROR);
            }
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public int ShopID
        {
            get
            {
                return GetFormValue("shopId", 0);
            }
        }

        public int UserId
        {
            get
            {
                return GetFormValue("userid", 0);
            }
        }


        public string action { get { return GetFormValue("action", ""); } }

        public string json { get; set; }

        private void DoRequest(HttpContext context)
        {
            //全部转换为大写
            try
            {
                switch (action.ToUpper())
                {
                    case "GETSHOPLIST": //获取门店列表
                        GetShopList();
                        break;
                    case "UPDATESHOP":
                        UpdateShopInfo();
                        break;

                    case "DELETESHOP":
                        DeleteShop();
                        break;
                    case "UPDATESHOPACTIVE":
                        UpdateShopActive();
                        break;

                    case "GETUSERLIST":
                        GetUserList();
                        break;
                    case "EDITUSER":
                        EditUser();
                        break;

                    case "DELETEUSER":
                        DeleteUser();
                        break;
                    case "UPDATEUSERACTIVE":
                        UpdateUserActive();
                        break;
                    case "GETUSERINFO":
                        GetUserInfo();
                        break;
                    case "GETALLYLIST":
                        GetAllyList();
                        break;
                    case "GETCUSTOMERLISTBYUSERID":
                        GetCustomerListByUserId();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("action:{0} StackTrace:{1} Message:{2}", action, ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.SERVICEERROR));
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(json);

        }


        /// <summary>
        /// 获取门店列表
        /// </summary>
        private void GetShopList()
        {
            int shopType = 1;
            int shopBelongId = 0;

            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = "",
                endTime = "",
                key = GetFormValue("key", ""),
                city = GetFormValue("city", ""),
                province = GetFormValue("prov", "")
            };
            var data = ShopLogic.GetShopList(shopType, shopBelongId, model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }
        /// <summary>
        /// 修改门店信息
        /// </summary>
        private void UpdateShopInfo()
        {
            string shopname = GetFormValue("shopname", "");
            string username = GetFormValue("username", "");
            string usermobile = GetFormValue("usermobile", "");
            string userloginname = GetFormValue("userloginname", "");
            string password = GetFormValue("password", "");
            string shopprov = GetFormValue("shopprov", "");
            string shopcity = GetFormValue("shopcity", "");

            bool flag = ShopLogic.EditShopInfo(new ShopModel()
            {
                ShopID = ShopID,
                ShopName = shopname,
                ShopArea = "",
                ShopAddress = "",
                ShopBelongId = 0,
                ShopCity = shopcity,
                ShopProv = shopprov,
                Contacts = username,
                ContactWay = usermobile,
                LoginName = userloginname,
                LoginPassword = password,
                IsActive = 1,
                ShopType = 1
            });
            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ShopID > 0 ? ApiStatusCode.更新失败 : ApiStatusCode.添加失败));
        }


        private void DeleteShop()
        {
            if (ShopLogic.DeleteShop(ShopID))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.删除失败));
        }


        private void UpdateShopActive()
        {
            int active = GetFormValue("active", 1);
            if (ShopLogic.UpdateShopActive(ShopID, active))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
        }

        /// <summary>
        /// 获取用户列表(总后台，获取盟主列表)
        /// </summary>
        private void GetUserList()
        {

            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = "",
                endTime = "",
                key = GetFormValue("key", ""),
                searchType = GetFormValue("searchType", 0)
            };
            var data = UserLogic.GetUserList(0, GetFormValue("ally", 1), model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        private void EditUser()
        {
            //TODO:方便测试，此处门店ID写死，真实逻辑是，根据当前登录的门店账户，获取门店ID
            bool flag = UserLogic.EditUserInfo(new UserRegisterModel()
            {
                loginName = GetFormValue("userloginname", ""),
                username = GetFormValue("username", ""),
                nickname = GetFormValue("usernickname", ""),
                loginPassword = GetFormValue("password", ""),
                mobile = GetFormValue("usermobile", ""),
                storeId = ConstConfig.storeId,
                ShopId = 1,
                UserIdentity = 1,
                UserId = UserId
            });
            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ShopID > 0 ? ApiStatusCode.更新失败 : ApiStatusCode.添加失败));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        private void DeleteUser()
        {
            if (UserLogic.DeleteUser(UserId))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.删除失败));
        }

        /// <summary>
        /// 冻结、解冻用户账户
        /// </summary>
        private void UpdateUserActive()
        {
            int active = GetFormValue("active", 1);
            if (UserLogic.UpdateUserActive(UserId, active))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        private void GetUserInfo()
        {
            var data = UserLogic.GetModel(UserId);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 根据盟主ID，获取他的盟友列表
        /// </summary>
        private void GetAllyList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                UserId = UserId
            };
            var data = UserLogic.GetAllyList(model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }
        /// <summary>
        /// 根据用户ID获取该用户下的客户列表
        /// </summary>
        private void GetCustomerListByUserId()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                UserId = UserId
            };
            var data = CustomerLogic.GetCustomerListByUserId(model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }
    }
}