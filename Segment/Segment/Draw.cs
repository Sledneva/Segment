using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Segment
{
    public class Draw
    {
        public class myPoint 
        {
            public int x;
            public int y;
            public myPoint(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        
        public Rectangle rect;

        public void PrintAll(float x, myPoint A, myPoint B, myPoint O, Graphics g)
        {
            Point[] points =                 
            {
                new Point(A.x,A.y),
                new Point(B.x,B.y),
                new Point(O.x,O.y),
            };
            Clear(g);            
            g.FillPie(Brushes.Red, rect, 0.0F, x);
            g.FillPolygon(Brushes.White, points);
        }
        public void Clear(Graphics g)
        {
            g.FillRectangle(Brushes.White, 0, 0, 2000, 2000);
        }
    }
}
