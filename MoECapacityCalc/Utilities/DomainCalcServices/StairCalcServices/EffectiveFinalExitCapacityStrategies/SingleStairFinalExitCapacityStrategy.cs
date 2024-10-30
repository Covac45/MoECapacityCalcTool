using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public class SingleStairFinalExitCapacityStrategy() : IStairFinalExitCapacityStrategy
    {

        public double GetEffectiveStairFinalExitCapacity(List<Exit> finalExitsServingStair)
        {
            return finalExitsServingStair.Sum(finalExit =>
                new ExitCapacityCalcService().CalcExitCapacity(finalExit).exitCapacity);
        }
    }
}
