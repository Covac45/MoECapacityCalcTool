using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;

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
