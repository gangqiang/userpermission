using System;
namespace UserPermission.Model
{
    /// <summary>
    /// USER_SHARE_VEHICLE_GROUP:实体类(属性说明自动提取数据库字段的描述信息)
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
        /// 外键，group表的Id
        /// </summary>
        public int SHAREGROUPID
        {
            set { _sharegroupid = value; }
            get { return _sharegroupid; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string TARGETID
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// 车机MACID
        /// </summary>
        public string MACID
        {
            set { _macid = value; }
            get { return _macid; }
        }
        #endregion Model

    }
}

