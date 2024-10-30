using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public interface IExitCapacityCalcStrategy
    {
        ExitCapacityStruct CalcExitCapacity(Exit exit);

    }
}
