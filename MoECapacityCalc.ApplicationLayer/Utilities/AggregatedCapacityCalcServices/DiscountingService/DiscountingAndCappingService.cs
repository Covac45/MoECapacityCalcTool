using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.DiscountingService
{
    public interface IDiscountingAndCappingService
    {
        public CapacityStruct GetTotalDiscountedMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
    }

    public class DiscountingAndCappingService : IDiscountingAndCappingService
    {

        public CapacityStruct GetTotalDiscountedMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            int numExitsFromArea = GetNumberOfEscapeRoutesFromArea(area);

            double sum = exitCapacityStructs.Select(e => e.Capacity).Sum();
            double max = exitCapacityStructs.Select(e => e.Capacity).DefaultIfEmpty().Max();

            return GetCappedExitCapacityStruct(area, numExitsFromArea, sum, max);
        }

        private static int GetNumberOfEscapeRoutesFromArea(Area area)
        {
            var numStairsServingArea = area.Relationships.GetStairs()
                                                        .Select(s => s)
                                                        .Where(s => s.Relationships.GetFromExits().Where(e => e.ExitType == ExitType.storeyExit) != null)
                                                        .Count();

            var numNonStairExitsServingArea = area.Relationships.GetToExits().Count();

            int numEscapeRoutesFromArea = numNonStairExitsServingArea + numStairsServingArea;
            return numEscapeRoutesFromArea;
        }

        private CapacityStruct GetCappedExitCapacityStruct(Area area, int numExitsFromArea, double sum, double max)
        {
            var cap = 0;
            var hmoeCapacityNote = "";
            switch (numExitsFromArea)
            {
                case 1:
                    cap = 60;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 60 as only a single escape route is provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case 2:
                    cap = 600;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 600 as only two escape routes are provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case > 2:

                    hmoeCapacityNote = "The means of escape capacity of this area is limited by the the capacity of escape routes. See escape route capacity assessment for further information.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sum - max,
                        CapacityNote = hmoeCapacityNote
                    };
                default:
                    hmoeCapacityNote = "No escape routes have been provided for the area.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = 0,
                        CapacityNote = hmoeCapacityNote
                    };

            }

        }

        public static double CapExitCapacity(double totalExitCapacity, double cap)
        {
            return Math.Min(totalExitCapacity, cap);
        }

    }
}
