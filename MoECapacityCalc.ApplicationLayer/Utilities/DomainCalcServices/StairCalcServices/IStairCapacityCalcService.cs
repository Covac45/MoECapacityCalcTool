using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices
{
    public interface IStairCapacityCalcService
    {
        public double CalcStairCapacity(Stair stair, Area area = null);

        public StairCapacityStruct GetStairCapacityStruct(Stair stair, Area area = null);
    }
}
