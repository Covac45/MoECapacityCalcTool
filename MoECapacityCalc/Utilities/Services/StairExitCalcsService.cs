using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class StairExitCalcsService
    {
        List<Exit>? StoreyExits;
        List<Exit>? FinalExits;
        public StairExitCalcsService(List<Exit>? storeyExits = null, List<Exit>? finalExits = null)
        {
            StoreyExits = storeyExits;
            FinalExits = finalExits;
        }

        public double TotalStoreyExitCapacity()
        {
            List<double> storeyExitCapacities = new List<double>();
            foreach (Exit anExit in StoreyExits)
            {
                storeyExitCapacities.Add(
                    new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }

            double storeyExitCapacity = storeyExitCapacities.Sum();
            return storeyExitCapacity;
        }

        public double TotalFinalExitCapacity()
        {
            List<double> finalExitCapacities = new List<double>();
            foreach (Exit anExit in FinalExits)
            {
                finalExitCapacities.Add(
                    new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }

            double finalExitCapacity = finalExitCapacities.Sum();
            return finalExitCapacity;
        }


    }
}
