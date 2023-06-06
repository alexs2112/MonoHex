using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoHex {
    public class Main : Game {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        private MouseHandler Mouse;

        private World World;
        private Layout Layout;
        private UnitFactory UnitFactory;
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

            Biome.LoadContent(Content);
            Layout.LoadContent(Content);

            Mouse = new MouseHandler();
            World = new World(3);
            UnitFactory = new UnitFactory(Content);
            World.SetupMap(UnitFactory);
            Layout = new Layout(World, new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight));

            base.Initialize();
        }

        protected override void LoadContent() {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Mouse.Update();

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
