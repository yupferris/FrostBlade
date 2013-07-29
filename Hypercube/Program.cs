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
                var database = new Database("Database");
                var alg = database.Algorithms.Where(x => x.Name == "Ja").FirstOrDefault();
                var cube = new Cube();
                cube.Apply(alg);
                cube.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
