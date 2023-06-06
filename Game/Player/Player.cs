using Microsoft.Xna.Framework;

namespace MonoHex {
    public class Player {
        public Color Colour { get; private set; }

        public int Food;
        public int Material;
        public int Gold;
        public int Crystal;

        public Hex Capital;

        public Player(Color colour) {
            Colour = colour;
        }
    }
}
