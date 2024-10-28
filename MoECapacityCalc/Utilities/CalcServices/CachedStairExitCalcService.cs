using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Services;

namespace MoECapacityCalc.Utilities.CalcServices
{
    internal class CachedStairExitCalcService : IStairExitCalcService
    {
        private readonly IStairExitCalcService _stairExitCalcService;

        Dictionary<List<Stair>, Dictionary<Stair, double>> MergingFlowCapacities = new Dictionary<List<Stair>, Dictionary<Stair, double>>();

        public CachedStairExitCalcService(IStairExitCalcService stairExitCalcService) 
        {
            _stairExitCalcService = stairExitCalcService;
        }


        public double CalcFinalExitLevelCapacity(Stair stair)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Stair, double> CalcMergingFlowCapacities(List<Stair> stairs)
        {
            if (MergingFlowCapacities.TryGetValue(stairs, out Dictionary<Stair, double> value))
            {
                return value;
            }

            value = _stairExitCalcService.CalcMergingFlowCapacities(stairs);
            MergingFlowCapacities.Add(stairs, value);

            return value;
        }

        public double CalcStoreyExitLevelCapacity(Stair stair)
        {
            throw new NotImplementedException();
        }

        public double TotalFinalExitCapacity(Stair stair)
        {
            throw new NotImplementedException();
        }

        public double TotalStoreyExitCapacity(Stair stair)
        {
            throw new NotImplementedException();
        }
    }
}
