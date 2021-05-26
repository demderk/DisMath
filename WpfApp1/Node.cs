using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public abstract class Node
    {
        public virtual int Data { get; set; }

        public virtual List<Node> Children { get; protected set; }

        public bool Visited { get; set; }
    }
}
