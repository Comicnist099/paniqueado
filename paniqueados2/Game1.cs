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

    public class Bala {
    int balaX;
    int balaY;
    char estado;

    public Bala(int x, int y) {
        this.balaX = x;
        this.balaY = y;
        estado = 'A';
    }

    public int getX() {
        return (this.balaX);
    }

    public int getY() {
        return (this.balaY);
    }

    public void cambiarEstado(char estadoNuevo) {
        this.estado = estadoNuevo;
    }
}

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        private Rectangle _rectangule;
        private Vector2 posicionPlayer;
        private Vector2 posicionPlayerAnte;

        float time;
        Int32 velocidadBala = 10;
        bool visible = false;
        int contador = 0;
        int a = 0;

        historyLine[] _historyLine = new historyLine[7000];


        public List<Vector2> pixelScreen = new List<Vector2>();
        List<Bala> balasPantalla = new List<Bala>();

        //Rastroo
        private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        int LimitX = 1000;
        int LimitY = 700;



        Texture2D pixel;

        public bool verificarRepetido(List<Bala> listabalas, Bala balaAgregar) {
            bool res = false;
            for(int i = 0; i < listabalas.Count; i++) {
                if(balaAgregar.getX() == listabalas[i].getX() && balaAgregar.getY() == listabalas[i].getY()) {
                    res = true;
                }
            }
            return res;
        }

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
            contador++;
            time = contador / 1000;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            LimitMap();


            _rectangule = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
            _rectanguleRastro = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);


            historyLine _historyLineDraw = new historyLine(pixel, posicionPlayerAnte);

            _historyLine[a] = _historyLineDraw;
       
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Disparar(balasPantalla);
            }
            else
            {

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) )
            {

                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X += 6;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.X -= 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.Y -= 6;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) )
            {
                posicionPlayerAnte = posicionPlayer;
                posicionPlayer.Y += 6;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string playerX = "";
            string playerY = "";
            Vector2 position2 = new Vector2(10, 10);
            Vector2 textMiddlePoint = font.MeasureString("text") / 2;


            _spriteBatch.Begin();
            for(int i = 0; i < balasPantalla.Count; i++) {
                _spriteBatch.Draw(pixel, new Rectangle(balasPantalla[i].getX(), balasPantalla[i].getY(), 10, 10), Color.Red);
            }
            
            ///Texto
            playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
            playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
            _spriteBatch.Draw(_textura, _rectangule, Color.White);
            _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + "Array:" + _historyLine[a].getPosition(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            ///PUNTO

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void Disparar(List<Bala> balasPantalla)
        {
            Bala balaNueva = new Bala((int) posicionPlayer.X, (int) posicionPlayer.Y);
            if (!verificarRepetido(balasPantalla, balaNueva)) {
                balasPantalla.Add(balaNueva);
            }
            else {
                
            }
        }
    }

}
