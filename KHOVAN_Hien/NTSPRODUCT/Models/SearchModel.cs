﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTSPRODUCT.Models
{
    public class SearchModel
    {
        public string CateId { get; set; }
        public int? Type { get; set; }
        public int? Position { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Description { get; set; }


        //phan trang
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}