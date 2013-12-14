using System;
namespace UserPermission.Model
{
	/// <summary>
	/// USER_SHARE_ROLE_GROUP:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 角色Id
		/// </summary>
        public int ROLEID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 角色所属的分组ID
		/// </summary>
        public int SHAREGROUPID
		{
			set{ _sharegroupid=value;}
			get{return _sharegroupid;}
		}
		#endregion Model

	}
}

