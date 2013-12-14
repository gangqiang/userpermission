
using System;
using System.Text.RegularExpressions;
using System.Web;
namespace UserPermission.Utils
{
    public class ValidatorHelper
    {

        #region 验证
        #region 数字验证
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Regex.IsMatch(str.Trim(), @"^\d+$");
            else
                return false;
        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="strNum">待测试的字符串</param>
        /// <returns>是则返回true,否则返回false</returns>
        public static bool IsNumber(string strNum)
        {
            if (strNum == null)
                return false;
            return Regex.IsMatch(strNum.Trim(), "^(0|[1-9][0-9]*)$");
        }


        /// <summary>
        /// 是否是正整数
        /// </summary>
        /// <param name="strNum">待测试的字符串</param>
        /// <param name="bIncludeZero">true:含0；false:不含0</param>
        /// <returns></returns>
        public static bool IsPositiveInteger(string strNum, bool bIncludeZero)
        {
            if (strNum == null)
                return false;
            if (bIncludeZero) //0也认为是正整数
                return Regex.IsMatch(strNum.Trim(), @"^\d+$");
            else
                return Regex.IsMatch(strNum.Trim(), "^[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 是否是负整数
        /// </summary>
        /// <param name="strNum">待测试的字符串</param>
        /// <param name="bIncludeZero">true:含0；false:不含0</param>
        /// <returns></returns>
        public static bool IsNegativeInteger(string strNum, bool bIncludeZero)
        {
            if (strNum == null)
                return false;
            if (bIncludeZero) //0也认为是负整数
                return Regex.IsMatch(strNum.Trim(), @"^((-\d+)|(0+))$");
            else
                return Regex.IsMatch(strNum.Trim(), "^-[0-9]*[1-9][0-9]*$");
        }


        /// <summary>
        /// 是否是浮点数
        /// 如果满足浮点数格式则返回true;否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool IsDecimal(string strFloatNum)
        {
            if (strFloatNum == null)
                return false;
            return Regex.IsMatch(strFloatNum.Trim(), @"^(-?\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 是否是正浮点数
        /// </summary>
        /// <param name="strFloatNum">待测试字符串</param>
        /// <param name="bInculudeZero">true:含0；false:不含0</param>
        /// <returns></returns>
        public static bool IsPositiveDecimal(string strFloatNum, bool bInculudeZero)
        {
            if (strFloatNum == null)
                return false;
            if (bInculudeZero) //0也认为是正浮点数
                return Regex.IsMatch(strFloatNum.Trim(), @"^\d+(\.\d+)?$");
            else
                return Regex.IsMatch(strFloatNum.Trim(), @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 是否是负浮点数
        /// </summary>
        /// <param name="strFloatNum">待测试字符串</param>
        /// <param name="bIncludeZero">true:含0；false:不含0</param>
        /// <returns>如果是</returns>
        public static bool IsNegativeDecimal(string strFloatNum, bool bIncludeZero)
        {
            if (strFloatNum == null)
                return false;
            if (bIncludeZero) //0也认为是负浮点数
                return Regex.IsMatch(strFloatNum.Trim(), @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$");
            else
                return Regex.IsMatch(strFloatNum.Trim(), @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        }

        #endregion

        #region Email验证
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(string strEmail)
        {
            if (!string.IsNullOrEmpty(strEmail))
                return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            else
                return false;
        }
        #endregion

        #region 手机验证
        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="strMobile">待测试手机号字符串</param>
        /// <returns>是手机格式就返回true;否则返回false</returns>
        public static bool IsMobile(string strMobile)
        {
            if (strMobile == null)
                return false;
            return Regex.IsMatch(strMobile.Trim(), @"^((\(\d{2,3}\))|(\d{3}\-))?(13\d{9})|(15[0,3,6,8,9]\d{8})$");
        }
        #endregion

        #region 电话号码验证
        /// <summary>
        /// 是否是电话号码
        /// </summary>
        /// <param name="strPhone"></param>
        /// <returns>是电话格式就返回true;否则返回false</returns>
        public static bool IsPhone(string strPhone)
        {
            if (strPhone == null)
                return false;
            return Regex.IsMatch(strPhone.Trim(), @"^(\+86\s{1,1})?((\d{3,4}\-)\d{7,8})$");
        }
        #endregion

        #region 身份证验证
        /// <summary>
        /// 是否是身份证
        /// </summary>
        /// <param name="strCardNo"></param>
        /// <returns></returns>
        public static bool IsIdentityCard(string strCardNo)
        {
            if (strCardNo == null)
                return false;
            return Regex.IsMatch(strCardNo.Trim(), @"^d{15}|d{}18$");
        }

        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 字符串验证
        /// <summary>
        /// 验证是否是空或null字符串
        /// 如果是空或null则返回true,strRealString为null或string.Empty
        /// 否则返回false,strRealString为经过Trim操作的String;
        /// </summary>
        /// <param name="strSource">待查看的string</param>
        /// <param name="strRealString">经过Trim操作的string</param>
        /// <returns>
        /// 如果是空或null则返回true,strRealString为null或string.Empty
        /// 否则返回false,strRealString为经过Trim操作的String;
        /// </returns>
        public static bool IsNullOrEmptyString(string strSource, out string strRealString)
        {
            strRealString = null;
            if (strSource == null)
                return true;
            strRealString = strSource.Trim();
            if (strRealString == string.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// 验证是否是空或null字符串
        /// 如果是空或null则返回true,否则返回false
        /// </summary>
        /// <param name="strSource">待查看的string</param>
        /// <returns>
        /// 如果是空或null则返回true,否则返回false
        /// </returns>
        public static bool IsNullOrEmptyString(string strSource)
        {
            if (strSource == null)
                return true;
            if (strSource.Trim() == string.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// DataRow的value或从数据库中取出的Object型数据验证,验证取出的object是否是DBNull,空或null]
        /// 如果是DBNull,null或空字符串则返回true
        /// </summary>
        /// <param name="objSource">待验证的object</param>
        /// <returns>
        /// 如果是DBNull,null或空字符串则返回true
        /// </returns>
        public static bool IsDBNullOrNullOrEmptyString(object objSource)
        {
            if ((objSource == DBNull.Value) || (objSource == null))
                return true;
            string strSource = objSource.ToString();
            if (strSource.Trim() == string.Empty)
                return true;
            return false;
        }
        /// <summary>
        /// 如果为NUll 返回Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FinalString(string str)
        {
            if (IsNullOrEmptyString(str))
                return string.Empty;
            str = str.Trim();
            return str;
        }

        public static string FinalString(object str)
        {
            if (str == null)
                return string.Empty;
            else return FinalString(str.ToString());
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {

            string[] userip = ValidatorHelper.SplitString(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = ValidatorHelper.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }
        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(FinalString(str), @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(FinalString(str), @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 防止sql注入，替换
        /// </summary>
        /// <param name="str">要替换的字符传</param>
        /// <returns></returns>
        public static string SafeSql(string str)
        {
            return FinalString(str).Replace("'", "''");
        }


        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }


        #endregion

        #region 汉字验证
        /// <summary>
        /// 是否是中文
        /// </summary>
        /// <param name="strWords">待测试的字符串</param>
        /// <returns>是则返回true;否则返回false</returns>
        public static bool IsChineseWord(string strWords)
        {
            if (strWords == null)
                return false;
            bool bResult = true;
            foreach (char charWord in strWords)
            {
                if (!Regex.IsMatch(charWord.ToString(), "[\u4e00-\u9fa5]+"))
                {
                    bResult = false;
                    break;
                }
            }

            return bResult;
        }
        #endregion

        #region 日期验证
        /// <summary>
        /// 是否是日期类型
        /// </summary>
        /// <returns></returns>
        public static bool IsDateTime(string strDateValue)
        {
            string strRealValue = null;
            if (IsNullOrEmptyString(strDateValue, out strRealValue))
                return false;
            DateTime dtDate = DateTime.MinValue;
            return DateTime.TryParse(strRealValue, out dtDate);
        }
        #endregion
        #endregion

        #region 字符串操作

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }
        /// <summary>
        /// 客户端输入字符串验证
        /// </summary>
        /// <param name="text">客户端输入</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>清理后的字符串</returns>
        public static string ClearInputText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//移除两个以上的空格
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//移除Br
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//移除&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//移除其他一些标志
            text = text.Replace("'", "''");//防止注入
            return text;
        }
        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!ValidatorHelper.IsNullOrEmptyString(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                {
                    string[] tmp = { strContent };
                    return tmp;
                }
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
            {
                return new string[0] { };
            }
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];

            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        #endregion

        #region 转换

        #region 整型转换方法

        /// <summary>
        /// 转换成整型数字
        /// 转换失败则返回由nDefault指定的数字
        /// 转换成功则返回真实转换的数字
        /// </summary>
        /// <param name="objInfo">待转换的对象</param>
        /// <param name="nDefault">指定传回的默认值</param>
        /// <returns>转换失败则返回由nDefault指定的数字;转换成功则返回真实转换的数字</returns>
        public static int ToInt(object obj, int defaultValue)
        {
            int result = defaultValue;
            if (obj != null && obj != DBNull.Value)
            {
                if (!int.TryParse(obj.ToString().Trim(), out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        #endregion

        #region 浮点转换方法

        /// <summary>
        /// 转换成浮点数，
        /// </summary>
        /// <param name="objFloat">待转换的对象</param>
        /// <param name="dDefault">指定传回的默认值</param>
        /// <returns>转换失败则返回由dDefault指定的数字;转换成功则返回真实转换的数字</returns>
        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            decimal result = defaultValue;
            if (obj != null && obj != DBNull.Value)
            {
                if (!decimal.TryParse(obj.ToString().Trim(), out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        /// <summary>
        /// 获取待收款（对帐单处使用） --- 在decimal类型要求计算剩余金额时，用下面的方法
        /// 1，根据总金额，已收款，当前状态，得到待收款。
        /// 2，如果待收款小于0则返回0
        /// </summary>
        /// <param name="money">总金额</param>
        /// <param name="rMoney">已收款</param>
        /// <param name="isClear">状态：1清款，0未清款</param>
        /// <returns></returns>
        public static decimal GetDetailNoRetriveMoneyToDecimal(object money, object rMoney, string isClear)
        {
            decimal result = 0;
            if (isClear == null || isClear.Trim() != "1")
            {
                decimal dmoney = 0;
                decimal drMoney = 0;
                if (money != null && money.ToString().Trim() != "")
                    dmoney = decimal.Parse(money.ToString().Trim());
                if (rMoney != null && rMoney.ToString().Trim() != "")
                    drMoney = decimal.Parse(rMoney.ToString().Trim());
                result = dmoney - drMoney;
            }

            result = result < 0 ? 0 : result;
            return result;
        }

        #endregion

        #region 日期转换方法

        /// <summary>
        /// UTC时间转换成windows时间
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static DateTime UTCToDateTime(double l)
        {
            DateTime dtZone = new DateTime(1970, 1, 1, 0, 0, 0);
            dtZone = dtZone.AddSeconds(l);
            return dtZone.ToLocalTime();
        }

        /// <summary>
        /// Windows时间转换成UTC时间
        /// </summary>
        /// <param name="vDate"></param>
        /// <returns></returns>
        public static double DateTimeToUTC(DateTime vDate)
        {
            TimeZone tz = TimeZone.CurrentTimeZone;
            vDate = vDate.ToUniversalTime();
            DateTime dtZone = new DateTime(1970, 1, 1, 0, 0, 0);
            return vDate.Subtract(dtZone).TotalSeconds;
        }

        /// <summary>
        /// 转换成日期类型
        /// 转换失败则返回由dtDefault指定的日期
        /// 转换成功则返回真实日期
        /// </summary>
        /// <param name="objDateValue">待转换的对象</param>
        /// <param name="dtDefault">指定传回的默认值</param>
        /// <returns>转换失败则返回由dtDefault指定的日期;转换成功则返回真实日期</returns>
        public static DateTime ToDateTime(object objDateValue, DateTime dtDefault)
        {
            if (objDateValue == null)
                return dtDefault;
            return ToDateTime(objDateValue.ToString(), dtDefault);
        }

        /// <summary>
        /// 转换成日期类型
        /// 转换失败则返回由dtDefault指定的日期
        /// 转换成功则返回真实日期 
        /// </summary>
        /// <param name="strDateValue">待转换的字符串</param>
        /// <param name="dtDefault">指定传回的默认值</param>
        /// <returns>转换失败则返回由dtDefault指定的日期;转换成功则返回真实日期 </returns>
        public static DateTime ToDateTime(string strDateValue, DateTime dtDefault)
        {
            string strRealDate = null;
            if (IsNullOrEmptyString(strDateValue, out strRealDate))
                return dtDefault;
            DateTime dtResult = DateTime.MinValue;
            if (DateTime.TryParse(strRealDate, out dtResult))
                return dtResult;
            else
                return dtDefault;
        }
        #endregion

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 尝试获取身份证号码中的生日日期。
        /// 若成功则返回True，并且设置参数birthday的值。
        /// 若失败则返回False，并且设置参数birthday的值为DateTime.MinValue。
        /// </summary>
        /// <param name="certNo">身份证号码</param>
        /// <returns></returns>
        public static bool TryCertNoBirthday(string certNo, out DateTime birthday)
        {
            bool done = false;
            string year;
            string month;
            string day;
            DateTime D = DateTime.MinValue;
            if (certNo.Length == 15)
            {
                year = certNo.Substring(6, 2);
                month = certNo.Substring(8, 2);
                day = certNo.Substring(10, 2);
                done = DateTime.TryParse("19" + year + "-" + month + "-" + day, out D);
            }
            else if (certNo.Length == 18)
            {
                year = certNo.Substring(6, 4);
                month = certNo.Substring(10, 2);
                day = certNo.Substring(12, 2);
                done = DateTime.TryParse(year + "-" + month + "-" + day, out D);
            }
            birthday = D;
            return done;
        }
        #endregion

        #region 获取真实的访问IP

        public static string GetRealIP()
        {
            string strRealIP = string.Empty;    //真实IP
            string strForwardIP = null;         //HTTP_X_FORWARDED_FOR
            string strRemoteIP = null;          //REMOTE_ADDR
            bool bHasForwardIP = !IsNullOrEmptyString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], out strForwardIP);
            bool bHasRemoteIP = !IsNullOrEmptyString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], out strRemoteIP);
            if (!bHasForwardIP && !bHasRemoteIP) //两个都不存在则返回空
                return strRealIP;
            if (!bHasForwardIP && bHasRemoteIP) //只有REMOTE_ADDR存在
                return strRemoteIP;
            if (strForwardIP.IndexOf(',') != -1) //有多级代理
            {
                string[] aryForwardIPs = strForwardIP.Split(',');
                //优先取IP数组的第一个IP,如果第一个不可用则去第二个，再不可用就取REMOTE_ADDR
                if (CanUseIP(aryForwardIPs[0].Trim()))
                    strRealIP = aryForwardIPs[0].Trim();
                else if ((aryForwardIPs.Length >= 2) && (CanUseIP(aryForwardIPs[1].Trim())))
                    strRealIP = aryForwardIPs[1].Trim();
                else if (bHasRemoteIP)
                    strRealIP = strRemoteIP;
            }
            else if (CanUseIP(strForwardIP))    //只有一个代理
                strRealIP = strForwardIP;
            else if (bHasRemoteIP)
                strRealIP = strRemoteIP;
            return strRealIP;
        }
        /// <summary>
        /// 是否能够使用这个IP地址，根据一些IP过滤条件来判断
        /// </summary>
        /// <param name="strIPAddress"></param>
        /// <returns>如果可以使用则返回true</returns>
        private static bool CanUseIP(string strIP)
        {
            return !(string.IsNullOrEmpty(strIP) ||
                   strIP.StartsWith("10.") || strIP.StartsWith("192.168.") ||
                   strIP.StartsWith("172.16.") || strIP.StartsWith("172.17.") ||
                   strIP.StartsWith("172.18.") || strIP.StartsWith("172.19.") ||
                   strIP.StartsWith("172.20.") || strIP.StartsWith("172.21.") ||
                   strIP.StartsWith("172.22.") || strIP.StartsWith("172.23.") ||
                   strIP.StartsWith("172.24.") || strIP.StartsWith("172.25.") ||
                   strIP.StartsWith("172.26.") || strIP.StartsWith("172.27.") ||
                   strIP.StartsWith("172.28.") || strIP.StartsWith("172.29.") ||
                   strIP.StartsWith("172.30.") || strIP.StartsWith("172.31."));
        }
        #endregion

        #region md5加密数据
        /// <summary>
        /// md5加密数据
        /// </summary>
        /// <param name="strData">需要加密的数据</param>
        /// <param name="keyCode">自己的密钥(也可以为空)</param>
        /// <returns>加密过的数据</returns>
        public static string MD5Encrypt(string strData, string keyCode)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strData + keyCode, "md5").ToUpper();
        }
        #endregion

        #region 返回文件是否存在
        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }
        #endregion

        #region 返回Ajax返回值
        /// <summary>
        /// 返回Ajax返回值
        /// </summary>
        /// <param name="msg"></param>
        public static void ReturnAjax(System.Web.UI.Page page, string msg)
        {
            page.Response.Clear();
            page.Response.ContentType = "text/xml";
            page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            page.Response.Write(msg);
            page.Response.End();
        }
        #endregion
    }
}
