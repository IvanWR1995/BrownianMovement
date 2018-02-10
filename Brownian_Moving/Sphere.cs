using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
namespace Brownian_Moving
{
    [Serializable]
    class Sphere{
        int mass;
        Point LeftTop,Center;
        int R;
        float width, height;
        Point V,n;
        Rectangle ClientRect;
        public Point GetCenter
        {
            get { return Center; }
        }
        public int Get_R
        {
            get
            {
                return R;
            }
        }
        public int Get_Mass
        {
            get
            {
                return mass;
            }
        }
        public Point Get_V
        {
            get 
            {
                return V;
            }
            set
            {
                V = value;
            }
            
        }
        public Rectangle ClientRect_Set
        {
            set
            {
                ClientRect = value;
            }
        }
        
        public Rectangle GetEllipse
        {
            get 
            {
                return new Rectangle(LeftTop, new Size((int)width, (int)height));
            }
        }
        public Sphere(int mass_in, int R_in, Point Center_in,Point V_in,Rectangle ClientRect_in)
        {
            LeftTop = new Point();
            Center = new Point();
            n = new Point();
            R = R_in;
            V = V_in;
            n.X = Math.Sign(V.X);
            n.Y = Math.Sign(V.Y);

            ClientRect = new Rectangle();
            ClientRect = ClientRect_in;
            Center = Center_in;
            LeftTop.X = Center.X - R;
            LeftTop.Y = Center.Y - R;
            mass = mass_in;
            width = 2 * R;
            height = 2 * R;
 
 
        }
        public void Draw(Graphics graph,Pen Pen_in)
        {

            graph.DrawEllipse(Pen_in, LeftTop.X, LeftTop.Y, width, height); 
        

        }
        public static void RandomSphere( List<Sphere> ListSphere,Rectangle ClientRect, int Mass_Max, int R_Max)
        {
            Random obj_rand = new Random();
            Point Center_loc;
            int R_rand;
            int V_x, V_y,tmp_V = 0;
            int Rand_Mass = obj_rand.Next(20, Mass_Max); 
            do
            {
                tmp_V=obj_rand.Next(1, 7);
                V_x = tmp_V * (int)Math.Pow(-1, obj_rand.Next(1, 5));
                V_y = tmp_V * (int)Math.Pow(-1, obj_rand.Next(1, 5));
         
                Rand_Mass = obj_rand.Next(20, Mass_Max);
                R_rand = obj_rand.Next(10, R_Max);
                Center_loc = new Point(obj_rand.Next(ClientRect.Location.X + 20, ClientRect.Location.X + ClientRect.Width), obj_rand.Next(ClientRect.Location.Y + 20, ClientRect.Location.Y + ClientRect.Height));
            } while (Sphere.IsSphereVisible(ListSphere, ClientRect,R_rand, Center_loc));
            ListSphere.Add(new Sphere(Rand_Mass, R_rand, Center_loc, new Point(V_x,V_y),ClientRect));

        }
        static bool IsSphereVisible(List<Sphere> ListSphere,Rectangle ClientRect ,int R_in, Point Center_in)
        {

            foreach (Sphere index in ListSphere)
            {

                if (!(index.R + R_in < Math.Sqrt(Math.Pow(Center_in.X - index.Center.X, 2) + Math.Pow(Center_in.Y - index.Center.Y, 2))))
                    return true;

            }
            if (!ClientRect.Contains(new Rectangle(Center_in.X - R_in, Center_in.Y - R_in, 2 * R_in, 2 * R_in)))
                return true;

            return false;
        }
      public  bool IsVisible(List<Sphere> ListSphere, Point Center_in)
        {
            foreach (Sphere index in ListSphere)
            {

                if (((Center!= index.Center)&&(!(index.R + R < Math.Sqrt(Math.Pow(Center_in.X - index.Center.X, 2) + Math.Pow(Center_in.Y - index.Center.Y, 2))))))
                    return true;

            }
            if (!ClientRect.Contains(new Rectangle(Center_in.X - R, Center_in.Y - R, 2 * R, 2 * R)))
                return true;
            return false;
 
        }
        
