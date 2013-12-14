 
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;
using System;

namespace UserPermission.Utils
{
    public sealed class EnumPlus
    {
        /// <summary>
        /// 将含有描述信息的字典数据绑定到列表控件中
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="enumType"></param>
        public static void BindDictionaryToListControl(ListControl listControl, Dictionary<string, string> dicData)
        {
            //清楚列表控件中数据
            listControl.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in dicData)
            {
                listControl.Items.Add(new ListItem(kvp.Value, kvp.Key));
            }
        }


        /// <summary>
        /// 获得枚举值的Description特性的值，一般是消息的搜索码
        /// </summary>
        /// <param name="value">要查找特性的枚举值</param>
        /// <returns>返回查找到的Description特性的值，如果没有，就返回.ToString()</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(
              typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// 获取枚举类子项描述信息 
        /// </summary>
        /// <param name="enumtype">枚举类型</param>
        /// <param name="enumSubitem">值</param>        
        public static string GetEnumDescription(System.Type enumtype, string strVlaue)
        {
            FieldInfo fi;
            DescriptionAttribute da;
            foreach (Enum enumValue in Enum.GetValues(enumtype))
            {
                fi = enumtype.GetField((enumValue.ToString()));
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (da != null && CommonMethod.FinalString(strVlaue).Equals(enumValue.ToString("d")))
                {
                    return da.Description;
                }
            }
            return string.Empty;
        }
    }
}
