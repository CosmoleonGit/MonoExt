using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoExt
{
    public static class Input
    {
        private static MouseState lastMouseState;
        private static MouseState mouseState;

        private static KeyboardState lastKeyboardState;
        private static KeyboardState keyboardState;

        public static Texture2D CursorTex { get; private set; }

        static Game game;

        public static void LoadData(Game _game)
        {
            game = _game;
        }

        public static void Update()
        {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        #region Mouse Functions

        public static Point MousePosition => mouseState.Position;

        public static Vector2 MousePositionMatrix(Matrix matrix)
        {
            return Vector2.Transform(MousePosition.ToVector2(),
                                     Matrix.Invert(matrix));
        }

        public static int MouseX => mouseState.X;
        public static int MouseY => mouseState.Y;

        public static Point LastMousePosition => lastMouseState.Position;
        public static int LastMouseX => lastMouseState.X;
        public static int LastMouseY => lastMouseState.Y;

        public static bool LeftMouseDown()
        {
            return game.IsActive && mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftMouseUp()
        {
            return !game.IsActive || mouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMousePressed()
        {
            return game.IsActive && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMouseReleased()
        {
            return !game.IsActive || mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool RightMouseDown()
        {
            return game.IsActive && mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool RightMouseUp()
        {
            return !game.IsActive || mouseState.RightButton == ButtonState.Released;
        }

        public static bool RightMousePressed()
        {
            return game.IsActive && mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
        }

        public static bool RightMouseReleased()
        {
            return !game.IsActive || mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool InRectBounds(Rectangle rect)
        {
            return game.IsActive && MouseX >= rect.Left && MouseX < rect.Right &&
                                    MouseY >= rect.Top && MouseY < rect.Bottom;
        }

        public static bool InRectBoundsMatrix(Rectangle rect, Matrix matrix)
        {
            var p = MousePositionMatrix(matrix);

            return game.IsActive && p.X >= rect.Left && p.X < rect.Right &&
                                    p.Y >= rect.Top && p.Y < rect.Bottom;
        }

        static readonly Point cursorSize = new Point(16);

        public static void DrawCursor(SpriteBatch spriteBatch)
        {
            if (CursorTex == null) CursorTex = game.Content.Load<Texture2D>("Cursor");
            spriteBatch.Draw(CursorTex, new Rectangle(MousePosition, cursorSize), Color.White);
        }

        public static int GetScrollDelta()
        {
            return game.IsActive ? (mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue) : 0;
        }

        #endregion

        #region Keyboard Functions
        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        public static Keys[] AllPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        #endregion
    }
}
