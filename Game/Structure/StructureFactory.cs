using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class StructureFactory {
        private Dictionary<string, Texture2D> Sprites;

        public StructureFactory(ContentManager content) {
            Sprites = new Dictionary<string, Texture2D>();
            Sprites.Add("Capital", content.Load<Texture2D>("Structures/Capital"));
        }

        public Structure NewCapital(Player p) {
            Structure s = new Structure(p, "Capital", Sprites["Capital"]);
            return s;
        }
    }
}
