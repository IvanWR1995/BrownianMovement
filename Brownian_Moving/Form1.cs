using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Brownian_Moving
{
    
    public partial class Form1 : Form
    {
        
        List<Sphere> ListSphere;
        ClientRect ClientRectObj;
        FileStream LogFile;
        Point LastDot;
        int tmp_R, tmp_V, tmp_Mass;
        public Form1()
        {
            InitializeComponent();
            ListSphere = new List<Sphere>();
            LogFile = File.Create("logfile.txt");

             ClientRectObj = new ClientRect( new Rectangle( ClientRectangle.Location.X, ClientRectangle.Location.Y+menuStrip1.Height,ClientRectangle.Width,ClientRectangle.Height-menuStrip1.Height));
            LastDot = new Point();
            GraphicsPath ListGraph = new GraphicsPath();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ClientRectObj.Draw(e.Graphics);
            foreach (Sphere index in ListSphere)
            {
                index.Draw(e.Graphics, new Pen(Color.Black));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left)&&(!timer1.Enabled))
            {
                if (ClientRectObj.IsChanged(e.Location, e.X - LastDot.X, e.Y - LastDot.Y,ListSphere))
                {
                    foreach (Sphere index in ListSphere)
                    {
                        index.ClientRect_Set = ClientRectObj.GetRect;
                    }
                    
                
                }
                foreach (Sphere index in ListSphere)
                {
                    if (index.MoveSphere(e.Location, e.X - LastDot.X, e.Y - LastDot.Y, ListSphere))
                    {
                       
                        break;
                    }
                }

             //   LastDot = e.Location;
                Invalidate();
            }
           
            LastDot = e.Location;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
       
            for (int k = 0; k != ListSphere.Count; k++)
            {

               //for (int m = 0; m !=2; m++)
                for (int i = 0; i != ListSphere.Count; i++)
                {
                   

                    for (int j = 0; j !=ListSphere.Count; j++)
                    {
                        if (k != j)
                        {
                            
                            
                            ListSphere[k].Blow(ListSphere[j],LogFile,k,j);
                               
                               
                            
                     
                        }
                    }
                }

                ListSphere[k].Move();
                
            }
           
            Invalidate();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                timer1.Enabled = !timer1.Enabled;
                if (timer1.Enabled)
                {
                    menuStrip1.Items[1].Text = "Стоп";
                }
                else
                {
                    menuStrip1.Items[1].Text = "Запуск";
                }
              
            }
        }

        private void StartItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                menuStrip1.Items[1].Text = "Стоп";
            }
            else
            {
                menuStrip1.Items[1].Text = "Запуск";
            }
        }
        public void SettingsSet(int V_in, int R_in, int Mass_in)
        {

            tmp_V = V_in;
            tmp_R = R_in;
            tmp_Mass = Mass_in;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogFile.Close();
            Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (!timer1.Enabled))
            {
                foreach (Sphere index in ListSphere)
                {
                    if (index.IsSelectSphere(e.Location))
                    {
                        int V = (int)Math.Min(10,(int)Math.Round(Math.Sqrt(Math.Pow(index.Get_V.X, 2) + Math.Pow(index.Get_V.Y, 2))));
                        Settings Dialog_Setting = new Settings(index.Get_Mass, V, index.Get_R);
                        Dialog_Setting.SettingsSet = new Action<int, int, int>(SettingsSet);

                        if (Dialog_Setting.ShowDialog() == DialogResult.OK)
                        {

                            if (index.SettingsSet(ListSphere, tmp_V, tmp_R, tmp_Mass))
                                Invalidate();
                            else
                            {
                                MessageBox.Show("Для изменения радиуса передвиньте шарки или увеличте область", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            
                           
                        }
                    }
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                BinaryFormatter BinForm = new BinaryFormatter();
                Stream fstream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
              
                BinForm.Serialize(fstream, ListSphere);
                BinForm.Serialize(fstream, ClientRectObj);
                fstream.Close();
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                BinaryFormatter BinForm = new BinaryFormatter();
                Stream fstream = File.OpenRead(openFileDialog1.FileName);
                ListSphere=(List<Sphere>) BinForm.Deserialize(fstream);
                ClientRectObj=(ClientRect)BinForm.Deserialize(fstream);
                fstream.Close();
                Invalidate();
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About About_obj = new About();
            About_obj.ShowDialog();
        }
        public void RecreateSphere(int number)
        {
            ListSphere.Clear();
            for (int i = 0; i != number; i++)
            {

                Sphere.RandomSphere(ListSphere, ClientRectObj.GetRect, 300, 40);

            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings obj = new GlobalSettings(ListSphere.Count);
            obj.CreateSphere = new Action<int>(RecreateSphere);
            obj.ShowDialog();
            Invalidate();


        }

    }
}
