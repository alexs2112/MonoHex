using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class UnitFactory {
        private Dictionary<string, Sprite> Sprites;

        public UnitFactory(ContentManager content) {
            Sprites = new Dictionary<string, Sprite>();
            Sprites.Add("Stick", new Sprite("Units", new Rectangle(0, 0, 128, 96)));
        }

        public Unit NewStick(Player p) {
            Unit u = new Unit(p, "Stickman", Sprites["Stick"]);
            u.SetStats(2, 2, 2, 2, 1, 2);
            return u;
        }
    }
}
