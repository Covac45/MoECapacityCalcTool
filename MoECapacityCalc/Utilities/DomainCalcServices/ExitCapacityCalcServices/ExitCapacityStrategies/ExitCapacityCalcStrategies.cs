using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ExitId = exit.Id,
                    exitCapacity = 60,
                    capacityNote = "The exit capacity is limited by the door swing."
                };
            }
            return null;
        }
    }
}
