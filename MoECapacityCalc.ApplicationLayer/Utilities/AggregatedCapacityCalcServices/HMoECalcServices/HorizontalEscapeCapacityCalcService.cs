using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using System.Linq;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public interface IHorizontalEscapeCapacityCalcService
    {
        List<ExitCapacityStruct> CalcExitCapacities(Area area);
        public CapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
    }

    public class HorizontalEscapeCapacityCalcService : IHorizontalEscapeCapacityCalcService
    {
        private readonly IExitCapacityCalcService _exitCapacityCalcService;
        private readonly IExitCapacityStructCapService _exitCapacityStructCapService;
        private readonly IStairExitCalcService _stairExitCalcService;
        private readonly IStairCapacityCalcService _stairCapacityCalcService;
        private readonly IExitCapacityStructsService _exitCapacityStructsService;

        public HorizontalEscapeCapacityCalcService(IExitCapacityCalcService exitCapacityCalcService,
            IExitCapacityStructCapService exitCapacityStructCapService, IStairExitCalcService stairExitCalcService,
            IStairCapacityCalcService stairCapacityCalcService, IExitCapacityStructsService exitCapacityStructsService)
        {
            _exitCapacityCalcService = exitCapacityCalcService;
            _exitCapacityStructCapService = exitCapacityStructCapService;
            _stairExitCalcService = stairExitCalcService;
            _stairCapacityCalcService = stairCapacityCalcService;
            _exitCapacityStructsService = exitCapacityStructsService;
        }

        public List<ExitCapacityStruct> CalcExitCapacities(Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var exits = area.Relationships.GetToExits();

            var storeyExits = exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            var finalExits = exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
            var altExits = exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

            //For HMoE not associated with stairs
            var nonStairExitCapacityStructs = _exitCapacityStructsService.GetExitCapacityStructsForNonStairExits(storeyExits, finalExits);

            //ForHMoEAssociated with Stairs
            var stairExitCapacityStructs = _exitCapacityStructsService.GetExitCapacityStructsForStairExits(area, stairs);

            var summedStairExitCapacityStructs = _exitCapacityStructsService
                .SumExitCapacityStructsById(stairExitCapacityStructs
                .Values
                .SelectMany(ecsList => ecsList)
                .ToList());

            var allExitCapacityStructs = new List<ExitCapacityStruct>();
            allExitCapacityStructs.AddRange(nonStairExitCapacityStructs);
            allExitCapacityStructs.AddRange(summedStairExitCapacityStructs);

            var cappedExitCapacityStructs = _exitCapacityStructCapService.GetCappedExitCapacityStructs(allExitCapacityStructs);

            return cappedExitCapacityStructs;
        }

        //Implements discounting logic for multiple exits (i.e. remove the most capacious exit)
        //Also implements capping logic based on number of storey exits (single exit: 60 people, two exits: 600 people)
        public CapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            int numExitsFromArea = GetNumberOfEscapeRoutesFromArea(area);

            double sum = exitCapacityStructs.Select(e => e.Capacity).Sum();
            double max = exitCapacityStructs.Select(e => e.Capacity).DefaultIfEmpty().Max();

            return GetCappedHmoeCapacityStruct(area, numExitsFromArea, sum, max);
        }

        private static int GetNumberOfEscapeRoutesFromArea(Area area)
        {
            var numStairsServingArea = area.Relationships.GetStairs()
                                                        .Select(s => s)
                                                        .Where(s => s.Relationships.GetFromExits().Where(e => e.ExitType == ExitType.storeyExit) != null)
                                                        .Count();

            var numNonStairExitsServingArea = area.Relationships.GetToExits().Count();

            int numEscapeRoutesFromArea = numNonStairExitsServingArea + numStairsServingArea;
            return numEscapeRoutesFromArea;
        }

        private CapacityStruct GetCappedHmoeCapacityStruct(Area area, int numExitsFromArea, double sum, double max)
        {
            var cap = 0;
            var hmoeCapacityNote = "";
            switch (numExitsFromArea)
            {
                case 1:
                    cap = 60;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 60 as only a single escape route is provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case 2:
                    cap = 600;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 600 as only two escape routes are provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case > 2:

                    hmoeCapacityNote = "The means of escape capacity of this area is limited by the the capacity of escape routes. See escape route capacity assessment for further information.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sum - max,
                        CapacityNote = hmoeCapacityNote
                    };
                default:
                    hmoeCapacityNote = "No escape routes have been provided for the area.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = 0,
                        CapacityNote = hmoeCapacityNote
                    };
            }
        }


        private double CapExitCapacity(double totalExitCapacity, double cap)
        {
            return Math.Min(totalExitCapacity, cap);
        }
    }
}
