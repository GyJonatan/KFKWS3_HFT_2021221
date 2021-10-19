using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    class LeasingRepository : ILeasingRepository
    {
        KFKWS3DbContext context;

        public LeasingRepository(KFKWS3DbContext context)
        {
            this.context = context;
        }

        public void Create(Leasing leasing)
        {
            context.Leasings.Add(leasing);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Leasing leasing = ReadOne(id);

            context.Leasings.Remove(leasing);
            context.SaveChanges();
        }

        public Leasing ReadOne(int id)
        {
            return context
                .Leasings
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Leasing> ReadAll()
        {
            return context.Leasings;
        }

        public void Update(Leasing leasing)
        {
            Leasing old = ReadOne(leasing.Id);

            if (old == null)
            {
                throw new NullReferenceException();
            }

            old.Id = leasing.Id;
            old.CompanyName = leasing.CompanyName;
        }
    }
}
