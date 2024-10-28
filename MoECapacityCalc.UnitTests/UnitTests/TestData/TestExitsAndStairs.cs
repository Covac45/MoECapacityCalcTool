using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.UnitTests.TestHelpers;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.TestData
{
    public class TestExitsAndStairs
    {
        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData1()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            //Exits associated with stairs
            List<Exit> stair1Exits = new List<Exit> { storeyExit1, finalExit1 };
            List<Exit> stair2Exits = new List<Exit> { storeyExit2, finalExit2 };

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit3, finalExit3 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1)];

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2)];


            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (exits, stairs);
        }


        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData2()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            //Exits associated with stairs
            List<Exit> stair1Exits = new List<Exit> { storeyExit1, finalExit1 };
            List<Exit> stair2Exits = new List<Exit> { storeyExit2, finalExit2 };

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit3, finalExit3 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit2)];

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit1),
                                new Relationship<Stair, Exit>(stair2, finalExit2)];

            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (exits, stairs);
        }


        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData3()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            //Exits associated with stairs
            List<Exit> stair1Exits = new List<Exit> { storeyExit1, finalExit1 };
            List<Exit> stair2Exits = new List<Exit> { storeyExit2, finalExit2 };

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit3 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit2)];

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit3)];

            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (exits, stairs);
        }



        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData4()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit4 = new Exit("storey exit 4", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit4 = new Exit("final exit 4", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit5 = new Exit("storey exit 5", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit5 = new Exit("final exit 5", ExitType.finalExit, DoorSwing.with, 1050);

            Exit finalExit6 = new Exit("final exit 6", ExitType.finalExit, DoorSwing.with, 1050);
            Exit finalExit7 = new Exit("final exit 7", ExitType.finalExit, DoorSwing.with, 1050);


            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit5,  finalExit7};

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);
            

            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit5)];

            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit5)];


            Stair stair3 = new Stair("stair 1", 1000, 3, 0);
         
            stair3.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair3, storeyExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit6)];
            
            Stair stair4 = new Stair("stair 2", 1100, 3, 0);

            stair4.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair4, storeyExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit6)];


            List<Stair> stairs = new List<Stair> { stair1, stair2, stair3, stair4 };

            return (exits, stairs);
        }


        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData5()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.against, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.against, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.against, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit4 = new Exit("storey exit 4", ExitType.storeyExit, DoorSwing.against, 1050);
            Exit finalExit4 = new Exit("final exit 4", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit5 = new Exit("storey exit 5", ExitType.storeyExit, DoorSwing.against, 1050);
            Exit finalExit5 = new Exit("final exit 5", ExitType.finalExit, DoorSwing.with, 1050);

            Exit finalExit6 = new Exit("final exit 6", ExitType.finalExit, DoorSwing.with, 1050);

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit5 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);


            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit5)];

            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit5)];


            Stair stair3 = new Stair("stair 1", 1000, 3, 0);

            stair3.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair3, storeyExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit6)];

            Stair stair4 = new Stair("stair 2", 1100, 3, 0);

            stair4.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair4, storeyExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit6)];


            List<Stair> stairs = new List<Stair> { stair1, stair2, stair3, stair4 };

            return (exits, stairs);
        }


        public static (List<Exit>, List<Stair>) GetExitsAndStairsTestData6()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit4 = new Exit("storey exit 4", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit4 = new Exit("final exit 4", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit5 = new Exit("storey exit 5", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit5 = new Exit("final exit 5", ExitType.finalExit, DoorSwing.with, 1050);

            Exit finalExit6 = new Exit("final exit 6", ExitType.finalExit, DoorSwing.with, 1050);

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit5 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);


            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit5)];

            Stair stair2 = new Stair("stair 2", 1100, 3, 0);

            stair2.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair2, storeyExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit2),
                                new Relationship<Stair, Exit>(stair2, finalExit5)];


            Stair stair3 = new Stair("stair 1", 1000, 3, 0);

            stair3.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair3, storeyExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit3),
                                new Relationship<Stair, Exit>(stair3, finalExit6)];

            Stair stair4 = new Stair("stair 2", 1100, 3, 0);

            stair4.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair4, storeyExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit4),
                                new Relationship<Stair, Exit>(stair4, finalExit6)];


            List<Stair> stairs = new List<Stair> { stair1, stair2, stair3, stair4 };

            return (exits, stairs);
        }



    }
}
