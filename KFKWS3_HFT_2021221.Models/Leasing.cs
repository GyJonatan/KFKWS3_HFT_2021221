﻿using System;
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
        public string CompanyName { get; set; }
        
        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public Leasing()
        {
            Cars = new HashSet<Car>();
        }
    }
}