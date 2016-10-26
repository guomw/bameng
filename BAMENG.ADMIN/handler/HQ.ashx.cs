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
    /// HQ 的摘要说明
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



        private void DoRequest(HttpContext context)
        {
            string json = string.Empty;
            string action = GetFormValue("action", "");
            //全部转换为大写
            switch (action.ToUpper())
            {
                case "GETSHOPLIST": //获取门店列表
                    {
                        int shopType = 1;
                        int shopBelongId = 0;
                        SearchModel model = new SearchModel()
                        {
                            PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                            PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                            startTime = "",
                            endTime = ""
                        };
                        var data = ShopLogic.GetShopList(shopType, shopBelongId, model);
                        json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
                    }
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(json);

        }

    }
}