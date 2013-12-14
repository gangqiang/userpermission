using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 产品表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_PRODUCTMODEL
	{
		public USER_SHARE_PRODUCTMODEL()
		{}
		#region Model
		private int _productid;
		private int _projectid=0;
		private string _productname="";
		private string _productdesc;
		private int _productflag;
		/// <summary>
		/// 产品ID 
		/// </summary>
		public int PRODUCTID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 所属项目ID
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string PRODUCTNAME
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 产品描述
		/// </summary>
		public string PRODUCTDESC
		{
			set{ _productdesc=value;}
			get{return _productdesc;}
		}
		/// <summary>
		/// 产品状态 (0:正常 1：无效  2：已删除)
		/// </summary>
		public int PRODUCTFLAG
		{
			set{ _productflag=value;}
			get{return _productflag;}
		}
		#endregion Model

	}
}

