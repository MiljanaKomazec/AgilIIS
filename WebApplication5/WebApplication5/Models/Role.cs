using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Role
    {
        [Key]
        public Guid IDRole { get; set; }
        public string NameRole { get; set; }

        
    }
}
