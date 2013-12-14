using System;
namespace UserPermission.Model
{
	/// <summary>
	/// ��˾�˵���
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_COMPANYFUNMODEL
	{
		public USER_SHARE_COMPANYFUNMODEL()
		{}

		#region Model
		private int _cfid;
		private int _fmid;
		private int _projectid=0;
		private int _companyid;
		private string _cfname;
		private string _cfanothername;
		private string _cfpageurl;
		private int? _cfparentid=0;
		private int? _cfsortnum=0;
		private string _cfstep;
		private int? _cfislast;
		private string _cfdesc;
		private int? _cfstatus=0;


		/// <summary>
		/// ����ID
		/// </summary>
		public int CFID
		{
			set{ _cfid=value;}
			get{return _cfid;}
		}
		/// <summary>
		/// ���ܲ˵�Id
		/// </summary>
		public int FMID
		{
			set{ _fmid=value;}
			get{return _fmid;}
		}
		/// <summary>
		/// ������ĿId
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// ��˾����
		/// </summary>
		public int COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// ���ܲ˵�����
		/// </summary>
		public string CFNAME
		{
			set{ _cfname=value;}
			get{return _cfname;}
		}
		/// <summary>
		/// �˵�����
		/// </summary>
		public string CFANOTHERNAME
		{
			set{ _cfanothername=value;}
			get{return _cfanothername;}
		}
		/// <summary>
		/// ����ҳ���ַ
		/// </summary>
		public string CFPAGEURL
		{
			set{ _cfpageurl=value;}
			get{return _cfpageurl;}
		}
		/// <summary>
		/// ��������Id
		/// </summary>
		public int? CFPARENTID
		{
			set{ _cfparentid=value;}
			get{return _cfparentid;}
		}
		/// <summary>
		/// ��������ֵ(Խ��Խ��ǰ)
		/// </summary>
		public int? CFSORTNUM
		{
			set{ _cfsortnum=value;}
			get{return _cfsortnum;}
		}
		/// <summary>
		/// �˵��㼶
		/// </summary>
		public string CFSTEP
		{
			set{ _cfstep=value;}
			get{return _cfstep;}
		}
		/// <summary>
		/// �Ƿ�ĩ���˵�
		/// </summary>
		public int? CFISLAST
		{
			set{ _cfislast=value;}
			get{return _cfislast;}
		}
		/// <summary>
		/// ��������(���ܵ�������Ϣ���������ڲ˵��������ʾ)
		/// </summary>
		public string CFDESC
		{
			set{ _cfdesc=value;}
			get{return _cfdesc;}
		}
		/// <summary>
		/// ����״̬(������Ŀ�Լ���������)
		/// </summary>
		public int? CFSTATUS
		{
			set{ _cfstatus=value;}
			get{return _cfstatus;}
		}
		#endregion Model

	}
}

