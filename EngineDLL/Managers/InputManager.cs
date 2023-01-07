using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Engine.Managers
{
    abstract class InputManager
    {
        protected bool[] keysPressed = new bool[255];

        public InputManager() {}

        public void ClearInputs()
        {
            keysPressed = new bool[255];
        }

        public void Keyboard_KeyDown(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = true;
            keyDown(e);
        }

        public void Keyboard_KeyUp(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = false;
            keyUp(e);
        }

        protected abstract void keyUp(KeyboardKeyEventArgs e);

        protected abstract void keyDown(KeyboardKeyEventArgs e);

        public abstract void ProcessInputs();

        public abstract void Close();
    }
}
