using BAMENG.CONFIG;
using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BAMENG.API.Controllers
{
    /// <summary>
    /// 客户接口
    /// </summary>
    public class CustomerController : BaseController
    {
        /// <summary>
        /// 审核列表 POST: customer/auditList
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public JsonResult auditList()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 审核 POST: customer/audit
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public JsonResult audit()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 创建客户新 POST: customer/create
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public JsonResult create()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }

        /// <summary>
        /// 客户列表 POST: customer/list
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:{}}]]></returns>
        public JsonResult list()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
    }
}