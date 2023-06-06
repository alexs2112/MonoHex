using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
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

        private static Dictionary<Type, Texture2D> Textures;

        public static void LoadContent(ContentManager content) {
            Textures = new Dictionary<Type, Texture2D>();

            Textures.Add(Type.PLAINS, content.Load<Texture2D>("Tiles/Plains"));
            Textures.Add(Type.FOREST, content.Load<Texture2D>("Tiles/Forest"));
            Textures.Add(Type.ROCKS, content.Load<Texture2D>("Tiles/Rocks"));
            Textures.Add(Type.CRYSTAL, content.Load<Texture2D>("Tiles/Crystal"));
            Textures.Add(Type.MOUNTAIN, content.Load<Texture2D>("Tiles/Mountain"));
        }

        public static Type RandomType() {
            System.Random r = new System.Random();
            Type[] types = new Type[] { Type.PLAINS, Type.FOREST, Type.ROCKS, Type.CRYSTAL, Type.MOUNTAIN };
            return types[r.Next(0, types.Length)];
        }

        public static Texture2D GetSprite(Type type) {
            return Textures[type];
        }
    }
}
