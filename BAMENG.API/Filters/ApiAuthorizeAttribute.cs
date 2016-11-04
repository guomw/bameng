using BAMENG.CONFIG;
using BAMENG.MODEL;
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
        /// <summary>
        /// Ons the excute.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="AllowSites">The allow sites.</param>
        /// <returns></returns>
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
                if (AllowSites.Contains(origin)/* || AllowSites.Contains("*")*/)
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
        /// <summary>
        /// Ons the excute.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>ApiRequestStatusCode.</returns>
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
            //LogHelper.Log(string.Format("currentSign:{0},requestSign:{1}", currentSign, requestSign));
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
    /// <seealso cref="System.Web.Mvc.ActionFilterAttribute" />
    public class ActionAuthorizeAttribute : ActionFilterAttribute
    {
        private bool _AllowCrossDomainVisit = false;
        /// <summary>
        /// 是否允许跨域访问，默认不开启跨域访问
        /// </summary>
        /// <value>true if [allow cross domain visit]; otherwise, false.</value>
        public bool AllowCrossDomainVisit
        {
            get
            {
                return _AllowCrossDomainVisit;
            }

            set
            {
                _AllowCrossDomainVisit = value;
            }
        }
        /// <summary>
        /// 设置允许跨域访问的网站
        /// </summary>
        /// <value>The allow sites.</value>
        public string[] AllowSites { get; set; }

        /// <summary>
        /// 身份授权登录
        /// </summary>
        /// <value>true if authentication; otherwise, false.</value>
        public bool AuthLogin
        {
            get
            {
                return _Auth;
            }

            set
            {
                _Auth = value;
            }
        }
        private bool _Auth = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断当前访问域是否有访问权限
            bool flag = AllowCrossDomainVisit ? AllowOriginAttribute.onExcute(filterContext, AllowSites) : true;

            //验证授权登录
            if (flag)
            {
                //如果启用了授权登录，需要验证登录信息是否正确
                if (AuthLogin)
                {
                    BaseController.Instance.AuthUserData = new UserModel() { };
                }
                else
                {
                    BaseController.Instance.AuthUserData = null;
                }
                //如果非测试环境下，需要验证签名
                if (!WebConfig.debugMode())
                {
                    ApiAuthorizeAttribute authorize = new ApiAuthorizeAttribute();
                    //签名验证,并返回验证结果
                    ApiRequestStatusCode apiCode = authorize.onExcute(filterContext.HttpContext);

                    filterContext.HttpContext.Response.StatusCode = 200;// (int)apiCode;
                }
            }
        }

    }
}