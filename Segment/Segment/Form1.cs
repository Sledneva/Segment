using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Segment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
        }

        Draw draw = new Draw();
        public Graphics g;
        public int R;
        public Draw.myPoint O, A, B;
        float x = 90.0F;
             

        private void buttonGo_Click(object sender, EventArgs e)
        {
             Graphics g = panel1.CreateGraphics();            
             SetCoordinat();

        }

        public void SetCoordinat()
        {
            if (textBoxX.Text != "" && textBoxY.Text != "" && textBoxR.Text != "")
            {
                R = Convert.ToInt32(textBoxR.Text);
                O = new Draw.myPoint(Convert.ToInt32(textBoxX.Text),
                    Convert.ToInt32(textBoxY.Text));//позиция
                draw.rect = new Rectangle(O.x - R, O.y - R, 2 * R, 2 * R);
                A = new Draw.myPoint(O.x + R, O.y);//вычисляю края точек
                B = new Draw.myPoint(Convert.ToInt32(O.x - 3 - R * Math.Sin((x - 90F) * Math.PI / 180)),
                    Convert.ToInt32(O.y + R * Math.Cos((x - 90F) * Math.PI / 180)));
                g = panel1.CreateGraphics();
                draw.PrintAll(x, A, B, O, g);       
                
            }
        }
        void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                R += 5;
                textBoxR.Text = Convert.ToString(R);
                SetCoordinat();
            }
            else
            {
                if (R > 10)
                {
                    R -= 5;
                    textBoxR.Text = Convert.ToString(R);
                }
                SetCoordinat();  
            }
        }        
        private bool isDown;          
        private int pointXdown, pointYdown;   
        private int deltaAx, deltaAy, deltaBx, deltaBy;

        bool pointArh = false;
        bool pointBld = false;
        bool center = false; 
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            pointXdown = e.X;
            pointYdown = e.Y;
            isDown = true;
            if (Math.Abs((A.x + B.x) / 2 - e.X) < 0.3 * R && Math.Abs((A.y + B.y) / 2 - e.Y) < 0.3 * R)
            {
                center = true;               
            }
            if (Math.Abs(e.X - A.x) < 10 && Math.Abs(e.Y - A.y) < 10)
            {
                pointArh = true;                
            }
            if (Math.Abs(e.X - B.x) < 10 && Math.Abs(e.Y - B.y) < 10)
            {
                pointBld = true;                
            }
        }        
        private int deltaX, deltaY;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pointBld = false;
            pointArh = false;
            center = false;
            isDown = false;
            Cursor = Cursors.Default;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);
            if (isDown && center)
            {
                deltaX = e.X - pointXdown;
                deltaY = e.Y - pointYdown;
                O.x += deltaX;
                O.y += deltaY;
                textBoxX.Text = Convert.ToString(O.x);
                textBoxY.Text = Convert.ToString(O.y);
                pointXdown += deltaX;
                pointYdown += deltaY;
                SetCoordinat();
            }

            if (isDown && pointArh)
            {
                deltaAx = -pointXdown + e.X;
                deltaAy = pointYdown - e.Y;
                pointXdown += deltaAx;
                pointYdown += deltaAy;

                R = Math.Abs(R + deltaAx);
                textBoxR.Text = Convert.ToString(R);
                if (deltaAx > 0)
                    x += 2F;
                else
                    x -= 2F;
                if (R > 10)
                    SetCoordinat();
            }
            if (isDown && pointBld)
            {
                deltaBx = pointXdown - e.X;
                deltaBy = -pointYdown + e.Y;
                pointXdown += deltaBx;
                pointYdown += deltaBy;
                R = Math.Abs(R + deltaBy);
                textBoxR.Text = Convert.ToString(R);
                if (deltaBy > 0)
                    x += 1F;
                else
                    x -= 1F;

                if (R > 10)
                    SetCoordinat();
            }
        }


        


        
    }
}
