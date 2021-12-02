using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CanBeNull : Attribute
    {
        public CanBeNull()
        {

        }
    }
}
