using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;

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
                    ExitId = exit.Id,
                    exitCapacity = 0,
                    capacityNote = "The exit has insufficient width to be used as a means of escape."
                };
            }
            return null; // Return null if this rule does not apply
        }
    }
}
