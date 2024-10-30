using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices
{
    public interface IVerticalEscapeCapacityCalcService
    {
        public List<StairCapacityStruct> CalcStairCapacities(Area area);
        public double CalcTotalVMoECapacity(List<StairCapacityStruct> stairCapacityStructs, Area area);
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


            stairs.ForEach(s => stairCapacityStructs.Add(_stairCapacityCalcService.GetStairCapacityStruct(area, s)));

            return stairCapacityStructs;
        }

        public double CalcTotalVMoECapacity(List<StairCapacityStruct> stairCapacityStructs, Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var numStairs = stairs.Count();
            var sumStairCapacity = stairCapacityStructs.Select(s => s.stairCapacity).Sum();

            var maxUnprotectedStairCapacity = stairCapacityStructs.Where(scs => stairs
                                                                            .Where(s => s.IsSmokeProtected == false)
                                                                            .Any(s => s.Id == scs.StairId))
                                                                            .Max(scs => scs.stairCapacity);

            double VmoeCapacity = 0;
            switch (numStairs)
            {
                case 1:
                    VmoeCapacity = sumStairCapacity;
                    return VmoeCapacity;
                case > 1:
                    VmoeCapacity = sumStairCapacity - maxUnprotectedStairCapacity;
                    return VmoeCapacity;
                default:
                    return VmoeCapacity;
            }
        }
    }
}
