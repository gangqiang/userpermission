using System;
namespace UserPermisson.Model
{
	/// <summary>
	/// ��ɫ����Ȩ�ޱ�
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
		/// ��ɫId
		/// </summary>
		public int? ROLEID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// ���ܲ˵�Id
		/// </summary>
		public int? FUNID
		{
			set{ _funid=value;}
			get{return _funid;}
		}
		#endregion Model

	}
}

