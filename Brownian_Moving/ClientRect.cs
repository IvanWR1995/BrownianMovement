using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace Brownian_Moving
{
    [Serializable]
    class ClientRect
    {
        Point[] rect;
        Rectangle ClientRectWin;
        public Rectangle GetRect
        {
            get
            {
                return new Rectangle(rect[0], new Size(rect[2].X - rect[0].X, rect[2].Y - rect[0].Y));
            }
        }
        public ClientRect(Rectangle WinRect)
        {
            ClientRectWin= new Rectangle();
            ClientRectWin = WinRect;
            rect = new Point[4];
            rect[0].X = 100;
            rect[0].Y = 100;
            rect[1].X = 100;
            rect[1].Y = 400;
            rect[2].X = 800;
            rect[2].Y = 400;
            rect[3].X = 800;
            rect[3].Y = 100;



        }
        public void Draw(Graphics graph)
        {
            graph.DrawPolygon(new Pen(Color.Black, 3), rect);
            for (int i = 0; i != rect.Length; i++)
            {
                graph.DrawEllipse(new Pen(Color.Red), rect[i].X - 10, rect[i].Y - 10, 20, 20);
            }
        }

        public bool IsChanged(Point Dot, int X_Shift, int Y_Shift, List<Sphere> ListSphere)
        {
            Point[] rect_tmp = new Point[4];
            rect.CopyTo(rect_tmp, 0);
            
            for (int i = 0; i != rect.Length; i++)
            {
            
                GraphicsPath Ellips = new GraphicsPath();
                Ellips.AddEllipse(rect_tmp[i].X - 10, rect_tmp[i].Y - 10, 20, 20);
                if (Ellips.IsVisible(Dot))
                {
                    switch (i)
                    {
                        case 3:
                            rect_tmp[3].X += X_Shift;
                            rect_tmp[3].Y += Y_Shift;
                            rect_tmp[2].X += X_Shift;
                            rect_tmp[0].Y += Y_Shift;
                            break;
                        case 2:
                            rect_tmp[2].X += X_Shift;
                            rect_tmp[2].Y += Y_Shift;
                            rect_tmp[1].Y += Y_Shift;
                            rect_tmp[3].X += X_Shift;
                            break;
                        case 1:
                            rect_tmp[1].X += X_Shift;
                            rect_tmp[1].Y += Y_Shift;
                            rect_tmp[2].Y += Y_Shift;
                            rect_tmp[0].X += X_Shift;

                            break;
                        case 0:
                            rect_tmp[0].X += X_Shift;
                            rect_tmp[0].Y += Y_Shift;
                            rect_tmp[3].Y += Y_Shift;
                            rect_tmp[1].X+= X_Shift;
                            break;

                    }
                    Rectangle tmp_client_rect = new Rectangle(rect_tmp[0], new Size(rect_tmp[2].X - rect_tmp[0].X, rect_tmp[2].Y - rect_tmp[0].Y));
                    foreach(Sphere index in ListSphere)
                    {
                        if (!tmp_client_rect.Contains(index.GetEllipse))
                            return false;
                     }
                    if (ClientRectWin.Contains(new Rectangle(rect_tmp[0].X, rect_tmp[0].Y, rect_tmp[2].X - rect_tmp[0].X, rect_tmp[2].Y - rect_tmp[0].Y)))
                    rect_tmp.CopyTo(rect,0);
                    return true;

                }

            }
            return false;


        }

    }
}
