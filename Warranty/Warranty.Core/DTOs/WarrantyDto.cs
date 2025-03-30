﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.DTOs
{
    public class WarrantyDto
    {
        public int Id { get; set; }

        public string NameProduct { get; set; }
        
        public string LinkFile { get; set; }

        public DateTime ExpirationDate { get; set; }

        //public int CompanyId { get; set; }
        //public CompanyDto Company { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }

        public List<RecordModel> Records { get; set; }

    }
}
