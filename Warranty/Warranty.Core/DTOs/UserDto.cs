using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string NameUser { get; set; }

        public string Email { get; set; }
        public RoleModel Role { get; set; }
        public bool IsAccessEmails { get; set; }



    }
}
