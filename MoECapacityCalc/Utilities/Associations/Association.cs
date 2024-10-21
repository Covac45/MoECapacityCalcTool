using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Association<T1,T2>
    {
        public T1 Object1 { get; }
        public T2 Object2 { get; }

        public Association() { }

        public Association(T1 object1, T2 object2) 
        {
            Object1 = object1;
            Object2 = object2;
        }
    }

    internal class ClientCode
    {
        private void TestMethod()
        {
            var stair = new Stair();
            var exit = new Exit();
            var relationship = new Association<Stair, Exit>(stair, exit);
        }
    }
}
