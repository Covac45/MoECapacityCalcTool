using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.CalcServices
{
    /*public class AreaCalcService
    {

        private Area Area;

        private List<Stair> Stairs = new List<Stair>();

        public List<Exit> Exits;

        private List<Exit> StoreyExits = new List<Exit>();

        private List<Exit> FinalExits = new List<Exit>();

        private List<Exit> AltExits = new List<Exit>();

        public AreaCalcService(Area area)
        {
            Area = area;
            Stairs = Area.Relationships.GetStairs();

            Exits = Area.Relationships.GetExits();

            StoreyExits = Exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            FinalExits = Exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
            AltExits = Exits.Where(exit => exit.ExitType == ExitType.exit).ToList();
        }

        public double CalcDiscountedExitCapacity()
        {
            List<double> exitCapacities = new List<double>();

            foreach (Exit anExit in StoreyExits)
            {
                exitCapacities.Add(new ExitCapacityCalcService().CalcExitCapacity(anExit).exitCapacity);
            }

            foreach (Exit anExit in FinalExits)
            {
                exitCapacities.Add(new ExitCapacityCalcService().CalcExitCapacity(anExit).exitCapacity);
            }

            foreach (Stair aStair in Stairs)
            {
                if (Area.FloorLevel != aStair.FinalExitLevel)
                {
                    exitCapacities.Add(new StairExitCalcService().CalcStoreyExitLevelCapacity(aStair));
                }
                else if (Area.FloorLevel == aStair.FinalExitLevel)
                {
                    exitCapacities.Add(new StairExitCalcService().CalcFinalExitLevelCapacity(aStair));
                }
            }


            //implements discounting logic for multiple exits (i.e. remove the most capacious exit)
            //Also implements capping logic based on number of storey exits (single exit: 60 people, two exits: 600 people)
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
                    return 0;
                    throw new Exception("The number of exits prodived to this area is less than 1. This is not supported");
            }
        }

        private double CapExitCapacity(double cap, double totalExitCapacity)
        {
            return Math.Min(totalExitCapacity, cap);
        }

    }*/
}
