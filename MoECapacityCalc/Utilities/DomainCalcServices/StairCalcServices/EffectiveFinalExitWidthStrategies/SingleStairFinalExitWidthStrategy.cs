using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public class SingleStairFinalExitWidthStrategy() : IStairFinalExitWidthStrategy
    {
        public double GetEffectiveStairFinalExitWidth(List<Exit> finalExitsServingStair)
        {
            return finalExitsServingStair.Sum(finalExit => finalExit.ExitWidth);
        }
    }
}
