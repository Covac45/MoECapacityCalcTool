using MoECapacityCalc.DomainEntities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.CalcServices
{
    public class ExitCapacityStructCapService
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
