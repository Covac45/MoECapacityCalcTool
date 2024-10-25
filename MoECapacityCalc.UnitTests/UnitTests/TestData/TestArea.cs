using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.UnitTests.UnitTests.TestData
{
    public class TestArea : TestExitsAndStairs
    {
        public static Area GetAreaTestData()
        {
            var (exits, stairs) = GetExitsAndStairsTestData();

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
