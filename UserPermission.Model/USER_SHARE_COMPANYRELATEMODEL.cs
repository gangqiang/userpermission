using System;
namespace UserPermission.Model
{
    /// <summary>
    /// ��˾��Ϣ��
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
        /// ��˾Id
        /// </summary>
        public int CID
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// ��˾����
        /// </summary>
        public int? COMPANYTYPE
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// ��˾����
        /// </summary>
        public string COMPANYNAME
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// ��ӦCompanyId
        /// </summary>
        public int? COMPANYID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// ƽ̨ע��GroupId
        /// </summary>
        public string GROUPID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }

        /// <summary>
        /// ƽ̨ע��GroupIdn
        /// </summary>
        public string GROUPIDN
        {
            set { _groupidn = value; }
            get { return _groupidn; }
        }

        /// <summary>
        /// ��ĿID
        /// </summary>
        public string PROJECTIDS
        {
            get { return _projectids; }
            set { _projectids = value; }
        }

        /// <summary>
        /// ��ƷId
        /// </summary>
        public string PRODUCTIDS
        {
            set { _productids = value; }
            get { return _productids; }
        }
        /// <summary>
        /// ��˾����
        /// </summary>
        public int COMPANYCODE
        {
            set { _companycode = value; }
            get { return _companycode; }
        }

        /// <summary>
        /// ������˾��ID
        /// </summary>
        public int? SHARECOMPANYID
        {
            set { _sharecompanyid = value; }
            get { return _sharecompanyid; }
        }

        /// <summary>
        /// ע������
        /// </summary>
        public DateTime CREATEDATE
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        /// <summary>
        /// ��ʼ�˺ŵ�Id
        /// </summary>
        public int ADMINID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }

        /// <summary>
        /// ״̬(0:���� 1����ɾ��)
        /// </summary>
        public int? STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}

