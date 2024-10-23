namespace MoECapacityCalc.Database.Interfaces
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}