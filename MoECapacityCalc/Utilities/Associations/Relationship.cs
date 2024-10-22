using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Relationship<T1,T2>
    {
        public T1 Object1 { get; }
        public T2 Object2 { get; }

        public Relationship() { }

        public Relationship(T1 object1, T2 object2) 
        {
            Object1 = object1;
            Object2 = object2;
        }
    }
}
