using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for NodeControl.xaml
    /// </summary>
    /// 
    public partial class NodeControl : UserControl, ICloneable
    {

        public ObservableNode Node { get; }

        public Point Position { get; set; }

        public int Data { get => Node.Data; set => Node.Data = value; }

        public event EventHandler NodeChanged;

        public NodeControl(int data)
        {
            InitializeComponent();
            DataBlock.Text = data.ToString();
            Node = new ObservableNode(data);
            Node.DataChanged += (s, e) =>
            {
                DataBlock.Text = Node.Data.ToString();
                NodeChanged?.Invoke(this, null);
            };
            Node.VisitedChanged += (s, e) =>
            {
                BGEllipse.Fill = Node.Visited ? Brushes.Blue : Brushes.DarkCyan;
            };

        }

        public static Line Bind(NodeControl first, NodeControl second) 
        {
            second.Node.Children.Add(first.Node);
            first.Node.Children.Add(second.Node);
            Line bind = new Line();
            bind.X1 = first.Position.X+16;
            bind.Y1 = first.Position.Y;
            bind.X2 = second.Position.X+16;
            bind.Y2 = second.Position.Y;
            bind.StrokeThickness = 1;
            bind.Stroke = Brushes.Black;
            return bind;
        }

        public object Clone()
        {
            var s = new NodeControl(this.Data)
            {
                Position = new Point(Position.X, Position.Y)
            };
            s.Node.Visited = this.Node.Visited;
            Canvas.SetLeft(s, s.Position.X - 16);
            Canvas.SetTop(s, s.Position.Y - 16);
            Panel.SetZIndex(s, 1);
            return s;
        }
    }
}
