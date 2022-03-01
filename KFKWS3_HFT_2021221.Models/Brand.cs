using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{

    [Table("brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        
        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        [ForeignKey(nameof(Leasing))]
        public int LeasingId { get; set; }

        [NotMapped]
        [JsonIgnore]
        [CanBeNull]
        public virtual Leasing Leasing { get; set; }

        public Brand()
        {
            Cars = new HashSet<Car>();
        }
        public override string ToString()
        {
            return $"#{Id}: {Name}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Brand)
            {
                Brand other = obj as Brand;
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
