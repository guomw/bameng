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
    /// 资讯接口
    /// </summary>
    public class ArticleController : BaseController
    {

        /// <summary>
        /// 资讯列表  article/list
        /// </summary>
        /// <returns><![CDATA[{status:200,statusText:"OK",data:[{},{}]}]]></returns>        
        public JsonResult list()
        {
            return Json(new ResultModel(ApiStatusCode.OK));
        }
    }
}