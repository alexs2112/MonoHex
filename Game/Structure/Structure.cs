using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class Structure {
        public Player Owner { get; private set; }
        public string Name { get; private set; }
        public Texture2D Sprite { get; private set; }

        public int Food { get; private set; }
        public int Material { get; private set; }
        public int Gold { get; private set; }
        public int Crystal { get; private set; }

        public Structure(Player owner, string name, Texture2D sprite) {
            Owner = owner;
            Name = name;
            Sprite = sprite;
        }

        public void Upkeep() {
            Owner.Food += Food;
            Owner.Material += Material;
            Owner.Gold += Gold;
            Owner.Crystal += Crystal;
        }
    }
}
