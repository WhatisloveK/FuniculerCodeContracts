#define CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funicular.NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            IFunicular funicular = new Funicular(4);

            funicular.OpenTheDoor();
            funicular.BoardPassengers(2);
            funicular.CloseTheDoor();
            funicular.MoveUpwards();
            funicular.StopMoving();
            funicular.OpenTheDoor();
            funicular.BoardPassengers(-1);
            funicular.BoardPassengers(3);
        }
    }
}