        public bool MoveSphere(Point e, int Shift_X, int Shift_Y, List<Sphere> ListSphere)
        {
          
            Point tmp = new Point(Center.X + Shift_X,Center.Y + Shift_Y);
            if ((IsSelectSphere(e)) && (!IsVisible(ListSphere, tmp)))
            {
                LeftTop.X += Shift_X;
                LeftTop.Y += Shift_Y;
                Center= tmp;
                return true;

            }
            return false;
        }
        public bool IsSelectSphere(Point e)
        {
            GraphicsPath Graphics_Loc = new GraphicsPath();
            Graphics_Loc.AddEllipse(LeftTop.X, LeftTop.Y, width, height);
            return Graphics_Loc.IsVisible(e);
        }

        public bool Blow(Sphere Sphere_in,FileStream LogFile,int Number_1,int Number_2)
        {
            if (((R + Sphere_in.Get_R) >=(int)Math.Round((Math.Sqrt(Math.Pow(Center.X+V.X- Sphere_in.Center.X-Sphere_in.V.X, 2) + Math.Pow(Center.Y+V.Y - Sphere_in.Center.Y-Sphere_in.V.Y, 2))),2)))
            {
                string DataString; 
                Point Last_V1 = new Point();
                Point Last_V2 = new Point();
                int Y, X;
                Last_V1 = V;
                Last_V2 = Sphere_in.Get_V;
                V.X =(int)Math.Round((double)(Last_V1.X * (mass - Sphere_in.Get_Mass)  +Last_V2.X * 2 * Sphere_in.Get_Mass) / (mass + Sphere_in.Get_Mass));
                V.Y = (int)Math.Round((double)(Last_V1.Y * (mass - Sphere_in.Get_Mass) + Last_V2.Y * 2 * Sphere_in.Get_Mass) / (mass + Sphere_in.Get_Mass));
                V.X = (int)Math.Min(10, Math.Abs(V.X)) * Math.Sign(V.X);
                V.Y = (int)Math.Min(10,Math.Abs(V.Y)) * Math.Sign(V.Y);
                if ((V.X == 0) && (V.Y == 0))
                {
                    n.X = Math.Sign(Last_V1.X);
                    n.Y = Math.Sign(Last_V1.Y);
                }
              
                X = (int)Math.Round((double)(Last_V1.X * 2 * mass +  Last_V2.X * (Sphere_in.Get_Mass - mass)) / (mass + Sphere_in.Get_Mass));
                Y = (int)Math.Round((double)(Last_V1.Y * 2 * mass +  Last_V2.Y * (Sphere_in.Get_Mass - mass)) / (mass + Sphere_in.Get_Mass));
                X = (int)Math.Min(10, Math.Abs(X)) * Math.Sign(X);
                Y = (int)Math.Min(10, Math.Abs(Y)) * Math.Sign(Y);
                if ((X == 0) && (Y == 0))
                {
                    Sphere_in.n.X = Math.Sign(Last_V2.X);
                    Sphere_in.n.Y = Math.Sign(Last_V2.Y);
                }

                Sphere_in.Get_V = new Point(X,Y);
                DataString = "Координаты центров во время удара:\r\n";
                DataString += String.Format("Шар №{0}:Координаты центра=({1},{2})\r\n", Number_1, Center.X, Center.Y);
                DataString += String.Format("Шар №{0}:Координаты центра=({1},{2})\r\n", Number_2, Sphere_in.Center.X, Sphere_in.Center.Y);
                DataString += "До столкновения:\r\n";
               DataString+= String.Format("Шар №{0}: Скорость по X={1} Скорость по Y={2}\r\n",Number_1,Last_V1.X,Last_V1.Y,Center.X,Center.Y);
               DataString += String.Format("Шар №{0}: Скорость по X={1} Скорость по Y={2}\r\n", Number_2, Last_V2.X, Last_V2.Y, Sphere_in.Center.X, Sphere_in.Center.Y);
               DataString += "После столкнрвения\r\n";
               DataString += String.Format("Шар №{0}: Скорость по X={1} Скорость по Y={2}\r\n", Number_1, V.X, V.Y, Center.X, Center.Y);
               DataString += String.Format("Шар №{0}: Скорость по X={1} Скорость по Y={2}\r\n", Number_2, X,Y, Sphere_in.Center.X, Sphere_in.Center.Y);
               DataString += "------------------------------------------------------------------\r\n";
               byte[] ArrayToWrite = Encoding.Default.GetBytes(DataString);
               LogFile.Write(ArrayToWrite, 0, ArrayToWrite.Length );
                return true;
            }
            return false;
        }
        private int IsMin(int[] ArrayRast)
        {
            int res = 0, min = ArrayRast[0];
            for (int i = 0; i != ArrayRast.Length; i++)
            {

                int tmp = Math.Min(min, ArrayRast[i]);
                if (tmp != min)
                {
                    min = tmp;
                    res = i;
                }

            }
            return res;
        }
         public void  IsVisibleShape()
        {
           
            Rectangle Shape_rect = new Rectangle(LeftTop.X + V.X, LeftTop.Y + V.Y,(int) width, (int)height);
            GraphicsPath Shape_tmp = new GraphicsPath();
            Shape_tmp.AddRectangle(Shape_rect);
            PointF[] ArrayDots = Shape_tmp.PathPoints.ToArray();
           
            foreach (PointF index in ArrayDots)
            {

                if (!ClientRect.Contains((int)index.X,(int)index.Y))
                {
                   
                    int[] rast = new int[4];
                    rast[0] = Math.Abs(ClientRect.Location.X- Center.X);
                    rast[1] = Math.Abs(ClientRect.Location.Y - Center.Y);
                    rast[2] = Math.Abs(ClientRect.Width + ClientRect.Location.X - Center.X );
                    rast[3] = Math.Abs(ClientRect.Height + ClientRect.Location.Y - Center.Y);
                    int tmp = IsMin(rast);
                    switch (tmp)
                    {
                        case 0:
                            V.X *=  (-1);
                            return;
                        case 1:
                            V.Y *= (-1);
                            return;
                        case 2:
                            V.X*= (-1);
                          
                            return;
                        case 3:
                            V.Y *= (-1) ;
                            return;
                    }

                }
            }
          

        }

