using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Stairs.StairFinalExits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Areas
{
    public class Area
    {
        public List<Exit> Exits;
        public List<Stair> Stairs;

        public Area(List<Exit> exits, List<Stair> stairs)
        {
            Exits = exits;
            Stairs = stairs;
        }

        public double CalcDiscountedExitCapacity()
        {
            List<double> exitCapacities = new List<double>();

            foreach (Exit anExit in Exits)
            {
                exitCapacities.Add(anExit.CalcExitCapacity());
            }

            double discountedExitCapacity = exitCapacities.Sum() - exitCapacities.Max();
            return discountedExitCapacity;
        }
    }
}
