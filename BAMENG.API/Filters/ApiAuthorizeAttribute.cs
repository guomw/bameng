using BAMENG.CONFIG;
using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BAMENG.API
{

    /// <summary>
    /// 验证跨域访问权限
    /// </summary>
    public class AllowOriginAttribute
    {
        public static bool onExcute(ControllerContext context, string[] AllowSites)
        {
            bool Success = true;
            var origin = context.HttpContext.Request.Headers["Origin"];
            Action action = () =>
            {
                context.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", origin);
                context.HttpContext.Response.StatusCode = (int)ApiRequestStatusCode.OK;
            };
            if (AllowSites != null && AllowSites.Any())
            {
                if (AllowSites.Contains(origin) || AllowSites.Contains("*"))
                {
                    Success = true;
                    action();
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)ApiRequestStatusCode.禁止请求;
                    Success = false;
                }
            }
            return Success;
        }
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    public class ApiAuthorizeAttribute
    {
        HttpContextBase ctx = null;
        public ApiRequestStatusCode onExcute(HttpContextBase context)
        {
            ctx = context;
            Dictionary<string, object> prams = GetParams(context.Request);
            string requestSign = string.Empty;
            if (prams.ContainsKey("sign"))
                requestSign = prams["sign"].ToString();

            if (string.IsNullOrEmpty(requestSign))
            {
                LogHelper.Log(string.Format("currentSign:{0}", requestSign));
                return ApiRequestStatusCode.未授权;
            }

            Dictionary<string, string> paramters = new Dictionary<string, string>();
            foreach (var item in prams)
            {
                if (item.Key != "sign" && !string.IsNullOrEmpty(item.Value.ToString()))
                {
                    paramters.Add(item.Key.ToLower(), context.Server.UrlDecode(item.Value.ToString()));
                }
            }
            string currentSign = SignatureHelper.BuildSign(paramters, ConstConfig.SECRET_KEY);
            LogHelper.Log(string.Format("currentSign:{0},requestSign:{1}", currentSign, requestSign));
            if (!requestSign.Equals(currentSign))
            {
                return ApiRequestStatusCode.未授权;
            }
            return ApiRequestStatusCode.OK;
        }
        /// <summary>
        /// 获取 get/post 数据
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetParams(HttpRequestBase Request)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (Request.HttpMethod.ToLower() == "post") //post 数据请求
            {
                NameValueCollection values = HttpContext.Current.Request.Form;
                foreach (string m in values.Keys)
                {
                    p.Add(m, HttpUtility.UrlDecode(values[m]));
                }
            }
            else  //get 数据请求
            {
                string url = Request.Url.ToString();
                if (url == null || url == "")
                    return p;
                int questionMarkIndex = url.IndexOf('?');
                if (questionMarkIndex == url.Length - 1)
                    return p;
                string ps = url.Substring(questionMarkIndex + 1);
                if (ps != null && !string.IsNullOrEmpty(ps))
                {
                    ps = HttpContext.Current.Server.UrlDecode(ps);
                    // 开始分析参数对   
                    Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                    MatchCollection mc = re.Matches(ps);
                    foreach (Match m in mc)
                    {
                        p.Add(m.Result("$2"), HttpUtility.UrlDecode(m.Result("$3")));
                    }
                }
            }
            return p;
        }

    }

    /// <summary>
    /// 设置action的访问权限
    /// </summary>
    public class ActionAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] AllowSites { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断当前访问域是否有访问权限
            bool flag = AllowOriginAttribute.onExcute(filterContext, AllowSites);
            if (!WebConfig.debugMode() && flag)
            {
                ApiAuthorizeAttribute authorize = new ApiAuthorizeAttribute();
                //签名验证,并返回验证结果
                ApiRequestStatusCode apiCode = authorize.onExcute(filterContext.HttpContext);

                filterContext.HttpContext.Response.StatusCode = (int)apiCode;
            }
        }

    }
}