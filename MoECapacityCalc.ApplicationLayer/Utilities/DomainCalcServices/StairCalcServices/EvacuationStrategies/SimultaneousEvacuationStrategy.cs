using MoECapacityCalc.DomainEntities;


namespace MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies
{
    public class SimultaneousEvacuationStrategy : IEvacuationStrategy
    {
        public double GetStairCapacityPerFloor(double stairCapacity, double upperFloorsServed)
        {
            double stairCapacityPerFloor = stairCapacity / upperFloorsServed;
            return stairCapacityPerFloor;
        }
    }
}
