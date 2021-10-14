using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{

    [Table("cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("car_id", TypeName ="int")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Model { get; set; }

        public int? BasePrice { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [NotMapped]
        public virtual Brand Brand { get; set; }
        
    }
}
