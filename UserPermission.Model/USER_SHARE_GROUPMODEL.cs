using System;
namespace UserPermission.Model
{
    /// <summary>
    /// USER_SHARE_GROUP:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public partial class USER_SHARE_GROUPMODEL
    {
        public USER_SHARE_GROUPMODEL()
        { }
        #region Model
        private int _id;
        private int _companycode;
        private string _groupname;
        private int _parentid;
        private int _state;
        private string _grade;
        private int _vehiclenum;
        private string _groupdesc = string.Empty;

        /// <summary>
        /// ���� 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// ��������
        /// </summary>
        public string GROUPNAME
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public int PARENTID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// ״̬��0 ��Ч 1 ��Ч
        /// </summary>
        public int STATE
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// �㼶
        /// </summary>
        public string GRADE
        {
            set { _grade = value; }
            get { return _grade; }
        }

        /// <summary>
        /// �������������
        /// </summary>
        public int VEHICLENUM
        {
            get { return _vehiclenum; }
            set { _vehiclenum = value; }
        }

        /// <summary>
        /// ������������ע
        /// </summary>
        public string GROUPDESC
        {
            get { return _groupdesc; }
            set { _groupdesc = value; }
        }
        #endregion Model

    }
}

