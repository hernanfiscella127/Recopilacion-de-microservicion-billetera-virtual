﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class ChangePasswordRequest
    {
        public string EmailOrPhone { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
