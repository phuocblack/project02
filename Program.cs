using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_02
{
    class Program
    {
        static void Main(string[] args)
        {
            AdjacencyList al = new AdjacencyList();
            al.readAdjacencyList("..//..//input-1.txt");
            //AdjacencyMatrix g = al.transformToAdjacencyMatrix();
            //g.showAdjacencyMatrix();

            GraphUtils.printEuler(al);
        }
    }
}
