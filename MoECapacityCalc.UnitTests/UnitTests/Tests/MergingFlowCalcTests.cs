using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.CalcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Services;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests
{
    public class MergingFlowCalcTests
    {

        [TestCase(315)]
        public void StairFinalExitLevelCapacityTests2(double expectedExitCapacity)
        {


            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Stair stair1 = new Stair("stair 1", 1100, 1, 0);
            Stair stair2 = new Stair("stair 2", 1000, 1, 0);


            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit3)]; ;

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit3)]; ;

            Dictionary<Stair, double> mergingFlowCapacities = new StairExitCalcService().CalcMergingFlowCapacities(new List<Stair>() { stair1, stair2 });

            Assert.That(mergingFlowCapacities.Values.Sum(), Is.EqualTo(expectedExitCapacity));
        }

    }
}
