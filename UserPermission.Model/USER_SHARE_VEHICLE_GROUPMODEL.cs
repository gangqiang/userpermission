using System;
namespace UserPermission.Model
{
    /// <summary>
    /// USER_SHARE_VEHICLE_GROUP:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public partial class USER_SHARE_VEHICLE_GROUPMODEL
    {
        public USER_SHARE_VEHICLE_GROUPMODEL()
        { }
        #region Model
        private int _sharegroupid;
        private string _targetid;
        private string _macid;
        /// <summary>
        /// �����group���Id
        /// </summary>
        public int SHAREGROUPID
        {
            set { _sharegroupid = value; }
            get { return _sharegroupid; }
        }
        /// <summary>
        /// ���ƺ�
        /// </summary>
        public string TARGETID
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// ����MACID
        /// </summary>
        public string MACID
        {
            set { _macid = value; }
            get { return _macid; }
        }
        #endregion Model

    }
}

