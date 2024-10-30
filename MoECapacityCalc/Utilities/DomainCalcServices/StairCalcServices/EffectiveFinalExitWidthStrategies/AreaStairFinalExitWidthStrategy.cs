using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public class AreaStairFinalExitWidthStrategy(Area area) : IStairFinalExitWidthStrategy
    {
        public double GetEffectiveStairFinalExitWidth(List<Exit> finalExitsServingStair)
        {
            var stairs = area.Relationships.GetStairs();
            return finalExitsServingStair.Sum(finalExit =>
            {
                var numStairsServed = stairs.Count(s => s.Relationships.GetExits().Any(e => e.Id == finalExit.Id));
                return finalExit.ExitWidth / numStairsServed;
            });
        }

    }
}
