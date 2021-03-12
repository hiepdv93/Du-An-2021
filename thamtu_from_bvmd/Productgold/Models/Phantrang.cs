using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Productgold.Models
{
    public class Phantrang
    {
        static string getUrl(string url, int numPage)
        {
            return url + "?page=" + numPage;
        }
        public static string PhanTrangAjax(int numItems, int curpage, int numOfNews, string url)
        {
            //int numItems = 10; // so san pham tren 1 trang
            //int numOfNews = 0; // tong so san pham da goi len duoc tu db
            int numpages = 0;   //tong so trang co duoc khi tien hanh phan trang
            string showpage = "";
            numpages = numOfNews / numItems;
            if (numOfNews % numItems > 0)
            {
                numpages += 1;
            }
            if (curpage < 0)
            {
                curpage = 0;
            }
            if (numOfNews > 0)
            {
                if (curpage == 0)
                {
                    showpage = "<ul  class=\"pagination\">";
                }
                else
                {
                    showpage = "<ul  class=\"pagination\">";
                }
                if (numpages == 1)
                {
                    showpage += "<li><span>1</span></li>";
                }
                else if (numpages < 10)
                {
                    if (curpage == 0)
                    {
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\">></a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\" >>></a></li>";
                    }
                    else if (curpage == numpages - 1)
                    {
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == numpages - 1)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\" >" + (i + 1) + "</a></li>";
                            }
                        }
                    }
                    else if (numpages < curpage + 2)
                    {
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\"><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\" >" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\" >>></a></li>";
                    }
                    else
                    {
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=1')\"><<</a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\"  ><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\" >" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\">></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\" >>></a></li>";
                    }
                }
                else if (numpages >= 10)
                {
                    if (curpage == 0)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\" >" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\" >>></a></li>";
                    }
                    else if (curpage == numpages - 1)
                    {
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 8)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\" >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                    }
                    else if (curpage == 1)
                    {
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 1)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (i + 1) + "')\"  >" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + numpages + "')\"  >>></a></li>";
                    }
                    else if (numpages == curpage + 2)
                    {
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 7)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\" >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\">></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\">>></a></li>";
                    }
                    else if (numpages == curpage + 3)
                    {
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 6)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\"  >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\"   >></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\"  >>></a></li>";
                    }
                    else if (numpages == curpage + 4)
                    {
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\"  ><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 5)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\" >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\">>></a></li>";
                    }
                    else if (numpages == curpage + 5)
                    {
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 4)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\" >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + numpages + "')\" >>></a></li>";
                    }
                    else if (numpages == curpage + 6)
                    {
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (curpage) + "')\" ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 3)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a  href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (numpages - 8 + i) + "')\" >" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + "?page=" + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + "?page=" + numpages + "\">>></a></li>";
                    }
                    else
                    {
                        showpage += "<li><a href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=1')\" ><<</a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + curpage + "')\"  ><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 2)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"javascript:void(0)\"  onclick=\"phantrang('" + url + "?page=" + (curpage - 1 + i) + "')\"  >" + (curpage - 1 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + (curpage + 2) + "')\" >></a></li>";
                        showpage += "<li><a href=\"javascript:void(0)\" onclick=\"phantrang('" + url + "?page=" + numpages + "')\"  >>></a></li>";
                    }
                }
            }
            else
            {
                if (curpage == 0)
                {
                    showpage = "<ul  class=\"pagination\"><li>";
                }
            }
            showpage += "</ul>";
            return showpage;
        }
        public static string PhanTrang(int numItems, int curpage, int numOfNews, string url)
        {
            if (url.Contains("?page"))
            {
                string[] mang = url.Split('?');
                url = mang[0];
            }
            else if (url.Contains("&page"))
            {
                string[] mang = url.Split('&');
                string strLast = "&" + mang[mang.Length - 1];
                url = url.Replace(strLast, "");
            }
            string pageStr = "?page=";
            if (url.Contains("?"))
            {
                pageStr = "&page=";
            }

            //int numItems = 10; // so san pham tren 1 trang
            //int numOfNews = 0; // tong so san pham da goi len duoc tu db
            int numpages = 0;   //tong so trang co duoc khi tien hanh phan trang
            string showpage = "";
            numpages = numOfNews / numItems;
            if (numOfNews % numItems > 0)
            {
                numpages += 1;
            }
            if (curpage < 0)
            {
                curpage = 0;
            }
            if (numOfNews > 0)
            {
                if (curpage == 0)
                {
                    showpage = "<div  class=\"pagination-container\"><ul class='pagination'>";
                }
                else
                {
                    showpage = "<div  class=\"pagination-container\"><ul class='pagination'>";
                }
                if (numpages == 1)
                {
                    showpage += "<li><span>1</span></li>";
                }
                else if (numpages < 10)
                {
                    if (curpage == 0)
                    {
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (curpage == numpages - 1)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == numpages - 1)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                    }
                    else if (numpages < curpage + 2)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < numpages; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                }
                else if (numpages >= 10)
                {
                    if (curpage == 0)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == curpage)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (curpage == numpages - 1)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 8)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                    }
                    else if (curpage == 1)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 1)
                            {
                                showpage += "<li><span>" + (i + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (i + 1) + "\">" + (i + 1) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (numpages == curpage + 2)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 7)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (numpages == curpage + 3)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 6)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (numpages == curpage + 4)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 5)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (numpages == curpage + 5)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 4)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else if (numpages == curpage + 6)
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + (curpage) + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 3)
                            {
                                showpage += "<li><span>" + (numpages - 8 + i) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (numpages - 8 + i) + "\">" + (numpages - 8 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                    else
                    {
                        showpage += "<li><a href=\"" + url + pageStr + "1\"><<</a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + curpage + "\"><</a></li>";
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 2)
                            {
                                showpage += "<li><span>" + (curpage + 1) + "</span></li>";
                            }
                            else
                            {
                                showpage += "<li><a href=\"" + url + pageStr + (curpage - 1 + i) + "\">" + (curpage - 1 + i) + "</a></li>";
                            }
                        }
                        showpage += "<li><a href=\"" + url + pageStr + (curpage + 2) + "\">></a></li>";
                        showpage += "<li><a href=\"" + url + pageStr + numpages + "\">>></a></li>";
                    }
                }
            }
            showpage += "</ul></div>";
            return showpage;
        }

    }
}