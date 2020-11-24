using Microsoft.Xna.Framework;

namespace MonoCamera2D
{
    public class Camera2D
    {
        public Camera2D(int w, int h)
        {
            size = new Vector2(w, h);
            offsetMatrix = Matrix.CreateTranslation(new Vector3(size / 2, 0f));
        }

        public Vector2 position;
        public Vector2 size;

        public float Zoom { get; set; } = 1.5f;

        public Vector2 RelativeSize => size / Zoom;

        Matrix offsetMatrix;

        Matrix PosMatrix => Matrix.CreateTranslation(new Vector3(-position, 0f));
        Matrix ZoomMatrix => Matrix.CreateScale(Zoom);

        public float Left => position.X - RelativeSize.X / 2;
        public float Right => position.X + RelativeSize.X / 2;
        public float Top => position.Y - RelativeSize.Y / 2;
        public float Bottom => position.Y + RelativeSize.Y / 2;

        public Matrix GetProjection()
        {
            return PosMatrix * ZoomMatrix * offsetMatrix;
        }

        public void Follow(Vector2 dest, float lerp)
        {
            position = Vector2.Lerp(position, dest, lerp);
        }
    }
}
