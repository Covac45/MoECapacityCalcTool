using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Utilities.Services;
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
        public List<Exit> Exits;
        
        //Stairs and storey exits leading to them & final exits leading from them within the area.
        public List<Stair> Stairs;

        private List<Exit> StoreyExits => Exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
        private List<Exit> FinalExits => Exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
        private List<Exit> AltExits => Exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

        public Area(int floorLevel, List<Exit> exits, List<Stair> stairs)
        {
            Exits = exits;
            FloorLevel = floorLevel;
            Stairs = stairs;
        }

        public double CalcDiscountedExitCapacity()
        {
            List<double> exitCapacities = new List<double>();

            foreach (Exit anExit in StoreyExits)
            {
                exitCapacities.Add(new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }

            foreach (Exit anExit in FinalExits)
            {
                exitCapacities.Add(new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }
            
            foreach (Stair aStair in Stairs)
            {
                if(this.FloorLevel != aStair.FinalExitLevel)
                {
                    exitCapacities.Add(new StairExitCalcService(aStair).CalcStoreyExitLevelCapacity());
                }
                else if (this.FloorLevel == aStair.FinalExitLevel)
                {
                    exitCapacities.Add(new StairExitCalcService(aStair).CalcFinalExitLevelCapacity());
                }
            }


            //implements discounting logic for multiple exits (i.e. remove the most capacious exit)
            //Also implements capping logic based on number of storey exits (single exit: 60 people, two exits: 600 people)
            double discountedExitCapacity;

            int numExits = Exits.Count();

            switch (numExits)
            {
                case 1:
                    return CapExitCapacity(exitCapacities.Sum(), 60);
                case 2:
                    return CapExitCapacity(exitCapacities.Sum() - exitCapacities.Max(), 600);
                case > 2:
                    return exitCapacities.Sum() - exitCapacities.Max();
                default:
                    discountedExitCapacity = 0;
                    throw new Exception("The number of exits prodived to this area is less than 1. This is not supported");
            }
        }

        private double CapExitCapacity(double cap, double totalExitCapacity)
        {
            return Math.Min(totalExitCapacity, cap);
        }

    }
}
