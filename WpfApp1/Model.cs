using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Model
    {
        private LinkedList<Node> path;

        public static void DFS(ObservableNode start)
        {
            int counter = 0;
            DFS(start,ref counter);
        }

        private static bool DFS(ObservableNode node,ref int counter)
        {
            node.Visited = true;
            node.Data = ++counter;
            foreach (ObservableNode child in node.Children.Where(x => !x.Visited))
            {
                DFS(child,ref counter);
            }
            return true;
        }
    }
}
