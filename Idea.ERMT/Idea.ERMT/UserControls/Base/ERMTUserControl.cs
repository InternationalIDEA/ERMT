using System;
using System.Windows.Forms;

namespace Idea.ERMT
{
    public class ERMTUserControl : UserControl
    {
        public ERMTUserControl()
        {
            InitializeComponent();
        }

        public virtual void Clear()
        {
        }

        protected virtual void InitializeComponent()
        {
        }

        public virtual void Print()
        { }

        public virtual void ShowTitle()
        { }
    }
}
