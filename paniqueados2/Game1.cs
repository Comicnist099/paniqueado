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
        private GraphicsDeviceManager _graphics;    //Usado para cargar gráficos
        private SpriteBatch _spriteBatch;           //Usado para dibujar texturas
        private Texture2D _textura;                 //Usado para cargar texturas
        private Rectangle _rectangule;              //Usado para dibujar en el juego
        private Vector2 posicionPlayer;             //Posición del jugador 
        private Vector2 posicionPlayerAnte;         //Posición anterior del jugador

        float time;
        Int32 PosBalaX = 0;
        Int32 PosBalaY = 0;
        Int32 velocidadBala = 10;
        bool visible = false;
        int contador = 0;
        int a = 0;

        historyLine[] _historyLine = new historyLine[7000];

        public List<Vector2> pixelScreen = new List<Vector2>();

        //Rastro
        private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        //Límites del campo de juego. Dimensiones.
        int LimitX = 1000;
        int LimitY = 700;



        Texture2D pixel;

        //Se encarga de los límites del espacio disponibles
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

        //Constructor. Clase padre de todo, indica cómo empezar.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        //Inicializador. Inicializa todo desde el inicio.
        protected override void Initialize()
        {
            posicionPlayerAnte = posicionPlayer;

            historyLine _historyLineDraw = new historyLine(pixel, posicionPlayerAnte);

            _historyLine[0] = _historyLineDraw;
            _historyLine[1] = _historyLineDraw;

            pixelScreen.Add(posicionPlayer);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });        // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        //Carga métodos que soni usados para el juego.
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

        //Se actualiza cada segundo, actualiza el estado del juego
        protected override void Update(GameTime gameTime)
        {
            contador++;
            time = contador / 1000;
            Debug.WriteLine(contador);

            //Si se presiona la tecla ESC sale del juego
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)){
                Exit();
            }

            LimitMap();

            _rectangule = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
            _rectanguleRastro = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);


            historyLine _historyLineDraw = new historyLine(pixel, posicionPlayerAnte);

            _historyLine[a] = _historyLineDraw;
       
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                visible = true;
                Disparar();
            }
            else
            {
                visible = false;
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

        //Es llamado en un intervalor regular para actualizar el estado del juego
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string playerX = "";
            string playerY = "";
            Vector2 position2 = new Vector2(10, 10);
            Vector2 textMiddlePoint = font.MeasureString("text") / 2;

            _spriteBatch.Begin();
            _spriteBatch.Draw(pixel, new Rectangle((int)_historyLine[a].getPosition().X, (int)_historyLine[a].getPosition().Y, 10, 10), Color.Red);

            //Texto
            playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
            playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
            _spriteBatch.Draw(_textura, _rectangule, Color.White);
            _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + "Array:" + _historyLine[a].getPosition(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            //Punto
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Disparar()
        {
            if (visible == true)
            {
                if (PosBalaX <= (Window.ClientBounds.Width - _textura.Width))
                {
                    PosBalaX += velocidadBala;
                }
                else
                {
                    PosBalaX = (int)posicionPlayer.X;
                    PosBalaY = (int)posicionPlayer.Y;
                    visible = false;
                }
            }
            else
            {
                PosBalaX = (int)posicionPlayer.X;
                PosBalaY = (int)posicionPlayer.Y;
            }
        }
    }

}
