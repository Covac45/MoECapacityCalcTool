//Means of Escape Capacity Calculator (based on ADB 2022)

using MoECapacityCalc;
using MoECapacityCalc.Database.Context;

using MoEContext context = new();
{
    context.Database.EnsureCreated();

}

//Menu menu = new Menu();
//menu.GetMenu();

//var exit1 = new Exit(ExitInfo.GetExitInfo().Item1, ExitInfo.GetExitInfo().Item2, ExitInfo.GetExitInfo().Item3);


//Console.WriteLine($"The storey exit capacity is: {exit1.CalcExitCapacity()}");