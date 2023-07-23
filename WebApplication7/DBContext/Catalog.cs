using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.DBContext
{
    public class Catalog
    {
        private string name = "Новая папка";
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name
        {
            get => name;
            set => name = value ?? "Новая папка";
        }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Catalog? ParentCatalog { get; set; }


    }
}

