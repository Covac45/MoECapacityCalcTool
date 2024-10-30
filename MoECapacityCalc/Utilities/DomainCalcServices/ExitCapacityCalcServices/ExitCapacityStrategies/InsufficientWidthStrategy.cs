using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public class InsufficientWidthStrategy : IExitCapacityCalcStrategy
    {
        public ExitCapacityStruct CalcExitCapacity(Exit exit)
        {
            if (exit.ExitWidth < 750)
            {
                return new ExitCapacityStruct
                {
                    Id = exit.Id,
                    Capacity = 0,
                    CapacityNote = "The exit has insufficient width to be used as a means of escape."
                };
            }
            return null; // Return null if this rule does not apply
        }
    }
}
