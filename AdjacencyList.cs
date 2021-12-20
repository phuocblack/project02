using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PROJECT_02
{
    class AdjacencyList
    {
        public int Size { get; set; }

        public int startPoint { get; set; }

        public List<List<int>> AdjaList { get; set; }

        public AdjacencyList()
        {
            this.AdjaList = new List<List<int>>();
        }

        public AdjacencyList(int size)
        {
            this.AdjaList = new List<List<int>>();
            this.Size = size;
            for (int i = 0; i < this.Size; ++i)
            {
                this.AdjaList.Add(new List<int>());
            }
        }

        public bool readAdjacencyList(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("This file does not exist.");
                return false;
            }
            string[] lines = File.ReadAllLines(filename);
            this.Size = Int32.Parse(lines[0]);
            for (int i = 0; i < this.Size; ++i)
            {
                this.AdjaList.Add(new List<int>());
            }
            this.startPoint = Int32.Parse(lines[1]);
            this.AdjaList = new List<List<int>>();
            for (int i = 0; i < this.Size; ++i)
            {
                string[] tokens = lines[i + 2].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> listVertices = new List<int>();
                //add number of vertices
                listVertices.Add(Int32.Parse(tokens[0]));
                for (int j = 0; j < Int32.Parse(tokens[0]); ++j)
                {
                    listVertices.Add(Int32.Parse(tokens[j + 1]));
                }
                this.AdjaList.Add(listVertices);

            }
            return true;
        }

        public void showAdjacencyList()
        {
            Console.WriteLine(this.Size);
            for (int i = 0; i < this.Size; ++i)
            {
                for (int j = 0; j < this.AdjaList[i].Count(); ++j)
                {
                    Console.Write(this.AdjaList[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public AdjacencyMatrix transformToAdjacencyMatrix()
        {
            AdjacencyMatrix result = new AdjacencyMatrix();
            result.Size = this.Size;
            result.Matrix = new int[this.Size, this.Size];
            for (int i = 0; i < this.Size; i++)
            {
                int numberOfVertice = this.AdjaList[i][0];
                //int startIdx = 1;
                for (int j = 0; j < this.Size; j++)
                {
                    if (numberOfVertice > 0)
                    {
                        if (this.AdjaList[i].IndexOf(j, 1) >= 1)
                        {
                            result.Matrix[i, j] = 1;
                        }
                    }
                    else
                    {
                        result.Matrix[i, j] = 0;
                    }

                }
            }
            return result;
        }
    }
}
