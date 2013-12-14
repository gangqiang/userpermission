using System;
namespace UserPermission.Model
{
    /// <summary>
    /// 角色信息表
    /// </summary>
    [Serializable]
    public partial class USER_SHARE_ROLESMODEL
    {
        public USER_SHARE_ROLESMODEL()
        { }
        #region Model
        private int _roleid;
        private string _rolename;
        private string _roledesc;
        private int _projectid;
        private int _companyid = 0;
        private int? _status;
        private int _creatorid = 0;
        private DateTime _createdate = DateTime.Now;

        /// <summary>
        /// 角色Id
        /// </summary>
        public int ROLEID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string ROLENAME
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string ROLEDESC
        {
            set { _roledesc = value; }
            get { return _roledesc; }
        }
        /// <summary>
        /// 所属项目Id
        /// </summary>
        public int PROJECTID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int COMPANYID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 状态( 0:正常 1:已删除  )
        /// </summary>
        public int? STATUS
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public int CreatorId
        {
            get { return _creatorid; }
            set { _creatorid = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        #endregion Model

    }
}

