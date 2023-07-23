using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.DBContext
{
    [Table("SystemCatalog")]
    public class NewCatalogfromSystemcs
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string ?SName { get; set; }

        public int? SParentId { get; set; }

        [ForeignKey("SParentId")]
        public NewCatalogfromSystemcs? SParentCatalog { get; set; }
    }
}


