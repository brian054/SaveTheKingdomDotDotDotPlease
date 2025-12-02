using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SaveTheKingdomDotDotDotPlease
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D rectTexture;
        Texture2D spriteSheet;
        int tileSize;
        int scaledTileSize;
        KeyboardState keyboardState;

        Map map;

        public Game1()
        {
            // is this not default true already?
            IsFixedTimeStep = true; // so Update() will be called 60 times per second
            TargetElapsedTime = TimeSpan.FromMilliseconds(16.67); // 60 FPS

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Texture2D mapTexture = Content.Load<Texture2D>("bigMap");
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");

            map = new(20, 20, mapTexture, spriteSheet);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            map.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
