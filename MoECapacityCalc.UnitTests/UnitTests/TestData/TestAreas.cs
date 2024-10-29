using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.UnitTests.UnitTests.TestData
{
    public class TestAreas : TestExitsAndStairs
    {
        public static Area GetAreaTestData1()
        {
            var (exits, stairs) = GetExitsAndStairsTestData1();

            Area area1 = new Area(0, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData2()
        {
            var (exits, stairs) = GetExitsAndStairsTestData2();

            Area area1 = new Area(0, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData3()
        {
            var (exits, stairs) = GetExitsAndStairsTestData3();

            Area area1 = new Area(0, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData4()
        {
            var (exits, stairs) = GetExitsAndStairsTestData4();

            Area area1 = new Area(0, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData5()
        {
            var (exits, stairs) = GetExitsAndStairsTestData5();

            Area area1 = new Area(0, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }


        public static Area GetAreaTestData6()
        {
            var (exits, stairs) = GetExitsAndStairsTestData6();

            Area area1 = new Area(1, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData7()
        {
            var (exits, stairs) = GetExitsAndStairsTestData7();

            Area area1 = new Area(1, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));

            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }

        public static Area GetAreaTestData8()
        {
            var (exits, stairs) = GetExitsAndStairsTestData8();

            Area area1 = new Area(1, "Area 1", false);

            foreach (var exit in exits)
            {
                area1.Relationships.ExitRelationships.Add(new Relationship<Area, Exit>(area1, exit));
            }

            foreach (var stair in stairs)
            {
                area1.Relationships.StairRelationships.Add(new Relationship<Area, Stair>(area1, stair));
            }
            return area1;
        }



    }
}
