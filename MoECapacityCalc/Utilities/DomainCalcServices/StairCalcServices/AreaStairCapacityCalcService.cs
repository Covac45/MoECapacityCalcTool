using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Services;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices
{
    public class AreaStairCapacityCalcService : IStairCapacityCalcService
    {

        public AreaStairCapacityCalcService()
        {
        }

        public StairCapacityStruct GetStairCapacityStruct(Area area, Stair stair)
        {

            var stairCapacity = CalcStairCapacity(area, stair);

            double stairCapacityPerFloor = CalcStairCapacity(area, stair) / stair.FloorsServedPerEvacuationPhase;

            StairCapacityStruct stairCapacityStruct = new StairCapacityStruct()
            {
                StairId = stair.Id,
                stairCapacity = stairCapacity,
                stairCapacityPerFloor = stairCapacityPerFloor,
                capacityNote = "The stair capacity limited by clear width of the stairs"
            };

            return stairCapacityStruct;
        }

        public double CalcStairCapacity(Area area, Stair stair)
        {
            //If the width of the stair is greater than the width of final exits serving the stair, set the effective width of the stair equal to the width of the final exits serving the stair.
            double effectiveStairWidth = GetEffectiveStairWidth(area, stair);

            //Calculate the capacity of the stair
            double stairCapacity = CalcEffectiveStairCapacity(stair, effectiveStairWidth);
            stairCapacity = UpdateEffectiveStairCapacityWithDoorsSwingingAgainst(area, stair, stairCapacity);

            return stairCapacity;
        }



        private double GetEffectiveStairWidth(Area area, Stair stair)
        {
            var effectiveFinalExitWidth = GetEffectiveFinalExitWidthForDoorsSwingingWithEscape(area, stair);

            double effectiveStairWidth = 0;

            if (stair.StairWidth > effectiveFinalExitWidth)
            {
                effectiveStairWidth = effectiveFinalExitWidth;
            }
            else
            {
                effectiveStairWidth = stair.StairWidth;
            }

            return effectiveStairWidth;
        }

        private static double CalcEffectiveStairCapacity(Stair stair, double effectiveStairWidth)
        {
            double stairCapacity = 0;

            if (effectiveStairWidth >= 1100)
            {
                stairCapacity = 200 * (effectiveStairWidth / 1000) + 50 * (effectiveStairWidth / 1000 - 0.3) * (stair.FloorsServedPerEvacuationPhase - 1);
            }
            else if (effectiveStairWidth >= 1000 && effectiveStairWidth < 1100)
            {
                stairCapacity = 150 + (stair.FloorsServedPerEvacuationPhase - 1) * 40;
            }
            else if (effectiveStairWidth >= 800 && effectiveStairWidth < 1000)
            {
                stairCapacity = 50;
            }
            else
            {
                stairCapacity = 0;
            }

            return stairCapacity;
        }

        private double GetEffectiveFinalExitWidthForDoorsSwingingWithEscape(Area area, Stair stair)
        {
            var stairs = area.Relationships.GetStairs();

            var finalExitsServingStair = stair.Relationships.GetExits()
                                                                .Where(e => e.ExitType == ExitType.finalExit)
                                                                .Where(e => e.DoorSwing == DoorSwing.with)
                                                                .ToList();
            List<double> effectiveFinalExitWidths = new List<double>();

            foreach (var finalExit in finalExitsServingStair)
            {
                var numStairsServed = stairs.Select(s => s.Relationships.GetExits()
                                                    .SingleOrDefault(e => e.Id == finalExit.Id))
                                                    .Where(e => e != null)
                                                    .ToList()
                                                    .Count();

                var effectiveFinalExitWidth = finalExit.ExitWidth / numStairsServed;
                effectiveFinalExitWidths.Add(effectiveFinalExitWidth);
            }
            return effectiveFinalExitWidths.Sum();
        }

        private double GetEffectiveCapacityOfFinalExitDoorsSwingingAgainstEscape(Area area, Stair stair)
        {
            var stairs = area.Relationships.GetStairs();

            var finalExitsServingStair = stair.Relationships.GetExits()
                                                                .Where(e => e.ExitType == ExitType.finalExit)
                                                                .Where(e => e.DoorSwing == DoorSwing.against)
                                                                .ToList();
            List<double> effectiveFinalExitCapacities = new List<double>();

            foreach (var finalExit in finalExitsServingStair)
            {
                var numStairsServed = stairs.Select(s => s.Relationships.GetExits()
                                                    .SingleOrDefault(e => e.Id == finalExit.Id))
                                                    .Where(e => e != null)
                                                    .ToList()
                                                    .Count();

                var effectiveFinalExitCapacity = new ExitCapacityCalcService().CalcExitCapacity(finalExit).exitCapacity / numStairsServed;
                effectiveFinalExitCapacities.Add(effectiveFinalExitCapacity);
            }
            return effectiveFinalExitCapacities.Sum();
        }




        private double UpdateEffectiveStairCapacityWithDoorsSwingingAgainst(Area area, Stair stair, double stairCapacity)
        {
            double uninhibitedStairCapacity = CalcEffectiveStairCapacity(stair, stair.StairWidth);

            //If the stair has finalexits which swing against the direction of escape, add their capacity to the capacity of the stair, up to the uninhibited max of the stair.
            if (stairCapacity < uninhibitedStairCapacity)
            {
                stairCapacity += GetEffectiveCapacityOfFinalExitDoorsSwingingAgainstEscape(area, stair);

                if (stairCapacity > uninhibitedStairCapacity)
                {
                    stairCapacity = uninhibitedStairCapacity;
                }
            }

            return stairCapacity;
        }

    }
}
