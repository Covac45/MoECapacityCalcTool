using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using System.Collections.Generic;

namespace MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public class CachedExitCapacityStructService : IExitCapacityStructsService
    {
        Dictionary<List<Exit>,List<ExitCapacityStruct>> NonStairExitCapacityStructs = new();
        Dictionary<List<Stair>, Dictionary<Stair, List<ExitCapacityStruct>>> StairExitCapacityStructs = new();

        private readonly IExitCapacityStructsService _exitCapacityStructsService;
        public CachedExitCapacityStructService(IExitCapacityStructsService exitCapacityStructsService)
        {
            _exitCapacityStructsService = exitCapacityStructsService;
        }

        public List<ExitCapacityStruct> GetExitCapacityStructsForNonStairExits(List<Exit> storeyExits, List<Exit> finalExits)
        {
            var exits = new List<Exit>();

            storeyExits.ForEach(storeyExit => exits.Add(storeyExit));
            finalExits.ForEach(finalExit => exits.Add(finalExit));


            if (NonStairExitCapacityStructs.TryGetValue(exits, out List<ExitCapacityStruct> value))
            {
                return value;
            }

            value = _exitCapacityStructsService.GetExitCapacityStructsForNonStairExits(storeyExits, finalExits);
            NonStairExitCapacityStructs.Add(exits, value);

            return value;
        }

        public Dictionary<Stair, List<ExitCapacityStruct>> GetExitCapacityStructsForStairExits(Area area, List<Stair> stairs)
        {

            if (StairExitCapacityStructs.TryGetValue(stairs, out Dictionary<Stair, List<ExitCapacityStruct>> value))
            {
                return value;
            }

            value = _exitCapacityStructsService.GetExitCapacityStructsForStairExits(area, stairs);
            StairExitCapacityStructs.Add(stairs, value);

            return value;
        }

        public List<ExitCapacityStruct> SumExitCapacityStructsById(List<ExitCapacityStruct> exitCapacityStructs)
        {
            return _exitCapacityStructsService.SumExitCapacityStructsById(exitCapacityStructs);
        }
    }
}
