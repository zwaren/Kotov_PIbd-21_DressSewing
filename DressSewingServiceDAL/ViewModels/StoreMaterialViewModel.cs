﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressSewingServiceDAL.ViewModels
{
    public class StoreMaterialViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int Count { get; set; }
    }
}
