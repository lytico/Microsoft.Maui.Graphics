using System;

namespace Microsoft.Maui.Graphics
{
    public static class ArcUtils
    {
        public static void SVGArcTo(this Path aTarget, double rx, double ry, double angle, bool largeArcFlag, bool sweepFlag, double x, double y, double lastPointX, double lastPointY)
        {
            double[] vValues = ComputeSvgArc(rx, ry, angle, largeArcFlag, sweepFlag, x, y, lastPointX, lastPointY);
            DrawArc(vValues[0], vValues[1], vValues[2], vValues[3], vValues[4], vValues[5], vValues[6], aTarget);
        }

        /**
        * Converts a svg arc specification to a Degrafa arc.
        **/

        public static double[] ComputeSvgArc(double rx, double ry, double angle, bool largeArcFlag, bool sweepFlag, double x, double y, double lastPointX, double lastPointY)
        {
            //store before we do anything with it
            double xAxisRotation = angle;

            // Compute the half distance between the current and the final point
            double dx2 = (lastPointX - x) / 2.0f;
            double dy2 = (lastPointY - y) / 2.0f;

            // Convert angle from degrees to radians
            angle = Geometry.DegreesToRadians(angle);
            double cosAngle = Math.Cos(angle);
            double sinAngle = Math.Sin(angle);

            //Compute (x1, y1)
            double x1 = cosAngle * dx2 + sinAngle * dy2;
            double y1 = -sinAngle * dx2 + cosAngle * dy2;

            // Ensure radii are large enough
            rx = Math.Abs(rx);
            ry = Math.Abs(ry);
            double prx = rx * rx;
            double pry = ry * ry;
            double px1 = x1 * x1;
            double py1 = y1 * y1;

            // check that radii are large enough
            double radiiCheck = px1 / prx + py1 / pry;
            if (radiiCheck > 1)
            {
                rx = Math.Sqrt(radiiCheck) * rx;
                ry = Math.Sqrt(radiiCheck) * ry;
                prx = rx * rx;
                pry = ry * ry;
            }

            //Compute (cx1, cy1)
            double sign = largeArcFlag == sweepFlag ? -1 : 1;
            double sq = (prx * pry - prx * py1 - pry * px1) / (prx * py1 + pry * px1);
            sq = sq < 0 ? 0 : sq;
            double coef = sign * Math.Sqrt(sq);
            double cx1 = coef * (rx * y1 / ry);
            double cy1 = coef * -(ry * x1 / rx);

            //Compute (cx, cy) from (cx1, cy1)
            double sx2 = (lastPointX + x) / 2.0f;
            double sy2 = (lastPointY + y) / 2.0f;
            double cx = sx2 + (cosAngle * cx1 - sinAngle * cy1);
            double cy = sy2 + (sinAngle * cx1 + cosAngle * cy1);

            //Compute the angleStart (angle1) and the angleExtent (dangle)
            double ux = (x1 - cx1) / rx;
            double uy = (y1 - cy1) / ry;
            double vx = (-x1 - cx1) / rx;
            double vy = (-y1 - cy1) / ry;

            //Compute the angle start
            double n = Math.Sqrt(ux * ux + uy * uy);
            double p = ux;

            sign = uy < 0 ? -1.0f : 1.0f;

            double angleStart = Geometry.RadiansToDegrees(sign * Math.Acos(p / n));

            // Compute the angle extent
            n = Math.Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
            p = ux * vx + uy * vy;
            sign = ux * vy - uy * vx < 0 ? -1.0f : 1.0f;
            double angleExtent = Geometry.RadiansToDegrees(sign * Math.Acos(p / n));

            if (!sweepFlag && angleExtent > 0)
            {
                angleExtent -= 360;
            }
            else if (sweepFlag && angleExtent < 0)
            {
                angleExtent += 360;
            }

            angleExtent %= 360;
            angleStart %= 360;

            return new[] {cx, cy, angleStart, angleExtent, rx, ry, xAxisRotation};
        }

        /**
        * Draws an arc of type "open" only. Accepts an optional x axis rotation value
        **/

        public static void DrawArc(double x, double y, double startAngle, double arc, double radius, double yRadius, double xAxisRotation, Path aPath)
        {
            // Circumvent drawing more than is needed
            if (Math.Abs(arc) > 360)
            {
                arc = 360;
            }

            // Draw in a maximum of 45 degree segments. First we calculate how many
            // segments are needed for our arc.
            double segs = Math.Ceiling(Math.Abs(arc) / 45);

            // Now calculate the sweep of each segment
            double segAngle = arc / segs;

            double theta = Geometry.DegreesToRadians(segAngle);
            double angle = Geometry.DegreesToRadians(startAngle);

            // Draw as 45 degree segments
            if (segs > 0)
            {
                double beta = Geometry.DegreesToRadians(xAxisRotation);
                double sinbeta = Math.Sin(beta);
                double cosbeta = Math.Cos(beta);

                // Loop for drawing arc segments
                for (int i = 0; i < segs; i++)
                {
                    angle += theta;

                    double sinangle = Math.Sin(angle - theta / 2);
                    double cosangle = Math.Cos(angle - theta / 2);

                    double div = Math.Cos(theta / 2);
                    double cx = x + (radius * cosangle * cosbeta - yRadius * sinangle * sinbeta) / div;
                    //Why divide by Math.cos(theta/2)? - FIX THIS
                    double cy = y + (radius * cosangle * sinbeta + yRadius * sinangle * cosbeta) / div;
                    //Why divide by Math.cos(theta/2)? - FIX THIS

                    sinangle = Math.Sin(angle);
                    cosangle = Math.Cos(angle);

                    double x1 = x + (radius * cosangle * cosbeta - yRadius * sinangle * sinbeta);
                    double y1 = y + (radius * cosangle * sinbeta + yRadius * sinangle * cosbeta);

                    aPath.QuadTo(cx, cy, x1, y1);
                }
            }
        }
    }
}