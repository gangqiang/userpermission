using System;
namespace UserPermission.Model
{
    /// <summary>
    /// ��ɫ��Ϣ��
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
        /// ��ɫId
        /// </summary>
        public int ROLEID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string ROLENAME
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string ROLEDESC
        {
            set { _roledesc = value; }
            get { return _roledesc; }
        }
        /// <summary>
        /// ������ĿId
        /// </summary>
        public int PROJECTID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// ������˾ID
        /// </summary>
        public int COMPANYID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// ״̬( 0:���� 1:��ɾ��  )
        /// </summary>
        public int? STATUS
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// ������Id
        /// </summary>
        public int CreatorId
        {
            get { return _creatorid; }
            set { _creatorid = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        #endregion Model

    }
}

