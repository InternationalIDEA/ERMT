using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT
{
    public partial class DeleteDate : Form
    {
        public DeleteDate()
        {
            InitializeComponent();
        }

        public DateTime Date
        {
            get { return dateTimePicker1.Value; }
        }
    }
}
