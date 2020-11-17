using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace polyAngles
{
    class Triangle : Shape
    {
        int smallRadius = radius / 2;
        int side = Convert.ToInt32(radius * 1.73);

        public Triangle(int x, int y)
        {
            x0 = x;
            y0 = y;
        }

        private double tanAngle(int xA, int yA, int xB, int yB, int xC, int yC)
        {
            Point vector1 = new Point(xA - xB, yA - yB);
            Point vector2 = new Point(xC - xB, yC - yB);

            return Math.Tan(Math.Acos(
                (vector1.X * vector2.X + vector1.Y * vector2.Y) / 
                (Math.Sqrt(Math.Pow(vector1.X, 2) + Math.Pow(vector1.Y, 2)) *
                Math.Sqrt(Math.Pow(vector1.X, 2) + Math.Pow(vector1.Y, 2)))));

        }

        public override bool IsInside(int clickedX, int clickedY)
        {
            int lowerY = y0 + smallRadius;

            if (clickedY < lowerY)
            {
                /*double k1 = tanAngle(x0, lowerY - smallRadius * 3,
                leftX, lowerY,
                leftX + side, lowerY);*/
                int leftX = x0 - side / 2;

                // int a = smallRadius * 3;
                int b = x0 - leftX - side;
                int c = (leftX + side) * (lowerY - smallRadius * 3) - x0 * lowerY;

                if (clickedY >= - (smallRadius * 3 * clickedX + c) / b)
                {
                    // int a = smallRadius + 3;
                    b = x0 - leftX;
                    c = leftX * (lowerY - smallRadius * 3) - x0 * lowerY;

                    return clickedY >= -(smallRadius * 3 * clickedX + c) / b;
                }
            }

            return false;
        }


        public override void Draw(Graphics g)
        {
            int leftX = x0 - side / 2;
            int lowerY = y0 + smallRadius;

            g.DrawPolygon(shapePen,
                new Point[] {
                    new Point(leftX, lowerY),
                    new Point(leftX + side, lowerY),
                    new Point(x0, lowerY - smallRadius * 3) });
        }
    }
}
