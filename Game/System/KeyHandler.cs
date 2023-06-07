using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace MonoHex {
    public class KeyHandler {
        // A class to keep track of which keys are pressed and held between frames

        private Keys[] LastKeys { get; set; }
        private Keys[] CurrentKeys { get; set; }

        public KeyHandler() {
            LastKeys = new Keys[0];
            CurrentKeys = new Keys[0];
        }

        public void Update(Keys[] pressed) {
            LastKeys = CurrentKeys;
            CurrentKeys = pressed;
        }

        public bool KeyJustPressed() {
            return CurrentKeys.Count() > 0 && !CurrentKeys.SequenceEqual(LastKeys);
        }

        public bool KeyJustPressed(Keys key) {
            return CurrentKeys.Contains(key) && !LastKeys.Contains(key);
        }

        public bool KeyPressed(Keys key) {
            return CurrentKeys.Contains(key);
        }
    }
}
