using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrostBlade;
using FrostBlade.Algorithms;

namespace Hypercube
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var cube = new Cube();
                cube.Apply(Move.R2);
                cube.Apply(Move.U2);
                cube.Apply(Move.D);
                cube.Apply(Move.R);
                //cube.Apply(Move.UPrime);
                cube.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
