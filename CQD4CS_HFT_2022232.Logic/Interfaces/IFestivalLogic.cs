using CQD4CS_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Logic.Interfaces
{
    public interface IFestivalLogic
    {
        void Create(Festival item);
        void Delete(int id);
        Festival Read(int id);
        IQueryable<Festival> ReadAll();
        void Update(Festival item);
    }
}
