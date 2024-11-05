using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public class DoorSwingCapacityStrategy : IExitCapacityCalcStrategy
    {
        public ExitCapacityStruct CalcExitCapacity(Exit exit)
        {
            if (exit.ExitWidth >= 850 && exit.DoorSwing == DoorSwing.against)
            {
                return new ExitCapacityStruct
                {
                    Id = exit.Id,
                    Name = exit.Name,
                    Capacity = 60,
                    CapacityNote = "The exit capacity is limited by the door swing."
                };
            }
            return null;
        }
    }
}
