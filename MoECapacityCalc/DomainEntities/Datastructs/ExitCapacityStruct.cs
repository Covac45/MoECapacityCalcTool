namespace MoECapacityCalc.DomainEntities.Datastructs
{
    public class ExitCapacityStruct
    {
        public Guid ExitId { get; set; }
        public double exitCapacity { get; set; }
        public string capacityNote { get; set; }
        public ExitCapacityStruct() {}
    }
}
