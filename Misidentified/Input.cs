using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Misidentified
{
    public static class Input
    {
        public static KeyboardState CurrentKeyboard;
        public static KeyboardState PreviousKeyboard;

        public static MouseState CurrentMouse;
        public static MouseState PreviousMouse;

        public static void Update()
        {
            PreviousKeyboard = CurrentKeyboard;
            CurrentKeyboard = Keyboard.GetState();

            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return CurrentKeyboard.IsKeyDown(key);
        }

        public static bool WasKeyPressed(Keys key)
        {
            return CurrentKeyboard.IsKeyDown(key) && PreviousKeyboard.IsKeyUp(key);
        }

        public static Vector2 MouseDelta()
        {
            return new Vector2(
                CurrentMouse.X - PreviousMouse.X,
                CurrentMouse.Y - PreviousMouse.Y
            );
        }
    }
}
