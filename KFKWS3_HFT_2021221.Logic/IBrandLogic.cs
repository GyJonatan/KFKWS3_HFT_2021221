﻿using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    interface IBrandLogic : ILogic<Brand>
    {
        void ChangeName(int id, string newName);
        IList<MostCarsResult> Dosomething();
    }
}