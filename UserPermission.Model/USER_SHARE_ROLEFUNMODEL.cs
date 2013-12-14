using System;
namespace UserPermisson.Model
{
	/// <summary>
	/// 角色功能权限表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_ROLEFUNMODEL
	{
		public USER_SHARE_ROLEFUNMODEL()
		{}
		#region Model
		private int? _roleid=0;
		private int? _funid=0;
		/// <summary>
		/// 角色Id
		/// </summary>
		public int? ROLEID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 功能菜单Id
		/// </summary>
		public int? FUNID
		{
			set{ _funid=value;}
			get{return _funid;}
		}
		#endregion Model

	}
}

