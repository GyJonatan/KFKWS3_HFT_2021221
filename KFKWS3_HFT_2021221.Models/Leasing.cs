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
        
        [MaxLength(100)]
        [Required]
        public List<Company> Companies { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Brand> Brands { get; set; }

        public Leasing()
        {
            Brands = new HashSet<Brand>();
            Companies = new List<Company>();
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

        public static string GetInfo(List<Company> list, string name)
        {
            return list.Find(x => x.Name == name).Name;
        }
    }
}
