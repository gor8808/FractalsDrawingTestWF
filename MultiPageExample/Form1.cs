using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace MultiPageExample
{
    public partial class Form1 : Form
    {
        private float xmin = -60;
        private float ymin = -60;
        private float xmax = 60;
        private float ymax = 60;
        PointF A = new PointF(60, 0);
        PointF B = new PointF(0, 60);
        PointF C = new PointF(-60, 0);

        public Form1()
        {
            InitializeComponent();
        }
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.Clear(picCanvas.BackColor);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            picCanvas.BackColor = Color.Black;
            Pen pen = new Pen(Brushes.White);
            float height = picCanvas.Height;
            float width = picCanvas.Height;
            RectangleF rect = new RectangleF(
                    xmin, ymin, xmax - xmin, ymax - ymin);
            PointF[] pts =
            {
                    new PointF(0, height),
                    new PointF(width, height),
                    new PointF(0, 0),
                };
            gr.Transform = new Matrix(rect, pts);
            pen.Color = Color.Red;
            gr.DrawLine(pen, xmin, 0, xmax, 0);
            gr.DrawLine(pen, 0, ymin, 0, ymax);
            for (int x = (int)xmin; x <= xmax; x++)
            {
                gr.DrawLine(pen, x, -0.1f, x, 0.1f);
            }
            for (int y = (int)ymin; y <= ymax; y++)
            {
                gr.DrawLine(pen, -0.1f, y, 0.1f, y);
            }



            int cubeSide = 40;
            //PointF centre = new PointF(0 - cubeSide / 2, 0 - cubeSide / 2);
            pen.Color = Color.White;
            //gr.DrawRectangle(pen, centre.X, centre.Y, cubeSide, cubeSide);
            SierpinskiCarpet(gr, 5,new RectangleF(-60,-60,120,120));
            //float dist1 = cubeSide / 2 + cubeSide / 3;
            //float dist2 = 0 - dist1 - cubeSide / 3;
            //float dist3 = cubeSide / 2 + cubeSide / 3 + ((cubeSide / 2 * (float)Math.Sqrt(2)) + (cubeSide / 3 * (float)Math.Sqrt(2)));
            //float dist4 = 0 - cubeSide - cubeSide / 3;
            //PointF centre1;
            //cubeSide /= 3;
            //centre1 = new PointF(dist1, 0 - cubeSide / 2);
            //pen.Color = Color.Green;
            //gr.DrawRectangle(pen, centre1.X, centre1.Y, cubeSide, cubeSide);

            //centre1 = new PointF(dist2, 0 - cubeSide / 2);
            //pen.Color = Color.Green;
            //gr.DrawRectangle(pen, centre1.X, centre1.Y, cubeSide, cubeSide);

            //centre1 = new PointF(0 - cubeSide / 2, dist1);
            //pen.Color = Color.Green;
            //gr.DrawRectangle(pen, centre1.X, centre1.Y, cubeSide, cubeSide);

            //centre1 = new PointF(0 - cubeSide / 2, dist2);
            //pen.Color = Color.Green;
            //gr.DrawRectangle(pen, centre1.X, centre1.Y, cubeSide, cubeSide);

            //centre1 = GetPointByLengthAndAngle(centre, dist3, (float)Math.PI / 4);
            //pen.Color = Color.White;
            //gr.DrawRectangle(pen, centre1.X - cubeSide/2, centre1.Y - cubeSide/2, cubeSide, cubeSide);

            //centre1 = GetPointByLengthAndAngle(centre, dist3, (float)Math.PI / 4);
            //pen.Color = Color.White;
            //gr.DrawRectangle(pen, -1 * (centre1.X - cubeSide/2) - cubeSide / 3 * 1.5f, -1 * (centre1.Y - cubeSide/2) - cubeSide / 3 * 1.5f, cubeSide, cubeSide);

        }

        private void SierpinskiCarpet(Graphics gr, int level, RectangleF rect)
        {
            if (level == 0)
            {
                gr.FillRectangle(Brushes.Blue, rect);
            }
            else
            {
                // Divide the rectangle into 9 pieces.
                float wid = rect.Width / 3;
                float x0 = rect.Left;
                float x1 = x0 + wid;
                float x2 = x0 + wid * 2f;

                float hgt = rect.Height / 3f;
                float y0 = rect.Top;
                float y1 = y0 + hgt;
                float y2 = y0 + hgt * 2f;

                SierpinskiCarpet(gr, level - 1, new RectangleF(x0, y0, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x1, y0, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x2, y0, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x0, y1, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x2, y1, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x0, y2, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x1, y2, wid, hgt));
                SierpinskiCarpet(gr, level - 1, new RectangleF(x2, y2, wid, hgt));
            }
        }

        private PointF GetPointByLengthAndAngle(PointF p1, float length, float angle)
        {
            return new PointF((float)(p1.X + Math.Cos(angle) * length), (float)(p1.Y + Math.Sin(angle) * length));
        }
        private void Start_Click(object sender, EventArgs e)
        {
            int height = 500;
            int width = 500;
            Bitmap bitmap = new Bitmap(height, width);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bitmap.SetPixel(i, j, Color.Yellow);
                }
            }
            picCanvas.Image = bitmap;
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                RectangleF rect = new RectangleF(
                    xmin, ymin, xmax - xmin, ymax - ymin);
                PointF[] pts =
                {
                    new PointF(0, height),
                    new PointF(width, height),
                    new PointF(0, 0),
                };
                gr.Transform = new Matrix(rect, pts);
                using (Pen graph_pen = new Pen(Color.Blue, 0.1f))
                {
                    graph_pen.Color = Color.Red;
                    gr.DrawLine(graph_pen, xmin, 0, xmax, 0);
                    gr.DrawLine(graph_pen, 0, ymin, 0, ymax);
                    for (int x = (int)xmin; x <= xmax; x++)
                    {
                        gr.DrawLine(graph_pen, x, -0.1f, x, 0.1f);
                    }
                    for (int y = (int)ymin; y <= ymax; y++)
                    {
                        gr.DrawLine(graph_pen, -0.1f, y, 0.1f, y);
                    }
                    graph_pen.Color = Color.Blue;

                    gr.DrawLine(graph_pen, A.X, A.Y, B.X, B.Y);
                    gr.DrawLine(graph_pen, B.X, B.Y, C.X, C.Y);
                    gr.DrawLine(graph_pen, A.X, A.Y, C.X, C.Y);

                    for (int i = 0; i < 100; i++)
                    {
                        GetPoints(A, C, out PointF p1, out PointF p2, out PointF topPoint);

                        //graph_pen.Color = Color.Yellow;
                        //graph_pen.Width = 0.5f;
                        //gr.DrawLine(graph_pen, p1, p2);
                        graph_pen.Color = Color.Blue;
                        graph_pen.Width = 0.1f;

                        gr.DrawLine(graph_pen, p1, topPoint);
                        gr.DrawLine(graph_pen, p2, topPoint);
                    }
                }
            }
        }
        private void GetPoints(PointF startP, PointF endP, out PointF p1, out PointF p2, out PointF topPoint)
        {
            p1 = new PointF();
            p2 = new PointF();
            topPoint = new PointF();

            float d = GetDistance(startP, endP);
            p1 = new PointF(((startP.X * 2) + endP.X) / 3, ((startP.Y * 2) + endP.Y) / 3);
            p2 = new PointF((startP.X + (endP.X * 2)) / 3, (startP.Y + (endP.Y * 2)) / 3);

            float angle1 = (float)Math.PI / 2.5f;
            float length = d / 3;

            topPoint = new PointF((float)(p1.X + Math.Cos(angle1) * length), (float)(p1.Y + Math.Sin(angle1) * length));
        }
        private float GetDistance(PointF p1, PointF p2)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}