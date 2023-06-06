using static System.Math;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoHex {
    class Layout {
        private World World;
        private Rectangle Window;
        private int CenterX;
        private int CenterY;
        private static Dictionary<string, Texture2D> Textures;
        
        public Layout(World world, Rectangle window) {
            World = world;
            ResizeWindow(window);
        }

        public void ResizeWindow(Rectangle window) {
            Window = window;
            CenterX = (window.Width - window.X) / 2;
            CenterY = (window.Height - window.Y) / 2;
        }

        public static void LoadContent(ContentManager content) {
            Textures = new Dictionary<string, Texture2D>();

            Textures.Add("Hex", content.Load<Texture2D>("Tiles/Hex"));
            Textures.Add("Border", content.Load<Texture2D>("Tiles/Border"));
            Textures.Add("Highlight", content.Load<Texture2D>("Tiles/Highlight"));
        }

        public void DrawWorld(SpriteBatch spriteBatch, ContentManager content) {
            foreach (Hex h in World.Hexes) {
                spriteBatch.Draw(Biome.GetSprite(World.Biomes[h]), HexStart(h), Color.White);

                Unit u = World.GetUnit(h);
                if (u != null) {
                    // TODO: Make a better way of colouring units by owner
                    spriteBatch.Draw(u.Sprite, HexStart(h), u.Owner.Colour);
                }
            }
        }

        public void DrawOverlay(SpriteBatch spriteBatch, Hex selected) {
            if (selected != null) {
                spriteBatch.Draw(Textures["Border"], HexStart(selected), Color.Blue);

                Unit u = World.GetUnit(selected);
                if (u != null) {
                    // It is inefficient to calculate valid actions every single tick
                    List<Hex> moves = World.ValidMoves(selected);
                    foreach (Hex h in moves) {
                        spriteBatch.Draw(Textures["Highlight"], HexStart(h), new Color(Color.Green, 130));
                    }
                    List<Hex> attacks = World.ValidAttacks(selected);
                    foreach (Hex h in attacks) {
                        spriteBatch.Draw(Textures["Highlight"], HexStart(h), new Color(Color.Red, 130));
                    }
                }
            }
        }

        // A bunch of mathy functions
        // https://www.redblobgames.com/grids/hexagons/implementation.html#layout
        public const int HexWidth = 128;
        public const int HexHeight = 96;
        private static Vector4 Regular = new Vector4((float)(3.0 / 2.0), 0, (float)(Sqrt(3.0) / 2.0), (float)(Sqrt(3.0)));
        private static int SizeX = HexWidth / 2;
        private static int SizeY = (int)(HexHeight / Sqrt(3));

        private Vector2 HexStart(Hex hex) {
            int x = (int)((Regular.X * hex.q + Regular.Y * hex.r) * SizeX);
            int y = (int)((Regular.Z * hex.q + Regular.W * hex.r) * SizeY);
            return ApplyOffsetRegular(x, y);
        }

        public Hex GetHex(Point p) {
            Hex hex = HexRound(GetFractionalHex(p));
            if (Constants.Verbosity > 0) System.Console.WriteLine($"({hex.q}, {hex.r}, {hex.s})");
            return hex;
        }

        private Vector3 GetFractionalHex(Point p) {
            Vector2 p2 = ApplyOffsetInverse(p.X, p.Y);
            float q = (float)((2.0 * p2.X)/(3.0 * SizeX));
            float r = (float)(p2.Y/(SizeY * Sqrt(3)) - p2.X/(3.0 * SizeX));
            float s = -q - r;

            return new Vector3(q, r, s);
        }

        private Hex HexRound(Vector3 frac) {
            // Turns the FractionalHex Vector3 into the actual Hex
            int q = (int)(Round(frac.X));
            int r = (int)(Round(frac.Y));
            int s = (int)(Round(frac.Z));
            float q_diff = Abs(q - frac.X);
            float r_diff = Abs(r - frac.Y);
            float s_diff = Abs(s - frac.Z);
            if (q_diff > r_diff && q_diff > s_diff) {
                q = -r - s;
            } else if (r_diff > s_diff) {
                r = -q - s;
            } else {
                s = -q - r;
            }
            return new Hex(q, r, s);
        }

        private Vector2 ApplyOffsetRegular(int x, int y) {
            return new Vector2(x + CenterX - HexWidth/2, y + CenterY - HexHeight/2);
        }

        private Vector2 ApplyOffsetInverse(int x, int y) {
            return new Vector2(x - CenterX, y - CenterY);
        }

        // For testing purposes, clicking a hex will display a black hex based on relatively
        // where it thinks the mouse is
        // Uncomment this if we need to mess around with the hex math again
        /*
        private List<Vector2> Testing;

        // Call this on a given hex, it will add all pixels that "click" the hex to the list
        public void TestHex(int q, int r, int s) {
            Testing = new List<Vector2>();
            Hex i = new Hex(q, r, s);
            for (int x = 0; x < Constants.ScreenWidth; x++) {
                for (int y = 0; y < Constants.ScreenHeight; y++) {
                    Hex j = GetHex(new Point(x, y), false);
                    if (i.Equals(j)) {
                        Testing.Add(new Vector2(x, y));
                    }
                }
            }
        }

        // Add this to drawing the hex to draw where it thinks the hex is
        Texture2D dot2 = content.Load<Texture2D>("Tiles/Dot2"); // This should be a single pixel
        foreach (Vector2 p in Testing) {
            spriteBatch.Draw(dot2, p, Color.Black);
        }
        */
    }
}
