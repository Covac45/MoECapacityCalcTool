namespace MoECapacityCalc.DomainEntities.Datastructs
{
    public class StairCapacityStruct
    {
        public Guid StairId { get; set; }
        public double stairCapacity { get; set; }

        public double stairCapacityPerFloor { get; set; }
        public string capacityNote { get; set; }
        
        public StairCapacityStruct() { }
    }
}
