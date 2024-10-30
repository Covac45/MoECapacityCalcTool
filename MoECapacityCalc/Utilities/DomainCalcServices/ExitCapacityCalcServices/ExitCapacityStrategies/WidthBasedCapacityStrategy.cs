using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ExitId = exit.Id,
                    exitCapacity = 60,
                    capacityNote = "The exit capacity is limited by its width."
                };
            }
            if (exit.ExitWidth >= 850 && exit.ExitWidth < 1050 && exit.DoorSwing == DoorSwing.with)
            {
                return new ExitCapacityStruct
                {
                    ExitId = exit.Id,
                    exitCapacity = 110,
                    capacityNote = "The exit capacity is limited by its width."
                };
            }
            if (exit.ExitWidth >= 1050 && exit.DoorSwing == DoorSwing.with)
            {
                return new ExitCapacityStruct
                {
                    ExitId = exit.Id,
                    exitCapacity = 220 + (exit.ExitWidth - 1050) / 5,
                    capacityNote = "The exit capacity is limited by its width."
                };
            }
            return null;
        }
    }
}
