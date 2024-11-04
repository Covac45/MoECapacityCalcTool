using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies
{
    public interface IEvacuationStrategy
    {
        double GetStairCapacityPerFloor(double stairCapacity, double upperFloorsServed);
    }
}
