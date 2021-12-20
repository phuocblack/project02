using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_02
{
    class GraphUtils
    {
        private static int _edge = 0;
        private static int v_count = 0;
        public static bool isBridge(AdjacencyMatrix g, int start,
            int v)
        {
            int deg = 0;
            for (int i = 0; i < g.Size; ++i)
            {
                deg += g.Matrix[v, i];
            }
            if (deg > 1)
            {
                return false;
            }
            return true;
        }

        private static int DFS(AdjacencyMatrix g, 
            int prev, int start, ref bool[] visited)
        {
            int count = 1;
            visited[start] = true;
            for (int i = 0; i < g.Size; ++i)
            {
                if (prev != i)
                {
                    if (!visited[i])
                    {
                        if (g.Matrix[start, i] != 0)
                        {
                            count += DFS(g, start, i, ref visited);
                        }
                    }
                }
            }
            return count;
        }

        private static int edgeCount(AdjacencyMatrix g)
        {
            int count = 0;
            for (int i = 0; i < g.Size; ++i)
            {
                for (int j = i; j < g.Size; ++j)
                {
                    if (g.Matrix[i,j] != 0)
                    {
                        count += g.Matrix[i, j];
                    }
                }
            }
            return count;
        }

        public static void flueryAlgo(AdjacencyMatrix g, int start)
        {
            _edge = edgeCount(g);
            v_count = g.Size;
            for (int v = 0; v < g.Size; ++v)
            {
                if (g.Matrix[start, v] != 0)
                {
                    bool[] visited = Enumerable.Repeat(false, g.Size).ToArray();
                    if (isBridge(g, start, v))
                        v_count--;
                    int cnt = DFS(g, start, v, ref visited);
                    if (Math.Abs(v_count - cnt) <= 3)
                    {
                        Console.Write("{0}--{1} ", start, v);
                        if (isBridge(g, v, start))
                            v_count--;
                        g.Matrix[start, v] -= 1;
                        g.Matrix[v, start] -= 1;
                        _edge--;
                        flueryAlgo(g, v);
                    }
                }
            }
        }

        public static void FleuryAlgorithm(AdjacencyList al)
        {
            AdjacencyMatrix g = al.transformToAdjacencyMatrix();
            int[] deg = countDegrees(g);

            List<int> oddVertices = determineOddVertices(g, deg);
            if (oddVertices.Count == 0)
            {
                //apply Fleury algo normal and print out Euler circuit (chu trinh Euler)
                flueryAlgo(g, al.startPoint);
            }
            else if (oddVertices.Count == 2)
            {
                //check start vertice is one of 2 odd vertices (neu dinh bat dau la 1 trong 2 dinh bac le => ton tai duong di Euler)
                if (oddVertices.Contains(al.startPoint))
                {
                    //apply Fleury algo normal and print out Euler trail (duong di Euler)
                    flueryAlgo(g, al.startPoint);
                }
                else
                {
                    Console.WriteLine("Khong co loi giai.");
                }
            }
            else
            {
                Console.WriteLine("Khong co loi giai.");
            }
        }

        public static int[] countDegrees(AdjacencyMatrix g)
        {
            int[] degrees = new int[g.Size]; // Mang chua bac cua cac dinh
            for (int i = 0; i < g.Size; i++)
            {
                int count = 0;
                for (int j = 0; j < g.Size; j++)
                {
                    if (g.Matrix[i, j] != 0)
                    {
                        count += g.Matrix[i, j];
                        if (i == j) // xet truong hop canh khuyen
                            count += g.Matrix[i, i];
                    }
                    degrees[i] = count;
                }
            }

            return degrees;
        }

        public static List<int> determineOddVertices(AdjacencyMatrix g, int[] degrees)
        {
            List<int> count = new List<int>();
            for (int i = 0; i < g.Size; i++)
            {
                if (degrees[i] % 2 != 0)
                {
                    count.Add(i);
                }
            }
            return count;
        }

    }
}
