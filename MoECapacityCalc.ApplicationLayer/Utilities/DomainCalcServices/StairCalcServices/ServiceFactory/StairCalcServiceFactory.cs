using MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.ServiceFactory
{

    public class StairCalcServiceFactory
    {
        private IStairFinalExitWidthStrategy AreaStairFinalExitWidthCalcStrategy;
        private IStairFinalExitCapacityStrategy AreaStairFinalExitCapacityCalcStrategy;

        private IStairFinalExitWidthStrategy SingleStairFinalExitWidthCalcStrategy;
        private IStairFinalExitCapacityStrategy SingleStairFinalExitCapacityCalcStrategy;

        private IEvacuationStrategy EvacuationStrategy;


        public StairCalcServiceFactory()
        {

        }

        public IStairCapacityCalcService Create(Area area = null)
        {
            if (area == null)
            {
                SingleStairFinalExitWidthCalcStrategy = new SingleStairFinalExitWidthStrategy();
                SingleStairFinalExitCapacityCalcStrategy = new SingleStairFinalExitCapacityStrategy();
                EvacuationStrategy = new SimultaneousEvacuationStrategy();
                return new StairCapacityCalcService(SingleStairFinalExitWidthCalcStrategy, SingleStairFinalExitCapacityCalcStrategy, EvacuationStrategy);
            }
            
            AreaStairFinalExitWidthCalcStrategy = new AreaStairFinalExitWidthStrategy(area);
            AreaStairFinalExitCapacityCalcStrategy = new AreaStairFinalExitCapacityStrategy(area);
            EvacuationStrategy = new SimultaneousEvacuationStrategy();

            return new StairCapacityCalcService(AreaStairFinalExitWidthCalcStrategy, AreaStairFinalExitCapacityCalcStrategy, EvacuationStrategy);
        }

    }
}
