using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int PasswordId { get; set; }
        public Password Password  { get; set; }
    }
}
