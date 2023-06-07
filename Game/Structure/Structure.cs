using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class Structure {
        public Player Owner { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }

        public int Food { get; private set; }
        public int Material { get; private set; }
        public int Gold { get; private set; }
        public int Crystal { get; private set; }

        public Structure(Player owner, string name, Sprite sprite) {
            Owner = owner;
            Name = name;
            Sprite = sprite;
        }

        public void SetFood(int value) { Food = value; }
        public void SetMaterial(int value) { Material = value; }
        public void SetGold(int value) { Gold = value; }
        public void SetCrystal(int value) { Crystal = value; }

        public void Upkeep() {
            Owner.Food += Food;
            Owner.Material += Material;
            Owner.Gold += Gold;
            Owner.Crystal += Crystal;
        }
    }
}
