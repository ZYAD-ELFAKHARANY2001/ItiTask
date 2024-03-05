using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        // Add other user properties as needed (e.g., Email, FirstName, LastName)

        public ICollection<Role> Roles { get; set; } // Navigation property for many-to-many relationship
    }
}