        public void Move()
        {
           IsVisibleShape();
            LeftTop.X += V.X;
            LeftTop.Y += V.Y;
            Center.X += V.X;
            Center.Y += V.Y;

        }
        public bool SettingsSet(List<Sphere> ListSphere,int V_in,int R_in,int Mass_in)
        {
            Point New_V = new Point();

            foreach (Sphere index in ListSphere)
            {

                if (((Center != index.Center) && (!(index.R + R_in < Math.Sqrt(Math.Pow(Center.X - index.Center.X, 2) + Math.Pow(Center.Y - index.Center.Y, 2))))))
                    return false;

            }
            if (!ClientRect.Contains(new Rectangle(Center.X - R_in, Center.Y - R_in, 2 * R_in, 2 * R_in)))
                return false;
           
 
            mass = Mass_in;
            if ((V.X == 0)&&(V.Y==0))
            {
                New_V.X = n.X * (int)Math.Min(10, V_in);
                New_V.Y = n.Y * (int)Math.Min(10, V_in);
                V = New_V;
            }
            else 
            {
                New_V.X = (int)Math.Min(10, Math.Round(V.X * V_in / Math.Sqrt(Math.Pow(V.X, 2) + Math.Pow(V.Y, 2))));
                New_V.Y = (int)Math.Min(10, Math.Round(V.Y* V_in / Math.Sqrt(Math.Pow(V.X, 2) + Math.Pow(V.Y, 2))));
                if (V_in == 0)
                {
                    n.X = Math.Sign(V.X);
                    n.Y = Math.Sign(V.Y);
                }
                V = New_V;
           
 
            }
            R = R_in;
            width = 2 * R;
            height = 2 * R;
            LeftTop.X = Center.X - R;
            LeftTop.Y = Center.Y - R;
            return true;
 
        }
       

    }
}
