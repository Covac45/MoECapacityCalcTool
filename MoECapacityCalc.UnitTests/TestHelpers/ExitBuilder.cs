using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.UnitTests.TestHelpers
{
    internal class ExitBuilder
    {
        string _name;
        ExitType _exitType;
        DoorSwing _doorSwing;
        double _exitWidth;
        RelationshipSet<Exit> _relationships;

        public ExitBuilder(string name, ExitType exitType, DoorSwing doorSwing, double exitWidth, RelationshipSet<Exit> relationships)
        {
            _name = name;
            _exitType = exitType;
            _doorSwing = doorSwing;
            _exitWidth = exitWidth;
            _relationships = relationships ?? new RelationshipSet<Exit>();
        }

        public Exit Build()
        {

            return new Exit(_name, _exitType, _doorSwing, _exitWidth);
        }

        public ExitBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ExitBuilder WithExitType(ExitType exitType)
        {
            _exitType = exitType;
            return this;
        }

        public ExitBuilder WithDoorSwing(DoorSwing doorSwing)
        {
            _doorSwing = doorSwing;
            return this;
        }

        public ExitBuilder WithExitWidth(double exitWidth)
        {
            _exitWidth = exitWidth;
            return this;
        }

        public ExitBuilder WithAssociations(RelationshipSet<Exit> relationships)
        {
            _relationships = relationships;
            return this;
        }
    }
}
