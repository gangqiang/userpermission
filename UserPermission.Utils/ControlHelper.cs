 
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
namespace UserPermission.Utils
{
    public sealed class ControlHelper
    {
        #region DropDownList

        public static void BindListControl(ListControl lc, object datasource, string strText, string strValue)
        {
            lc.DataSource = datasource;
            lc.DataTextField = strText;
            lc.DataValueField = strValue;
            lc.DataBind();
        }

        /// <summary>
        /// 绑定数据到ListControl
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dt"></param>
        /// <param name="strText"></param>
        /// <param name="strValue"></param>
        /// <param name="FirstText">第一项文本</param>
        /// <param name="FirstValue">第一项值</param>
        public static void BindDrop(ListControl lc, DataTable dt, string strText, string strValue, string FirstText, string FirstValue)
        {
            lc.Items.Clear();
            lc.Items.Add(new ListItem(FirstText, FirstValue));
            if (lc.Items.Count > 0)
                lc.SelectedIndex = 0;
            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow myRow in dt.Rows)
            {
                lc.Items.Add(new ListItem(myRow[strText].ToString(), myRow[strValue].ToString()));
            }
        }

        /// <summary>
        /// 增加数据到ListControl
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dt"></param>
        /// <param name="strText"></param>
        /// <param name="strValue"></param>
        public static void AddListToDrop(ListControl lc, DataTable dt, string strText, string strValue)
        {
            if (lc.Items.Count > 0)
                lc.SelectedIndex = 0;
            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow myRow in dt.Rows)
            {
                lc.Items.Add(new ListItem(myRow[strText].ToString(), myRow[strValue].ToString()));
            }
        }

        /// <summary>
        ///		绑定指定范围的数字到ListControl
        /// </summary>
        /// <param name="list">要绑定的对象</param>
        public static void BindNumberToDrop(ListControl list, int iStart, int iEnd)
        {
            list.Items.Clear();
            for (int i = iStart; i <= iEnd; i++)
            {
                list.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// 绑定数据到ListControl
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dt"></param>
        /// <param name="strText"></param>
        /// <param name="strValue"></param>
        public static void BindDrop(ListControl lc, DataTable dt, string strText, string strValue)
        {
            lc.Items.Clear();

            if (dt == null || dt.Rows.Count == 0) return;
            foreach (DataRow myRow in dt.Rows)
            {
                lc.Items.Add(new ListItem(myRow[strText].ToString(), myRow[strValue].ToString()));
            }
        }

        /// <summary>
        /// 列表数据绑定 数据由枚举提供
        /// </summary>
        /// <param name="lc">要绑定的控件</param>
        /// <param name="enumtype">枚举类型</param>
        /// <param name="addText">要追加项的文本，不需要追加传入string.Empty</param>
        /// <param name="addValue">要追加项的值</param>
        /// <param name="selectedvalue">要选中的值</param>
        public static void ListContolDataBindFromEnum(ListControl lc, System.Type enumtype, string addText, string addValue, string selectedvalue)
        {
            lc.Items.Clear();

            //追加新项
            if (addText.Trim().Length > 0)
            {
                lc.Items.Add(new ListItem(addText, addValue));
            }
            FieldInfo fi;
            DescriptionAttribute da;
            foreach (Enum enumValue in Enum.GetValues(enumtype))
            {
                fi = enumtype.GetField((enumValue.ToString()));
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (da != null)
                {
                    lc.Items.Add(new ListItem(da.Description, enumValue.ToString("d")));
                }
            }

            SelectFlg(lc, selectedvalue);
        }
        /// <summary>
        ///根据指定值选定控件中中的项
        /// </summary>
        /// <param name="rbl">控件名</param>
        /// <param name="text">指定值</param>
        public static bool SelectFlg(ListControl rbl, string flg)
        {
            bool isSelect = false;
            int FlgCount = rbl.Items.Count;
            if (flg == null)
            {
                flg = "";
            }
            if (flg != null)
            {
                for (int i = 0; i <= FlgCount - 1; i++)
                {
                    if (rbl.Items[i].Value.Trim() == flg.Trim())
                    {
                        isSelect = true;
                        rbl.SelectedIndex = i;
                        break;
                    }
                }
            }
            return isSelect;
        }
        /// <summary>
        ///根据指定值选定控件中中的项
        /// </summary>
        /// <param name="rbl">控件名</param>
        /// <param name="flg">指定值(文本)</param>
        public static bool SelectText(ListControl rbl, string text)
        {
            bool isSelect = false;
            int FlgCount = rbl.Items.Count;
            if (text == null)
            {
                text = "";
            }
            if (text != null)
            {
                for (int i = 0; i <= FlgCount - 1; i++)
                {
                    if (rbl.Items[i].Text.Trim() == text.Trim())
                    {
                        isSelect = true;
                        rbl.SelectedIndex = i;
                        break;
                    }
                }
            }
            return isSelect;
        }

        #endregion
    }
}
