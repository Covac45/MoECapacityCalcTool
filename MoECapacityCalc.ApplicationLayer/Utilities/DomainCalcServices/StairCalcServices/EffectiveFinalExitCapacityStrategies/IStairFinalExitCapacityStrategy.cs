using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public interface IStairFinalExitCapacityStrategy
    {
        double GetEffectiveStairFinalExitCapacity(List<Exit> finalExitsServingStair);
    }
}
