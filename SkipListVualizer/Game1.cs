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
        SpriteFont mousePosition;

        Button[] buttons = new Button[10];

        Texture2D BUTTON;
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
            BUTTON = Content.Load<Texture2D>("button");

            int x = 0;
            int y = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button(Content.Load<Texture2D>("button"), new Vector2(x,y),0.05F, Color.Red);
                x += 100;
                y += 100;
            }

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
            MouseState mouseState = Mouse.GetState();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(NodeImage, new Rectangle(50, 50, 50, 50), Color.White);
            spriteBatch.DrawString(Font, "5", new Vector2(75, 75), Color.Black);
            spriteBatch.DrawString(Font, mouseState.Position.ToString(), new Vector2(10, 10), Color.Black);
            //spriteBatch.Draw(BUTTON,new Vector2(0,0), Color.White);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}