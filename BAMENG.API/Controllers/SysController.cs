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
    /// 
    /// </summary>
    public class SysController : Controller
    {
        /// <summary>
        /// 初始化接口  
        /// </summary>                
        [ActionAuthorize]
        [HttpGet]
        public ActionResult Init()
        {
            try
            {
                var data = AppSystemLogic.Instance.Initialize("1.0.0");
                return Json(new ResultModel(ApiStatusCode.OK, data), "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("Init error--->StackTrace:{0} message:{1}", ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                return Json(new ResultModel(ApiStatusCode.SERVICEERROR), "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUpdate(string clientVersion)
        {
            try
            {
                var versionData = AppSystemLogic.Instance.CheckUpdate(clientVersion, "android");

                return Json(new ResultModel(ApiStatusCode.OK, versionData), "application/json");
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("CheckUpdate error--->StackTrace:{0} message:{1}", ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                return Json(new ResultModel(ApiStatusCode.SERVICEERROR), "application/json");
            }
        }
    }
}