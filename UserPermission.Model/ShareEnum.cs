using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UserPermission.Model
{
    public class ShareEnum
    {
        /// <summary>
        /// 项目状态
        /// </summary>
        public enum ProjectStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("停用")]
            StopUse = 1
        }

        /// <summary>
        /// 产品状态
        /// </summary>
        public enum ProductFlag
        {
            [Description("正常")]
            Normal = 0,
            [Description("停用")]
            StopUse = 1
        }

        /// <summary>
        /// 账号状态
        /// </summary>
        public enum AccountStatus
        {
            [Description("有效")]
            Normal = 0,
            [Description("无效")]
            StopUse = 1,
            [Description("已删除")]
            Del = 2
        }

        /// <summary>
        /// 角色状态
        /// </summary>

        public enum RoleStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("已删除")]
            Del = 2
        }

        /// <summary>
        /// 功能菜单状态
        /// </summary>
        public enum FunMenuStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("无效")]
            StopUse = 1,
        }

        /// <summary>
        /// 公司菜单状态
        /// </summary>
        public enum CompanyFunMenuStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("无效")]
            StopUse = 1,
        }

        public enum CompanyGroupStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("无效")]
            StopUse = 1
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            [Description("新增项目信息")]
            AddProject = 1,
            [Description("修改项目信息")]
            EditProject = 2,
            [Description("更改项目状态")]
            ChangeStatus = 3,
            [Description("新增公司关联信息")]
            AddCompanyRelate = 4,
            [Description("修改公司关联信息")]
            EditCompanyRelate = 5,
            [Description("更改公司关联状态")]
            ChangeCompanyRelateStatus = 6,
            [Description("新增项目菜单")]
            AddProjectFunMenu = 7,
            [Description("修改项目菜单")]
            EditProjectFunMenu = 8,
            [Description("更改项目菜单状态")]
            ChangeProjectFunMenuStatus = 9,
            [Description("新增产品信息")]
            AddProduct = 10,
            [Description("修改产品信息")]
            EditProduct = 11,
            [Description("更改产品状态")]
            ChangeProductStatus = 12,
            [Description("新增账号")]
            AddAccount = 13,
            [Description("修改账号")]
            EditAccount = 14,
            [Description("删除账号")]
            DelAccount = 15,
            [Description("新增角色")]
            AddRole = 16,
            [Description("修改角色")]
            EditRole = 17,
            [Description("删除角色")]
            DelRole = 18,
            [Description("修改公司功能菜单")]
            EditCompanyFun = 19,
            [Description("停用公司功能菜单")]
            StopUseCompanyFun = 20,
            [Description("新增公司车辆分组")]
            AddCompanyGroup = 21,
            [Description("修改公司车辆分组")]
            EditCompanyGroup = 22,
            [Description("停用公司车辆分组")]
            StopUseCompanyGroup = 23
        }

        /// <summary>
        /// 公司类型
        /// </summary>
        public enum CompanyType
        {
            [Description("运管平台注册")]
            YgCompany = 1,
            [Description("平台注册")]
            PlatCompany = 2,
            [Description("应用系统注册")]
            ShareCompany = 3
        }

        /// <summary>
        /// 公司关联表状态
        /// </summary>
        public enum CompanyRelateStatus
        {
            [Description("正常")]
            Normal = 0,
            [Description("停用")]
            StopUse = 1
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public enum ApiResultStatus
        {
            [Description("成功")]
            Success = 0,
            [Description("错误的KEY")]
            KeyErr = 101,
            [Description("错误的Action")]
            ActionErr = 102,
            [Description("错误的传入参数")]
            InputParaErr = 103,
            [Description("错误的URL或方法")]
            UrlOrActionErr = 104,
            [Description("未知异常")]
            ExceptionErr = 105,
            [Description("并发冲突")]
            ConflitErr = 107,
            [Description("HTTP请求方法错误，只支持POST方法")]
            HttpMethodErr = 108,
            [Description("缺少请求内容")]
            RequireContentErr = 109,
            [Description("编码格式错误")]
            CodeTypeErr = 110,
            [Description("请求的XML格式错误")]
            XMLFormatErr = 111,
            [DescriptionAttribute("不存在的用户名")]
            UnKnownAccount = 201,
            [Description("用户名和密码不匹配")]
            UnValidUser = 202,
            [Description("原密码错误")]
            OldPassUnCorrect = 203,
            [Description("不存在的公司编码")]
            UnValidCompanyCode = 204,
            [Description("公司信息和账号信息不匹配")]
            CompanyNotMatchAccount = 205,
            [Description("账号名已经存在")]
            AccountNameExists = 206,
            [Description("公司已注册")]
            CompanyExists = 207 ,
            [Description("公司已停用")]
            CompanyStopUse=208
        }

        /// <summary>
        /// Action 定义
        /// </summary>
        public enum ServiceAction
        {
            [Description("新增账号")]
            AddAccount = 3,
            [Description("账号登陆")]
            AccountLogin = 4,
            [Description("修改密码")]
            EditAccountPwd = 5,
            [Description("危险品和运管平台自动注册接口")]
            AutoRegister = 6,
            [Description("TMS平台获取账号信息")]
            GetAccounts=7
        }


    }
}
