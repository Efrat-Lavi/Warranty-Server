﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.API.PostModels
{
    public class PermissionPostModel
    {
        public string NamePermission { get; set; }
        public string Description { get; set; }
    }
}
