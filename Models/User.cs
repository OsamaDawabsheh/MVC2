using System.ComponentModel.DataAnnotations;

namespace MVC3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
