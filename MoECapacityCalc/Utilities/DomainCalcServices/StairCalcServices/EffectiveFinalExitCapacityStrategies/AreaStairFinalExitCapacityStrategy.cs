using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public class AreaStairFinalExitCapacityStrategy(Area area) : IStairFinalExitCapacityStrategy
    {
        public double GetEffectiveStairFinalExitCapacity(List<Exit> finalExitsServingStair)
        {
            var stairs = area.Relationships.GetStairs();
            return finalExitsServingStair.Sum(finalExit =>
            {
                var numStairsServed = stairs.Count(s => s.Relationships.GetExits().Any(e => e.Id == finalExit.Id));
                return new ExitCapacityCalcService().CalcExitCapacity(finalExit).exitCapacity / numStairsServed;
            });
        }

    }
}
