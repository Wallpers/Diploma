﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Entities
{
    public class Role
    {
        public int ID { get; set; }

        public string Name { get; set; }

        ICollection<User> Users;
    }
}
