using System;

using OpenTK.Input;

namespace UnityEngine.InputManager
{
    public static class InputManager : Object
    {
        public static KeyboardState keyboardState;

        public static KeyboardState lastState;

        public static KeyboardState KeyboardState
        {
            get => keyboardState;

            private set => keyboardState = value;
        }

        public static Boolean IsKeyJustPressed(Key key)
        {
            if (keyboardState.IsKeyDown(key) && !lastState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        internal static void Update()
        {
            lastState = keyboardState;

            keyboardState = Keyboard.GetState();
        }
    }
}
