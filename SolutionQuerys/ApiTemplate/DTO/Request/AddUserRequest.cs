using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class AddUserRequest
    {
		[Required]
		public int RoleId { get; set; }
		[Required]
		[MinLength(18)]
		public string Curp { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string SecondLastName { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public Double Salary { get; set; }
		[Required]
		[Phone]
		public string Phone { get; set; }
		[Required]
		public string Account { get; set; }
		[Required]
		public string pount { get; set; }
	}
}
