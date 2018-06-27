﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class User
    {
        public int UserId { get; set; }
        public string UserPassword { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
