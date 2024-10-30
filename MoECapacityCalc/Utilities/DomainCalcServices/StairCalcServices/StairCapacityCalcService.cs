using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices
{
    public class StairCapacityCalcService : IStairCapacityCalcService
    {

        private readonly IStairFinalExitWidthStrategy _stairFinalExitWidthStrategy;
        private readonly IStairFinalExitCapacityStrategy _stairFinalExitCapacityStrategy;

        public StairCapacityCalcService(IStairFinalExitWidthStrategy stairFinalExitWidthStrategy,
            IStairFinalExitCapacityStrategy stairFinalExitCapacityStrategy)
        {
            _stairFinalExitWidthStrategy = stairFinalExitWidthStrategy;
            _stairFinalExitCapacityStrategy = stairFinalExitCapacityStrategy;
        }

        public StairCapacityStruct GetStairCapacityStruct(Stair stair, Area area = null)
        {
            double stairCapacity = CalcStairCapacity(stair, area);
            double stairCapacityPerFloor = stairCapacity / stair.FloorsServedPerEvacuationPhase;

            return new StairCapacityStruct
            {
                StairId = stair.Id,
                stairCapacity = stairCapacity,
                stairCapacityPerFloor = stairCapacityPerFloor,
                capacityNote = "The stair capacity is limited by the clear width of the stairs"
            };
        }

        public double CalcStairCapacity(Stair stair, Area area = null)
        {
            double effectiveStairWidth = GetEffectiveStairWidth(stair, area);
            double stairCapacity = CalcEffectiveStairCapacity(stair, effectiveStairWidth);
            return UpdateEffectiveStairCapacityWithDoorsSwingingAgainst(stair, stairCapacity, area);
        }

        protected double GetEffectiveStairWidth(Stair stair, Area area)
        {
            double effectiveFinalExitWidth = GetEffectiveFinalExitWidthForDoorsSwingingWithEscape(stair, area);
            return stair.StairWidth > effectiveFinalExitWidth ? effectiveFinalExitWidth : stair.StairWidth;
        }

        protected double GetEffectiveFinalExitWidthForDoorsSwingingWithEscape(Stair stair, Area area = null)
        {
            var finalExitsServingStair = stair.Relationships.GetExits()
                .Where(e => e.ExitType == ExitType.finalExit && e.DoorSwing == DoorSwing.with).ToList();

            return _stairFinalExitWidthStrategy.GetEffectiveStairFinalExitWidth(finalExitsServingStair);
        }

        private double CalcEffectiveStairCapacity(Stair stair, double effectiveStairWidth)
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

        protected virtual double UpdateEffectiveStairCapacityWithDoorsSwingingAgainst(Stair stair, double stairCapacity, Area area)
        {
            double uninhibitedStairCapacity = CalcEffectiveStairCapacity(stair, stair.StairWidth);
            if (stairCapacity < uninhibitedStairCapacity)
            {
                stairCapacity += GetEffectiveCapacityOfFinalExitDoorsSwingingAgainstEscape(stair, area);
                stairCapacity = Math.Min(stairCapacity, uninhibitedStairCapacity);
            }
            return stairCapacity;
        }

        protected double GetEffectiveCapacityOfFinalExitDoorsSwingingAgainstEscape(Stair stair, Area area = null)
        {
            var finalExitsServingStair = stair.Relationships.GetExits()
                .Where(e => e.ExitType == ExitType.finalExit && e.DoorSwing == DoorSwing.against).ToList();

            return _stairFinalExitCapacityStrategy.GetEffectiveStairFinalExitCapacity(finalExitsServingStair);
        }
    }
}
