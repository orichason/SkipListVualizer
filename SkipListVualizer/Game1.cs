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

        Button[] buttons = new Button[10];

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
            Texture2D imageTexture = Content.Load<Texture2D>("button");
            const int buttonSize = 80;

            int startingX = GraphicsDevice.Viewport.Width - buttonSize * 3;

            int x = startingX;
            int y = -buttonSize;
            for (int i = 0; i < buttons.Length; i++)
            {
                if(i % 3 == 0)
                {
                    x = startingX;
                    y += buttonSize;
                }

                buttons[i] = new Button(imageTexture, new Vector2(x,y), (float)buttonSize / imageTexture.Width , Color.Red, Font, $"{i}", Color.White);
                x += buttonSize;
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Check if button was clicked to do stuff

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(NodeImage, new Rectangle(50, 50, 50, 50), Color.White);
            spriteBatch.DrawString(Font, "5", new Vector2(75, 75), Color.Black);
            spriteBatch.DrawString(Font, mouseState.Position.ToString(), new Vector2(10, 10), Color.Black);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}