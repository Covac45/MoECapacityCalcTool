using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;

namespace MoECapacityCalc.Utilities.Services
{
    public interface IExitCapacityCalcService
    {
        ExitCapacityStruct CalcExitCapacity(Exit exit);
    }

    public class ExitCapacityCalcService : IExitCapacityCalcService
    {

        public ExitCapacityCalcService()
        {

        }

        public ExitCapacityStruct CalcExitCapacity(Exit exit)
        {
            double exitCapacity = 0;
            string note = "";


            if (exit.ExitWidth < 750)
            {
                exitCapacity = 0;
                note = "The exit has insufficient width to be used as a means of escape.";
            }
            else if (exit.ExitWidth >= 750 && exit.ExitWidth < 850)
            {
                exitCapacity = 60;
                note = "The exit capacity is limited by its width.";
            }
            else if (exit.ExitWidth >= 850 && exit.ExitWidth < 1050)
            {
                exitCapacity = 110;
                note = "The exit capacity is limited by its width.";
            }
            else if (exit.ExitWidth >= 1050)
            {
                exitCapacity = 220 + (exit.ExitWidth - 1050) / 5;
                note = "The exit capacity is limited by its width.";
            }

            if (exit.ExitWidth >= 850 && exit.DoorSwing == DoorSwing.against)
            {
                exitCapacity = 60;
                note = "The exit capacity is limited by the door swing.";
            }

            ExitCapacityStruct exitCapacityStruct = new()
            {
                ExitId = exit.Id,
                exitCapacity = exitCapacity,
                capacityNote = note
            };

            return exitCapacityStruct;
        }

    }
}
