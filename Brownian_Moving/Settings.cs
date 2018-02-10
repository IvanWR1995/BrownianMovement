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
    public partial class Settings : Form
    {
        
        int Mass,V,R;
       public  Action<int, int, int> SettingsSet;
       public int Get_Mass
        {
            get { return Mass; }
        }
       public  int Get_Speed
        {
            get { return V; }
        }
        public int Get_R
        {
            get { return R; }
        }
        public Settings(int mass_in,int V_in,int R_in)
        {
            InitializeComponent();
            Mass = mass_in;
            V = V_in;
            R = R_in;
            Mass_value.Value = Mass;
            R_Value.Value = R;
            Speed_Value.Value = V;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
            
         
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Mass = Mass_value.Value;
            V = Speed_Value.Value;
            R = R_Value.Value;
            SettingsSet(V, R, Mass);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
