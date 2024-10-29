using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.DomainEntities.Datastructs
{
    public class StairCapacityStruct
    {
        public Guid StairId { get; set; }
        public double stairCapacity { get; set; }

        public double stairCapacityPerFloor { get; set; }
        public string capacityNote { get; set; }
        
        public StairCapacityStruct() { }
    }
}
