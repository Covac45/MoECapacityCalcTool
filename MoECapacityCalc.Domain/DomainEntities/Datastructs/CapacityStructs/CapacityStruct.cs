namespace MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs
{
    public class CapacityStruct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }
        public string CapacityNote { get; set; }
        public CapacityStruct() { }
    }
}
