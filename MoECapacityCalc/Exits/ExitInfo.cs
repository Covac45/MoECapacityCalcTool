using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Exits.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class ExitInfo
    {
        public (ExitType, double, DoorSwing) GetExitInfo()
        {
            ExitType exitType;
            double exitWidth;
            DoorSwing doorSwing;


            Console.WriteLine("What type of exit is the door?");
            string type = Console.ReadLine().ToLower();

            switch (type)
            {
                case "exit":
                    exitType = (ExitType)Enum.Parse(typeof(ExitType), type);
                    break;
                case "storey exit":
                    exitType = (ExitType)Enum.Parse(typeof(ExitType), type); break;
                case "final exit":
                    exitType = (ExitType)Enum.Parse(typeof(ExitType), type); break;
                default:
                    throw new NotSupportedException("This type of exit type is not supported");
            }

            Console.WriteLine("What is the storey exit width in mm?: ");
            exitWidth = int.Parse(Console.ReadLine());

            Console.WriteLine("Does the door swing with or against the direction of escape? ");
            string swing = Console.ReadLine().ToLower();

            switch (swing)
            {
                case "with":
                    doorSwing = (DoorSwing)Enum.Parse(typeof(DoorSwing), swing);
                    break;
                case "against":
                    doorSwing = (DoorSwing)Enum.Parse(typeof(DoorSwing), swing);
                    break;
                default:
                    throw new NotSupportedException("This type of door swing is not supported");
            }

            return (exitType, exitWidth, doorSwing);
        }
    }
}
