using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Inventory_API.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
