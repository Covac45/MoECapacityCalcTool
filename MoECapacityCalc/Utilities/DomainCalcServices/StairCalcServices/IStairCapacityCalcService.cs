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
        public double CalcStairCapacity(Area area, Stair stair);

        public StairCapacityStruct GetStairCapacityStruct(Area area, Stair stair);
    }
}
