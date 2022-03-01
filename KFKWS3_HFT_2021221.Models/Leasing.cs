using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{
    [Table("leasing")]
    public class Leasing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("leasing_id", TypeName = "int")]
        public int Id { get; set; }

        [Range(0,10000000)]
        public int Budget { get; set; }

        [Required]
        public string HQLocation { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Brand> Brands { get; set; }

        public Leasing()
        {
            Brands = new HashSet<Brand>();
        }

        public override string ToString()
        {
            return $"#{Id}: {Name}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Leasing)
            {
                Leasing other = obj as Leasing;
                return this.Id == other.Id &&
                       this.Name == other.Name;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
