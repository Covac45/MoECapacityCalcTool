using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using System.Linq;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.DiscountingService;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public interface IHorizontalEscapeCapacityCalcService
    {
        List<ExitCapacityStruct> CalcExitCapacities(Area area);
        public CapacityStruct CalcTotalDiscountedHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
    }

    public class HorizontalEscapeCapacityCalcService : IHorizontalEscapeCapacityCalcService
    {
        private readonly IExitCapacityCalcService _exitCapacityCalcService;
        private readonly IExitCapacityStructCapService _exitCapacityStructCapService;
        private readonly IStairExitCalcService _stairExitCalcService;
        private readonly IStairCapacityCalcService _stairCapacityCalcService;
        private readonly IExitCapacityStructsService _exitCapacityStructsService;
        private readonly IDiscountingAndCappingService _discountingAndCappingService;

        public HorizontalEscapeCapacityCalcService(IExitCapacityCalcService exitCapacityCalcService,
            IExitCapacityStructCapService exitCapacityStructCapService, IStairExitCalcService stairExitCalcService,
            IStairCapacityCalcService stairCapacityCalcService, IExitCapacityStructsService exitCapacityStructsService,
            IDiscountingAndCappingService discountingAndCappingService)
        {
            _exitCapacityCalcService = exitCapacityCalcService;
            _exitCapacityStructCapService = exitCapacityStructCapService;
            _stairExitCalcService = stairExitCalcService;
            _stairCapacityCalcService = stairCapacityCalcService;
            _exitCapacityStructsService = exitCapacityStructsService;
            _discountingAndCappingService = discountingAndCappingService;
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

            var LimitingFactorExitCapacityStructs = _exitCapacityStructCapService.GetLimitingFactorExitCapacityStructs(allExitCapacityStructs);

            return LimitingFactorExitCapacityStructs;
        }

        //Implements discounting logic for multiple exits (i.e. remove the most capacious exit)
        //Also implements capping logic based on number of storey exits (single exit: 60 people, two exits: 600 people)
        public CapacityStruct CalcTotalDiscountedHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            return _discountingAndCappingService.GetTotalDiscountedMoECapacity(exitCapacityStructs, area);
        }
    }
}
