using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    public class Biome {
        public enum Type {
            PLAINS,
            FOREST,
            ROCKS,
            CRYSTAL,
            MOUNTAIN
        }

        private static Dictionary<Type, Sprite> Sprites;

        public static void LoadContent(ContentManager content) {
            Sprites = new Dictionary<Type, Sprite>();

            Sprites.Add(Type.PLAINS, new Sprite("Hexes", new Rectangle(0, 96, 128, 96)));
            Sprites.Add(Type.FOREST, new Sprite("Hexes", new Rectangle(128, 96, 128, 96)));
            Sprites.Add(Type.ROCKS, new Sprite("Hexes", new Rectangle(256, 96, 128, 96)));
            Sprites.Add(Type.CRYSTAL, new Sprite("Hexes", new Rectangle(384, 96, 128, 96)));
            Sprites.Add(Type.MOUNTAIN, new Sprite("Hexes", new Rectangle(512, 96, 128, 96)));
        }

        public static Type RandomType() {
            System.Random r = new System.Random();
            Type[] types = new Type[] { Type.PLAINS, Type.FOREST, Type.ROCKS, Type.CRYSTAL, Type.MOUNTAIN };
            return types[r.Next(0, types.Length)];
        }

        public static Sprite GetSprite(Type type) {
            return Sprites[type];
        }
    }
}
