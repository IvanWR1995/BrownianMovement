using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brownian_Moving
{
    public partial class GlobalSettings : Form
    {
        public Action<int> CreateSphere;
        public GlobalSettings(int num)
        {
            InitializeComponent();
            numericUpDown1.Value = num;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateSphere((int)numericUpDown1.Value);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
