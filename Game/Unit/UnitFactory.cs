using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class UnitFactory {
        private Dictionary<string, Texture2D> Sprites;

        public UnitFactory(ContentManager content) {
            Sprites = new Dictionary<string, Texture2D>();
            Sprites.Add("Stick", content.Load<Texture2D>("Units/Stick"));
        }

        public Unit NewStick(Player p) {
            Unit u = new Unit(p, "Stickman", Sprites["Stick"]);
            u.SetStats(2, 2, 2, 2, 1, 2);
            return u;
        }
    }
}
