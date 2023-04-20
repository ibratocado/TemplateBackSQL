using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string? Acount { get; set; }
        public string? Pount { get; set; }
        public int? RoleId { get; set; }
    }
}
