using System;
namespace UserPermission.Model
{
    /// <summary>
    /// USER_SHARE_GROUP:实体类(属性说明自动提取数据库字段的描述信息)
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
        /// 主键 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 分组名称
        /// </summary>
        public string GROUPNAME
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int PARENTID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 状态，0 有效 1 无效
        /// </summary>
        public int STATE
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 层级
        /// </summary>
        public string GRADE
        {
            set { _grade = value; }
            get { return _grade; }
        }

        /// <summary>
        /// 分组包含车辆数
        /// </summary>
        public int VEHICLENUM
        {
            get { return _vehiclenum; }
            set { _vehiclenum = value; }
        }

        /// <summary>
        /// 分组描述，备注
        /// </summary>
        public string GROUPDESC
        {
            get { return _groupdesc; }
            set { _groupdesc = value; }
        }
        #endregion Model

    }
}

