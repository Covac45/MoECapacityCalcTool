using MoECapacityCalc.Exits.Datastructs;
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
        public int UpperfloorsServed;

        public Stair(double width, int floorsServed)
        {
            StairWidth = width;
            UpperfloorsServed = floorsServed;
        }

        public double CalcStairCapacity()
        {
            double stairCapacity = 0;

            if (StairWidth >= 1100)
            {
                stairCapacity = (200 * (StairWidth / 1000)) + (50 * ((StairWidth / 1000) - 0.3) * (UpperfloorsServed - 1));
            }
            else if (StairWidth >= 1000 && StairWidth < 1100)
            {
                stairCapacity = 150 + (UpperfloorsServed - 1) * 40;
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

    }
}
