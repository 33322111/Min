using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polyAngles
{
    public partial class Form1 : Form
    {
        public List<Shape> pointsList = new List<Shape>();
        int res = 1;
        bool isInside = false;


        public Form1()
        {
            InitializeComponent();
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

            foreach (Shape point in pointsList)
            {
                point.Draw(e.Graphics);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int clickedX = e.X;
            int clickedY = e.Y;


            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].IsInside(clickedX, clickedY))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        pointsList[i].isClicked = true;
                        isInside = true;

                        pointsList[i].dx = clickedX - pointsList[i].x0;
                        pointsList[i].dy = clickedY - pointsList[i].y0;

                        break;
                    }

                    else
                    {
                        pointsList.RemoveAt(i);

                        if (isInside) isInside = false;

                        break;
                    }
                }
            }

            if (!isInside && e.Button == MouseButtons.Left)
            {
                if (res == 1)
                {
                    pointsList.Add(new Circle(clickedX, clickedY));
                }
                else if (res == 2)
                {
                    pointsList.Add(new Square(clickedX, clickedY));
                }
                else if (res == 3)
                {
                    pointsList.Add(new Triangle(clickedX, clickedY));
                }
            }

            panel1.Invalidate();
        }

        

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isInside)
            {
                foreach (Shape vertex in pointsList)
                {
                    if (vertex.isClicked)
                    {
                        vertex.x0 = e.X - vertex.dx;
                        vertex.y0 = e.Y - vertex.dy;
                        // label1.Text = Convert.ToString(vertex.x0) + ' ' +  Convert.ToString(vertex.y0);

                        panel1.Invalidate();
                    }
                }
            }
        }


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isInside)
            {
                foreach (Shape vertex in pointsList)
                {
                    if (vertex.isClicked)
                    {
                        vertex.isClicked = false;
                        isInside = false;

                        panel1.Invalidate();

                        break;
                    }
                }
            }
        }


        private void Definition(Graphics g)
        {
            Shape[] pointsArray = pointsList.ToArray();

            Point firstPoint;
            Point secondPoint;

            int k;
            int b;
            int isLess;
            int isMore;

            bool isGoodPair;

            for (int i = 0; i < pointsArray.Length; i++)
            {
                for (int j = i + 1; i < pointsArray.Length; j++)
                {
                    firstPoint = new Point(pointsArray[i].x0, pointsArray[i].y0);
                    secondPoint = new Point(pointsArray[j].x0, pointsArray[j].y0);

                    isLess = 0;
                    isMore = 0;

                    isGoodPair = true;

                    if (firstPoint.X == secondPoint.Y)
                    {
                        for (int z = 0; z < pointsArray.Length; z++)
                        {
                            if (z != i && z != j) 
                            {
                                if (pointsArray[z].x0 > firstPoint.X)
                                {
                                    isMore += 1;
                                }

                                else
                                {
                                    isLess += 1;
                                }
                            }

                            if (!(isMore == 0 || isLess == 0))
                            {
                                isGoodPair = false;
                                break;
                            }

                        }
                    }

                    else
                    {
                        k = (firstPoint.Y - secondPoint.Y) / (firstPoint.X - secondPoint.X);
                        b = firstPoint.Y - k * firstPoint.X;

                        for (int z = 0; z < pointsArray.Length; z++)
                        {
                            if (z != i && z != j)
                            {
                                if (k * pointsArray[z].x0 + b > firstPoint.Y)
                                {
                                    isMore += 1;
                                }

                                else
                                {
                                    isLess += 1;
                                }
                            }

                            if (!(isMore == 0 || isLess == 0))
                            {
                                isGoodPair = false;
                                break;
                            }
                        }
                    }

                    if (isGoodPair)
                    {

                    }

                }
            }
        }
        

        private void JarvisMarch()
        {

        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            res = 1;
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            res = 2;
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            res = 3;
        }
    }
}
