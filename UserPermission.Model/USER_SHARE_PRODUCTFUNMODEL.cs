using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 产品功能表
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
		/// 产品Id
		/// </summary>
		public int PROCUTID
		{
			set{ _procutid=value;}
			get{return _procutid;}
		}
		/// <summary>
		/// 功能Id
		/// </summary>
		public int FUNID
		{
			set{ _funid=value;}
			get{return _funid;}
		}
		#endregion Model

	}
}

