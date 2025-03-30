﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.Core.DTOs
{
    public class PermissionDto
    {
    
        public int Id { get; set; }
        public string NamePermission { get; set; }
        public string Description { get; set; }
    }
}
