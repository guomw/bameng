using BAMENG.CONFIG;
using BAMENG.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace BAMENG.API.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [ActionAuthorize]
        [HttpGet]
        public ResultModel Login()
        {            
            return new ResultModel(ApiStatusCode.OK);
        }
    }
}
