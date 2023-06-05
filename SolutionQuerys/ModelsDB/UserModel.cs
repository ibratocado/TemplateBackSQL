using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB
{
    public class UserModel
    {
		public Guid Id { get; set; }
		public string Acount { get; set; }
		public string Role { get; set; }
		public int RoleId { get; set; }
		public string Curp { get; set; }
		public string LastName { get; set; }
		public string SecondLastName { get; set; }
		public string Name { get; set; }
		public Double Salary { get; set; }
		public string Phone { get; set; }
	}
}
