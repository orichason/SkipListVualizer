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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        SpriteFont font;
        MouseState mouseState;
        KeyboardState previousKeyState = Keyboard.GetState();

        Button[] buttons = new Button[10];
        TextBox outputTextBox;
        int output = 0;

        Button submitButton;

        SkipList<int> userList;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //graphics.PreferredBackBufferWidth = 1600;
            //graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            Random random = new Random();

            userList = new SkipList<int>(random); //Use SkipList to display

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Uploading image & font files
            font = Content.Load<SpriteFont>("Font");
            Texture2D inputTexture = Content.Load<Texture2D>("button");

            const int buttonSize = 80;

            int startingX = GraphicsDevice.Viewport.Width - buttonSize * 3;

            //Creating and positioning input buttons
            int x = startingX;
            int y = -buttonSize;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i % 3 == 0)
                {
                    x = startingX;
                    y += buttonSize;
                }

                buttons[i] = new Button(inputTexture, new Vector2(x, y), (float)buttonSize / inputTexture.Width, Color.Red, font, $"{i}", Color.White);
                x += buttonSize;
            }
            //Get position of last button to use for output's position
            Vector2 lastButtonPosition = buttons[9].GetPosition();

            //Output Text Box
            Texture2D outputTexture = Content.Load<Texture2D>("rectangle");
            const int outputTextBoxSize = 150;
            outputTextBox = new TextBox(outputTexture, new Vector2(lastButtonPosition.X, lastButtonPosition.Y + buttonSize + 20), (float)outputTextBoxSize / outputTexture.Width, Color.Blue, font, "", Color.White);

            submitButton = new Button(outputTexture, new Vector2(outputTextBox.GetPosition().X, outputTextBox.GetPosition().Y + buttonSize + 20), (float)outputTextBoxSize / outputTexture.Width, Color.Green, font, "Enter", Color.White);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            mouseState = Mouse.GetState();
            KeyboardState currentKeyState = Keyboard.GetState();
            //drawNode = false;

            //Check if button was clicked to do stuff
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].isClicked(mouseState))
                {
                    outputTextBox.AddText($"{buttons[i].GetTextString()}");
                    output *= 10;
                    output += buttons[i].GetTextInt();
                }
            }

            if ((previousKeyState.IsKeyUp(Keys.Back) && currentKeyState.IsKeyDown(Keys.Back)) ||
                (previousKeyState.IsKeyUp(Keys.Delete) && currentKeyState.IsKeyDown(Keys.Delete)))
            {
                output /= 10;
                if (output == 0) outputTextBox.SetText("");

                else outputTextBox.SetText(output.ToString());
            }

            if (submitButton.isClicked(mouseState))
            {
                userList.Insert(output);

            }



            // TODO: Add your update logic here

            previousKeyState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.DrawString(font, mouseState.Position.ToString(), new Vector2(10, 10), Color.Black);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            outputTextBox.Draw(spriteBatch);
            submitButton.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}