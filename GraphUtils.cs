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
                    if (Math.Abs(v_count - cnt) <= 2)
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

    }
}
