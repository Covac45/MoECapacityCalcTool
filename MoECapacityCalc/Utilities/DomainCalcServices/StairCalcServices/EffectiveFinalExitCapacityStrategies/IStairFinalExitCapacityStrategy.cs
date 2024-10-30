using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public interface IStairFinalExitCapacityStrategy
    {
        double GetEffectiveStairFinalExitCapacity(List<Exit> finalExitsServingStair);
    }
}
