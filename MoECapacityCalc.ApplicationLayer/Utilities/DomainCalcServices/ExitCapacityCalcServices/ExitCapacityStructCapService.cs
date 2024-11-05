using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices
{
    public interface IExitCapacityStructCapService
    {
        List<ExitCapacityStruct> GetLimitingFactorExitCapacityStructs(List<ExitCapacityStruct> exitCapacityStructs);
    }

    public class ExitCapacityStructCapService : IExitCapacityStructCapService
    {
        public ExitCapacityStructCapService() { }

        public List<ExitCapacityStruct> GetLimitingFactorExitCapacityStructs(List<ExitCapacityStruct> exitCapacityStructs)
        {

            return exitCapacityStructs.GroupBy(e => e.Id).Select(GetMinExitCapacityStructFromGroup()).ToList();
        }

        private static Func<IGrouping<Guid, ExitCapacityStruct>, ExitCapacityStruct> GetMinExitCapacityStructFromGroup()
        {
            return grouping => grouping.OrderBy(x => x.Capacity).First();
        }
    }
}
