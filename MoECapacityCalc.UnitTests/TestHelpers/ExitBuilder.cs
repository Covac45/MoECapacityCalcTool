using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.UnitTests.TestHelpers
{
    internal class ExitBuilder
    {
        string _name;
        ExitType _exitType;
        DoorSwing _doorSwing;
        double _exitWidth;
        Association? _associations;

        public ExitBuilder(string name, ExitType exitType, DoorSwing doorSwing, double exitWidth, Association? associations = null)
        {
            _name = name;
            _exitType = exitType;
            _doorSwing = doorSwing;
            _exitWidth = exitWidth;
            _associations = associations;
        }

        public Exit Build()
        {

            return new Exit(_name, _exitType, _doorSwing, _exitWidth, _associations);
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

        public ExitBuilder WithAssociations(Association associations)
        {
            _associations = associations;
            return this;
        }
    }
}
