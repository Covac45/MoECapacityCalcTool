using MoECapacityCalc.DomainEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class StairCapacityCalcService
    {

        public StairCapacityCalcService()
        {
        }

        public double CalcStairCapacity(Stair stair)
        {
            double stairCapacity = 0;

            if (stair.StairWidth >= 1100)
            {
                stairCapacity = (200 * (stair.StairWidth / 1000)) + (50 * ((stair.StairWidth / 1000) - 0.3) * (stair.FloorsServed - 1));
            }
            else if (stair.StairWidth >= 1000 && stair.StairWidth < 1100)
            {
                stairCapacity = 150 + (stair.FloorsServed - 1) * 40;
            }
            else if (stair.StairWidth >= 800 && stair.StairWidth < 1000)
            {
                stairCapacity = 50;
            }
            else
            {
                throw new NotSupportedException();
            }
            return stairCapacity;
        }

        public double CalcStairCapacityPerFloor(Stair stair)
        {
            double stairCapacityPerFloor = CalcStairCapacity(stair) / stair.FloorsServed;
            return stairCapacityPerFloor;
        }

    }
}
