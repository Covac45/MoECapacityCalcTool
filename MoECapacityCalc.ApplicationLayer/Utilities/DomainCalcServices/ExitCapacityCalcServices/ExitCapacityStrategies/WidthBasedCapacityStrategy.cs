using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies
{
    public class WidthBasedCapacityStrategy : IExitCapacityCalcStrategy
    {
        public ExitCapacityStruct CalcExitCapacity(Exit exit)
        {
            if (exit.ExitWidth >= 750 && exit.ExitWidth < 850 && exit.DoorSwing == DoorSwing.with)
            {
                return new ExitCapacityStruct
                {
                    Id = exit.Id,
                    Name = exit.Name,
                    Capacity = 60,
                    CapacityNote = "The exit capacity is limited by its width."
                };
            }
            if (exit.ExitWidth >= 850 && exit.ExitWidth < 1050 && exit.DoorSwing == DoorSwing.with)
            {
                return new ExitCapacityStruct
                {
                    Id = exit.Id,
                    Name = exit.Name,
                    Capacity = 110,
                    CapacityNote = "The exit capacity is limited by its width."
                };
            }
            if (exit.ExitWidth >= 1050 && exit.DoorSwing == DoorSwing.with)
            {
                return new ExitCapacityStruct
                {
                    Id = exit.Id,
                    Name = exit.Name,
                    Capacity = 220 + (exit.ExitWidth - 1050) / 5,
                    CapacityNote = "The exit capacity is limited by its width."
                };
            }
            return null;
        }
    }
}
