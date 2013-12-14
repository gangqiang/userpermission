using System;
namespace UserPermission.Model
{
	/// <summary>
	/// ��Ŀ��Ϣ��
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_PROJECTMODEL
	{
		public USER_SHARE_PROJECTMODEL()
		{}
		#region Model
		private int _projectid;
		private string _projectname="";
		private string _apiservicekey="";
		private DateTime _createdate= Convert.ToDateTime(DateTime.Now);
		private string _projectremark;
		private int? _status;
		/// <summary>
		/// ��ĿId
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string PROJECTNAME
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// �ӿڷ�����Կ
		/// </summary>
		public string APISERVICEKEY
		{
			set{ _apiservicekey=value;}
			get{return _apiservicekey;}
		}
		/// <summary>
		/// ��ͨ����
		/// </summary>
		public DateTime CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// ��Ŀ��ע
		/// </summary>
		public string PROJECTREMARK
		{
			set{ _projectremark=value;}
			get{return _projectremark;}
		}
		/// <summary>
		/// ״̬
		/// </summary>
		public int? STATUS
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

