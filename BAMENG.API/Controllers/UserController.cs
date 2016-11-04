using BAMENG.CONFIG;
using BAMENG.MODEL;
using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;

namespace BAMENG.API.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    public class UserController : BaseController
    {

        /// <summary>
        /// 登陆接口 POST:  user/login
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize(AuthLogin = false)]
        public JsonResult Login(string loginName, string password)
        {
            return Json(new ResultModel(ApiStatusCode.OK, new UserModel() { CreateTime = DateTime.Now, token = EncryptHelper.MD5("") }));
        }
        /// <summary>
        /// 签到  POST: user/signin
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize(AuthLogin = false)]        
        public JsonResult SignIn()
        {
            return Json(new ResultModel(ApiStatusCode.OK, AuthUserData));
        }

        /// <summary>
        /// 忘记密码   POST: user/forgetpwd
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <param name="password">The password.</param>
        /// <param name="verifyCode">The verify code.</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ForgetPwd(string mobile, string password, string verifyCode)
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 待结算盟豆列表 POST: user/tempsettlebeanlist
        /// </summary>
        /// <returns><![CDATA[{ status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult TempSettleBeanList()
        {
            return Json(new ResultModel(ApiStatusCode.OK, AuthUserData));
        }

        /// <summary>
        /// 兑换盟豆 POST: user/ConvertToBean
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ConvertToBean()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }


        /// <summary>
        /// 兑换记录流水 POST: user/ConvertFlow
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ConvertFlow()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 盟友列表 POST: user/allylist
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult allylist()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 兑换审核列表 POST: user/ConvertAuditList
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ConvertAuditList()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 兑换审核 POST: user/ConvertAudit
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ConvertAudit()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 个人信息 POST: user/myinfo
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult MyInfo()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 修改用户信息 POST: user/UpdateInfo
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult UpdateInfo()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 设置盟友奖励 POST: user/setallyaward
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult setallyaward()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 积分列表 POST: user/scoreList
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult scoreList()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }


        /// <summary>
        /// 盟豆流水列表 POST: user/BeanFlowList
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult BeanFlowList()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 盟友详情 POST: user/AllyInfo
        /// </summary>
        /// <returns></returns>
        [ActionAuthorize]
        public JsonResult AllyInfo()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 申请盟友接口 POST: user/AllyApply
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult AllyApply()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 盟友申请列表 POST: user/AllyApplylist
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult AllyApplylist()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 盟友申请审核 POST: user/AllyApplyAudit
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult AllyApplyAudit()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 修改密码 POST: user/ChanagePassword
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ChanagePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 修改手机号 POST: user/ChanageMobile
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <param name="verifyCode">The verify code.</param>
        /// <param name="newMobile">The new mobile.</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult ChanageMobile(string mobile, string verifyCode, string newMobile)
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 我的现金券列表 POST: user/MyCashCouponList
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult MyCashCouponList()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 我的业务 POST: user/MyBusiness
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult MyBusiness()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 我的财富 POST: user/MyTreasure
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult MyTreasure()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 盟友首页汇总 POST: user/AllyHomeSummary
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public JsonResult AllyHomeSummary()
        {
            return Json(new ResultModel(ApiStatusCode.OK));

        }
    }
}
