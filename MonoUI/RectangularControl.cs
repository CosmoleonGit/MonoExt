using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public abstract class RectangularControl : Control
    {
        public RectangularControl(ControlGroup manager) : base(manager) { }

        public Point Size { get; set; }
        public Rectangle Rectangle => new Rectangle(Position, Size);

        public int Right { get => Position.X + Size.X; set { Position.X = value - Size.X; } }
        public int Bottom { get => Position.Y + Size.Y; set { Position.Y = value - Size.Y; } }

        public Point Centre
        {
            get
            {
                return Position + new Point(Size.X / 2, Size.Y / 2);
            }
            set
            {
                Position = value - new Point(Size.X / 2, Size.Y / 2);
            }
        }
    }
}
