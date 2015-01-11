using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsGUI
{
    class Disjoint_Set
    {
        public int[] p;
        private int size;

        public Disjoint_Set(int _size)
        {
            size = _size;
            p = new int[size];
            for (int i = 0; i < size; ++i)
                p[i] = i;
        }

        public int Find(int element)
        {
            return p[element];
        }

        public void Join(int x, int y)
        {
            int z = Math.Min(x, y);
            for (int i = 0; i < size; ++i)
                if (p[i] == x || p[i] == y)
                    p[i] = z;
        }
    }
}
