using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public class HmoeCapacityStruct
    {
        public Guid AreaId { get; set; }
        public double HmoeCapacity { get; set; }
        public string HmoeCapacityNote { get; set; }
        public HmoeCapacityStruct() { }

    }
}
