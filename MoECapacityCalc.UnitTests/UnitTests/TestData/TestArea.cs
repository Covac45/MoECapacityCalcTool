using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.UnitTests.UnitTests.TestData
{
    public class TestArea : TestExitsAndStairs
    {
        public static Area GetAreaTestData1()
        {
            var (exits, stairs) = GetExitsAndStairsTestData1();

            Area area1 = new Area(0, "Area 1");

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

            Area area1 = new Area(0, "Area 1");

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
