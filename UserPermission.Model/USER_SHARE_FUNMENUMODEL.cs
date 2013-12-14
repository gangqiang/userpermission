using System;
namespace UserPermission.Model
{
    /// <summary>
    /// 功能菜单表
    /// </summary>
    [Serializable]
    public partial class USER_SHARE_FUNMENUMODEL
    {
        public USER_SHARE_FUNMENUMODEL()
        { }
        #region Model
        private int _fmid;
        private int? _projectid;
        private string _fmname;
        private string _fmpageurl;
        private int _fmparentid = 0;
        private int? _fmsortnum = 0;
        private string _fmstep;
        private int? _fmislast;
        private string _fmdesc;
        private int? _fmstatus = 0;
        /// <summary>
        /// 功能菜单Id
        /// </summary>
        public int FMID
        {
            set { _fmid = value; }
            get { return _fmid; }
        }
        /// <summary>
        /// 项目Id
        /// </summary>
        public int? PROJECTID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 功能菜单名称
        /// </summary>
        public string FMNAME
        {
            set { _fmname = value; }
            get { return _fmname; }
        }
        /// <summary>
        /// 功能页面地址
        /// </summary>
        public string FMPAGEURL
        {
            set { _fmpageurl = value; }
            get { return _fmpageurl; }
        }
        /// <summary>
        /// 父级功能Id
        /// </summary>
        public int FMPARENTID
        {
            set { _fmparentid = value; }
            get { return _fmparentid; }
        }
        /// <summary>
        /// 功能排序值(越大越靠前)
        /// </summary>
        public int? FMSORTNUM
        {
            set { _fmsortnum = value; }
            get { return _fmsortnum; }
        }
        /// <summary>
        /// 菜单层级
        /// </summary>
        public string FMSTEP
        {
            set { _fmstep = value; }
            get { return _fmstep; }
        }
        /// <summary>
        /// 是否末级菜单
        /// </summary>
        public int? FMISLAST
        {
            set { _fmislast = value; }
            get { return _fmislast; }
        }
        /// <summary>
        /// 功能描述(功能的描述信息，可以用于菜单的鼠标提示)
        /// </summary>
        public string FMDESC
        {
            set { _fmdesc = value; }
            get { return _fmdesc; }
        }

        /// <summary>
        /// 功能状态 (0：正常 1：无效  2：已删除)
        /// </summary>
        public int? FMSTATUS
        {
            set { _fmstatus = value; }
            get { return _fmstatus; }
        }
        #endregion Model

    }
}

