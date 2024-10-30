using MoECapacityCalc.DomainEntities.Datastructs;

namespace MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices
{
    public interface IExitCapacityStructCapService
    {
        List<ExitCapacityStruct> GetCappedExitCapacityStructs(List<ExitCapacityStruct> exitCapacityStructs);
    }

    public class ExitCapacityStructCapService : IExitCapacityStructCapService
    {
        public ExitCapacityStructCapService() { }

        public List<ExitCapacityStruct> GetCappedExitCapacityStructs(List<ExitCapacityStruct> exitCapacityStructs)
        {

            return exitCapacityStructs.GroupBy(e => e.ExitId).Select(GetMinExitCapacityStructFromGroup()).ToList();
        }

        private static Func<IGrouping<Guid, ExitCapacityStruct>, ExitCapacityStruct> GetMinExitCapacityStructFromGroup()
        {
            return grouping => grouping.OrderBy(x => x.exitCapacity).First();
        }
    }
}
