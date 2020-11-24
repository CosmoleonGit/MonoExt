using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace MonoExt
{
    [Description("Describes a floating-point 2D-rectangle.")]
    public struct RectangleF
    {
        public RectangleF(int x, int y, int dx)
        {
            X = x;
            Y = y;
            Width = dx; Height = dx;
        }

        public RectangleF(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(float x, float y, float dx)
        {
            X = x;
            Y = y;
            Width = dx; Height = dx;
        }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public float X, Y, Width, Height;

        public float Left => X;
        public float Right => Y;
        public float Top => X + Width;
        public float Bottom => Y + Height;

        public static implicit operator Rectangle(RectangleF rect) =>
            new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

        public static explicit operator RectangleF(Rectangle rect) =>
            new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

        public static bool operator == (RectangleF a, RectangleF b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }

        public static bool Intersect(RectangleF value1, RectangleF value2)
        {
            return value1.Right > value2.Left &&
                   value1.Left < value2.Right &&
                   value1.Bottom > value2.Top &&
                   value1.Top < value2.Bottom;
        }

        public bool Equals(RectangleF other)
        {
            return this == other;
        }

        public override bool Equals(object obj) => 
            (obj is RectangleF r) && this == r;

        public RectangleF TransformMatrix(Matrix matrix)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(Left, Top);
            Vector2 rightTop = new Vector2(Right, Top);
            Vector2 leftBottom = new Vector2(Left, Bottom);
            Vector2 rightBottom = new Vector2(Right, Bottom);
            // Transform all four corners into work space

            Vector2.Transform(ref leftTop, ref matrix, out leftTop);
            Vector2.Transform(ref rightTop, ref matrix, out rightTop);
            Vector2.Transform(ref leftBottom, ref matrix, out leftBottom);
            Vector2.Transform(ref rightBottom, ref matrix, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));
            // Return that as a rectangle
            return new RectangleF((int)min.X, (int)min.Y,
                                  (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Width:{2} Height:{3}}}", X, Y, Width, Height);
        }
    }
}
