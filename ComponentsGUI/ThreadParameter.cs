using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsGUI
{
    enum Task { Square, Linear, Log };

    class ThreadParameter
    {
        public Task task;
        public Action<int, long, string, int> action;

        public ThreadParameter(Task _task, Action<int, long, string, int> _action)
        {
            task = _task;
            action = _action;
        }
    }
}
