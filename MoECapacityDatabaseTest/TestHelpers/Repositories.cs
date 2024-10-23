using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityDatabaseTest.TestHelpers
{
    public class Repositories
    {
        public ExitsRepository ExitsRepository { get; set; }
        public StairsRepository StairsRepository { get; set; }
        public AreasRepository AreasRepository { get; set; }
    }
}
