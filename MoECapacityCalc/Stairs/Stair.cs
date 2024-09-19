using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Stairs.StairFinalExits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Services;

namespace MoECapacityCalc.Stairs
{
    public class Stair : IStair
    {
        public string StairName;
        public double StairWidth;
        public int FloorsServed;
        public int FinalExitLevel;
        public List<Exit> FinalExits;
        public List<Exit> StoreyExits;

        public Stair(string name, double width, int floorsServed, int finalExitLevel, List<Exit> finalExits, List<Exit> storeyExits = null)
        {
            StairName = name;
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExitLevel = finalExitLevel;
            FinalExits = finalExits;
            StoreyExits = storeyExits;
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

        public double CalcFinalExitLevelCapacity()
        {
            //Calculate total storey exit and final exit capacity
            StairExitCalcsService exitCapacityCalcs = new StairExitCalcsService(this.StoreyExits, this.FinalExits);
            double storeyExitCapacity = exitCapacityCalcs.TotalStoreyExitCapacity();
            double finalExitCapacity = exitCapacityCalcs.TotalFinalExitCapacity();

            //Calculate merging flow capacity
            StairFinalExit stairFinalExit = new StairFinalExit(this);
            double mergingFlowCapacity = stairFinalExit.CalcMergingFlowCapacity();

            //calculate limiting factor
            var capacities = new List<double> {mergingFlowCapacity, storeyExitCapacity, finalExitCapacity};

            return capacities.Min();
        }

        public double CalcStoreyExitLevelCapacity()
        {
            double stairCapacityPerFloor = this.CalcStairCapacityPerFloor();

            //Calculate total storey exit capacity
            StairExitCalcsService exitCapacityCalcs = new StairExitCalcsService(this.StoreyExits, this.FinalExits);
            double storeyExitCapacity = exitCapacityCalcs.TotalStoreyExitCapacity();

            var capacities = new List<double> {stairCapacityPerFloor, storeyExitCapacity};

            return capacities.Min();
        }

    }
}
