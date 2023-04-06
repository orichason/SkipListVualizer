using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;

namespace SkipListVualizer
{
    enum OPERATIONS
    {
        Add,
        Remove
    }
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
        Button deleteButton;
        Button undoButton;
        Button redoButton;

        SkipList<int> userList;
        Node<int>[] userArray = new Node<int>[0];
        Texture2D nodeTexture;
        List<TextBox> textBoxes = new List<TextBox>(0);

        Stack<(OPERATIONS Op, Node<int> node)> UndoStack = new ();

        Stack<(OPERATIONS Op, Node<int> node)> RedoStack = new ();


        #region Skip list drawing functions

        protected Node<T>[] GetVisualInformation<T>(SkipList<T> userList) where T : IComparable
        {            
            
            Node<T>[] valueArray = new Node<T>[userList.Count + 1];
            Node<T> current = userList.Head;

            int i;

            Stack<Node<T>> SentinalStack = new Stack<Node<T>>();

            while (current != null)
            {
                SentinalStack.Push(current);
                current = current.Below;
            }

            current = SentinalStack.Pop();

            for (i = 0; current != null; i++)
            {
                valueArray[i] = current;
                current = current.Right;
            }

            while (SentinalStack.Count > 0)
            {
                //Go through each sentinal to its right and replace that node with the one already in the array

                current = SentinalStack.Pop();

                while (current != null)
                {
                    for (int j = 0; j < valueArray.Length; j++)
                    {
                        if (current.Below == valueArray[j])
                        {
                            valueArray[j] = current;
                            break;
                        }
                    }
                    current = current.Right;
                }
            }
            return valueArray;
        }

        protected void GenerateTextBoxList()
        {
            textBoxes.Clear();

            float x = 0;
            const float screenSizeX = 1250f;
            float screenSizeY = (float)GraphicsDevice.Viewport.Height;

            float xScale = screenSizeX / userArray.Length / nodeTexture.Width;
            float yScale = screenSizeY / userArray.Length / nodeTexture.Height;

            float maxSize = Math.Min(xScale, yScale);

            for (int i = 0; i < userList.Count + 1; i++)
            {
                float y = screenSizeY - (nodeTexture.Height * maxSize);
                for (int j = 1; j < userArray[i].Height + 1; j++)
                {
                    TextBox node = new TextBox(nodeTexture, new Vector2(x, y), maxSize, Color.Black, font, userArray[i].Value.ToString(), Color.White);
                    y -= nodeTexture.Height * maxSize;
                    textBoxes.Add(node);
                }
      
                x += nodeTexture.Width * maxSize;
            }
        }
        #endregion
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
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
            nodeTexture = Content.Load<Texture2D>("circle");

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
            const float outputTextBoxSize = 150;
            outputTextBox = new TextBox(outputTexture, new Vector2(lastButtonPosition.X, lastButtonPosition.Y + buttonSize + 20), outputTextBoxSize / outputTexture.Width, Color.Blue, font, "", Color.White);

            submitButton = new Button(outputTexture, new Vector2(outputTextBox.GetPosition().X, outputTextBox.GetPosition().Y + buttonSize + 20), (float)outputTextBoxSize / outputTexture.Width, Color.Green, font, "Enter", Color.White);

            deleteButton = new Button(outputTexture, new Vector2(submitButton.GetPosition().X, submitButton.GetPosition().Y + buttonSize + 20), (float)outputTextBoxSize / outputTexture.Width, Color.Black, font, "Delete", Color.White);

            undoButton = new Button(outputTexture, new Vector2(1200, 50), (float)outputTextBoxSize / outputTexture.Width, Color.Red, font, "Undo", Color.Red);

            redoButton = new Button(outputTexture, new Vector2(1200, undoButton.GetPosition().Y + buttonSize + 20), (float)outputTextBoxSize / outputTexture.Width, Color.Red, font, "Redo", Color.Red);

            userArray = GetVisualInformation(userList);
            GenerateTextBoxList();
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
                    //outputTextBox.AddText($"{buttons[i].GetTextString()}");
                    output *= 10;
                    output += buttons[i].GetTextInt();
                    outputTextBox.SetText(output.ToString());
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
                int height = userList.Insert(output);
                UndoStack.Push((OPERATIONS.Add, new Node<int>(output, height)));
                userArray = GetVisualInformation(userList);
                GenerateTextBoxList();
                outputTextBox.SetText("0");
                output = 0;
            }

            if(undoButton.isClicked(mouseState))
            {
                if (UndoStack.Count > 0)
                {
                    RedoStack.Push(UndoStack.Pop());
                }
                
                Node<int> node = new Node<int>(RedoStack.Peek().node.Value, RedoStack.Peek().node.Height);

                    
                if(RedoStack.Peek().Op == OPERATIONS.Remove)
                {
                    userList.Insert2(node.Value, node.Height);
                    userArray = GetVisualInformation(userList);
                    GenerateTextBoxList();
                }

                else if(RedoStack.Peek().Op == OPERATIONS.Add)
                {
                    userList.Delete(node.Value);
                    userArray = GetVisualInformation(userList);
                    GenerateTextBoxList();
                }
            }

            if(redoButton.isClicked(mouseState))
            {
                if (RedoStack.Count > 0)
                {
                    UndoStack.Push(RedoStack.Pop());


                    Node<int> node = new Node<int>(UndoStack.Peek().node.Value, UndoStack.Peek().node.Height);

                    if (UndoStack.Peek().Op == OPERATIONS.Remove)
                    {
                        userList.Delete(node.Value);
                        userArray = GetVisualInformation(userList);
                        GenerateTextBoxList();
                    }

                    else if (UndoStack.Peek().Op == OPERATIONS.Add)
                    {
                        userList.Insert2(node.Value, node.Height);
                        userArray = GetVisualInformation(userList);
                        GenerateTextBoxList();
                    }
                }
            }

            if(deleteButton.isClicked(mouseState))
            {
                int height = userList.Delete(output);
                UndoStack.Push((OPERATIONS.Remove, new Node<int>(output, height)));
                userList.Delete(output);
                userArray = GetVisualInformation(userList);
                GenerateTextBoxList();
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
            spriteBatch.DrawString(font, $"Count: {userList.Count}", new Vector2(1200, 10), Color.Black);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            for (int i = 0; i < textBoxes.Count; i++)
            {
                textBoxes[i].Draw(spriteBatch);
            }

            outputTextBox.Draw(spriteBatch);
            submitButton.Draw(spriteBatch);
            deleteButton.Draw(spriteBatch);
            undoButton.Draw(spriteBatch);
            redoButton.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}