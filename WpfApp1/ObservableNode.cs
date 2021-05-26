using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ObservableNode : Node
    {
        public ObservableNode(int data)
        {
            Children = new BindingList<ObservableNode>();
            Data = data;
        }

        public event EventHandler DataChanged;

        public event EventHandler VisitedChanged;

        private int data;

        private bool visited;

        public override int Data
        {
            get => data;
            set
            {
                data = value;
                DataChanged?.Invoke(this, null);
            }
        }

        new public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
                VisitedChanged?.Invoke(this, null);
            }
        }

        new public BindingList<ObservableNode> Children { get; set; }
    }
}
