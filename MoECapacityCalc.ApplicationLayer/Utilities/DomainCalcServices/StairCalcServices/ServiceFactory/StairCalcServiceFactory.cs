using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies;

namespace MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.ServiceFactory
{

    public class StairCalcServiceFactory
    {
        private IStairFinalExitWidthStrategy _AreaStairFinalExitWidthCalcStrategy;
        private IStairFinalExitCapacityStrategy _AreaStairFinalExitCapacityCalcStrategy;

        private IStairFinalExitWidthStrategy _SingleStairFinalExitWidthCalcStrategy;
        private IStairFinalExitCapacityStrategy _SingleStairFinalExitCapacityCalcStrategy;


        public StairCalcServiceFactory()
        {

        }

        public IStairCapacityCalcService Create(Area area = null)
        {
            if (area == null)
            {
                _SingleStairFinalExitWidthCalcStrategy = new SingleStairFinalExitWidthStrategy();
                _SingleStairFinalExitCapacityCalcStrategy = new SingleStairFinalExitCapacityStrategy();
                return new StairCapacityCalcService(_SingleStairFinalExitWidthCalcStrategy, _SingleStairFinalExitCapacityCalcStrategy);
            }
            
            _AreaStairFinalExitWidthCalcStrategy = new AreaStairFinalExitWidthStrategy(area);
            _AreaStairFinalExitCapacityCalcStrategy = new AreaStairFinalExitCapacityStrategy(area);

            return new StairCapacityCalcService(_AreaStairFinalExitWidthCalcStrategy, _AreaStairFinalExitCapacityCalcStrategy);
        }

    }
}
