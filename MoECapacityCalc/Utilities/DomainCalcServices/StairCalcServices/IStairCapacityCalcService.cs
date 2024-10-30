using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices
{
    public interface IStairCapacityCalcService
    {
        public double CalcStairCapacity(Stair stair, Area area = null);

        public StairCapacityStruct GetStairCapacityStruct(Stair stair, Area area = null);
    }
}
