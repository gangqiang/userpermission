using System;
namespace UserPermission.Model
{
	/// <summary>
	/// ��Ʒ���ܱ�
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_PRODUCTFUNMODEL
	{
		public USER_SHARE_PRODUCTFUNMODEL()
		{}
		#region Model
		private int _procutid;
		private int _funid;
		/// <summary>
		/// ��ƷId
		/// </summary>
		public int PROCUTID
		{
			set{ _procutid=value;}
			get{return _procutid;}
		}
		/// <summary>
		/// ����Id
		/// </summary>
		public int FUNID
		{
			set{ _funid=value;}
			get{return _funid;}
		}
		#endregion Model

	}
}

