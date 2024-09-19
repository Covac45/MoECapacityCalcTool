using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Stairs.StairFinalExits;
using System;
using System.Collections;
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
        public int FinalExitLevel;
        public Exit FinalExit;
        public Exit StoreyExit;

        public Stair(double width, int floorsServed, int finalExitLevel, Exit finalExit, Exit storeyExit = null)
        {
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExitLevel = finalExitLevel;
            FinalExit = finalExit;
            StoreyExit = storeyExit;
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
            StairFinalExit stairFinalExit = new StairFinalExit(this);

            double mergingFlowCapacity = stairFinalExit.CalcMergingFlowCapacity();

            double storeyExitCapacity = StoreyExit.CalcExitCapacity();

            double finalExitCapacity = this.FinalExit.CalcExitCapacity();

            var capacities = new List<double> {mergingFlowCapacity, storeyExitCapacity, finalExitCapacity};

            return capacities.Min();
        }

        public double CalcStoreyExitLevelCapacity()
        {
            double stairCapacityPerFloor = this.CalcStairCapacityPerFloor();

            double storeyExitCapacity = StoreyExit.CalcExitCapacity();

            var capacities = new List<double> {stairCapacityPerFloor, storeyExitCapacity};

            return capacities.Min();
        }

    }
}
