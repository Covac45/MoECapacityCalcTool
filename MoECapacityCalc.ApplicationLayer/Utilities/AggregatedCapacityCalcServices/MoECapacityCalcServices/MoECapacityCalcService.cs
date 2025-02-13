using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.DiscountingService;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.MoECapacityCalcServices
{
    public interface IMoeCapacityCalcService
    {
        public List<ExitCapacityStruct> GetMoECapacityStructs(Area area);
        public CapacityStruct GetTotalDiscountedMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
    }
    public class MoECapacityCalcService : IMoeCapacityCalcService
    {
        private readonly IExitCapacityStructsService _exitCapacityStructsService;
        private readonly IVerticalEscapeCapacityCalcService _verticalEscapeCapacityCalcService;
        private readonly IDiscountingAndCappingService _discountingAndCappingService;


        public MoECapacityCalcService(IExitCapacityStructsService exitCapacityStructsService,
            IVerticalEscapeCapacityCalcService verticalEscapeCapacityCalcService,
            IDiscountingAndCappingService discountingAndCappingService)
        {
            _exitCapacityStructsService = exitCapacityStructsService;
            _verticalEscapeCapacityCalcService = verticalEscapeCapacityCalcService;
            _discountingAndCappingService = discountingAndCappingService;
        }

        public List<ExitCapacityStruct> GetMoECapacityStructs(Area area)
        {
            var exitsFromArea = area.Relationships.GetExits();
            var storeyExits = exitsFromArea.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            var finalExits = exitsFromArea.Where(exit => exit.ExitType == ExitType.finalExit).ToList();

            var NonStairMoeCapacityStructs = _exitCapacityStructsService.GetExitCapacityStructsForNonStairExits(storeyExits, finalExits);
            var StairMoeCapacityStructs = GetMoECapacityStructForStairs(area);

            var MoECapacityStructs = NonStairMoeCapacityStructs;
            MoECapacityStructs.AddRange(StairMoeCapacityStructs);

            return MoECapacityStructs;
        }

        public CapacityStruct GetTotalDiscountedMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            return _discountingAndCappingService.GetTotalDiscountedMoECapacity(exitCapacityStructs, area);
        }

        private List<ExitCapacityStruct> GetMoECapacityStructForStairs(Area area)
        {
            var stairs = area.Relationships.GetStairs();
            var stairCapacityStructs = _verticalEscapeCapacityCalcService.GetStairCapacityStructs(area);
            var stairExitCapacitystructs = _exitCapacityStructsService.GetExitCapacityStructsForStairExits(area, stairs);

            var StairMoECapacityStructs = new List<ExitCapacityStruct>();

            foreach (var aStair in stairs)
            {
                var aStairCapacityStruct = stairCapacityStructs.SingleOrDefault(scs => scs.Id == aStair.Id);
                var aStairExitCapacityStructs = stairExitCapacitystructs.SingleOrDefault(secs => secs.Key == aStair).Value;

                var stairExitCapacity = aStairExitCapacityStructs.Select(secs => secs.Capacity).Sum();

                //Division by storey exit count is a strategy - this needs to be decoupled by strategy pattern
                var storeyExitCount = aStairExitCapacityStructs.Count();

                var aStairExitCapacityStruct = new ExitCapacityStruct();
                if (area.FloorLevel != aStair.FinalExitLevel)
                {
                    if (aStairCapacityStruct.CapacityPerFloor < stairExitCapacity)
                    {
                        aStairExitCapacityStructs = aStairExitCapacityStructs.Select(aSECS => new ExitCapacityStruct
                        {
                            Id = aSECS.Id,
                            Name = aSECS.Name,
                            Capacity = aStairCapacityStruct.CapacityPerFloor / storeyExitCount,
                        }).ToList();

                        aStairExitCapacityStruct = new ExitCapacityStruct
                        {
                            Id = aStairCapacityStruct.Id,
                            Name = aStairCapacityStruct.Name,
                            Capacity = aStairExitCapacityStructs.Select(secs => secs.Capacity).Sum(),
                            CapacityNote = "The capacity of the exit is limited by the capacity of the stair it serves"
                        };
                    }
                    else
                    {
                        aStairExitCapacityStruct = new ExitCapacityStruct
                        {
                            Id = aStairCapacityStruct.Id,
                            Name = aStairCapacityStruct.Name,
                            Capacity = aStairExitCapacityStructs.Select(secs => secs.Capacity).Sum(),
                            CapacityNote = "The capacity of the exit is limited by the capacity exits serving the stair "
                        };
                    }
                }
                else
                {
                    aStairExitCapacityStruct = new ExitCapacityStruct
                    {
                        Id = aStairCapacityStruct.Id,
                        Name = aStairCapacityStruct.Name,
                        Capacity = aStairExitCapacityStructs.Select(secs => secs.Capacity).Sum(),
                        CapacityNote = "The capacity of the exit is limited by the capacity exits serving the stair "
                    };
                }


                StairMoECapacityStructs.Add(aStairExitCapacityStruct);
            }
            return StairMoECapacityStructs;
        }
    }
}
