using System;
namespace UserPermission.Model
{
    /// <summary>
    /// 公司信息表
    /// </summary>
    [Serializable]
    public partial class USER_SHARE_COMPANYRELATEMODEL
    {
        public USER_SHARE_COMPANYRELATEMODEL()
        { }
        #region Model
        private int _cid;
        private int? _companytype;
        private string _companyname;
        private int? _companyid;
        private string _groupid;
        private string _groupidn;
        private string _productids;
        private string _projectids;
        private int _companycode;
        private int? _sharecompanyid;
        private int? _status;
        private int _adminid = 0;
        private DateTime _createdate = DateTime.Now;
        /// <summary>
        /// 公司Id
        /// </summary>
        public int CID
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 公司类型
        /// </summary>
        public int? COMPANYTYPE
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string COMPANYNAME
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 对应CompanyId
        /// </summary>
        public int? COMPANYID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 平台注册GroupId
        /// </summary>
        public string GROUPID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }

        /// <summary>
        /// 平台注册GroupIdn
        /// </summary>
        public string GROUPIDN
        {
            set { _groupidn = value; }
            get { return _groupidn; }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string PROJECTIDS
        {
            get { return _projectids; }
            set { _projectids = value; }
        }

        /// <summary>
        /// 产品Id
        /// </summary>
        public string PRODUCTIDS
        {
            set { _productids = value; }
            get { return _productids; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public int COMPANYCODE
        {
            set { _companycode = value; }
            get { return _companycode; }
        }

        /// <summary>
        /// 新增公司表ID
        /// </summary>
        public int? SHARECOMPANYID
        {
            set { _sharecompanyid = value; }
            get { return _sharecompanyid; }
        }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime CREATEDATE
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        /// <summary>
        /// 初始账号的Id
        /// </summary>
        public int ADMINID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }

        /// <summary>
        /// 状态(0:正常 1：已删除)
        /// </summary>
        public int? STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}

