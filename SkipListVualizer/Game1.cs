using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace SkipListVualizer
{
    //dotnet tool install -g dontet-mgcb
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        Texture2D NodeImage;
        SpriteFont Font;


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            Random random = new Random();

            SkipList<int> List = new SkipList<int>(random);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            NodeImage = Content.Load<Texture2D>("circle");
            Font = Content.Load<SpriteFont>("Font");

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(NodeImage, new Rectangle(50, 50, 50, 50), Color.White);
            spriteBatch.DrawString(Font, "5", new Vector2(75, 75), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}