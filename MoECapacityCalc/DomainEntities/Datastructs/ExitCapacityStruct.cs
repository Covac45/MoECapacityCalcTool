using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.DomainEntities.Datastructs
{
    public class ExitCapacityStruct
    {
        public Guid ExitId { get; set; }
        public double exitCapacity { get; set; }
        public string capacityNote { get; set; }
        public ExitCapacityStruct() {}
    }
}
