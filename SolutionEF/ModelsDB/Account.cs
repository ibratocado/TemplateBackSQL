using System;
using System.Collections.Generic;

namespace ApiTemplate
{
    public partial class Account
    {
        public Guid Id { get; set; }
        public string? Acount { get; set; }
        public string? Pount { get; set; }
        public int? RoleId { get; set; }

        public virtual Rol? Role { get; set; }
    }
}
