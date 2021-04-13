using System;

namespace Microsoft.Maui.Graphics
{
    public static class ArcUtils
    {
        public static void SVGArcTo(this Path aTarget, double rx, double ry, double angle, bool largeArcFlag, bool sweepFlag, double x, double y, double lastPointX, double lastPointY)
        {
            var vValues = ComputeSvgArc(rx, ry, angle, largeArcFlag, sweepFlag, x, y, lastPointX, lastPointY);
            DrawArc(vValues[0], vValues[1], vValues[2], vValues[3], vValues[4], vValues[5], vValues[6], aTarget);
        }

        /**
        * Converts a svg arc specification to a Degrafa arc.
        **/

        public static double[] ComputeSvgArc(double rx, double ry, double angle, bool largeArcFlag, bool sweepFlag, double x, double y, double lastPointX, double lastPointY)
        {
            //store before we do anything with it
            var xAxisRotation = angle;

            // Compute the half distance between the current and the final point
            var dx2 = (lastPointX - x) / 2.0f;
            var dy2 = (lastPointY - y) / 2.0f;

            // Convert angle from degrees to radians
            angle = Geometry.DegreesToRadians(angle);
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            //Compute (x1, y1)
            var x1 = cosAngle * dx2 + sinAngle * dy2;
            var y1 = -sinAngle * dx2 + cosAngle * dy2;

            // Ensure radii are large enough
            rx = Math.Abs(rx);
            ry = Math.Abs(ry);
            var prx = rx * rx;
            var pry = ry * ry;
            var px1 = x1 * x1;
            var py1 = y1 * y1;

            // check that radii are large enough
            var radiiCheck = px1 / prx + py1 / pry;
            if (radiiCheck > 1)
            {
                rx = Math.Sqrt(radiiCheck) * rx;
                ry = Math.Sqrt(radiiCheck) * ry;
                prx = rx * rx;
                pry = ry * ry;
            }

            //Compute (cx1, cy1)
            double sign = largeArcFlag == sweepFlag ? -1 : 1;
            var sq = (prx * pry - prx * py1 - pry * px1) / (prx * py1 + pry * px1);
            sq = sq < 0 ? 0 : sq;
            var coef = sign * Math.Sqrt(sq);
            var cx1 = coef * (rx * y1 / ry);
            var cy1 = coef * -(ry * x1 / rx);

            //Compute (cx, cy) from (cx1, cy1)
            var sx2 = (lastPointX + x) / 2.0f;
            var sy2 = (lastPointY + y) / 2.0f;
            var cx = sx2 + (cosAngle * cx1 - sinAngle * cy1);
            var cy = sy2 + (sinAngle * cx1 + cosAngle * cy1);

            //Compute the angleStart (angle1) and the angleExtent (dangle)
            var ux = (x1 - cx1) / rx;
            var uy = (y1 - cy1) / ry;
            var vx = (-x1 - cx1) / rx;
            var vy = (-y1 - cy1) / ry;

            //Compute the angle start
            var n = Math.Sqrt(ux * ux + uy * uy);
            var p = ux;

            sign = uy < 0 ? -1.0f : 1.0f;

            var angleStart = Geometry.RadiansToDegrees(sign * Math.Acos(p / n));

            // Compute the angle extent
            n = Math.Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
            p = ux * vx + uy * vy;
            sign = ux * vy - uy * vx < 0 ? -1.0f : 1.0f;
            var angleExtent = Geometry.RadiansToDegrees(sign * Math.Acos(p / n));

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
            var segs = Math.Ceiling(Math.Abs(arc) / 45);

            // Now calculate the sweep of each segment
            var segAngle = arc / segs;

            var theta = Geometry.DegreesToRadians(segAngle);
            var angle = Geometry.DegreesToRadians(startAngle);

            // Draw as 45 degree segments
            if (segs > 0)
            {
                var beta = Geometry.DegreesToRadians(xAxisRotation);
                var sinbeta = Math.Sin(beta);
                var cosbeta = Math.Cos(beta);

                // Loop for drawing arc segments
                for (var i = 0; i < segs; i++)
                {
                    angle += theta;

                    var sinangle = Math.Sin(angle - theta / 2);
                    var cosangle = Math.Cos(angle - theta / 2);

                    var div = Math.Cos(theta / 2);
                    var cx = x + (radius * cosangle * cosbeta - yRadius * sinangle * sinbeta) / div;
                    //Why divide by Math.cos(theta/2)? - FIX THIS
                    var cy = y + (radius * cosangle * sinbeta + yRadius * sinangle * cosbeta) / div;
                    //Why divide by Math.cos(theta/2)? - FIX THIS

                    sinangle = Math.Sin(angle);
                    cosangle = Math.Cos(angle);

                    var x1 = x + (radius * cosangle * cosbeta - yRadius * sinangle * sinbeta);
                    var y1 = y + (radius * cosangle * sinbeta + yRadius * sinangle * cosbeta);

                    aPath.QuadTo(cx, cy, x1, y1);
                }
            }
        }
    }
}