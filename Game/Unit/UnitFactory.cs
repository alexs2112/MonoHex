using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class UnitFactory {
        private Dictionary<string, Sprite> Sprites;

        public UnitFactory(ContentManager content) {
            Sprites = new Dictionary<string, Sprite>();
            Sprites.Add("Worker", new Sprite("Units", new Rectangle(0, 0, 32, 32)));
        }

        public Unit NewWorker(Player p) {
            Unit u = new Unit(p, "Worker", Sprites["Worker"]);
            u.SetStats(2, 2, 2, 2, 1, 2);
            return u;
        }
    }
}
