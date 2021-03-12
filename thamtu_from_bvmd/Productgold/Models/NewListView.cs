using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productgold.Models
{
    public class NewListView
    {
        public Category cateNew { get; set; }
        public List<News> newList { get; set; }
        public NewListView()
        {
            cateNew = new Category();
            newList = new List<News>();
        }
    }
}