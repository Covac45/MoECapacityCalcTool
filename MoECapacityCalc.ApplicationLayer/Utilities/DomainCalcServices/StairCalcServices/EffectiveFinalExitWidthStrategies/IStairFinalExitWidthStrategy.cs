using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies
{
    public interface IStairFinalExitWidthStrategy
    {
        double GetEffectiveStairFinalExitWidth(List<Exit> finalExitsServingStair);
    }
}
