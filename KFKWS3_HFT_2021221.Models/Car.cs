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

    [Table("cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("car_id", TypeName ="int")]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Model { get; set; }
        
        [Required]
        public int BasePrice { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [NotMapped]
        [JsonIgnore]
        [CanBeNull]
        public virtual Brand Brand { get; set; }

        public override string ToString()
        {
            return $"#{Id}: {Model} of Brand #{BrandId}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Car)
            {
                Car other = obj as Car;
                return this.Id == other.Id &&
                       this.Model == other.Model &&
                       this.BasePrice == other.BasePrice &&
                       this.BrandId == other.BrandId;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
