using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class UnitFactory {
        private Dictionary<string, Sprite> Sprites;
        private Dictionary<string, Sprite> Highlights;

        public UnitFactory(ContentManager content) {
            Sprites = new Dictionary<string, Sprite>();
            Highlights = new Dictionary<string, Sprite>();
            Sprites.Add("Worker", new Sprite("Units", new Rectangle(0, 0, 32, 32), new Rectangle(64, 0, 32, 32), 2));
        }

        public Unit NewWorker(Player p) {
            Unit u = new Unit(p, "Worker", Sprites["Worker"]);
            u.SetStats(2, 2, 2, 2, 1, 2);
            u.AddAbility(new BuildFarm());
            return u;
        }
    }
}
