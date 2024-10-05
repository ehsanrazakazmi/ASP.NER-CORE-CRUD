using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,50,ErrorMessage ="Display ko 1 se 50 tak rakho")]
        public int DisplayOrder{ get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
