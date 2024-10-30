using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices.ExitCapacityStrategies;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices
{
    public interface IExitCapacityCalcService
    {
        ExitCapacityStruct CalcExitCapacity(Exit exit);
    }

    public class ExitCapacityCalcService : IExitCapacityCalcService
    {

        private readonly List<IExitCapacityCalcStrategy> _strategies;

        public ExitCapacityCalcService()
        {
            _strategies = new List<IExitCapacityCalcStrategy>
            {
                new InsufficientWidthStrategy(),
                new WidthBasedCapacityStrategy(),
                new DoorSwingCapacityStrategy()
            };
        }
            

        public ExitCapacityStruct CalcExitCapacity(Exit exit)
        {
            foreach (var strategy in _strategies)
            {
                var result = strategy.CalcExitCapacity(exit);
                if (result != null)
                {
                    return result; // Use the first strategy that applies
                }
            }
            return new ExitCapacityStruct { ExitId = exit.Id, exitCapacity = 0, capacityNote = "No applicable capacity rule found." };
        }

    }
}
