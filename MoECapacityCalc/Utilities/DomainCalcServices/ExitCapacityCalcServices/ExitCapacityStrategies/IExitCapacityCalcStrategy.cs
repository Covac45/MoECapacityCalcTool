using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public interface IExitCapacityCalcStrategy
    {
        ExitCapacityStruct CalcExitCapacity(Exit exit);

    }
}
