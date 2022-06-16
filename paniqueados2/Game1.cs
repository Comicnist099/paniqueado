using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace paniqueados2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        private Rectangle _rectangule;
        private Vector2 posicionPlayer;
        private Vector2 posicionPlayerAnte;

        int a = 0;
        private bool press = false;



        public List<Vector2> pixelScreen = new List<Vector2>();



        //Rastroo
        private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        int LimitX = 1000;
        int LimitY = 700;

        public int[] pixelScreenX = new int[700000];

        public int[] pixelScreenY = new int[700000];

        Texture2D pixel;


        public void LimitMap()
        {
            if (posicionPlayer.X <= 0)
            {
                posicionPlayer.X = 0;
            }
            if (posicionPlayer.X >= LimitX)
            {
                posicionPlayer.X = LimitX - 10;
            }
            if (posicionPlayer.Y <= 0)
            {
                posicionPlayer.Y = 0;
            }
            if (posicionPlayer.Y >= LimitY)
            {
                posicionPlayer.Y = LimitY - 10;
            }
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pixelScreen.Add(posicionPlayer);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });        // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            int total = LimitX * LimitY;




            _spriteBatch = new SpriteBatch(GraphicsDevice);
            posicionPlayer = new Vector2(0, 0);
            posicionPlayerAnte = new Vector2(0, 0);

            font = Content.Load<SpriteFont>("File");

            _textura = Content.Load<Texture2D>("puntito");
            _texturaRastro = Content.Load<Texture2D>("rastro");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            LimitMap();

            _rectangule = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
            _rectanguleRastro = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {

                pixelScreenX[0]=(int)posicionPlayer.X;
                pixelScreen.Add(posicionPlayer);
                



                press = true;
            }
            else
            {
                press = false;
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X += 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X -= 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.Y -= 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                posicionPlayerAnte = posicionPlayer;

                posicionPlayer.Y += 6;
            }




            base.Update(gameTime);
        }
        Random rnd = new Random();

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string playerX = "";
            string playerY = "";
            Vector2 position2 = new Vector2(10, 10);
            Vector2 textMiddlePoint = font.MeasureString("text") / 2;

            _spriteBatch.Begin();


            if (a + 1 < pixelScreen.Count)
            {
                a++;

            }
            _spriteBatch.Draw(pixel, new Rectangle((int)pixelScreen[a].X, (int)pixelScreen[a].Y, 10, 10), Color.Red);         ///Texto

            Action<int> action = new Action<int>(drawing);
            Array.ForEach(pixelScreenX, action);



            playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
            playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
            _spriteBatch.Draw(_textura, _rectangule, Color.White);

            _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + "Array:" + pixelScreen[a], position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            ///PUNTO

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        int delay = 6 * 10; // ticks to delay for
        int cooldown = 0;

        public void loop()
        {

            if (press == true)
            {
                if (cooldown <= 0)
                {
                    cooldown = delay;

                }

            }

            if (cooldown > 0)
            {
                a++;
                cooldown -= 1;


            }

        }
        public void drawing(int posicion)
        {
            _spriteBatch.Begin();

            _spriteBatch.End();
        }
    }

}
