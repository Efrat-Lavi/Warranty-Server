﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.Core.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
   
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebLink { get; set; }
    }
}
