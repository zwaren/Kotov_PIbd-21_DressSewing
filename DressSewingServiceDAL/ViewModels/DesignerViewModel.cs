﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DressSewingServiceDAL.ViewModels
{
    [DataContract]
    public class DesignerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public string DesignerFIO { get; set; }

        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
