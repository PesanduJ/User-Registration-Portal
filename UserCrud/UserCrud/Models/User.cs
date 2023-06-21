using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCrud.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string employeeId { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string password { get; set; }

    }
}
