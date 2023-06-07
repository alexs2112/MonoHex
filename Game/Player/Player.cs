using Microsoft.Xna.Framework;

namespace MonoHex {
    public class Player {
        public Color Colour { get; private set; }
        public string Name { get; private set; }

        public int Food;
        public int Material;
        public int Gold;
        public int Crystal;

        public Hex Capital;

        public Player(string name, Color colour) {
            Name = name;
            Colour = colour;
        }
    }
}
