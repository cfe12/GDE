﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Objects.General;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a shape that has a height and a width.</summary>
    public abstract class WidthHeightShape : Shape, IHasHeight, IHasWidth
    {
        /// <summary>The width of the shape.</summary>
        public double Width { get; set; }
        /// <summary>The height of the shape.</summary>
        public double Height { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="WidthHeightShape"/> class.</summary>
        /// <param name="both">The length of both dimensions of the shape.</param>
        public WidthHeightShape(double both) : this(both, both) { }
        /// <summary>Initializes a new instance of the <seealso cref="WidthHeightShape"/> class.</summary>
        /// <param name="width">The width of the shape.</param>
        /// <param name="height">The height of the shape.</param>
        public WidthHeightShape(double width, double height)
            : base()
        {
            Width = width;
            Height = height;
        }

        /// <summary>Returns the distance between the center of the shape and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public override double GetRadiusAtRotation(double rotation)
        {
            double r = rotation % 180;
            if (r == 0)
                return Width / 2;
            if (r == -90 || r == 90)
                return Height / 2;
            double rad = rotation * Math.PI / 180;
            double w = Width / 2;
            double h = Height / 2;
            double radius = GetMaxRadius();
            // The points (x, y) are an ellipse containing the rectangle
            double x = Math.Cos(rad) * radius;
            double y = Math.Sin(rad) * radius;
            Point result = new Point(x, y);
            // Check whether the point in the ellipse is outside the rectangle
            if (y > h || y < -h)
                result *= h / y;
            else if (x > w || x < -w)
                result *= w / x;
            return new Point(0).DistanceFrom(result);
        }
        /// <summary>Returns the maximum distance between the center of the shape and its edge.</summary>
        public override double GetMaxRadius() => Math.Sqrt(Width * Width / 4 + Height * Height / 4);

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point)
        {
            Point start = new Point(-Width / 2, -Height / 2);
            Point end = new Point(Width / 2, Height / 2);
            return start <= point && point <= end;
        }
    }
}
