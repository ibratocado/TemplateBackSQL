using System;
using System.Collections.Generic;

namespace ApiTemplate
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
        }

        public Guid Id { get; set; }
        public string? Acount { get; set; }
        public string? Pount { get; set; }
        public int? RoleId { get; set; }

        public virtual Rol? Role { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
