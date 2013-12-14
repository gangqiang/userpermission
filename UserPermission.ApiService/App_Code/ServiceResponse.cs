//======================================================================
// Copyright (c) 苏州天泽信息科技有限公司. All rights reserved.
// 所属项目：PermissionApi
// 创 建 人：wgq
// 创建日期：2012-1-7 09:47:36
// 用    途：权限接口项目
//====================================================================== 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;

/// <summary>
///服务响应
/// </summary>
public class ServiceResponse
{
    private const string root = "<?xml version=\"1.0\" encoding=\"gbk\"?><response>{0}</response>";

    public ServiceResponse()
    {

    }

    public ShareEnum.ServiceAction ActionType;

    public ShareEnum.ApiResultStatus ErrorType = ShareEnum.ApiResultStatus.Success;

    public string ErrorDesc = string.Empty;

    public string Result = string.Empty;

    public string GetXML()
    {

        string response = string.Empty;

        string errorDesc = EnumPlus.GetEnumDescription(typeof(ShareEnum.ApiResultStatus), this.ErrorType.ToString("d"));
        if (!string.IsNullOrEmpty(ErrorDesc))
        {
            errorDesc = ErrorDesc;
        }
        response += string.Format("<result><code>{0}</code><desc><![CDATA[{1}]]></desc>{2}</result>"
                                  , Convert.ToInt16(this.ErrorType)
                                  , errorDesc, this.Result);
        response = string.Format(root, response);
        return response;

    }
}