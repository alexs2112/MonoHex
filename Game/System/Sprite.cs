using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoHex {
    public class Sprite {
        // Dictionary of Texture Atlases
        private static Dictionary<string, Texture2D> Textures;
        public static void LoadTextures(ContentManager content) {
            Textures = new Dictionary<string, Texture2D>();

            Textures.Add("Hexes", content.Load<Texture2D>("Hexes"));
            Textures.Add("Units", content.Load<Texture2D>("Units"));
            Textures.Add("Structures", content.Load<Texture2D>("Structures"));
        }

        private string Atlas;
        private Rectangle Source;
        private int Frames;
        private int CurrentFrame;
        public Sprite(string atlas, Rectangle source) { Atlas = atlas; Source = source; Frames = 1; }
        public Sprite(string atlas, Rectangle source, int frames) { Atlas = atlas; Source = source; Frames = frames; }

        // Currently unused, for animations
        public void Update() {
            if (Frames == 1) { return; }
            CurrentFrame++;
            if (CurrentFrame >= Frames) { CurrentFrame = 0; }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location) { Draw(spriteBatch, location, Color.White); }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color colour) {
            Rectangle s = Source;
            if (Frames > 1) { s.X += Source.Width * CurrentFrame; }
            
            Rectangle dest = new Rectangle((int)location.X, (int)location.Y, Source.Width, Source.Height);
            spriteBatch.Draw(Textures[Atlas], dest, s, colour);
        }
        public void DrawCentered(SpriteBatch spriteBatch, Rectangle destination) { DrawCentered(spriteBatch, destination, Color.White); }
        public void DrawCentered(SpriteBatch spriteBatch, Rectangle destination, Color colour) {
            int x = (destination.Width - Source.Width) / 2 + destination.X;
            int y = (destination.Height - Source.Height) / 2 + destination.Y;
            Draw(spriteBatch, new Vector2(x, y), colour);
        }
    }
}
