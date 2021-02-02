using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Models
{
    public class StringClass
    {
        /// <summary>
        /// Ma hoa chuoi ky tu (MD5)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = md5.ComputeHash(valueArray);
            var sb = new StringBuilder();
            for (int i = 0; i < valueArray.Length; i++)
                sb.Append(valueArray[i].ToString("x2").ToLower());
            return sb.ToString();
        }
        /// <summary>
        /// Tao chuoi dung cho rewrite url
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        #region Name To Tag
        public static string NameToTag(string strName)
        {
            strName = strName.ToLower();
            string strReturn = "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(strName, "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion
        /// <summary>
        /// Xoa ky tu unicode tu chuoi
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        #region Remove Unicode
        public static string RemoveUnicode(string strString)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string strFormD = strString.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion
        #region[ShowNameLevel]
        public static string ShowNameLevel(string Name, string Level)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel; i++)
            {
                strReturn = strReturn + ".....";
            }
            strReturn += Name;
            return strReturn;
        }
        #endregion
        /// <summary>
        /// Tao mot chuoi ngau nghien
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        #region Random String
        public static string RandomString(int size)
        {
            Random rnd = new Random();
            string srds = "";
            string[] str = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < size; i++)
            {
                srds = srds + str[rnd.Next(0, 61)];
            }
            return srds;
        }
        #endregion

        public static string ShowPageType(string ActiveCode)
        {
            return ActiveCode == "1" ? "Trang liên kết" : "Trang nội dung";
        }
        public static string ShowSupportType(string type)
        {
            string strString = "";
            string[] myArr = new string[] { "0,yahoo", "1,skype", "2,hotline" };
            char[] splitter = { ',', ';' };
            for (int i = 0; i < myArr.Length; i++)
            {
                string[] arr = myArr[i].Split(splitter);
                if (arr[0].Equals(type))
                {
                    strString = arr[1];
                    break;
                }
            }
            return strString;
        }
        #region[Xem kieu thanh vien]
        public static string ShowTypeMember(string type)
        {
            string strString = "";
            string[] myArr = new string[] { "0,Tài khoản thường", "1,Tài khoản Vip" };
            char[] splitter = { ',', ';' };
            for (int i = 0; i < myArr.Length; i++)
            {
                string[] arr = myArr[i].Split(splitter);
                if (arr[0].Equals(type))
                {
                    strString = arr[1];
                    break;
                }
            }
            return strString;
        }
        #endregion
        #region[Xem kieu tinh trang don hang]
        public static string ShowStateBill(string state)
        {
            string strString = "";
            string[] myArr = new string[] { "0,Chưa giao hàng", "1,Đã giao hàng", "2,Đã hủy" };
            char[] splitter = { ',', ';' };
            for (int i = 0; i < myArr.Length; i++)
            {
                string[] arr = myArr[i].Split(splitter);
                if (arr[0].Equals(state))
                {
                    strString = arr[1];
                    break;
                }
            }
            return strString;
        }
        #endregion
        #region[Cat chuoi text de hien thi]
        public static string FormatContentNews(string value, int count)
        {
            try
            {
                string _value = value;
                if (value!=null &&_value.Length >= count)
                {
                    string ValueCut = _value.Substring(0, count - 3);
                    string[] valuearray = ValueCut.Split(' ');
                    string valuereturn = "";
                    for (int i = 0; i < valuearray.Length - 1; i++)
                    {
                        valuereturn = valuereturn + " " + valuearray[i];
                    }
                    return valuereturn;
                }
                else
                {
                    return _value;
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
        #region [Format_Price]
        public static string Format_Price(string Price)
        {
            Price = Price.Replace(".", "");
            Price = Price.Replace(",", "");
            string tmp = "";
            while (Price.Length > 3)
            {
                tmp = "." + Price.Substring(Price.Length - 3) + tmp;
                Price = Price.Substring(0, Price.Length - 3);
            }
            tmp = Price + tmp;
            return tmp;
        }
        #endregion
        #region [Format_date]
        public static string FormatDate_2_Longdate(DateTime date, string lang)
        {
            string datename = date.DayOfWeek.ToString();
            switch (date.DayOfWeek.ToString())
            {
                case "Saturday":
                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ bảy";
                        //datename = "Saturday";
                    }
                    break;
                case "Sunday":

                    if (lang.Equals("VI"))
                    {
                        datename = "Chủ nhật";
                        //datename = "Sunday";
                        break;
                    }
                    break;
                case "Monday":

                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ hai";
                        //datename = "Monday";
                        break;
                    }
                    break;
                case "Tuesday":

                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ ba";
                        //datename = "Tuesday";
                        break;
                    }
                    break;
                case "Wednesday":
                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ tư";
                        //datename = "Wednesday";
                        break;
                    }

                    break;
                case "Thursday":
                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ năm";
                        //datename = "Thursday";
                        break;
                    }
                    break;
                case "Friday":
                    if (lang.Equals("VI"))
                    {
                        datename = "Thứ sáu";
                        //datename = "Friday";
                        break;
                    }
                    break;
            }
            return datename.ToString() + ", Ngày " + date.ToString("dd/MM/yyyy hh:mm");
        }
        #endregion
        public static string Fmdate(string id)
        {
            try
            {
                var date = DateTime.Parse(id).ToShortDateString();
                return date;
            }
            catch (Exception)
            {
                return id;
            }
        }

        #region[thu vien unicode]
        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };
        #endregion

        #region[ham gen chuoi dang dau tru]
        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            var data = str.Replace(" ", "-");
            return data;
        }
        #endregion

        public static string Tinhtrang(string status)
        {
            return status == "0" ? "Chưa xử lý" : status == "1" ? "Đã xử lý" : "Đã hủy";
        }

    }
}