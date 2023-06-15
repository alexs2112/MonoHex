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

        public int NumberPressed() {
            if (CurrentKeys.Contains(Keys.D1)) { return 1; }
            if (CurrentKeys.Contains(Keys.D2)) { return 2; }
            if (CurrentKeys.Contains(Keys.D3)) { return 3; }
            // if (CurrentKeys.Contains(Keys.D4)) { return 4; }
            // if (CurrentKeys.Contains(Keys.D5)) { return 5; }
            // if (CurrentKeys.Contains(Keys.D6)) { return 6; }
            // if (CurrentKeys.Contains(Keys.D7)) { return 7; }
            // if (CurrentKeys.Contains(Keys.D8)) { return 8; }
            // if (CurrentKeys.Contains(Keys.D9)) { return 9; }
            return -1;
        }
    }
}
