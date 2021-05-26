using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace WpfApp1
{
    enum EditMode
    {
        Peak,
        Edge,
        Pen
    }

    class ViewModel : BindableBase
    {
        public EditMode Mode { get; set; }

        public ViewModel()
        {
            cmd = new DelegateCommand(() =>
                {
                    
                }
                );
            LabelText = "Hello!";
        }

        public string LabelText { get; set; }

        private int Counter { get; set; }

        public DelegateCommand cmd { get; }
    }
}
