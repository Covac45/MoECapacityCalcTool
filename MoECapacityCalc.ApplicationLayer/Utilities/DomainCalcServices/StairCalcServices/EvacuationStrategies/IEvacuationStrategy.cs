namespace MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies
{
    public interface IEvacuationStrategy
    {
        double GetStairCapacityPerFloor(double stairCapacity, double upperFloorsServed);
    }
}
