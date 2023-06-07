using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class StructureFactory {
        private Dictionary<string, Sprite> Sprites;

        public StructureFactory(ContentManager content) {
            Sprites = new Dictionary<string, Sprite>();
            Sprites.Add("Capital", new Sprite("Structures", new Rectangle(0, 0, 128, 96)));
        }

        public Structure NewCapital(Player p) {
            Structure s = new Structure(p, "Capital", Sprites["Capital"]);
            s.SetFood(10);
            s.SetMaterial(10);
            s.SetGold(10);
            s.SetCrystal(1);
            return s;
        }
    }
}
