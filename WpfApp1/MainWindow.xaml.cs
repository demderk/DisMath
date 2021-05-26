using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int Counter = 1;

        BindingList<NodeControl> nodes = new BindingList<NodeControl>();

        NodeControl lastNode = null;

        BindingList<List<UIElement>> Snapshots = new BindingList<List<UIElement>>();

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode.SelectedIndex != 0)
            {
                return;
            }
            var canvas = (Canvas)sender;
            NodeControl node = new NodeControl(Counter);
            node.Position = new Point(e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y);
            Canvas.SetLeft(node, node.Position.X - 16);
            Canvas.SetTop(node, node.Position.Y - 16);
            Panel.SetZIndex(node, 1);
            node.NodeChanged += OnNodeChanged;
            nodes.Add(node);
            canvas.Children.Add(nodes.Last());
            Counter++;
            node.MouseLeftButtonDown += (s, ev) =>
            {
                if (lastNode == null)
                {
                    lastNode = node;
                    return;
                }
                canvas.Children.Add(NodeControl.Bind(lastNode, node));
                lastNode = null;

            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.DFS((nodes[int.Parse(IndexSelect.Text) - 1] as NodeControl).Node);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int input = -1;
            if (int.TryParse($"{(sender as TextBox).Text}{e.Text}", out input) && input <= CanvasInk.Children.Count && input > 0)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void OnNodeChanged(object sender, EventArgs e)
        {
            List<UIElement> elements = new List<UIElement>();
            foreach (UIElement item in CanvasInk.Children)
            {
                if (item is NodeControl node)
                {
                    UIElement ui = (NodeControl)node.Clone();
                    elements.Add(ui);
                }
                if (item is Line)
                {
                    elements.Add(item);
                }
            }
            Snapshots.Add(elements);
            SnapshotsControl.Items.Add("Snapshot");
        }

        private void SnapshotsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanvasInk.Children.Clear();
            for (int i = 0; i < Snapshots[SnapshotsControl.SelectedIndex - 1 == -1 ? 0 : SnapshotsControl.SelectedIndex - 1].Count; i++)
            {
                CanvasInk.Children.Add(Snapshots[SnapshotsControl.SelectedIndex - 1 == -1 ? 0 : SnapshotsControl.SelectedIndex - 1][i]);
            }
        }
    }
}
