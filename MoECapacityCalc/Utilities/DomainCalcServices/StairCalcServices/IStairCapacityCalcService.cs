using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices
{
    public interface IStairCapacityCalcService
    {
        public double CalcStairCapacity(Stair stair, Area area = null);

        public StairCapacityStruct GetStairCapacityStruct(Stair stair, Area area = null);
    }
}
