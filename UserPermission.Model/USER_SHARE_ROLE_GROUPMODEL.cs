using System;
namespace UserPermission.Model
{
	/// <summary>
	/// USER_SHARE_ROLE_GROUP:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_ROLE_GROUPMODEL
	{
		public USER_SHARE_ROLE_GROUPMODEL()
		{}
		#region Model
		private int _roleid;
        private int _sharegroupid;
		/// <summary>
		/// ��ɫId
		/// </summary>
        public int ROLEID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// ��ɫ�����ķ���ID
		/// </summary>
        public int SHAREGROUPID
		{
			set{ _sharegroupid=value;}
			get{return _sharegroupid;}
		}
		#endregion Model

	}
}

