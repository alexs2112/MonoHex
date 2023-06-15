using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class StructureFactory {
        private Dictionary<string, Sprite> Sprites;

        public StructureFactory(ContentManager content) {
            Sprites = new Dictionary<string, Sprite>();
            Sprites.Add("Capital", new Sprite("Structures", new Rectangle(0, 0, 64, 96), new Rectangle(64, 0, 64, 96)));
            Sprites.Add("Farm", new Sprite("Structures", new Rectangle(0, 96, 64, 64), new Rectangle(64, 96, 64, 64)));
            Sprites.Add("Camp", new Sprite("Structures", new Rectangle(0, 160, 64, 64), new Rectangle(64, 160, 64, 64)));
            Sprites.Add("Mine", new Sprite("Structures", new Rectangle(0, 224, 64, 64), new Rectangle(64, 224, 64, 64)));
        }

        public Structure NewCapital(Player p) {
            Structure s = new Structure(p, "Capital", Sprites["Capital"]);
            s.SetFood(10);
            s.SetMaterial(10);
            s.SetGold(10);
            s.SetCrystal(1);
            return s;
        }

        public Structure NewFarm(Player p) {
            Structure s = new Structure(p, "Farm", Sprites["Farm"]);
            s.SetFood(10);
            return s;
        }

        public Structure NewCamp(Player p) {
            Structure s = new Structure(p, "Camp", Sprites["Camp"]);
            s.SetMaterial(10);
            return s;
        }

        public Structure NewMine(Player p) {
            Structure s = new Structure(p, "Mine", Sprites["Mine"]);
            s.SetGold(10);
            return s;
        }
    }
}
