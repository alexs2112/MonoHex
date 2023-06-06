using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoHex {
    public class MouseHandler {
        private MouseState State;
        private MouseState LastState;

        public MouseHandler() {
            State = Mouse.GetState();
        }

        public void Update() {
            LastState = State;
            State = Mouse.GetState();
        }

        public Point Position() {
            return new Point(State.X, State.Y);
        }

        public bool LeftClicked() {
            return State.LeftButton == ButtonState.Pressed && LastState.LeftButton != ButtonState.Pressed;
        }

        public bool RightClicked() {
            return State.RightButton == ButtonState.Pressed && LastState.RightButton != ButtonState.Pressed;
        }

        public bool ButtonClicked() {
            return LeftClicked() || RightClicked();
        }
    }
}
