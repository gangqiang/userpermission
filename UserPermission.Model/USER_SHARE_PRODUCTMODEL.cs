using System;
namespace UserPermission.Model
{
	/// <summary>
	/// ��Ʒ��
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
		/// ��ƷID 
		/// </summary>
		public int PRODUCTID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// ������ĿID
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string PRODUCTNAME
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string PRODUCTDESC
		{
			set{ _productdesc=value;}
			get{return _productdesc;}
		}
		/// <summary>
		/// ��Ʒ״̬ (0:���� 1����Ч  2����ɾ��)
		/// </summary>
		public int PRODUCTFLAG
		{
			set{ _productflag=value;}
			get{return _productflag;}
		}
		#endregion Model

	}
}

