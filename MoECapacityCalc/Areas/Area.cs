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
        public int FloorLevel;
        //Exits not associated with stairs within the area
        public List<Exit> StoreyExits;
        public List<Exit> FinalExits;

        //Stairs and storey exits leading to them & final exits leading from them within the area.
        public List<Stair> Stairs;

        public Area(int floorLevel, List<Exit> storeyExits, List<Exit> finalExits, List<Stair> stairs)
        {
            FloorLevel = floorLevel;
            StoreyExits = storeyExits;
            FinalExits = finalExits;
            Stairs = stairs;
        }

        public double CalcDiscountedExitCapacity()
        {
            List<double> exitCapacities = new List<double>();

            foreach (Exit anExit in StoreyExits)
            {
                exitCapacities.Add(anExit.CalcExitCapacity());
            }

            foreach (Exit anExit in FinalExits)
            {
                exitCapacities.Add(anExit.CalcExitCapacity());
            }
            
            foreach (Stair aStair in Stairs)
            {
                if(this.FloorLevel != aStair.FinalExitLevel)
                {
                    exitCapacities.Add(aStair.CalcStoreyExitLevelCapacity());
                }
                else if (this.FloorLevel == aStair.FinalExitLevel)
                {
                    exitCapacities.Add(aStair.CalcFinalExitLevelCapacity());
                }
            }


            double discountedExitCapacity = exitCapacities.Sum() - exitCapacities.Max();
            return discountedExitCapacity;
        }
    }
}
