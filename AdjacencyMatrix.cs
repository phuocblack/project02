using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PROJECT_02
{
    class AdjacencyMatrix
    {
        public int Size { get; set; }
        public int[,] Matrix { get; set; }

        public bool readAdjacencyMatrix(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("This file does not exist.");
                return false;
            }
            string[] lines = File.ReadAllLines(filename);
            this.Size = Int32.Parse(lines[0]);
            this.Matrix = new int[this.Size, this.Size];
            for (int i = 0; i < this.Size; ++i)
            {
                string[] tokens = lines[i + 1].Trim().Split(new char[]
               { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < this.Size; ++j)
                    this.Matrix[i, j] = Int32.Parse(tokens[j]);
            }
            return true;
        }

        public void showAdjacencyMatrix()
        {
            Console.WriteLine(this.Size);
            for (int i = 0; i < this.Size; ++i)
            {
                for (int j = 0; j < this.Size; ++j)
                {
                    Console.Write(this.Matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public AdjacencyList transformToAdjacencyList()
        {
            AdjacencyList adjacencyList = new AdjacencyList(this.Size);
            for (int i = 0; i < this.Size; ++i)
            {
                List<int> count = new List<int>();
                for (int j = 0; j < this.Size; ++j)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        count.Add(j);
                        //adjacencyList.AdjaList[i].Add(j);
                    }
                }
                adjacencyList.AdjaList[i].Add(count.Count);
                foreach (int v in count)
                {
                    adjacencyList.AdjaList[i].Add(v);
                }
            }
            return adjacencyList;
        }

        public List<Edge> getListEdges()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < this.Size; ++i)
            {
                for (int j = i; j < this.Size; ++j)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        Edge e = new Edge();
                        e.V = i;
                        e.W = j;
                        e.Weight = this.Matrix[i, j];
                        edges.Add(e);
                    }
                }
            }
            return edges;
        }
    }
}
