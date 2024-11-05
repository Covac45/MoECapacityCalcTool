using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
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
        private readonly IExitCapacityStructsService _exitCapacityStructsService;
        private readonly IExitCapacityStructCapService _exitCapacityStructCapService;
        private readonly IDiscountingAndCappingService _discountingAndCappingService;

        public HorizontalEscapeCapacityCalcService(IExitCapacityStructsService exitCapacityStructsService,
            IExitCapacityStructCapService exitCapacityStructCapService,
            IDiscountingAndCappingService discountingAndCappingService)
        {
            _exitCapacityStructsService = exitCapacityStructsService;
            _exitCapacityStructCapService = exitCapacityStructCapService;
            _discountingAndCappingService = discountingAndCappingService;
        }

        public List<ExitCapacityStruct> CalcExitCapacities(Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var exits = area.Relationships.GetToExits();

            var storeyExits = exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            var finalExits = exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
            var altExits = exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

            var nonStairExitCapacityStructs = _exitCapacityStructsService.GetExitCapacityStructsForNonStairExits(storeyExits, finalExits);

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

        public CapacityStruct CalcTotalDiscountedHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            return _discountingAndCappingService.GetTotalDiscountedMoECapacity(exitCapacityStructs, area);
        }
    }
}
