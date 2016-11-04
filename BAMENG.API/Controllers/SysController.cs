using BAMENG.CONFIG;
using BAMENG.LOGIC;
using BAMENG.MODEL;
using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace BAMENG.API.Controllers
{
    /// <summary>
    /// 全局接口
    /// </summary>
    public class SysController : BaseController
    {


        /// <summary>
        /// 初始化接口 POST: sys/init
        /// </summary>  
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        [ActionAuthorize]
        public ActionResult Init()
        {
            try
            {
                var data = AppSystemLogic.Instance.Initialize("1.0.0");
                return Json(new ResultModel(ApiStatusCode.OK, data));
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("Init error--->StackTrace:{0} message:{1}", ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                return Json(new ResultModel(ApiStatusCode.SERVICEERROR));
            }
        }

        /// <summary>
        /// 检查更新 POST: sys/checkupdate
        /// </summary>
        /// <param name="clientVersion">客户的版本号</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public ActionResult CheckUpdate(string clientVersion)
        {
            try
            {
                var versionData = AppSystemLogic.Instance.CheckUpdate(clientVersion, "android");

                return Json(new ResultModel(ApiStatusCode.OK, versionData));
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("CheckUpdate error--->StackTrace:{0} message:{1}", ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                return Json(new ResultModel(ApiStatusCode.SERVICEERROR));
            }
        }

        /// <summary>
        /// 发送短信 POST: sys/sendsms
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public ActionResult SendSms(string mobile)
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 焦点图片 POST: sys/focuspic
        /// </summary>
        /// <param name="type">0集团轮播图，2 首页轮播图</param>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public ActionResult FocusPic(int type)
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }


        /// <summary>
        /// 上传图片 POST: sys/uploadpic
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public ActionResult UploadPic()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }




    }
}