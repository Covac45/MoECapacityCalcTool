using MoECapacityCalc.DomainEntities;

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
