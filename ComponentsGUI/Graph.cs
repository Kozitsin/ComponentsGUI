using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsGUI
{
    class Graph
    {
        // sizes for graph
        private int numOfVertex;
        private int numOfEdges;
        Random rand = new Random();

        // vertex <-- list with tuple of 2 elements
        private List<Tuple<int, int>> vertex;

        // check if array steel keep the same edge or it is loop
        private bool Exist(int x, int y)
        {
            if (x == y)
                return true;
            for (int i = 0; i < vertex.Count; ++i)
                if ((vertex[i].Item1 == x && vertex[i].Item2 == y) || (vertex[i].Item1 == y && vertex[i].Item2 == x))
                    return true;
            return false;
        }
        // utility function: generate random Vertex
        private int _GenerateRandomVertex()
        {
            int x = rand.Next() % numOfVertex;
            return x;
        }

        // utility function: add edge
        private void _AddEdge(int x, int y)
        {
            vertex.Add(Tuple.Create<int, int>(x, y));
            numOfEdges = vertex.Count();
        }

        // class constructor. generates random graph
        public Graph(int _numOfVertex, int _numOfEdges)
        {
            numOfVertex = _numOfVertex;
            numOfEdges = _numOfEdges;
            vertex = new List<Tuple<int, int>>();
            AddEdges(numOfEdges);
        }
        // return number of edges
        public int GetNumOfEdges()
        {
            return vertex.Count;
        }
        // expand number of vertecies
        public void AddVertex(int n)
        {
            numOfVertex += n;
        }

        // add range of edges in random way
        public void AddEdges(int n)
        {
            int x = default(int);
            int y = default(int);

            for (int i = 0; i < n; ++i)
            {
                x = _GenerateRandomVertex();
                y = _GenerateRandomVertex();

                while (Exist(x, y))
                {
                    x = _GenerateRandomVertex();
                    y = _GenerateRandomVertex();
                }
                _AddEdge(x, y);
            }
        }

        // implementation of naive approach
        public int[] FindComponents_Naive()
        {
            int q = default(int);
            int[] component = new int[numOfVertex];

            for (int i = 0; i < numOfVertex; ++i)
                component[i] = i;

            for (int i = 0; i < numOfVertex; ++i)
            {
                for (int j = 0; j < numOfEdges; ++j)
                {
                    q = Math.Min(component[vertex[j].Item1], component[vertex[j].Item2]);
                    component[vertex[j].Item1] = q;
                    component[vertex[j].Item2] = q;
                }
            }
            return component;
        }

        // implementation of Ram approach
        public int[] FindComponents_Ram()
        {
            int x = default(int);
            int y = default(int);

            Disjoint_Set obj = new Disjoint_Set(numOfVertex);

            for (int i = 0; i < numOfEdges; ++i)
            {
                x = obj.Find(vertex[i].Item1);
                y = obj.Find(vertex[i].Item2);

                if (x != y)
                    obj.Join(x, y);
            }
            return obj.p;
        }
    }
}
