using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public class LeasingRepository : Repository<Leasing>, ILeasingRepository
    {
        public LeasingRepository(KFKWS3DbContext context) : base(context) { }
        public override void Create(Leasing item)
        {
            context.Leasings.Add(item);
            context.SaveChanges();
        }
        public override void Delete(int id)
        {
            Leasing leasing = ReadOne(id);

            context.Leasings.Remove(leasing);
            context.SaveChanges();
        }
        public override Leasing ReadOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public void Update(Leasing leasing)
        {
            Leasing old = ReadOne(leasing.Id);

            if (old == null)
            {
                throw new InvalidOperationException($"***ERROR***\nUPDATE LEASING0: leasing({old.Id}) not found");
            }

            old.Id = leasing.Id;
            old.Name = leasing.Name;
        }
        public void ChangeCompanyName(int id, string newName)
        {
            var leasing = ReadOne(id);
            if (leasing == null)
            {
                throw new InvalidOperationException($"***ERROR***\nCHANGE COMPANY NAME: leasing({leasing.Id}) not found");
            }
            leasing.Name = newName;
            context.SaveChanges();
        }
    }
}
