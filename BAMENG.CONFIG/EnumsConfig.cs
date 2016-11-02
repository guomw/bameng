using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.CONFIG
{
    /// <summary>
    ///  HTTP请求状态码 (谨慎修改!!!)
    ///  作者:郭孟稳
    /// </summary>
    public enum ApiRequestStatusCode
    {
        /// <summary>
        /// 务器已成功处理了请求
        /// </summary>
        [Description("OK")]
        OK = 200,
        /// <summary>
        /// 服务器成功处理了请求，但没有返回任何内容。
        /// </summary>
        [Description("服务器成功处理了请求，但没有返回任何内容")]
        无返回 = 204,
        /// <summary>
        /// 要完成请求，需要进一步操作
        /// </summary>
        [Description("要完成请求，需要进一步操作")]
        失败 = 300,
        /// <summary>
        /// 请求要求身份验证
        /// </summary>
        [Description(" 请求要求身份验证")]
        未授权 = 401,
        /// <summary>
        /// 服务器拒绝请求。
        /// </summary>
        [Description("服务器拒绝请求")]
        禁止请求 = 403,
        /// <summary>
        /// 服务器找不到请求的网页。
        /// </summary>
        [Description("服务器找不到请求的网页")]
        地址错误 = 404,
        /// <summary>
        /// 服务器遇到错误，无法完成请求
        /// </summary>
        [Description("服务器遇到错误，无法完成请求")]
        服务器错误 = 500,
    }


    /// <summary>
    /// 接口业务状态码
    /// 作者:郭孟稳
    /// </summary>
    public enum ApiStatusCode
    {
        /// <summary>
        /// 服务器遇到错误
        /// </summary>
        [Description("服务器遇到错误，无法完成请求")]
        SERVICEERROR = 500,
        /// <summary>
        /// 无数据
        /// </summary>
        [Description("无数据")]
        NULL = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        OK = 200,

        [Description("更新失败")]
        更新失败 = 6001,
        [Description("操作失败")]
        操作失败 = 6002,
        [Description("删除失败")]
        删除失败 = 6003,
        [Description("添加失败")]
        添加失败 = 6004,

        [Description("账户不存在")]
        账户不存在 = 7001,
        [Description("账户或密码不正确")]
        账户密码不正确 = 7002,
        [Description("账户已禁用")]
        账户已禁用 = 7003,
        [Description("用户信息丢失，请重新登录")]
        没有登录 = 70034
    }


    /// <summary>
    /// 搜索类型
    /// </summary>
    public enum SearchType
    {
        姓名 = 1,
        昵称 = 2,
        手机 = 3,
        门店 = 4,
        标题 = 5,
    }


    public enum OSOptions
    {
        android,
        iphone
    }
}
