﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiFin.Models
{
    internal class UserBasicDetail
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string UserAvatar { get; set; }
        public string AccountNo { get; set; }
    }
}
