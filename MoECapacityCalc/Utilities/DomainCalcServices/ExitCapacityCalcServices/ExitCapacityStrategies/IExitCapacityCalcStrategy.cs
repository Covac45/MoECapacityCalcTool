using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public interface IExitCapacityCalcStrategy
    {
        ExitCapacityStruct CalcExitCapacity(Exit exit);

    }
}
