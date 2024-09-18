using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Stairs.StairFinalExits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Stairs
{
    public class Stair : IStair
    {
        public double StairWidth;
        public int FloorsServed;
        public Exit FinalExit;

        public Stair(double width, int floorsServed, Exit finalExit)
        {
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExit = finalExit;
        }

        public double CalcStairCapacity()
        {
            double stairCapacity = 0;

            if (StairWidth >= 1100)
            {
                stairCapacity = (200 * (StairWidth / 1000)) + (50 * ((StairWidth / 1000) - 0.3) * (FloorsServed - 1));
            }
            else if (StairWidth >= 1000 && StairWidth < 1100)
            {
                stairCapacity = 150 + (FloorsServed - 1) * 40;
            }
            else if (StairWidth >= 800 && StairWidth < 1000)
            {
                stairCapacity = 50;
            }
            else
            {
                throw new NotSupportedException();
            }
            return stairCapacity;
        }

        public double CalcStairCapacityPerFloor()
        {
            double stairCapacityPerFloor = this.CalcStairCapacity() / FloorsServed;

            return stairCapacityPerFloor;
        }
    }
}
