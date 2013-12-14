using System;
namespace UserPermission.Model
{
    /// <summary>
    /// ���ܲ˵���
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
        /// ���ܲ˵�Id
        /// </summary>
        public int FMID
        {
            set { _fmid = value; }
            get { return _fmid; }
        }
        /// <summary>
        /// ��ĿId
        /// </summary>
        public int? PROJECTID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// ���ܲ˵�����
        /// </summary>
        public string FMNAME
        {
            set { _fmname = value; }
            get { return _fmname; }
        }
        /// <summary>
        /// ����ҳ���ַ
        /// </summary>
        public string FMPAGEURL
        {
            set { _fmpageurl = value; }
            get { return _fmpageurl; }
        }
        /// <summary>
        /// ��������Id
        /// </summary>
        public int FMPARENTID
        {
            set { _fmparentid = value; }
            get { return _fmparentid; }
        }
        /// <summary>
        /// ��������ֵ(Խ��Խ��ǰ)
        /// </summary>
        public int? FMSORTNUM
        {
            set { _fmsortnum = value; }
            get { return _fmsortnum; }
        }
        /// <summary>
        /// �˵��㼶
        /// </summary>
        public string FMSTEP
        {
            set { _fmstep = value; }
            get { return _fmstep; }
        }
        /// <summary>
        /// �Ƿ�ĩ���˵�
        /// </summary>
        public int? FMISLAST
        {
            set { _fmislast = value; }
            get { return _fmislast; }
        }
        /// <summary>
        /// ��������(���ܵ�������Ϣ���������ڲ˵��������ʾ)
        /// </summary>
        public string FMDESC
        {
            set { _fmdesc = value; }
            get { return _fmdesc; }
        }

        /// <summary>
        /// ����״̬ (0������ 1����Ч  2����ɾ��)
        /// </summary>
        public int? FMSTATUS
        {
            set { _fmstatus = value; }
            get { return _fmstatus; }
        }
        #endregion Model

    }
}

