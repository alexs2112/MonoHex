using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoHex {
    public class Main : Game {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        private MouseHandler Mouse;
        private KeyHandler KeyHandler;

        private World World;
        private Layout Layout;
        private UnitFactory UnitFactory;
        private StructureFactory StructureFactory;

        private int TurnCount;
        private Hex SelectedHex;

        public Main() {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize() {
            Graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            Graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Graphics.ApplyChanges();

            Mouse = new MouseHandler();
            KeyHandler = new KeyHandler();

            base.Initialize();
        }

        protected override void LoadContent() {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Sprite.LoadTextures(Content);
            Biome.LoadContent(Content);
            Layout.LoadContent(Content);

            World = new World(3);
            UnitFactory = new UnitFactory(Content);
            StructureFactory = new StructureFactory(Content);
            World.SetupMap(UnitFactory, StructureFactory);
            Layout = new Layout(World, new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight));
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Mouse.Update();
            KeyHandler.Update(Keyboard.GetState().GetPressedKeys());

            // Mouse Commands
            if (Mouse.LeftClicked()) {
                Hex nextHex = Layout.GetHex(Mouse.Position());
                if (!World.InWorld(nextHex)) { SelectedHex = null; }
                else if (SelectedHex == null) { SelectedHex = nextHex; }
                else if (World.GetUnit(SelectedHex) != null) {
                    if (World.IsValidMove(SelectedHex, nextHex)) {
                        World.MoveUnit(SelectedHex, nextHex);
                    } else if (World.IsValidAttack(SelectedHex, nextHex)) {
                        World.ResolveCombat(SelectedHex, nextHex);
                    }
                    SelectedHex = null;
                } else {
                    SelectedHex = nextHex;
                }
            
            // Keyboard Commands
            } else if (KeyHandler.KeyJustPressed()) {
                if (KeyHandler.KeyJustPressed(Keys.Enter)) {
                    Upkeep();
                } else if (KeyHandler.KeyJustPressed(Keys.Escape)) {
                    Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            Layout.DrawWorld(SpriteBatch, Content);
            Layout.DrawOverlay(SpriteBatch, SelectedHex);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        // This happens at the beginning of a new round
        private void Upkeep() {
            TurnCount++;

            (int food, int material, int gold, int crystal)[] deltas = new (int food, int material, int gold, int crystal)[World.Players.Count];
            if (Constants.Verbosity > 0) {
                for (int i = 0; i < World.Players.Count; i++) {
                    Player p = World.Players[i];
                    deltas[i] = (p.Food, p.Material, p.Gold, p.Crystal);
                }
            }

            List<Structure> structures = World.GetStructures();
            foreach (Structure s in structures) {
                s.Upkeep();
            }

            if (Constants.Verbosity > 0) {
                System.Console.WriteLine($"\nTurn {TurnCount}");
                for (int i = 0; i < World.Players.Count; i++) {
                    Player p = World.Players[i];
                    System.Console.WriteLine($"{p.Name}:");
                    System.Console.WriteLine($"  Food:     {p.Food} ({p.Food - deltas[i].food})");
                    System.Console.WriteLine($"  Material: {p.Material} ({p.Material - deltas[i].material})");
                    System.Console.WriteLine($"  Gold:     {p.Gold} ({p.Gold - deltas[i].gold})");
                    System.Console.WriteLine($"  Crystal:  {p.Crystal} ({p.Crystal - deltas[i].crystal})");
                }
            }
        }

        public void OnResize(object sender, EventArgs e) {
            if (Constants.Verbosity > 0) System.Console.WriteLine($"Resizing window size to: {Window.ClientBounds.Width} {Window.ClientBounds.Height}");
            Constants.ScreenWidth = Window.ClientBounds.Width;
            Constants.ScreenHeight = Window.ClientBounds.Height;
            Graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            Graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Graphics.ApplyChanges();
            Layout.ResizeWindow(new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight));
        }
    }
}
