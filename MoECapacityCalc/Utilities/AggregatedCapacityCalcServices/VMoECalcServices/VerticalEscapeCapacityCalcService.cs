using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices
{
    public interface IVerticalEscapeCapacityCalcService
    {
        public List<StairCapacityStruct> CalcStairCapacities(Area area);
        public CapacityStruct CalcTotalVMoECapacity(List<StairCapacityStruct> stairCapacityStructs, Area area);
    }

    public class VerticalEscapeCapacityCalcService : IVerticalEscapeCapacityCalcService
    {
        private readonly IStairCapacityCalcService _stairCapacityCalcService;
        public VerticalEscapeCapacityCalcService(IStairCapacityCalcService stairCapacityCalcService)
        {
            _stairCapacityCalcService = stairCapacityCalcService;
        }

        public List<StairCapacityStruct> CalcStairCapacities(Area area)
        {
            var stairs = area.Relationships.GetStairs();

            List<StairCapacityStruct> stairCapacityStructs = new();


            stairs.ForEach(s => stairCapacityStructs.Add(_stairCapacityCalcService.GetStairCapacityStruct(s, area)));

            return stairCapacityStructs;
        }

        public CapacityStruct CalcTotalVMoECapacity(List<StairCapacityStruct> stairCapacityStructs, Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var numStairs = stairs.Count();
            var sumStairCapacity = stairCapacityStructs.Select(s => s.Capacity).Sum();

            var maxUnprotectedStairCapacity = stairCapacityStructs.Where(scs => stairs
                                                                    .Where(s => s.IsSmokeProtected == false)
                                                                    .Any(s => s.Id == scs.Id))
                                                                    .DefaultIfEmpty(new StairCapacityStruct() { Capacity = 0 })
                                                                    .Max(scs => scs.Capacity);

            double vmoeCapacity = 0;
            switch (numStairs)
            {
                case 1:
                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sumStairCapacity,
                        CapacityNote = "The means of escape capacity of this area is limited by the capacity of stairs serving this area."
                    };

                case > 1:
                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sumStairCapacity - maxUnprotectedStairCapacity,
                        CapacityNote = "The means of escape capacity of this area is limited by the capacity of stairs serving this area."
                    };
                default:
                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sumStairCapacity - maxUnprotectedStairCapacity,
                        CapacityNote = "No viable escape routes have been provided for the area."
                    };
            }
        }
    }
}
