namespace MoECapacityCalc
{
    public class Menu
    {
        public void GetMenu()
        {
            Console.WriteLine("Welcome to the means of escape capacity calculator.");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Calculate horizontal means of escape capacity");
            string menuSelection = Console.ReadLine();

            switch (menuSelection)
            {
                case "1":
                    Console.WriteLine("You have selected horizontal means of escape capacity");
                    break;
                default:
                    throw new NotSupportedException("The selected option is not supported");
            }
        }
    }
        
}
