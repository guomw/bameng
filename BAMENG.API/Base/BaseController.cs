using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BAMENG.API.Base;
using System.Reflection;
using BAMENG.MODEL;

namespace BAMENG.API
{
    /// <summary>
    /// Class BaseController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class BaseController : Controller
    {

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static BaseController Instance
        {
            get
            {
                return new BaseController();
            }
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式的 <see cref="T:System.Web.Mvc.JsonResult" /> 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。</returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new CustomJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        /// <summary>
        /// Jsons the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="jsonRequest">The json request.</param>
        /// <returns>JsonResult.</returns>
        public new JsonResult Json(object data, string contentType, JsonRequestBehavior jsonRequest)
        {
            return new CustomJsonResult { Data = data, ContentType = contentType, JsonRequestBehavior = jsonRequest };
        }


        /// <summary>
        /// Jsons the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="jsonRequest">The json request.</param>
        /// <returns>JsonResult.</returns>
        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new CustomJsonResult { Data = data, ContentType = "application/json", JsonRequestBehavior = jsonRequest };
        }


        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 <see cref="T:System.Web.Mvc.JsonResult" /> 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        public new JsonResult Json(object data)
        {
            return new CustomJsonResult { Data = data, ContentType = "application/json", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// 授权登录用户数据
        /// </summary>
        /// <value>The user data.</value>
        public UserModel AuthUserData { get; set; }
    }
}